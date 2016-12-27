/* 
An Action_item is used to specify actions to be performed on the remainder of
a document that is being spell-checked. 
If "foobar" is to be skipped, then an Action_item is created that 
has SKIP as the action, and "foobar" as the match_str.
If "foobar" is to be replaced by "dingbat", then 
an Action_item is created that has REPLACE as the action, 
"foobar" for the match_str, and "dingbat" for the replace_str.

The comparison function compares the match_str of two Action_items.

The default constructor initializes the object to NOTHING and two empty strings.
*/

/* NOTE: The comment "fill this in" means remove the comment and replace
it with the proper code. Remove this comment and all other comments starting with /***. */

enum Action_e {NOTHING, SKIP, REPLACE};

struct Action_item {
	Action_item()
	/*** fill this in with the constructor code */

	Action_item(Action_e action_, const String& replace_str_, const String& match_str_)
	/*** fill this in with the constructor code */
	
	
	enum Action_e action;
	String replace_str;
	String match_str;
};

// comparison function - returns strcmp on match_str
int Action_comp(const Action_item&, const Action_item&);

// output "Skip match_str" or "Replace match_str with replace_str"
std::ostream& operator<< (std::ostream& os, const Action_item& action);
