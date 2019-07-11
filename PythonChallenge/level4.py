# http://www.pythonchallenge.com/pc/def/linkedlist.php

import re
import urllib

url_base = "http://www.pythonchallenge.com/pc/def/linkedlist.php?nothing="

def find_next_nothing(cur_nothing):
    result = urllib.request.urlopen(url_base + cur_nothing).read()
    match = re.search("the next nothing is (\d+)", result.decode("utf-8"))
    print(match.group(1))
    return match.group(1)
    
#next_nothing = "12345"
#next_nothing = "8022" # Needed to divide by two
# next_nothing = "82682" # Needed to look for text that says the next nothing is before the number
next_nothing = 66831 # Right before the end

while True:
    next_nothing = find_next_nothing(next_nothing)
