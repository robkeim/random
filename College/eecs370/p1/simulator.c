// Rob Keim
// EECS 370
// Project 1 - Simulator

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define NUMMEMORY 65536 /* maximum number of words in memory */
#define NUMREGS 8 /* number of machine registers */
#define MAXLINELENGTH 1000

typedef struct stateStruct {
    int pc;
    int mem[NUMMEMORY];
    int reg[NUMREGS];
    int numMemory;
} stateType;

void printState(stateType *);

int
main(int argc, char *argv[])
{
    char line[MAXLINELENGTH];
    stateType state;
    FILE *filePtr;
    stateType *statePtr;
    statePtr = &state;

    if (argc != 2) {
	printf("error: usage: %s <machine-code file>\n", argv[0]);
	exit(1);
    }

    filePtr = fopen(argv[1], "r");
    if (filePtr == NULL) {
	printf("error: can't open file %s", argv[1]);
	perror("fopen");
	exit(1);
    }

	state.pc = 0;
	
 	int ii = 0;
	for (ii = 0; ii < NUMREGS; ii++)
	{
		state.reg[ii] = 0;
	}

	/*for (ii = 0; ii < NUMMEMORY; ii++)
	{
		state.mem[ii] = 0;
	}*/	
	
    /* read in the entire machine-code file into memory */
    for (state.numMemory = 0; fgets(line, MAXLINELENGTH, filePtr) != NULL; state.numMemory++) 
    {
		if (sscanf(line, "%d", state.mem+state.numMemory) != 1) 
		{
			printf("error in reading address %d\n", state.numMemory);
			exit(1);
		}
		printf("memory[%d]=%d\n", state.numMemory, state.mem[state.numMemory]);
    }
    
    int instructions = 0;
    int halted = 0;
    while (!halted)
    {	
    	instructions++;
    	printState(statePtr);
    	
		int opcode = (state.mem[state.pc] >> 22) & 7;
		
		int regA = 0, regB = 0, destReg = 0, offsetField = 0;
		
		if (opcode == 0 || opcode == 1)
		{
			regA = (state.mem[state.pc] >> 19) & 7;
			regB = (state.mem[state.pc] >> 16) & 7;
			destReg = (state.mem[state.pc]) & 7;
					
			if (opcode == 0) // add
			{
				state.reg[destReg] = state.reg[regA] + state.reg[regB];
			}
			if (opcode == 1) // nand
			{
				state.reg[destReg] = ~(state.reg[regA] & state.reg[regB]);
			}
		}	
		if (opcode == 2 || opcode == 3 || opcode == 4)
		{
			regA = (state.mem[state.pc] >> 19) & 7;
			regB = (state.mem[state.pc] >> 16) & 7;			
			offsetField = convertNum((state.mem[state.pc]) & 65535);
					
			if (opcode == 2) // lw
			{		
				state.reg[regB] = state.mem[state.reg[regA] + offsetField];	
			}
			if (opcode == 3) // sw
			{
				state.mem[state.reg[regA] + offsetField] = state.reg[regB];
			}
			if (opcode == 4 && (state.reg[regA] == state.reg[regB])) // beq
			{
				state.pc += offsetField; // Does not add 1 since state.pc++ at end of loop will take care of this
			}
		}
		if (opcode == 5) // jalr
		{
			regA = (state.mem[state.pc] >> 19) & 7;
			regB = (state.mem[state.pc] >> 16) & 7;
			
			state.reg[regB] = state.pc + 1;
			
			state.pc = state.reg[regA] - 1; // Subtacts 1 to account for state.pc++ at end of loop (didn't subtract one to test)
		}
		if (opcode == 6) // halt
		{
			halted = 1;
		}
		state.pc++;	
	}
	
	printf("machine halted\n");
	printf("total of %d instructions executed\n", instructions);
	printf("final state of machine:\n");

	printState(statePtr);
	
    return(0);
}

void
printState(stateType *statePtr)
{
    int i;
    printf("\n@@@\nstate:\n");
    printf("\tpc %d\n", statePtr->pc);
    printf("\tmemory:\n");
	for (i=0; i<statePtr->numMemory; i++) {
	    printf("\t\tmem[ %d ] %d\n", i, statePtr->mem[i]);
	}
    printf("\tregisters:\n");
	for (i=0; i<NUMREGS; i++) {
	    printf("\t\treg[ %d ] %d\n", i, statePtr->reg[i]);
	}
    printf("end state\n");
}

int convertNum(int num)
{
	// Converts 16-bit number into a 32-bit number
	if (num & (1 << 15))
	{
		num -= (1 << 16);
	}
	return num;
}


