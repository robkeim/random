// Rob Keim
// EECS 370
// Project 4 - Cache

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define NUMMEMORY 65536 /* maximum number of words in memory */
#define NUMREGS 8 /* number of machine registers */
#define MAXLINELENGTH 1000
#define MAX_SIZE 256

typedef struct stateStruct {
    int pc;
    int mem[NUMMEMORY];
    int reg[NUMREGS];
    int numMemory;
} stateType;

typedef struct structCache
{
	int valid;
	int dirty;
	int lru;
	int min;
} typeCache;

void printState(stateType *);

int convertNum(int num);

enum actionType
        {cacheToProcessor, processorToCache, memoryToCache, cacheToMemory,
        cacheToNowhere};
/*
 * Log the specifics of each cache action.
 *
 * address is the starting word address of the range of data being transferred.
 * size is the size of the range of data being transferred.
 * type specifies the source and destination of the data being transferred.
 *     cacheToProcessor: reading data from the cache to the processor
 *     processorToCache: writing data from the processor to the cache
 *     memoryToCache: reading data from the memory to the cache
 *     cacheToMemory: evicting cache data by writing it to the memory
 *     cacheToNowhere: evicting cache data by throwing it away
 */
void
printAction(int address, int size, enum actionType type)
{
    printf("@@@ transferring word [%d-%d] ", address, address + size - 1);
    if (type == cacheToProcessor) {
        printf("from the cache to the processor\n");
    } else if (type == processorToCache) {
        printf("from the processor to the cache\n");
    } else if (type == memoryToCache) {
        printf("from the memory to the cache\n");
    } else if (type == cacheToMemory) {
        printf("from the cache to the memory\n");
    } else if (type == cacheToNowhere) {
        printf("from the cache to nowhere\n");
    }
}

typeCache cache[MAX_SIZE][MAX_SIZE];

void shit(int address, int blockSizeInWords, int numberOfSets, int blocksPerSet, int type)
// type:
//   0 - fetch
//   1 - lw
//   2 - sw
{
	int set = (address / blockSizeInWords) % numberOfSets;
	
	int minInSet = address;
	while ((minInSet % blockSizeInWords) != 0)
	{
		minInSet--;
	}	
	
	int i = 0, lruValue = -1, lruBlock = -1, foundEmpty = 0, emptyBlock = -1;
	for (i = 0; i < blocksPerSet; i++)
	{
		if (!foundEmpty && (cache[set][i].valid == 0)) // Check for first empty block
		{
			emptyBlock = i;
			foundEmpty = 1;
		}
		
		if ((cache[set][i].valid == 1) && (cache[set][i].lru > lruValue))
		{
			lruValue = cache[set][i].lru;
			lruBlock = i;
		}			
		
		if ((cache[set][i].min == minInSet) && (cache[set][i].valid == 1)) // Check for cache hit
		{				
			int j = 0;
			for (j = 0; j < blocksPerSet; j++)
			{
				if (cache[set][j].valid == 1)
				{
					cache[set][j].lru++; // Increment all LRUs
				}
			}	
			cache[set][i].lru = 0; // Set cache hit LRU to 0
			
			if (type == 0 || type == 1)
			{
				printAction(address, 1, 0);
			}
			if (type == 2)
			{
				printAction(address, 1, 1);
				cache[set][i].dirty = 1;
			}				
			return;			
		}		
	}	
		
if (type == 0 || type == 1)
{
	if (foundEmpty == 1) // place at empty block
	{
		int j = 0;
		for (j = 0; j < blocksPerSet; j++)
		{
			if (cache[set][j].valid == 1)
			{
				cache[set][j].lru++; // Increment all LRUs
			}
		}		
		cache[set][emptyBlock].valid = 1;
		cache[set][emptyBlock].dirty = 0;
		cache[set][emptyBlock].lru = 0;	
		cache[set][emptyBlock].min = minInSet;						
		
		printAction(minInSet, blockSizeInWords, 2);
		printAction(address, 1, 0);
	}
	else
	{
		if (cache[set][lruBlock].dirty == 1)
		{
			printAction(cache[set][lruBlock].min, blockSizeInWords, 3);
		}
		else
		{
			printAction(cache[set][lruBlock].min, blockSizeInWords, 4);			
		}
		
		int j = 0;
		for (j = 0; j < blocksPerSet; j++)
		{
			if (cache[set][j].valid == 1)
			{
				cache[set][j].lru++; // Increment all LRUs
			}
		}		
		cache[set][lruBlock].valid = 1;
		cache[set][lruBlock].dirty = 0;
		cache[set][lruBlock].lru = 0;	
		cache[set][lruBlock].min = minInSet;						
		
		printAction(minInSet, blockSizeInWords, 2);
		printAction(address, 1, 0);		
		
	}
}	

if (type == 2)
{
	if (foundEmpty == 1) // place at empty block
	{
		int j = 0;
		for (j = 0; j < blocksPerSet; j++)
		{
			if (cache[set][j].valid == 1)
			{
				cache[set][j].lru++; // Increment all LRUs
			}
		}		
		cache[set][emptyBlock].valid = 1;
		cache[set][emptyBlock].dirty = 1;
		cache[set][emptyBlock].lru = 0;	
		cache[set][emptyBlock].min = minInSet;						
		
		printAction(minInSet, blockSizeInWords, 2);
		printAction(address, 1, 1);
	}
	else
	{
		if (cache[set][lruBlock].dirty == 1)
		{
			printAction(cache[set][lruBlock].min, blockSizeInWords, 3);
		}
		else
		{
			printAction(cache[set][lruBlock].min, blockSizeInWords, 4);			
		}
		
		int j = 0;
		for (j = 0; j < blocksPerSet; j++)
		{
			if (cache[set][j].valid == 1)
			{
				cache[set][j].lru++; // Increment all LRUs
			}
		}		
		cache[set][lruBlock].valid = 1;
		cache[set][lruBlock].dirty = 1;
		cache[set][lruBlock].lru = 0;	
		cache[set][lruBlock].min = minInSet;						
		
		printAction(minInSet, blockSizeInWords, 2);
		printAction(address, 1, 1);		
		
	}
}	
	return;
}	

int
main(int argc, char *argv[])
{
    char line[MAXLINELENGTH];
    stateType state;
    FILE *filePtr;
    stateType *statePtr;
    statePtr = &state;
    
    int blockSizeInWords = atoi(argv[2]),
         numberOfSets = atoi(argv[3]),
         blocksPerSet = atoi(argv[4]);

   if (argc != 5) 
   {
		printf("error: usage: %s <machine-code file> <blckSizeInWords> <numberOfSets> <blocksPerSet>\n", argv[0]);
		exit(1);
   }

   filePtr = fopen(argv[1], "r");
   if (filePtr == NULL) 
   {
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
	
   /* read in the entire machine-code file into memory */
   for (state.numMemory = 0; fgets(line, MAXLINELENGTH, filePtr) != NULL; state.numMemory++) 
   {
		if (sscanf(line, "%d", state.mem+state.numMemory) != 1) 
		{
			printf("error in reading address %d\n", state.numMemory);
			exit(1);
		}
		//printf("memory[%d]=%d\n", state.numMemory, state.mem[state.numMemory]);
   }
    
 //  typeCache cache[MAX_SIZE][MAX_SIZE]; // Declare cache array
   //typeCache *cachePtr;
  // cachePtr = &cache;

	int row = 0, col = 0;
	
	for (row = 0; row < MAX_SIZE; row++) // Initialize cache array
	{
		for (col = 0; col < MAX_SIZE; col++)
		{
			cache[row][col].valid = 0;
			cache[row][col].dirty = 0;
			cache[row][col].lru = 0;
			cache[row][col].min = -1;
		}	
	}	
    int instructions = 0;
    int halted = 0;
    while (!halted)
    {	
    	instructions++;
    	//printState(statePtr);
    	
    	shit(state.pc, blockSizeInWords, numberOfSets, blocksPerSet, 0);
    	
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
				shit(offsetField + state.reg[regA], blockSizeInWords, numberOfSets, blocksPerSet, 1);
			}
			if (opcode == 3) // sw
			{
				state.mem[state.reg[regA] + offsetField] = state.reg[regB];
				shit(offsetField + state.reg[regA], blockSizeInWords, numberOfSets, blocksPerSet, 2);
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
			// Go through and clean the dirty bits
		}
		state.pc++;	
	}
	
	//printf("machine halted\n");
	//printf("total of %d instructions executed\n", instructions);
	//printf("final state of machine:\n");

	//printState(statePtr);
	
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


