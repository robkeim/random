#ifndef ACTION_ITEM_H
#define ACTION_ITEM_H

/* This header file must be used as-is, without modification, for Project 1 */

/* An Action_item is used to specify actions to be performed on the remainder of
a document that is being spell-checked. 
If "foobar" is to be skipped, then an Action_item is created that 
has SKIP as the action, and "foobar" pointed to by the match_str.
If "foobar" is to be replaced by "dingbat", then 
an Action_item is created that has REPLACE as the action, 
"foobar" pointed to by the match_str, and "dingbat" by the replace_str.

The comparison function compares the match_str of two Action_items.

Creating an Action_item is simplified by the creation function, which makes
copies of the supplied strings.

*/

enum Action_e {SKIP, REPLACE};

struct Action_item {
	enum Action_e action;
	char * replace_str;
	char * match_str;
};

/* comparison function - returns strcmp on match_str */
int Action_comp(struct Action_item *, struct Action_item *);

/* allocates a Action_item and sets its members to newly allocated strings
copied from the supplied ones */
struct Action_item * create_Action_item(enum Action_e action_in, char * replace_in, char * match_in);

/* deallocates the string memory and the supplied Action_item */
void destroy_Action_item(struct Action_item *);

#endif
