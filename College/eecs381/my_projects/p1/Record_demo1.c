#include "Record.h"

#include <assert.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main()
{
	FILE * file_ptr;
	
	struct Record * ptr = create_Record("DVD", "<TITLE>");

	if (get_Record_ID(ptr) != 1)
	{
		printf("Error with record's ID\n");
	}

	if (strcmp(get_Record_title(ptr),"<TITLE>"))
	{
		printf("Error with record's TITLE\n");
	}

	print_Record(ptr);
	set_Record_rating(ptr, 1);
	print_Record(ptr);

	file_ptr = fopen("tmp", "w");
	assert(file_ptr);
	save_Record(ptr, file_ptr);
	fclose(file_ptr);

	destroy_Record(ptr);
	ptr = NULL;

	file_ptr = fopen("tmp", "r");
	assert(file_ptr);
	ptr = load_Record(file_ptr);
	fclose(file_ptr);

	print_Record(ptr);

	file_ptr = fopen("tmp", "w");
	assert(file_ptr);
	save_Record(ptr, file_ptr);
	destroy_Record(ptr);
	ptr = NULL;
	ptr = create_Record("NOM", "NOM!");
	save_Record(ptr, file_ptr);
	fclose(file_ptr);

	destroy_Record(ptr);
	ptr = NULL;

	file_ptr = fopen("tmp", "r");
	assert(file_ptr);
	ptr = load_Record(file_ptr);
	print_Record(ptr);
	destroy_Record(ptr);
	ptr = NULL;
	ptr = load_Record(file_ptr);
	print_Record(ptr);
	fclose(file_ptr);

	destroy_Record(ptr);
	ptr = NULL;
	
	return 0;
}
