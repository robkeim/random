// Rob Keim
// EECS 370
// Project 1 - Assembler

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define MAXLINELENGTH 1000

int readAndParse(FILE *, char *, char *, char *, char *, char *);
int isNumber(char *);

int main(int argc, char *argv[])
{
	char *inFileString, *outFileString;
	FILE *inFilePtr, *outFilePtr;
	char label[MAXLINELENGTH], opcode[MAXLINELENGTH], arg0[MAXLINELENGTH],
	        arg1[MAXLINELENGTH], arg2[MAXLINELENGTH];
	
	int count = 0, number = 0, regA = 0, regB = 0, destReg = 0, offsetField = 0, OP = 0;          // What is OP???
	char *labelPtrs[MAXLINELENGTH];
	
	if (argc != 3) // Error Checking
	{
		printf("error: usage: %s <assembly-code-file> <machine-code-file>\n",
           argv[0]);
        exit(1);
    }
    
	inFileString = argv[1];
	outFileString = argv[2];
	
	inFilePtr = fopen(inFileString, "r");
	if (inFilePtr == NULL)
	{
		printf("error in opening %s\n", inFileString);
		exit(1);
	}
	
	outFilePtr = fopen(outFileString, "w");
	if (outFilePtr == NULL)
	{
		printf("error in openening %s\n", outFileString);
		exit(1);
	} 

	// Create array of labels
	while(readAndParse(inFilePtr, label, opcode, arg0, arg1, arg2))
	{
		labelPtrs[count] = strdup(label);
		count++;
	}
	
	int lastLabel = count;

	rewind(inFilePtr); // Move pointer back to beginning of file
	
	count = 0;
	
    while (readAndParse(inFilePtr, label, opcode, arg0, arg1, arg2))
    {        
	    number = 0, regA = 0, regB = 0, destReg = 0;
	    
        if (!strcmp(opcode, "add"))
        {
			OP = 0; // 000
			sscanf(arg0, "%d", &regA);
			sscanf(arg1, "%d", &regB);
			sscanf(arg2, "%d", &destReg);
			number = (OP << 22) | (regA << 19) | (regB << 16) | (destReg);
        }
        else if (!strcmp(opcode, "nand"))
        {   
			OP = 1; // 001
			sscanf(arg0, "%d", &regA);
			sscanf(arg1, "%d", &regB);
			sscanf(arg2, "%d", &destReg);
			number = (OP << 22) | (regA << 19) | (regB << 16) | (destReg);	
        }
        else if (!strcmp(opcode, "lw"))
        {
			OP = 2; // 010
			sscanf(arg0, "%d", &regA);
			sscanf(arg1, "%d", &regB);
			if (isNumber(arg2))
			{
				sscanf(arg2, "%d", &offsetField);
				if (offsetField < -32768 || offsetField > 32767)
				{
					// invalid offsetField
					exit(1);
				}
				if (offsetField < 0)
				{
					offsetField = offsetField & 65535;
				}
			}
			else
			{
				int tmp = 0, found = 0;
				while (!found && tmp < lastLabel)
				{
					if (!strcmp(labelPtrs[tmp], arg2))
					{
						found = 1;
						offsetField = tmp;
						if (offsetField < -32768 || offsetField > 32767)
						{
							// invalid offsetField
							exit(1);
						}						
					}
					else
					{
						tmp++;
					}						
				}
				if (!found)
				{
					// Undefined label
					exit(1);
				}
			}
			number = (OP << 22) | (regA << 19) | (regB << 16) | (offsetField);         
        }
        else if (!strcmp(opcode, "sw"))
        {
			OP = 3; // 011		
			sscanf(arg0, "%d", &regA);
			sscanf(arg1, "%d", &regB);
			if (isNumber(arg2))
			{
				sscanf(arg2, "%d", &offsetField);
				if (offsetField < -32768 || offsetField > 32767)
				{
					// invalid offsetField
					exit(1);
				}				
				if (offsetField < 0)
				{
					offsetField = offsetField & 65535;
				}
			}
			else
			{
				int tmp = 0, found = 0;
				while (!found && tmp < lastLabel)
				{
					if (!strcmp(labelPtrs[tmp], arg2))
					{
						found = 1;						
						offsetField = tmp;
						if (offsetField < -32768 || offsetField > 32767)
						{
							// invalid offsetField
							exit(1);
						}						
					}
					else
					{
						tmp++;
					}						
				}
				if (!found)
				{
					// Undefined label
					exit(1);
				}
			}
			number = (OP << 22) | (regA << 19) | (regB << 16) | (offsetField);            
        }
        else if (!strcmp(opcode, "beq"))
        {
			OP = 4; // 100	
			sscanf(arg0, "%d", &regA);
			sscanf(arg1, "%d", &regB);
			if (isNumber(arg2))
			{
				sscanf(arg2, "%d", &offsetField);
				if (offsetField < -32768 || offsetField > 32767)
				{
					// invalid offsetField
					exit(1);
				}				
				if (offsetField < 0)
				{
					offsetField = offsetField & 65535;
				}				
			}
			else
			{
				int tmp = 0, found = 0;
				while (!found && tmp < lastLabel)
				{
					if (!strcmp(labelPtrs[tmp], arg2))
					{
						found = 1;
						offsetField = tmp - (count + 1);
						if (offsetField < -32768 || offsetField > 32767)
						{
							// invalid offsetField
							exit(1);
						}						
						if (offsetField < 0)
						{
							offsetField = offsetField & 65535;
						}
					}
					else
					{
						tmp++;
					}						
				}
				if (!found)
				{
					// Undefined label
					exit(1);
				}					
			}
			number = (OP << 22) | (regA << 19) | (regB << 16) | (offsetField);
        }
        else if (!strcmp(opcode, "jalr")) // Haven't checked this yet, so not sure if it works
        {  		
			OP = 5; // 101
			sscanf(arg0, "%d", &regA);
			sscanf(arg1, "%d", &regB);
			number = (OP << 22) | (regA << 19) | (regB << 16);
        } 
        else if (!strcmp(opcode, "halt"))
        {   
			OP = 6; // 110 
			number = (OP << 22);
        }   
        else if (!strcmp(opcode, "noop"))
        { 
			OP = 7; // 111
			number = (OP << 22);
        }
        else if (!strcmp(opcode, ".fill"))
        // Not technically an opcode but has similar purpose
        {           
 			if (isNumber(arg0))
			{
				sscanf(arg0, "%d", &regA);
				number = regA;			
			}
			else
			{
				int tmp = 0, found = 0;
				while (!found && tmp < lastLabel)
				{
					if (!strcmp(labelPtrs[tmp], arg0))
					{
						found = 1;
						number = tmp;
					}
					else
					{
						tmp++;
					}						
				}
				if (!found)
				{
					// Undefined label
					exit(1);
				}	
			} 
        }        
        else
        {
            // Invalid opcode
            exit(1);
        }
		
		fprintf(outFilePtr, "%d\n", number);
        
	
	if (isalpha(label[0])) //Error checking for duplicate labels
        {
            int ii;
            for (ii = 0; ii < count; ii++)
            {
                if (!strcmp(label,labelPtrs[ii]))
                {
                    exit(1);
                }
           }
        }
     
     if (!isalpha(label[0]) && label[0] != '\0') // Checks to see if the label starts with an invalid character
     {
     	exit(1);
     }   
     
     if (strlen(label) > 6) // Checks for valid string length
     {
     	exit(1);
     }
	 
	count++;       
    } 
       
	exit(0);
    return(0);
}

/*
 * Read and parse a line of the assembly-language file.  Fields are returned
 * in label, opcode, arg0, arg1, arg2 (these strings must have memory already
 * allocated to them).
 *
 * Return values:
 *     0 if reached end of file
 *     1 if all went well
 *
 * exit(1) if line is too long.
 */
int
readAndParse(FILE *inFilePtr, char *label, char *opcode, char *arg0,
    char *arg1, char *arg2)
{
    char line[MAXLINELENGTH];
    char *ptr = line;

    /* delete prior values */
    label[0] = opcode[0] = arg0[0] = arg1[0] = arg2[0] = '\0';

    /* read the line from the assembly-language file */
    if (fgets(line, MAXLINELENGTH, inFilePtr) == NULL) {
	/* reached end of file */
        return(0);
    }

    /* check for line too long (by looking for a \n) */
    if (strchr(line, '\n') == NULL) {
        /* line too long */
	printf("error: line too long\n");
	exit(1);
    }

    /* is there a label? */
    ptr = line;
    if (sscanf(ptr, "%[^\t\n ]", label)) {
	/* successfully read label; advance pointer over the label */
        ptr += strlen(label);
    }

    /*
     * Parse the rest of the line.  Would be nice to have real regular
     * expressions, but scanf will suffice.
     */
     sscanf(ptr,"%s%s%s%s",opcode,arg0,arg1,arg2);
    /*sscanf(ptr, "%*[\t\n ]%[^\t\n ]%*[\t\n ]%[^\t\n ]%*[\t\n ]%[^\t\n ]%*[\t\n ]%[^\t\n ]",
        opcode, arg0, arg1, arg2);*/
    return(1);
}

int
isNumber(char *string)
{
    /* return 1 if string is a number */
    int i;
    return( (sscanf(string, "%d", &i)) == 1);
}

