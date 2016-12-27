import re
import sys

def compare(a, b):
    if (len(a) != len(b)):
        return len(a) - len(b)
    else:
        return a < b

c1 = sys.argv[1]
c2 = sys.argv[2]
c3 = sys.argv[3]

f = open('dictionary.txt', 'r')

words = f.readlines()

re1 = re.compile('.*' + c1 + '.*' + c2 + '.*' + c3 + '.*')
re2 = re.compile('.*' + c3 + '.*' + c2 + '.*' + c1 + '.*')

matches = []

for word in words:
    word = word[:-1]
    if (re1.match(word) or re2.match(word)):
        matches.append(word)

matches.sort(compare)

num_matches = len(matches)

if (len(matches) > 500):
    matches = matches[:500]

for match in matches:
    print match

print 'Num Matches: ' + str(num_matches)

