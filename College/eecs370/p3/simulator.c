// Rob Keim
// EECS 370
// Project 3 - Simulator

#define NUMMEMORY 65536 /* maximum number of data words in memory */
#define NUMREGS 8 /* number of machine registers */
#define MAXLINELENGTH 1000

#define ADD 0
#define NAND 1
#define LW 2
#define SW 3
#define BEQ 4
#define JALR 5 /* JALR will not implemented for Project 3 */
#define HALT 6
#define NOOP 7

#define NOOPINSTRUCTION 0x1c00000

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

typedef struct IFIDStruct {
    int instr;
    int pcPlus1;
} IFIDType;

typedef struct IDEXStruct {
    int instr;
    int pcPlus1;
    int readRegA;
    int readRegB;
    int offset;
} IDEXType;

typedef struct EXMEMStruct {
    int instr;
    int branchTarget;
    int aluResult;
    int readRegB;
} EXMEMType;

typedef struct MEMWBStruct {
    int instr;
    int writeData;
} MEMWBType;

typedef struct WBENDStruct {
    int instr;
    int writeData;
} WBENDType;

typedef struct stateStruct {
    int pc;
    int instrMem[NUMMEMORY];
    int dataMem[NUMMEMORY];
    int reg[NUMREGS];
    int numMemory;
    IFIDType IFID;
    IDEXType IDEX;
    EXMEMType EXMEM;
    MEMWBType MEMWB;
    WBENDType WBEND;
    int cycles; /* number of cycles run so far */
} stateType;

void
printInstruction(int instr);

void
printState(stateType *statePtr)
{
    int i;
    printf("\n@@@\nstate before cycle %d starts\n", statePtr->cycles);
    printf("\tpc %d\n", statePtr->pc);

    printf("\tdata memory:\n");
	for (i=0; i<statePtr->numMemory; i++) {
	    printf("\t\tdataMem[ %d ] %d\n", i, statePtr->dataMem[i]);
	}
    printf("\tregisters:\n");
	for (i=0; i<NUMREGS; i++) {
	    printf("\t\treg[ %d ] %d\n", i, statePtr->reg[i]);
	}
    printf("\tIFID:\n");
	printf("\t\tinstruction ");
	printInstruction(statePtr->IFID.instr);
	printf("\t\tpcPlus1 %d\n", statePtr->IFID.pcPlus1);
    printf("\tIDEX:\n");
	printf("\t\tinstruction ");
	printInstruction(statePtr->IDEX.instr);
	printf("\t\tpcPlus1 %d\n", statePtr->IDEX.pcPlus1);
	printf("\t\treadRegA %d\n", statePtr->IDEX.readRegA);
	printf("\t\treadRegB %d\n", statePtr->IDEX.readRegB);
	printf("\t\toffset %d\n", statePtr->IDEX.offset);
    printf("\tEXMEM:\n");
	printf("\t\tinstruction ");
	printInstruction(statePtr->EXMEM.instr);
	printf("\t\tbranchTarget %d\n", statePtr->EXMEM.branchTarget);
	printf("\t\taluResult %d\n", statePtr->EXMEM.aluResult);
	printf("\t\treadRegB %d\n", statePtr->EXMEM.readRegB);
    printf("\tMEMWB:\n");
	printf("\t\tinstruction ");
	printInstruction(statePtr->MEMWB.instr);
	printf("\t\twriteData %d\n", statePtr->MEMWB.writeData);
    printf("\tWBEND:\n");
	printf("\t\tinstruction ");
	printInstruction(statePtr->WBEND.instr);
	printf("\t\twriteData %d\n", statePtr->WBEND.writeData);
}

int
field0(int instruction)
{
    return( (instruction>>19) & 0x7);
}

int
field1(int instruction)
{
    return( (instruction>>16) & 0x7);
}

int
field2(int instruction)
{
    return(instruction & 0xFFFF);
}

int opcode(int instruction)
{
    return(instruction>>22);
}

void
printInstruction(int instr)
{
    char opcodeString[10];
    if (opcode(instr) == ADD) {
	strcpy(opcodeString, "add");
    } else if (opcode(instr) == NAND) {
	strcpy(opcodeString, "nand");
    } else if (opcode(instr) == LW) {
	strcpy(opcodeString, "lw");
    } else if (opcode(instr) == SW) {
	strcpy(opcodeString, "sw");
    } else if (opcode(instr) == BEQ) {
	strcpy(opcodeString, "beq");
    } else if (opcode(instr) == JALR) {
	strcpy(opcodeString, "jalr");
    } else if (opcode(instr) == HALT) {
	strcpy(opcodeString, "halt");
    } else if (opcode(instr) == NOOP) {
	strcpy(opcodeString, "noop");
    } else {
	strcpy(opcodeString, "data");
    }

    printf("%s %d %d %d\n", opcodeString, field0(instr), field1(instr),
	field2(instr));
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

int
dataHazardsReg(int choose, stateType *state)
{
	int result;
	int regNum;
	if (choose == 0)
	{
		result = state->IDEX.readRegA;
		regNum = field0(state->IDEX.instr);			
	}
	else if (choose == 1)
	{
		result = state->IDEX.readRegB;
		regNum = field1(state->IDEX.instr);			
	}
	else
	{
		// Should never occur
	}
			
	if (((opcode(state->WBEND.instr) == ADD || opcode(state->WBEND.instr) == NAND)) && (field2(state->WBEND.instr) == regNum))
	{
		result = state->WBEND.writeData;
	}
	if (((opcode(state->MEMWB.instr) == ADD || opcode(state->MEMWB.instr) == NAND)) && (field2(state->MEMWB.instr) == regNum))
	{
		result = state->MEMWB.writeData;
	}
	if (((opcode(state->EXMEM.instr) == ADD || opcode(state->EXMEM.instr) == NAND)) && (field2(state->EXMEM.instr) == regNum))
	{
		result = state->EXMEM.aluResult;
	}	
	if (opcode(state->WBEND.instr) == LW && field1(state->WBEND.instr) == regNum)
	{
		result = state->WBEND.writeData;
	}
	if (opcode(state->MEMWB.instr) == LW && field1(state->MEMWB.instr) == regNum)
	{
		result = state->MEMWB.writeData;
	}	
	
	return result;
}

int main(int argc, char *argv[])
{
    char line[MAXLINELENGTH];
    stateType state, newState;
    FILE *filePtr;

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

    /* read in the entire machine-code file into memory */
    for (state.numMemory = 0; fgets(line, MAXLINELENGTH, filePtr) != NULL; state.numMemory++) 
    {
		if (sscanf(line, "%d", state.instrMem+state.numMemory) != 1) 
		{
			printf("error in reading address %d\n", state.numMemory);
			exit(1);
		}
		printf("memory[%d]=%d\n", state.numMemory, state.instrMem[state.numMemory]);
    }
    
    printf("%d memory words\n\tinstruction memory:\n", state.numMemory);
    int i = 0;
    for (i = 0; i < state.numMemory; i++)
    {
    	state.dataMem[i] = state.instrMem[i];
    	printf("\t\tinstrMem[ %d ] ", i);
    	printInstruction(state.instrMem[i]);
    }

	
	state.pc = 0;
	state.cycles = 0;
	
	for (i = 0; i < NUMREGS; i++)
	{
		state.reg[i] = 0;
	}
	
	state.IFID.instr = NOOPINSTRUCTION;
	state.IDEX.instr = NOOPINSTRUCTION;
	state.EXMEM.instr = NOOPINSTRUCTION;
	state.MEMWB.instr = NOOPINSTRUCTION;
	state.WBEND.instr = NOOPINSTRUCTION;
	
    while (1) 
    {
		printState(&state);

		/* check for halt */
		if (opcode(state.MEMWB.instr) == HALT) {
			printf("machine halted\n");
			printf("total of %d cycles executed\n", state.cycles);
			exit(0);
		}

		newState = state;
		newState.cycles++;

		/* --------------------- IF stage --------------------- */
		newState.pc = state.pc + 1;
		newState.IFID.instr = state.instrMem[state.pc];
		newState.IFID.pcPlus1 = state.pc + 1;
	
		/* --------------------- ID stage --------------------- */
		newState.IDEX.instr = state.IFID.instr;
		newState.IDEX.pcPlus1 = state.IFID.pcPlus1;
		newState.IDEX.readRegA = state.reg[field0(state.IFID.instr)];
		newState.IDEX.readRegB = state.reg[field1(state.IFID.instr)];
		newState.IDEX.offset = convertNum(field2(state.IFID.instr));
		
		if (opcode(state.IDEX.instr) == LW)
		{
			int dependReg = field1(state.IDEX.instr);
			
			if (opcode(state.IFID.instr) == SW || opcode(state.IFID.instr) == ADD || 
			      opcode(state.IFID.instr) == NAND || opcode(state.IFID.instr) == BEQ)
			{
				if (field0(state.IFID.instr) == dependReg || field1(state.IFID.instr) == dependReg)
				{
					newState.IDEX.instr = NOOPINSTRUCTION;
					newState.IFID.instr = state.IFID.instr;
					newState.pc = state.pc;
					newState.IFID.pcPlus1 = state.IFID.pcPlus1;
				}
			}
			if (opcode(state.IFID.instr) == LW)
			{
				if (field0(state.IFID.instr) == dependReg)
				{
					newState.IDEX.instr = NOOPINSTRUCTION;
					newState.IFID.instr = state.IFID.instr;
					newState.pc = state.pc;
					newState.IFID.pcPlus1 = state.IFID.pcPlus1;
				}
			}	      
		}

		/* --------------------- EX stage --------------------- */
		newState.EXMEM.instr = state.IDEX.instr;
		
		newState.EXMEM.branchTarget = state.IDEX.offset + state.IDEX.pcPlus1;
		newState.EXMEM.readRegB = state.IDEX.readRegB;
		
		int regA = dataHazardsReg(0, &state),
			regB = dataHazardsReg(1, &state);
			
		newState.EXMEM.readRegB = regB;
		
		if (opcode(state.IDEX.instr) == ADD)
		{	
			newState.EXMEM.aluResult = regA + regB;
		}
		else if (opcode(state.IDEX.instr) == NAND)
		{
			newState.EXMEM.aluResult = ~(regA & regB);	
		}
		else if (opcode(state.IDEX.instr) == LW)
		{
			newState.EXMEM.aluResult = regA + state.IDEX.offset;		
		}	
		else if (opcode(state.IDEX.instr) == SW)
		{
			newState.EXMEM.aluResult = regA + state.IDEX.offset;	
		}
		else if (opcode(state.IDEX.instr) == BEQ)
		{
			if (regA == regB)
			{
				newState.EXMEM.aluResult = 1;
			}
			else
			{
				newState.EXMEM.aluResult = 0;
			}
		}
		else if (opcode(state.IDEX.instr) == JALR)
		{
		}	
		else if (opcode(state.IDEX.instr) == HALT)
		{
		}	
		else if (opcode(state.IDEX.instr) == NOOP)
		{
		}
		else
		{
			// Invalid instruction
		}

		if (opcode(state.EXMEM.instr) == BEQ && state.EXMEM.aluResult == 1)
		{
			newState.pc = state.EXMEM.branchTarget;
			newState.IFID.instr = NOOPINSTRUCTION;
			newState.IDEX.instr = NOOPINSTRUCTION;
			newState.EXMEM.instr = NOOPINSTRUCTION;
		}	
		
		/* --------------------- MEM stage --------------------- */
		newState.MEMWB.instr = state.EXMEM.instr;
	
		if (opcode(state.EXMEM.instr) == ADD)
		{
			newState.MEMWB.writeData = state.EXMEM.aluResult;
		}
		else if (opcode(state.EXMEM.instr) == NAND)
		{
			newState.MEMWB.writeData = state.EXMEM.aluResult;	
		}
		else if (opcode(state.EXMEM.instr) == LW)
		{
			newState.MEMWB.writeData = state.dataMem[state.EXMEM.aluResult];	
		}	
		else if (opcode(state.EXMEM.instr) == SW)
		{	
			newState.dataMem[state.EXMEM.aluResult] = state.EXMEM.readRegB;
		}
		else if (opcode(state.EXMEM.instr) == BEQ)
		{
		}
		else if (opcode(state.EXMEM.instr) == JALR)
		{
		}	
		else if (opcode(state.EXMEM.instr) == HALT)
		{
		}	
		else if (opcode(state.EXMEM.instr) == NOOP)
		{
		}
		else
		{
			// Invalid instruction
		}
	
		/* --------------------- WB stage --------------------- */
		newState.WBEND.instr = state.MEMWB.instr;
		newState.WBEND.writeData = state.MEMWB.writeData;

		if (opcode(state.MEMWB.instr) == ADD)
		{
			newState.reg[field2(state.MEMWB.instr)] = state.MEMWB.writeData;
		}
		else if (opcode(state.MEMWB.instr) == NAND)
		{	
			newState.reg[field2(state.MEMWB.instr)] = state.MEMWB.writeData;		
		}
		else if (opcode(state.MEMWB.instr) == LW)
		{	
			newState.reg[field1(state.MEMWB.instr)] = state.MEMWB.writeData;
		}	
		else if (opcode(state.MEMWB.instr) == SW)
		{	
		}
		else if (opcode(state.MEMWB.instr) == BEQ)
		{
		}
		else if (opcode(state.MEMWB.instr) == JALR)
		{
		}	
		else if (opcode(state.MEMWB.instr) == HALT)
		{
		}	
		else if (opcode(state.MEMWB.instr) == NOOP)
		{
		}
		else
		{
			// Invalid instruction
		}


		state = newState; /* this is the last statement before end of the loop.
					It marks the end of the cycle and updates the
					current state with the values calculated in this
					cycle */	    		    
    }   
    return 0;
}

