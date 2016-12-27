/* This isn't an excerpt from the instructor's solution, but rather an experiment by Mitchell Bloch to
explore the capabilities of C's scanf function, which is capabable of elaborate parsing.

His goal was to see if he could perform the word extraction using scanf's parsing mechanisms to do most of the 
work done by the code used in the actual instructor's solution. It works, but as you can see, it requires 
some additional careful code to specify to scanf the same information that is available from isalpha(), isspace(), and ispunct.

This will be of interest to those who want to do more extensive future work purely in C. However, it cannot be used in subsequent
projects because only C++ input functions can be used in the remaining projects.
*/


#include <ctype.h>
#include <stdio.h>
#include <string.h>

#define str(s) #s
#define xstr(c) str(c)

#define MIN_WORD_LENGTH          2
#define MAX_WORD_LENGTH          31
#define MAX_LINE_LENGTH          127

#define VALID_WORD_LETTERS       "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
#define VALID_WHITESPACE         " \t\v\f\r\n"
#define VALID_PUNCTUATION        "]!\"#$%&'()*+,-./:;<=>?@[\\^_`{|}~" /* Note that ']' being first is critically important. */
#define VALID_WORD_CHARACTERS    VALID_WORD_LETTERS "'"
#define VALID_RUBBISH_CHARACTERS VALID_PUNCTUATION VALID_WHITESPACE

#define SCANF_FORMAT_WORD         "%" xstr(MAX_WORD_LENGTH) "[" VALID_WORD_CHARACTERS "]s"
#define SCANF_FORMAT_RUBBISH      "%" xstr(MAX_LINE_LENGTH) "[" VALID_PUNCTUATION VALID_WHITESPACE "]s"
#define SCANF_FORMAT_RUBBISH_WORD "%" xstr(MAX_LINE_LENGTH) "[^" VALID_PUNCTUATION VALID_WHITESPACE "]s"

/* Process an input_file, calling word_processor for each word encountered.
If output_file is non-NULL, it dumps the modified stream to the output_file. */
static void process(FILE * input_file);

/* Process a line of input, calling word_processor for each word encountered. */
static void process_line(char * line);

/* Cut the string, by the insertion of a '\0', so that only a valid word part remains.
  Then return the length. */
static int enforce_word(char * str);

/* Dummy function - Replace with something useful */
static void print_word(char * word);

/* Dummy function - Replace with something useful */
static void print_nonword(char * nonword);

int main(int argc, char **argv)
{
 if(argc == 2) {
   FILE * input = fopen(argv[1], "r");
   if(!input) {
     fprintf(stderr, "Error: '%s' could not be opened for reading.\n", argv[1]);
     return 2;
   }

   process(input);

   fclose(input);
 }
 else {
   fprintf(stderr, "Syntax: parser.exe input_file.txt\n");
   return 1;
 }

 return 0;
}

/* Process an input_file, calling word_processor for each word encountered.
If output_file is non-NULL, it dumps the modified stream to the output_file. */
void
process(FILE * input_file)
{
 char line[MAX_LINE_LENGTH + 1];

 while(fgets(line, MAX_LINE_LENGTH + 1, input_file))
   process_line(line);
}

/* Process a line of input, calling word_processor for each word encountered. */
void
process_line(char * line)
{
 char chunk[MAX_LINE_LENGTH + 1];
 int i, end;

 for(i = 0, end = strlen(line); i != end; end = strlen(line)) {
   int word_length;

   if((!i || isspace(line[i - 1]) || ispunct(line[i - 1])) &&
      sscanf(line + i, SCANF_FORMAT_WORD, chunk) == 1 &&
      (word_length = enforce_word(chunk)))
   {
     int word_end = i + word_length;
     if(word_end == end || isspace(line[word_end]) || ispunct(line[word_end])) {
       print_word(chunk); /* Process word */
       i += strlen(chunk);
     }
   }

   if(sscanf(line + i, SCANF_FORMAT_RUBBISH_WORD, chunk) == 1) {
     print_nonword(chunk); /* Process rubbish_word) */
     i += strlen(chunk);
   }

   if(sscanf(line + i, SCANF_FORMAT_RUBBISH, chunk) == 1) {
     print_nonword(chunk); /* Process rubbish) */
     i += strlen(chunk);
   }
 }
}

/* Cut the string, by the insertion of a '\0', so that only a valid word part remains.
  Then return the length. */
int
static enforce_word(char * str)
{
 int length = strlen(str);

 /* Strip punctuation from the end of the word. */

 for(; length && !isalpha(str[length - 1]); --length);

 if(length < MIN_WORD_LENGTH ||
    str[length - 1] == 's' && str[length - 2] == '\'' &&
    (length -= 2) < MIN_WORD_LENGTH)
 {
   str[0] = '\0';
   return 0;
 }

 /* Eliminate words that begin with punctuation. */

 if(ispunct(str[0])) {
   str[0] = '\0';
   return 0;
 }

 /* Return the new length */

 str[length] = '\0';
 return length;
}

/* Dummy function - Replace with something useful */
void
print_word(char * word)
{
 printf("1: %s\n", word);
}

/* Dummy function - Replace with something useful */
void
print_nonword(char * nonword)
{
 printf("0: %s\n", nonword);
}