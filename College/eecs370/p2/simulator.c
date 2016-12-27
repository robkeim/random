// Rob Keim
// EECS 370 - Project 2

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define NUMMEMORY 65536 /* maximum number of words in memory */
#define NUMREGS 8 /* number of machine registers */
#define MAXLINELENGTH 1000

#define ADD 0
#define NAND 1
#define LW 2
#define SW 3
#define BEQ 4
#define JALR 5
#define HALT 6
#define NOOP 7

typedef struct stateStruct {
    int pc;
    int mem[NUMMEMORY];
    int reg[NUMREGS];
    int memoryAddress;
    int memoryData;
    int instrReg;
    int aluOperand;
    int aluResult;
    int numMemory;
} stateType;

void printState(stateType *, char *);
void run(stateType);
int memoryAccess(stateType *, int);
int convertNum(int);

int
main(int argc, char *argv[])
{
    int i;
    char line[MAXLINELENGTH];
    stateType state;
    FILE *filePtr;

    if (argc != 2) {
	printf("error: usage: %s <machine-code file>\n", argv[0]);
	exit(1);
    }

    /* initialize memories and registers */
    for (i=0; i<NUMMEMORY; i++) {
	state.mem[i] = 0;
    }
    for (i=0; i<NUMREGS; i++) {
	state.reg[i] = 0;
    }

    state.pc=0;

    /* read machine-code file into instruction/data memory (starting at
	address 0) */

    filePtr = fopen(argv[1], "r");
    if (filePtr == NULL) {
	printf("error: can't open file %s\n", argv[1]);
	perror("fopen");
	exit(1);
    }

    for (state.numMemory=0; fgets(line, MAXLINELENGTH, filePtr) != NULL;
	state.numMemory++) {
	if (sscanf(line, "%d", state.mem+state.numMemory) != 1) {
	    printf("error in reading address %d\n", state.numMemory);
	    exit(1);
	}
	printf("memory[%d]=%d\n", state.numMemory, state.mem[state.numMemory]);
    }

    printf("\n");

    /* run never returns */
    run(state);

    return(0);
}

void
printState(stateType *statePtr, char *stateName)
{
    int i;
    static int cycle = 0;
    printf("\n@@@\nstate %s (cycle %d)\n", stateName, cycle++);
    printf("\tpc %d\n", statePtr->pc);
    printf("\tmemory:\n");
	for (i=0; i<statePtr->numMemory; i++) {
	    printf("\t\tmem[ %d ] %d\n", i, statePtr->mem[i]);
	}
    printf("\tregisters:\n");
	for (i=0; i<NUMREGS; i++) {
	    printf("\t\treg[ %d ] %d\n", i, statePtr->reg[i]);
	}
    printf("\tinternal registers:\n");
    printf("\t\tmemoryAddress %d\n", statePtr->memoryAddress);
    printf("\t\tmemoryData %d\n", statePtr->memoryData);
    printf("\t\tinstrReg %d\n", statePtr->instrReg);
    printf("\t\taluOperand %d\n", statePtr->aluOperand);
    printf("\t\taluResult %d\n", statePtr->aluResult);
}

/*
 * Access memory:
 *     readFlag=1 ==> read from memory
 *     readFlag=0 ==> write to memory
 * Return 1 if the memory operation was successful, otherwise return 0
 */
int
memoryAccess(stateType *statePtr, int readFlag)
{
    static int lastAddress = -1;
    static int lastReadFlag = 0;
    static int lastData = 0;
    static int delay = 0;

    if (statePtr->memoryAddress < 0 || statePtr->memoryAddress >= NUMMEMORY) {
	printf("memory address out of range\n");
	exit(1);
    }

    /*
     * If this is a new access, reset the delay clock.
     */
    if ( (statePtr->memoryAddress != lastAddress) ||
	     (readFlag != lastReadFlag) ||
	     (readFlag == 0 && lastData != statePtr->memoryData) ) {
	
	
	delay = statePtr->memoryAddress % 3;
	//delay = 0; The delay function is easy to break here
	
	lastAddress = statePtr->memoryAddress;
	lastReadFlag = readFlag;
	lastData = statePtr->memoryData;
    }

    if (delay == 0) {
	/* memory is ready */
	if (readFlag) {
	    statePtr->memoryData = statePtr->mem[statePtr->memoryAddress];
	} else {
	    statePtr->mem[statePtr->memoryAddress] = statePtr->memoryData;
	}
	return(1);
    } else {
	/* memory is not ready */
	delay--;
	return(0);
    }
}

int
convertNum(int num)
{
    /* convert a 16-bit number into a 32-bit integer */
    if (num & (1 << 15) ) {
	num -= (1 << 16);
    }
    return(num);
}

void 
run(stateType state)
{
	int bus = 0; // Declare / initialize bus value
	
	fetch:
		printState(&state, "fetch");
		bus = state.pc++;
		state.memoryAddress = bus;
		goto fetch2;
	fetch2:
		printState(&state, "fetch");
		if (!memoryAccess(&state, 1))
			goto fetch2;
		else
			goto decode;			
		
	decode:
		printState(&state, "decode");
		bus = state.memoryData;
		state.instrReg = bus;
		if ((bus >> 22) == ADD)
			goto add;
		if ((bus >> 22) == NAND)
			goto nand;			
		if ((bus >> 22) == LW)
			goto lw;
		if ((bus >> 22) == SW)
			goto sw;			
		if ((bus >> 22) == BEQ)
			goto beq;			
		if ((bus >> 22) == JALR)
			goto jalr;			
		if ((bus >> 22) == HALT)
			goto halt;			
		if ((bus >> 22) == NOOP)
			goto noop;
	
	add:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 19) & 7];
		state.aluOperand = bus;
		goto add2;
	add2:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 16) & 7];
		state.aluResult = state.aluOperand + bus;
		goto add3;
	add3:
		printState(&state, "execute");
		bus = state.aluResult;
		state.reg[state.instrReg & 7] = bus;
		goto fetch;
	
	nand:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 19) & 7];
		state.aluOperand = bus;
		goto nand2;
	nand2:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 16) & 7];
		state.aluResult = ~(state.aluOperand & bus);
		goto nand3;
	nand3:
		printState(&state, "execute");
		bus = state.aluResult;
		state.reg[state.instrReg & 7] = bus;
		goto fetch;	
		
	lw:
		printState(&state, "execute");	
		bus = state.reg[(state.instrReg >> 19) & 7];
		state.aluOperand = bus;		
		goto lw2;
	lw2:
		printState(&state, "execute");
		bus = convertNum(state.instrReg & 0xFFFF);
		state.aluResult = state.aluOperand + bus;
		goto lw3;
	lw3:
		printState(&state, "execute");
		bus = state.aluResult;
		state.memoryAddress = bus;
		goto lw4;
	lw4:
		printState(&state, "execute");
		if (!memoryAccess(&state, 1))
			goto lw4;
		else
			goto lw5;
	lw5:
		printState(&state, "execute");
		bus = state.memoryData;
		state.reg[(state.instrReg >> 16) & 7] = bus;
		goto fetch;
		
	sw:
		printState(&state, "execute");	
		bus = state.reg[(state.instrReg >> 19) & 7];
		state.aluOperand = bus;		
		goto sw2;
	sw2:
		printState(&state, "execute");
		bus = convertNum(state.instrReg & 0xFFFF);
		state.aluResult = state.aluOperand + bus;
		goto sw3;
	sw3:
		printState(&state, "execute");
		bus = state.aluResult;
		state.memoryAddress = bus;
		goto sw4;
	sw4:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 16) & 7];
		state.memoryData = bus;
		goto sw5;
	sw5:
		printState(&state, "execute");
		if (!memoryAccess(&state, 0))
			goto sw5;
		else
			goto fetch;		
			
	beq:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 19) & 7];
		state.aluOperand = bus;
		goto beq2;
	beq2:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 16) & 7];
		state.aluResult = state.aluOperand - bus;
		goto beq3;	
	beq3:
		printState(&state, "execute");
		if (state.aluResult != 0)
			goto fetch;
		bus = state.pc;
		state.aluOperand = bus;
		goto beq4;
	beq4:
		printState(&state, "execute");
		bus = convertNum(state.instrReg & 0xFFFF);
		state.aluResult = state.aluOperand + bus;
		goto beq5;
	beq5:
		printState(&state, "execute");
		bus = state.aluResult;
		state.pc = bus;
		goto fetch;
				
	jalr:
		printState(&state, "execute");
		bus = state.pc;
		state.reg[(state.instrReg >> 16) & 7] = bus;
		goto jalr2;
	jalr2:
		printState(&state, "execute");
		bus = state.reg[(state.instrReg >> 19) & 7];
		state.pc = bus;
		goto fetch;
			
	noop:					
		printState(&state, "execute");	
		goto fetch;
					
	halt:
		printState(&state, "halt");
		exit(0);
		
	// The function should never return if executed correctly
	return;
}

