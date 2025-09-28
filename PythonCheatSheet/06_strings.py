"""
Python Strings Cheat Sheet
==========================
String manipulation, formatting, and programming patterns
"""

# =============================================================================
# STRING CREATION AND BASICS
# =============================================================================

# Different ways to create strings
single_quotes = 'Hello World'
double_quotes = "Hello World"
triple_quotes = """Multi-line
string with
line breaks"""

# Raw strings (useful for regex)
raw_string = r"C:\Users\name\folder"  # Backslashes not escaped

# Unicode strings
unicode_str = "Hello 🌍"

# =============================================================================
# STRING INDEXING AND SLICING
# =============================================================================

text = "Python Programming"

# Indexing
first_char = text[0]     # 'P'
last_char = text[-1]     # 'g'

# Slicing [start:end:step]
substring = text[0:6]    # 'Python'
from_start = text[:6]    # 'Python'
to_end = text[7:]        # 'Programming'
reverse = text[::-1]     # 'gnimmargorP nohtyP'
every_second = text[::2] # 'Pto rgamn'

# =============================================================================
# STRING METHODS
# =============================================================================

text = "  Hello World Python  "

# Case methods
print(text.lower())      # '  hello world python  '
print(text.upper())      # '  HELLO WORLD PYTHON  '
print(text.title())      # '  Hello World Python  '
print(text.capitalize()) # '  hello world python  '
print(text.swapcase())   # '  hELLO wORLD pYTHON  '

# Whitespace methods
print(text.strip())      # 'Hello World Python'
print(text.lstrip())     # 'Hello World Python  '
print(text.rstrip())     # '  Hello World Python'

# Search methods
sentence = "The quick brown fox jumps over the lazy dog"
print(sentence.find('fox'))        # 16 (index of first occurrence)
print(sentence.find('cat'))        # -1 (not found)
print(sentence.rfind('the'))       # 31 (last occurrence)
print(sentence.index('fox'))       # 16 (like find, but raises exception if not found)
print(sentence.count('the'))       # 2 (number of occurrences)

# Boolean methods
text = "Hello123"
print(text.isalpha())    # False (contains digits)
print(text.isdigit())    # False (contains letters)
print(text.isalnum())    # True (alphanumeric)
print(text.isspace())    # False
print("   ".isspace())   # True

name = "alice"
print(name.islower())    # True
print(name.isupper())    # False
print(name.istitle())    # False

# =============================================================================
# STRING SPLITTING AND JOINING
# =============================================================================

# Split methods
sentence = "apple,banana,cherry,date"
fruits = sentence.split(',')        # ['apple', 'banana', 'cherry', 'date']
words = "hello world python".split()  # ['hello', 'world', 'python'] (splits on whitespace)

# Split with limit
limited = sentence.split(',', 2)    # ['apple', 'banana', 'cherry,date']

# Partition (splits into exactly 3 parts)
before, sep, after = "name=value".partition('=')  # ('name', '=', 'value')

# Join method
words = ['apple', 'banana', 'cherry']
joined = ', '.join(words)           # 'apple, banana, cherry'
path = '/'.join(['home', 'user', 'documents'])  # 'home/user/documents'

# =============================================================================
# STRING REPLACEMENT
# =============================================================================

text = "Hello World"

# Replace method
new_text = text.replace('World', 'Python')     # 'Hello Python'
new_text = text.replace('l', 'L')              # 'HeLLo WorLd'
new_text = text.replace('l', 'L', 1)           # 'HeLlo World' (replace first occurrence only)

# Translate method (character mapping)
translation_table = str.maketrans('aeiou', '12345')
translated = "hello world".translate(translation_table)  # 'h2ll4 w4rld'

# Remove characters
remove_vowels = str.maketrans('', '', 'aeiou')
no_vowels = "hello world".translate(remove_vowels)  # 'hll wrld'

# =============================================================================
# STRING FORMATTING
# =============================================================================

name = "Alice"
age = 30
score = 85.67

# Old-style formatting (%)
old_style = "Name: %s, Age: %d, Score: %.2f" % (name, age, score)

# str.format() method
formatted = "Name: {}, Age: {}, Score: {:.2f}".format(name, age, score)
formatted = "Name: {0}, Age: {1}, Score: {2:.2f}".format(name, age, score)
formatted = "Name: {n}, Age: {a}, Score: {s:.2f}".format(n=name, a=age, s=score)

# f-strings (Python 3.6+) - PREFERRED
f_string = f"Name: {name}, Age: {age}, Score: {score:.2f}"
f_string = f"Name: {name.upper()}, Next year: {age + 1}"

# Advanced f-string formatting
number = 1234567.89
print(f"{number:,}")        # 1,234,567.89 (thousands separator)
print(f"{number:.2e}")      # 1.23e+06 (scientific notation)
print(f"{42:08d}")          # 00000042 (zero-padded)
print(f"{'center':^20}")    # '       center       ' (centered)
print(f"{'left':<20}")      # 'left                ' (left-aligned)
print(f"{'right':>20}")     # '               right' (right-aligned)

# =============================================================================
# REGULAR EXPRESSIONS
# =============================================================================

import re

text = "Contact us at hello@example.com or support@test.org"

# Basic pattern matching
pattern = r'\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b'
emails = re.findall(pattern, text)  # ['hello@example.com', 'support@test.org']

# Search for pattern
match = re.search(r'(\w+)@(\w+\.\w+)', text)
if match:
    username = match.group(1)   # 'hello'
    domain = match.group(2)     # 'example.com'
    full_email = match.group(0) # 'hello@example.com'

# Replace using regex
phone_text = "Call me at 123-456-7890 or 987-654-3210"
clean_phones = re.sub(r'\d{3}-\d{3}-\d{4}', '[REDACTED]', phone_text)

# Split using regex
data = "apple,banana;cherry:date"
items = re.split(r'[,;:]', data)  # ['apple', 'banana', 'cherry', 'date']

# Compile pattern for reuse
email_pattern = re.compile(r'\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b')
emails = email_pattern.findall(text)

# Common regex patterns
patterns = {
    'email': r'\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b',
    'phone': r'\d{3}-\d{3}-\d{4}',
    'url': r'https?://(?:[-\w.])+(?:\:[0-9]+)?(?:/(?:[\w/_.])*(?:\?(?:[\w&=%.])*)?(?:\#(?:[\w.])*)?)?',
    'ip_address': r'\b(?:[0-9]{1,3}\.){3}[0-9]{1,3}\b',
    'word': r'\b\w+\b',
    'digits': r'\d+',
    'whitespace': r'\s+'
}

# =============================================================================
# STRING VALIDATION AND CHECKING
# =============================================================================

def is_valid_email(email):
    """Check if string is valid email format"""
    pattern = r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'
    return re.match(pattern, email) is not None

def is_strong_password(password):
    """Check if password meets strength requirements"""
    if len(password) < 8:
        return False
    if not re.search(r'[a-z]', password):  # lowercase
        return False
    if not re.search(r'[A-Z]', password):  # uppercase
        return False
    if not re.search(r'\d', password):     # digit
        return False
    if not re.search(r'[!@#$%^&*(),.?":{}|<>]', password):  # special char
        return False
    return True

def contains_only_letters_and_spaces(text):
    """Check if string contains only letters and spaces"""
    return re.match(r'^[a-zA-Z\s]+$', text) is not None

# =============================================================================
# STRING ALGORITHMS AND PATTERNS
# =============================================================================

def is_palindrome(s):
    """Check if string is palindrome (ignoring case and non-alphanumeric)"""
    # Clean string: only alphanumeric, lowercase
    cleaned = re.sub(r'[^a-zA-Z0-9]', '', s).lower()
    return cleaned == cleaned[::-1]

def longest_common_prefix(strings):
    """Find longest common prefix among array of strings"""
    if not strings:
        return ""
    
    prefix = strings[0]
    for string in strings[1:]:
        while not string.startswith(prefix):
            prefix = prefix[:-1]
            if not prefix:
                return ""
    
    return prefix

def group_anagrams(words):
    """Group words that are anagrams of each other"""
    from collections import defaultdict
    
    anagram_groups = defaultdict(list)
    
    for word in words:
        # Sort characters to create key
        key = ''.join(sorted(word.lower()))
        anagram_groups[key].append(word)
    
    return list(anagram_groups.values())

def string_compression(s):
    """Compress string by counting consecutive characters"""
    if not s:
        return s
    
    compressed = []
    current_char = s[0]
    count = 1
    
    for i in range(1, len(s)):
        if s[i] == current_char:
            count += 1
        else:
            compressed.append(current_char + str(count))
            current_char = s[i]
            count = 1
    
    # Add last group
    compressed.append(current_char + str(count))
    
    compressed_str = ''.join(compressed)
    return compressed_str if len(compressed_str) < len(s) else s

def is_rotation(s1, s2):
    """Check if s1 is rotation of s2"""
    if len(s1) != len(s2):
        return False
    
    # If s1 is rotation of s2, then s1 will be substring of s2+s2
    return s1 in s2 + s2

def reverse_words(s):
    """Reverse words in a string"""
    return ' '.join(s.split()[::-1])

def remove_duplicates(s):
    """Remove duplicate characters from string while preserving order"""
    seen = set()
    result = []
    
    for char in s:
        if char not in seen:
            seen.add(char)
            result.append(char)
    
    return ''.join(result)

# =============================================================================
# ADVANCED STRING OPERATIONS
# =============================================================================

def levenshtein_distance(s1, s2):
    """Calculate edit distance between two strings"""
    if len(s1) < len(s2):
        return levenshtein_distance(s2, s1)
    
    if len(s2) == 0:
        return len(s1)
    
    previous_row = list(range(len(s2) + 1))
    for i, c1 in enumerate(s1):
        current_row = [i + 1]
        for j, c2 in enumerate(s2):
            insertions = previous_row[j + 1] + 1
            deletions = current_row[j] + 1
            substitutions = previous_row[j] + (c1 != c2)
            current_row.append(min(insertions, deletions, substitutions))
        previous_row = current_row
    
    return previous_row[-1]

def find_all_substrings(s):
    """Generate all possible substrings"""
    substrings = []
    n = len(s)
    
    for i in range(n):
        for j in range(i + 1, n + 1):
            substrings.append(s[i:j])
    
    return substrings

def longest_palindromic_substring(s):
    """Find longest palindromic substring"""
    if not s:
        return ""
    
    start = 0
    max_len = 1
    
    for i in range(len(s)):
        # Check for odd-length palindromes (center at i)
        left, right = i, i
        while left >= 0 and right < len(s) and s[left] == s[right]:
            current_len = right - left + 1
            if current_len > max_len:
                start = left
                max_len = current_len
            left -= 1
            right += 1
        
        # Check for even-length palindromes (center between i and i+1)
        left, right = i, i + 1
        while left >= 0 and right < len(s) and s[left] == s[right]:
            current_len = right - left + 1
            if current_len > max_len:
                start = left
                max_len = current_len
            left -= 1
            right += 1
    
    return s[start:start + max_len]

# =============================================================================
# STRING PERFORMANCE TIPS
# =============================================================================

# Use join() instead of concatenation for multiple strings
# Slow:
result = ""
for word in ["apple", "banana", "cherry"]:
    result += word + " "

# Fast:
words = ["apple", "banana", "cherry"]
result = " ".join(words) + " "

# Use list comprehension with join for complex operations
# Slow:
result = ""
for i in range(100):
    result += str(i) + ","

# Fast:
result = ",".join(str(i) for i in range(100))

# Use f-strings for formatting (fastest)
name, age = "Alice", 30
# Slowest: "Name: " + name + ", Age: " + str(age)
# Slow: "Name: {}, Age: {}".format(name, age)
# Fast: f"Name: {name}, Age: {age}"

# =============================================================================
# COMMON STRING PROGRAMMING PROBLEMS
# =============================================================================

def first_unique_character(s):
    """Find index of first non-repeating character"""
    from collections import Counter
    char_count = Counter(s)
    
    for i, char in enumerate(s):
        if char_count[char] == 1:
            return i
    
    return -1

def valid_parentheses(s):
    """Check if parentheses are valid"""
    stack = []
    mapping = {')': '(', '}': '{', ']': '['}
    
    for char in s:
        if char in mapping:
            if not stack or stack.pop() != mapping[char]:
                return False
        else:
            stack.append(char)
    
    return len(stack) == 0

def is_subsequence(s, t):
    """Check if s is subsequence of t"""
    i = 0  # pointer for s
    
    for char in t:
        if i < len(s) and char == s[i]:
            i += 1
    
    return i == len(s)

# =============================================================================
# STRING ENCODING AND DECODING
# =============================================================================

# UTF-8 encoding/decoding
text = "Hello 🌍"
encoded = text.encode('utf-8')      # b'Hello \xf0\x9f\x8c\x8d'
decoded = encoded.decode('utf-8')   # 'Hello 🌍'

# Base64 encoding
import base64
text = "Hello World"
encoded = base64.b64encode(text.encode('utf-8'))  # b'SGVsbG8gV29ybGQ='
decoded = base64.b64decode(encoded).decode('utf-8')  # 'Hello World'

# URL encoding
from urllib.parse import quote, unquote
url_part = "hello world"
encoded = quote(url_part)    # 'hello%20world'
decoded = unquote(encoded)   # 'hello world'