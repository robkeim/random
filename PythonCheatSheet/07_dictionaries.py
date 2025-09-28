"""
Python Dictionaries Cheat Sheet
================================
Hash tables, dictionaries, and mapping operations for programming
"""

# =============================================================================
# DICTIONARY CREATION
# =============================================================================

# Empty dictionary
empty_dict = {}
empty_dict2 = dict()

# Dictionary with initial values
person = {"name": "Alice", "age": 30, "city": "New York"}
person2 = dict(name="Bob", age=25, city="Boston")

# From list of tuples
pairs = [("a", 1), ("b", 2), ("c", 3)]
dict_from_pairs = dict(pairs)  # {'a': 1, 'b': 2, 'c': 3}

# From two lists
keys = ["name", "age", "city"]
values = ["Charlie", 35, "Chicago"]
dict_from_lists = dict(zip(keys, values))

# Dictionary comprehension
squares = {x: x**2 for x in range(5)}  # {0: 0, 1: 1, 2: 4, 3: 9, 4: 16}
filtered = {k: v for k, v in squares.items() if v > 5}  # {3: 9, 4: 16}

# =============================================================================
# ACCESSING AND MODIFYING
# =============================================================================

person = {"name": "Alice", "age": 30, "city": "New York"}

# Accessing values
name = person["name"]           # "Alice" - raises KeyError if key doesn't exist
age = person.get("age")         # 30 - returns None if key doesn't exist
height = person.get("height", 0)  # 0 - returns default if key doesn't exist

# Adding/updating values
person["email"] = "alice@example.com"  # Add new key-value pair
person["age"] = 31                     # Update existing value
person.update({"phone": "555-1234", "age": 32})  # Update multiple values

# Removing values
del person["city"]              # Remove key-value pair
email = person.pop("email")     # Remove and return value
phone = person.pop("phone", "Not found")  # Remove with default if not exists
last_item = person.popitem()    # Remove and return arbitrary (key, value) pair

# Clear all items
person.clear()

# =============================================================================
# DICTIONARY METHODS
# =============================================================================

data = {"a": 1, "b": 2, "c": 3, "d": 4}

# Get keys, values, items
keys_list = list(data.keys())    # ['a', 'b', 'c', 'd']
values_list = list(data.values()) # [1, 2, 3, 4]
items_list = list(data.items())  # [('a', 1), ('b', 2), ('c', 3), ('d', 4)]

# Copying
shallow_copy = data.copy()
deep_copy = dict(data)  # Same as copy() for simple dictionaries

# Merge dictionaries
dict1 = {"a": 1, "b": 2}
dict2 = {"c": 3, "d": 4}

# Python 3.9+
merged = dict1 | dict2          # {'a': 1, 'b': 2, 'c': 3, 'd': 4}
dict1 |= dict2                  # In-place merge

# Python 3.5+
merged = {**dict1, **dict2}

# All versions
merged = dict1.copy()
merged.update(dict2)

# =============================================================================
# ITERATION PATTERNS
# =============================================================================

data = {"name": "Alice", "age": 30, "city": "New York"}

# Iterate over keys (default)
for key in data:
    print(key, data[key])

# Iterate over keys explicitly
for key in data.keys():
    print(key)

# Iterate over values
for value in data.values():
    print(value)

# Iterate over key-value pairs
for key, value in data.items():
    print(f"{key}: {value}")

# Dictionary comprehension with conditions
filtered = {k: v for k, v in data.items() if len(str(v)) > 2}

# =============================================================================
# COLLECTIONS MODULE - ADVANCED DICTIONARIES
# =============================================================================

from collections import defaultdict, Counter, OrderedDict, ChainMap

# DefaultDict - provides default values for missing keys
dd = defaultdict(int)
dd["count"] += 1        # No KeyError, starts at 0
dd["count"] += 1        # Now 2

dd_list = defaultdict(list)
dd_list["items"].append("apple")  # Creates list automatically

dd_set = defaultdict(set)
dd_set["tags"].add("python")

# Custom default factory
def make_default():
    return "N/A"

dd_custom = defaultdict(make_default)
print(dd_custom["missing"])  # "N/A"

# Counter - counting hashable objects
from collections import Counter

# Count characters
text = "hello world"
char_count = Counter(text)  # Counter({'l': 3, 'o': 2, 'h': 1, ...})

# Count words
words = ["apple", "banana", "apple", "cherry", "banana", "apple"]
word_count = Counter(words)  # Counter({'apple': 3, 'banana': 2, 'cherry': 1})

# Most common elements
most_common = word_count.most_common(2)  # [('apple', 3), ('banana', 2)]

# Counter arithmetic
c1 = Counter({'a': 3, 'b': 1})
c2 = Counter({'a': 1, 'b': 2})
print(c1 + c2)  # Counter({'a': 4, 'b': 3})
print(c1 - c2)  # Counter({'a': 2})
print(c1 & c2)  # Counter({'a': 1, 'b': 1}) (intersection)
print(c1 | c2)  # Counter({'a': 3, 'b': 2}) (union)

# OrderedDict - preserves insertion order (less needed in Python 3.7+)
od = OrderedDict()
od["first"] = 1
od["second"] = 2
od["third"] = 3

# Move to end
od.move_to_end("first")  # Move "first" to end

# ChainMap - combines multiple dictionaries
dict1 = {"a": 1, "b": 2}
dict2 = {"b": 3, "c": 4}
dict3 = {"c": 5, "d": 6}

combined = ChainMap(dict1, dict2, dict3)
print(combined["a"])  # 1 (from dict1)
print(combined["b"])  # 2 (from dict1, first in chain)
print(combined["c"])  # 4 (from dict2, first in chain)

# =============================================================================
# DICTIONARY ALGORITHMS AND PATTERNS
# =============================================================================

def group_by_key(items, key_func):
    """Group items by the result of key_func"""
    groups = defaultdict(list)
    for item in items:
        key = key_func(item)
        groups[key].append(item)
    return dict(groups)

# Example usage
students = [
    {"name": "Alice", "grade": "A"},
    {"name": "Bob", "grade": "B"},
    {"name": "Charlie", "grade": "A"},
    {"name": "David", "grade": "B"}
]
by_grade = group_by_key(students, lambda s: s["grade"])

def invert_dictionary(d):
    """Invert dictionary (values become keys)"""
    return {v: k for k, v in d.items()}

def merge_dictionaries(*dicts):
    """Merge multiple dictionaries, later ones override earlier"""
    result = {}
    for d in dicts:
        result.update(d)
    return result

def find_common_keys(*dicts):
    """Find keys that exist in all dictionaries"""
    if not dicts:
        return set()
    
    common = set(dicts[0].keys())
    for d in dicts[1:]:
        common &= set(d.keys())
    return common

def deep_get(dictionary, keys, default=None):
    """Get value from nested dictionary using list of keys"""
    for key in keys:
        if isinstance(dictionary, dict) and key in dictionary:
            dictionary = dictionary[key]
        else:
            return default
    return dictionary

# Example
nested = {"user": {"profile": {"name": "Alice", "age": 30}}}
name = deep_get(nested, ["user", "profile", "name"])  # "Alice"
missing = deep_get(nested, ["user", "settings", "theme"], "default")  # "default"

def flatten_dictionary(d, parent_key='', sep='_'):
    """Flatten nested dictionary"""
    items = []
    for k, v in d.items():
        new_key = f"{parent_key}{sep}{k}" if parent_key else k
        if isinstance(v, dict):
            items.extend(flatten_dictionary(v, new_key, sep=sep).items())
        else:
            items.append((new_key, v))
    return dict(items)

# Example
nested = {"user": {"name": "Alice", "details": {"age": 30, "city": "NYC"}}}
flat = flatten_dictionary(nested)
# {'user_name': 'Alice', 'user_details_age': 30, 'user_details_city': 'NYC'}

# =============================================================================
# COMMON PROGRAMMING PATTERNS
# =============================================================================

def two_sum(nums, target):
    """Find two numbers that add up to target using hash table"""
    num_to_index = {}
    
    for i, num in enumerate(nums):
        complement = target - num
        if complement in num_to_index:
            return [num_to_index[complement], i]
        num_to_index[num] = i
    
    return []

def first_unique_character(s):
    """Find first non-repeating character using Counter"""
    char_count = Counter(s)
    
    for i, char in enumerate(s):
        if char_count[char] == 1:
            return i
    
    return -1

def group_anagrams(strs):
    """Group anagrams together using sorted string as key"""
    anagram_groups = defaultdict(list)
    
    for s in strs:
        # Sort characters to create key
        key = ''.join(sorted(s))
        anagram_groups[key].append(s)
    
    return list(anagram_groups.values())

def is_isomorphic(s, t):
    """Check if two strings are isomorphic"""
    if len(s) != len(t):
        return False
    
    s_to_t = {}
    t_to_s = {}
    
    for char_s, char_t in zip(s, t):
        if char_s in s_to_t:
            if s_to_t[char_s] != char_t:
                return False
        else:
            s_to_t[char_s] = char_t
        
        if char_t in t_to_s:
            if t_to_s[char_t] != char_s:
                return False
        else:
            t_to_s[char_t] = char_s
    
    return True

def subarray_sum_equals_k(nums, k):
    """Count subarrays with sum equal to k using prefix sums"""
    count = 0
    prefix_sum = 0
    sum_count = defaultdict(int)
    sum_count[0] = 1  # Empty prefix sum
    
    for num in nums:
        prefix_sum += num
        # Check if prefix_sum - k exists
        count += sum_count[prefix_sum - k]
        sum_count[prefix_sum] += 1
    
    return count

def longest_substring_without_repeating(s):
    """Find longest substring without repeating characters"""
    char_index = {}
    max_length = 0
    start = 0
    
    for end, char in enumerate(s):
        if char in char_index and char_index[char] >= start:
            start = char_index[char] + 1
        
        char_index[char] = end
        max_length = max(max_length, end - start + 1)
    
    return max_length

# =============================================================================
# CACHING AND MEMOIZATION
# =============================================================================

# Simple memoization with dictionary
def fibonacci_memo():
    cache = {}
    
    def fib(n):
        if n in cache:
            return cache[n]
        
        if n < 2:
            return n
        
        cache[n] = fib(n - 1) + fib(n - 2)
        return cache[n]
    
    return fib

fib = fibonacci_memo()
print(fib(10))  # 55

# Using functools.lru_cache (built-in memoization)
from functools import lru_cache

@lru_cache(maxsize=128)
def fibonacci_cached(n):
    if n < 2:
        return n
    return fibonacci_cached(n - 1) + fibonacci_cached(n - 2)

# Manual LRU cache implementation
class LRUCache:
    def __init__(self, capacity):
        self.capacity = capacity
        self.cache = {}
        self.order = []
    
    def get(self, key):
        if key in self.cache:
            # Move to end (most recently used)
            self.order.remove(key)
            self.order.append(key)
            return self.cache[key]
        return -1
    
    def put(self, key, value):
        if key in self.cache:
            # Update existing key
            self.order.remove(key)
        elif len(self.cache) >= self.capacity:
            # Remove least recently used
            lru_key = self.order.pop(0)
            del self.cache[lru_key]
        
        self.cache[key] = value
        self.order.append(key)

# =============================================================================
# DICTIONARY PERFORMANCE TIPS
# =============================================================================

# Dictionary lookup is O(1) average case
large_list = list(range(100000))
large_dict = {i: i for i in range(100000)}

# Slow: O(n)
def slow_search(target):
    return target in large_list

# Fast: O(1)
def fast_search(target):
    return target in large_dict

# Use dict.get() with default instead of checking key existence
# Slower
if key in dictionary:
    value = dictionary[key]
else:
    value = default_value

# Faster
value = dictionary.get(key, default_value)

# Use defaultdict instead of checking keys
# Slower
regular_dict = {}
if key not in regular_dict:
    regular_dict[key] = []
regular_dict[key].append(value)

# Faster
from collections import defaultdict
dd = defaultdict(list)
dd[key].append(value)

# =============================================================================
# DICTIONARY COMPREHENSIONS
# =============================================================================

# Basic comprehension
numbers = [1, 2, 3, 4, 5]
squares = {n: n**2 for n in numbers}

# With condition
even_squares = {n: n**2 for n in numbers if n % 2 == 0}

# From existing dictionary
original = {"a": 1, "b": 2, "c": 3, "d": 4}
filtered = {k: v for k, v in original.items() if v > 2}
transformed = {k: v*2 for k, v in original.items()}

# Swap keys and values
swapped = {v: k for k, v in original.items()}

# Multiple data sources
keys = ["name", "age", "city"]
values = ["Alice", 30, "NYC"]
combined = {k: v for k, v in zip(keys, values)}

# Nested comprehension
matrix = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]
flattened = {i*3 + j: matrix[i][j] for i in range(3) for j in range(3)}

# =============================================================================
# COMMON GOTCHAS
# =============================================================================

# 1. Dictionary views (keys(), values(), items()) are dynamic
d = {"a": 1, "b": 2}
keys = d.keys()
print(list(keys))  # ['a', 'b']
d["c"] = 3
print(list(keys))  # ['a', 'b', 'c'] - keys view updated!

# 2. Modifying dictionary while iterating
d = {"a": 1, "b": 2, "c": 3}

# Wrong - RuntimeError
# for key in d:
#     if d[key] % 2 == 0:
#         del d[key]

# Correct
to_delete = [k for k, v in d.items() if v % 2 == 0]
for key in to_delete:
    del d[key]

# 3. Mutable default values in functions
def wrong_function(d={}):  # DON'T DO THIS
    d["new_key"] = "value"
    return d

def correct_function(d=None):  # DO THIS
    if d is None:
        d = {}
    d["new_key"] = "value"
    return d

# =============================================================================
# DICTIONARY VS OTHER DATA STRUCTURES
# =============================================================================

"""
When to use dictionaries:
✅ Need fast lookups by key (O(1) average)
✅ Need to associate data with unique identifiers
✅ Need to count occurrences (use Counter)
✅ Need to group data by some criteria
✅ Caching/memoization

When NOT to use dictionaries:
❌ Need ordered data (use list, though dict preserves order in Python 3.7+)
❌ Need indexed access by position (use list)
❌ Need mathematical operations (use sets for set operations)
❌ Memory is very constrained (dict has overhead)

Time Complexity:
Operation     | Average | Worst Case
--------------|---------|------------
Get/Set/Del   | O(1)    | O(n)
Contains      | O(1)    | O(n)
Iteration     | O(n)    | O(n)
Copy          | O(n)    | O(n)
"""