/* From p1.c
A whole input line is read into a char array with fgets, and then
the following function is used to extract each word for spell-checking.
*/

/*
find_word tries to find a word in the supplied C-string pointed into
by startp_in. and if found, copies it into the array pointed to by wordp_in,
and returns true. If a word is not found found, it returns false.
It starts scanning the input line from the character designated by startp_in.  
After finding a word, startp_in's referent is set to the first character of the word, 
and endp_in's referent is set to the character after the last character of the word. 
If no word is found, startp and endp don't mean anything.
To find the next word, call again with startp_in set to the last endp_in. */
int find_word(char * wordp_in, char ** startp_in, char ** endp_in)
{
	char * startp = *startp_in;	/* make our syntax easier */
	char * endp = *endp_in;
	
	/* repeat until we either find a word or hit the end of the input line */
	while(1) {
		int len;
		char * wordp = wordp_in;

		/* skip over any whitespace or punctuation */
		while(*startp && (isspace(*startp) || ispunct(*startp)))
			startp++;
	
		/* if now at a null byte, a word was not found */
		if (!*startp)
			return 0;
	
		/* Now look for the end of the word; include any apostrophes in the word, so the
		end is marked by non-alpha and non-apostrophe. But stop when we have included the maximum number of characters 
		that could be in the word. */
		for(endp = startp; *endp && (isalpha(*endp) || *endp == '\'') && endp < startp + MAX_WORD_LENGTH; endp++);
		
		/* do we have a valid word? if so, how long is it? */
		len = check_and_trim(startp, endp);
		if(len) {
			/* copy the trimmed word */
			strncpy(wordp, startp, len);
			*(wordp + len) = '\0';
			/* return the updated pointers */
			*startp_in = startp;
			/* the next word search starts at the beginning of any trimmed part! */
			*endp_in = startp + len;
			return 1;
			}
		
		/* if no valid word, skip until we hit end of the string or get whitespace or punctuation, and go around again. */
		while(*endp && !(isspace(*endp) || ispunct(*endp)))
			endp++;
		if(!*endp)	
			return 0;	/* no word remaining */
		startp = endp;

		}
}

/* if the word is not valid, return 0 for its length;
if valid, return length that the excludes the trimmed ending of the word 
- currently assumes that endings are lower case already
*/
int check_and_trim(char * startp, char * endp)
{
	int len = endp - startp;
	/* must be more than 1 character long and terminated by null byte, space, or punctuation 
	(a final apostrophe is part of word) */
	if(len < 2 || !( !(*endp) || isspace(*endp) || ispunct(*endp)))
		return 0;
	/* trim off some undesired endings and return the resulting length or a non-word result */
	if(len >= 4) {
		if(strncmp(endp - 2, "'s", 2) == 0)
			return len - 2;
		else if(strncmp(endp - 2, "s'", 2) == 0)
			return len - 1;
		}
	if(len == 3) {
		if(strncmp(endp - 2, "'s", 2) == 0)
			/* a one letter word with 's at the end, not a word */
			return 0;
		}
	if(len >= 3) {
		if(strncmp(endp - 1, "'", 1) == 0)
			return len - 1;
		}
	return len;
}
