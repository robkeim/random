"""
Python Tips & Tricks Cheat Sheet
=================================
Quick solutions, gotchas, and best practices
"""

# =============================================================================
# QUICK SOLUTIONS & ONE-LINERS
# =============================================================================

# Swap variables
a, b = 10, 20
a, b = b, a  # Pythonic way

# Reverse a string
s = "hello"
reversed_s = s[::-1]  # "olleh"

# Check if string is palindrome
def is_palindrome(s):
    return s == s[::-1]

# Count characters in string
from collections import Counter
text = "hello world"
char_count = Counter(text)  # Counter({'l': 3, 'o': 2, 'h': 1, ...})

# Find most common elements
most_common = char_count.most_common(2)  # [('l', 3), ('o', 2)]

# Flatten nested list
nested = [[1, 2], [3, 4], [5, 6]]
flattened = [item for sublist in nested for item in sublist]

# Remove duplicates while preserving order
def remove_duplicates(lst):
    seen = set()
    return [x for x in lst if not (x in seen or seen.add(x))]

# Find intersection of lists
list1 = [1, 2, 3, 4, 5]
list2 = [4, 5, 6, 7, 8]
intersection = list(set(list1) & set(list2))  # [4, 5]

# Get unique elements from list
unique = list(set([1, 2, 2, 3, 3, 4]))  # [1, 2, 3, 4]

# =============================================================================
# STRING MANIPULATION TRICKS
# =============================================================================

# Check if string contains only digits
text = "12345"
is_numeric = text.isdigit()  # True

# Check if string is alphanumeric
text = "abc123"
is_alnum = text.isalnum()  # True

# Title case
text = "hello world"
title = text.title()  # "Hello World"

# Split and join
sentence = "hello world python"
words = sentence.split()  # ["hello", "world", "python"]
rejoined = " ".join(words)  # "hello world python"

# Remove whitespace
text = "  hello world  "
stripped = text.strip()  # "hello world"
left_stripped = text.lstrip()  # "hello world  "
right_stripped = text.rstrip()  # "  hello world"

# Replace multiple spaces with single space
import re
text = "hello    world"
clean = re.sub(r'\s+', ' ', text)  # "hello world"

# Check if string starts/ends with substring
text = "hello world"
starts = text.startswith("hello")  # True
ends = text.endswith("world")  # True

# =============================================================================
# LIST TRICKS
# =============================================================================

# Initialize list with specific value
zeros = [0] * 5  # [0, 0, 0, 0, 0]

# List of lists (careful with references!)
# Wrong way:
matrix_wrong = [[0] * 3] * 3  # All rows reference same list!

# Right way:
matrix = [[0] * 3 for _ in range(3)]

# Get index and value
items = ['a', 'b', 'c']
for i, item in enumerate(items):
    print(f"Index {i}: {item}")

# Zip lists together
names = ['Alice', 'Bob', 'Charlie']
ages = [25, 30, 35]
combined = list(zip(names, ages))  # [('Alice', 25), ...]

# Unzip
names, ages = zip(*combined)

# Find index of element
items = [1, 2, 3, 4, 5]
try:
    index = items.index(3)  # 2
except ValueError:
    index = -1  # Element not found

# Sort with custom key
students = [('Alice', 85), ('Bob', 90), ('Charlie', 78)]
by_grade = sorted(students, key=lambda x: x[1])
by_name = sorted(students, key=lambda x: x[0])

# =============================================================================
# DICTIONARY TRICKS
# =============================================================================

# Dictionary comprehension
numbers = [1, 2, 3, 4, 5]
squares = {n: n**2 for n in numbers}  # {1: 1, 2: 4, 3: 9, ...}

# Default dict
from collections import defaultdict
dd = defaultdict(list)
dd['key'].append('value')  # No KeyError

dd_int = defaultdict(int)
dd_int['count'] += 1  # Starts at 0

# Get with default
data = {'a': 1, 'b': 2}
value = data.get('c', 0)  # Returns 0 if 'c' not found

# Merge dictionaries (Python 3.9+)
dict1 = {'a': 1, 'b': 2}
dict2 = {'c': 3, 'd': 4}
merged = dict1 | dict2  # {'a': 1, 'b': 2, 'c': 3, 'd': 4}

# Merge dictionaries (older versions)
merged = {**dict1, **dict2}

# Invert dictionary
original = {'a': 1, 'b': 2, 'c': 3}
inverted = {v: k for k, v in original.items()}  # {1: 'a', 2: 'b', 3: 'c'}

# =============================================================================
# MATH TRICKS
# =============================================================================

# Check if number is power of 2
def is_power_of_2(n):
    return n > 0 and (n & (n - 1)) == 0

# Get digits of number
number = 12345
digits = [int(d) for d in str(number)]  # [1, 2, 3, 4, 5]

# Sum of digits
digit_sum = sum(int(d) for d in str(number))  # 15

# Check if number is prime
def is_prime(n):
    if n < 2:
        return False
    for i in range(2, int(n**0.5) + 1):
        if n % i == 0:
            return False
    return True

# GCD and LCM
import math
def gcd(a, b):
    return math.gcd(a, b)

def lcm(a, b):
    return abs(a * b) // math.gcd(a, b)

# =============================================================================
# BINARY MANIPULATION TRICKS
# =============================================================================

# Count set bits
def count_bits(n):
    return bin(n).count('1')

# Check if bit is set
def is_bit_set(n, pos):
    return (n & (1 << pos)) != 0

# Set bit
def set_bit(n, pos):
    return n | (1 << pos)

# Clear bit
def clear_bit(n, pos):
    return n & ~(1 << pos)

# Toggle bit
def toggle_bit(n, pos):
    return n ^ (1 << pos)

# Get rightmost set bit
def rightmost_set_bit(n):
    return n & (-n)

# =============================================================================
# COMMON GOTCHAS & PITFALLS
# =============================================================================

# 1. Mutable default arguments
def wrong_function(lst=[]):  # DON'T DO THIS
    lst.append(1)
    return lst

def correct_function(lst=None):  # DO THIS
    if lst is None:
        lst = []
    lst.append(1)
    return lst

# 2. Late binding closures
funcs = []
for i in range(3):
    funcs.append(lambda: i)  # Wrong: all return 2

# Correct way:
funcs = []
for i in range(3):
    funcs.append(lambda x=i: x)  # Capture i at creation time

# 3. Modifying list while iterating
numbers = [1, 2, 3, 4, 5]

# Wrong:
for i, num in enumerate(numbers):
    if num % 2 == 0:
        numbers.remove(num)  # Modifies list during iteration

# Correct:
numbers = [num for num in numbers if num % 2 != 0]

# 4. Integer caching
a = 256
b = 256
print(a is b)  # True (integers -5 to 256 are cached)

a = 257
b = 257
print(a is b)  # False (not cached)

# 5. Comparing floats
print(0.1 + 0.2 == 0.3)  # False!
print(abs(0.1 + 0.2 - 0.3) < 1e-10)  # True

# =============================================================================
# PERFORMANCE TIPS
# =============================================================================

# Use set for membership testing
large_list = list(range(10000))
large_set = set(large_list)

# Slow: O(n)
exists = 5000 in large_list

# Fast: O(1)
exists = 5000 in large_set

# Use deque for queue operations
from collections import deque
queue = deque([1, 2, 3])
queue.appendleft(0)  # O(1)
queue.popleft()      # O(1)

# List operations at front are O(n)
lst = [1, 2, 3]
lst.insert(0, 0)     # O(n)
lst.pop(0)           # O(n)

# Use list comprehensions over loops
# Slower:
result = []
for i in range(100):
    if i % 2 == 0:
        result.append(i * 2)

# Faster:
result = [i * 2 for i in range(100) if i % 2 == 0]

# Use map() for simple transformations
numbers = [1, 2, 3, 4, 5]
squares = list(map(lambda x: x**2, numbers))

# =============================================================================
# DEBUGGING TIPS
# =============================================================================

# Print with variable names
x, y = 10, 20
print(f"{x=}, {y=}")  # x=10, y=20

# Pretty print data structures
import pprint
data = {'a': [1, 2, {'nested': 'value'}], 'b': (3, 4)}
pprint.pprint(data)

# Time code execution
import time
start = time.time()
# Your code here
end = time.time()
print(f"Execution time: {end - start:.4f} seconds")

# Or use timeit for micro-benchmarks
import timeit
time_taken = timeit.timeit('sum([1, 2, 3, 4, 5])', number=100000)

# =============================================================================
# DEVELOPMENT BEST PRACTICES
# =============================================================================

"""
BEFORE DEVELOPMENT:
1. Practice typing Python syntax fluently
2. Know the standard library (collections, itertools, functools)
3. Understand time/space complexity analysis
4. Practice explaining your thought process clearly

DURING DEVELOPMENT:
1. Ask clarifying questions:
   - What's the input size?
   - Are there any constraints?
   - What should I return for edge cases?

2. Think through your approach:
   - Plan your solution
   - Walk through examples
   - Consider trade-offs

3. Start simple, then optimize:
   - Get a working solution first
   - Then improve time/space complexity

4. Test your code:
   - Walk through with examples
   - Consider edge cases
   - Check for off-by-one errors

COMMON EDGE CASES TO CONSIDER:
- Empty input ([], "", None)
- Single element
- All elements the same
- Negative numbers
- Very large inputs
- Invalid input types

COMPLEXITY CHEAT SHEET:
Operation    | List | Dict | Set  | Deque
-------------|------|------|------|-------
Access       | O(1) | O(1) | -    | O(n)
Search       | O(n) | O(1) | O(1) | O(n)
Insert       | O(1) | O(1) | O(1) | O(1)
Delete       | O(n) | O(1) | O(1) | O(1)
"""

# =============================================================================
# USEFUL IMPORTS FOR DEVELOPMENT
# =============================================================================

"""
Essential imports to remember:

from collections import defaultdict, Counter, deque
from functools import lru_cache, reduce
from itertools import combinations, permutations, chain
import heapq
import bisect
import re
import math
"""