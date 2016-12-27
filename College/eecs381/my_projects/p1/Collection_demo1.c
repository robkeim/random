#include "Collection.h"
#include "Record.h"

#include <string.h>

int main()
{
	struct Collection * c1;
	struct Collection * c2;
	struct Collection * c3;
	struct Collection * c4;
	
	struct Record * r1;
	struct Record * r2;

	FILE * file;

	c1 = create_Collection("ASDF");
	c2 = create_Collection("junk");

	if (strcmp(get_Collection_name(c1), "ASDF"))
	{
		printf("ERROR: incorrect name\n");
	}

	print_Collection(c1);

	if (!Collection_empty(c1))
	{
		printf("ERROR: Collection should be empty\n");
	}

	r1 = create_Record("MED", "TITLE");

	if (add_Collection_member(c1, r1))
	{
		printf("ERROR: Failed to add record!\n");
	}

	if (!add_Collection_member(c1, r1))
	{
		printf("ERROR: Should not be able to add item again\n");
	}
	
	print_Collection(c1);
	if (Collection_empty(c1))
	{
		printf("ERROR: Collection shouldn't be empty\n");
	}

	r2 = create_Record("ABC", "123");
	add_Collection_member(c1, r2);

	if (!is_Collection_member_present(c1, r2))
	{
		printf("ERROR: Record not found\n");
	}

	print_Collection(c1);

	if (remove_Collection_member(c1, r2))
	{
		printf("ERROR: Could not remove record\n");
	}

	print_Collection(c1);

	if (is_Collection_member_present(c1, r2))
	{
		printf("ERROR: record should not be present\n");
	}

	if (!remove_Collection_member(c1, r2))
	{
		printf("ERROR: Record shouldn't be in the collection\n");
	}

	add_Collection_member(c1, r2);
	
	print_Collection(c1);

	file = fopen("tmp", "w");
	save_Collection(c1, file);
	save_Collection(c2, file);

	fclose(file);

	destroy_Collection(c1);
	destroy_Collection(c2);

	file = fopen("tmp", "r");
	/*c3 = load_Collection(file, XXX);
	c4 = load_Collection(file, XXX);

	print_Collection(c3);
	print_Collection(c4);

	destroy_Collection(c3);
	destroy_Collection(c4);*/

	return 0;
}

