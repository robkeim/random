from operator import itemgetter

import re
import urllib

# Keim
fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=2252742')

# Chang
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=2257500')

# Kirbitz
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=530565354')

# Zoufaly
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=2254573')

# Nicki
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=2247811')

# MZLee
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=2253401')

# Jessica
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=682513035')

# Leader
#fh = urllib.urlopen('http://apps.facebook.com/mousehunt/hunterprofile.php?snuid=666561064')

text = fh.read();
fh.close()

exp = re.compile("<li class=\"mousename\">(.*?)</li>.*?<li class=\"numcatches\">x(.*?)</li>", re.MULTILINE|re.DOTALL)

rem = re.compile(",")

pairs = {}
total = 0
while exp.search(text) != None:
    match = exp.search(text)
    text = text[match.span()[1]:]

    name = match.group(1)
    num = match.group(2)
    num = int(rem.sub("", match.group(2)))

    pairs[name] = num
    total += num

pairs = sorted(pairs.iteritems(), key=itemgetter(0))
pairs = sorted(pairs, key=itemgetter(1), reverse=True)

part = 0
for pair in pairs:
    part += pair[1]
    print '{0:30} {1:3}   {2:.2f}%   {3:.2f}%'.format(str(pair[0])[:30], pair[1], float(pair[1])/total*100, float(part)/total*100)

print ''
print 'Total Mice: ' + str(total)

