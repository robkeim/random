"""
Python Sets and Tuples Cheat Sheet
==================================
Set operations, tuple usage, and immutable collections
"""

# =============================================================================
# SET CREATION AND BASICS
# =============================================================================

# Empty set (note: {} creates empty dict, not set)
empty_set = set()

# Set with initial values
numbers = {1, 2, 3, 4, 5}
mixed_set = {1, "hello", 3.14, True}

# From list (removes duplicates)
list_with_dups = [1, 2, 2, 3, 3, 3, 4]
unique_set = set(list_with_dups)  # {1, 2, 3, 4}

# From string (each character becomes element)
char_set = set("hello")  # {'h', 'e', 'l', 'o'}

# Set comprehension
squares_set = {x**2 for x in range(5)}  # {0, 1, 4, 9, 16}
even_squares = {x**2 for x in range(10) if x % 2 == 0}

# =============================================================================
# SET OPERATIONS
# =============================================================================

set1 = {1, 2, 3, 4, 5}
set2 = {4, 5, 6, 7, 8}

# Adding elements
set1.add(6)           # {1, 2, 3, 4, 5, 6}
set1.update([7, 8])   # {1, 2, 3, 4, 5, 6, 7, 8}
set1.update({9, 10})  # Can update with another set

# Removing elements
set1.remove(1)        # Raises KeyError if element not found
set1.discard(1)       # No error if element not found
popped = set1.pop()   # Remove and return arbitrary element
set1.clear()          # Remove all elements

# Membership testing (very fast - O(1))
exists = 3 in {1, 2, 3, 4, 5}  # True
not_exists = 10 not in {1, 2, 3, 4, 5}  # True

# =============================================================================
# SET MATHEMATICAL OPERATIONS
# =============================================================================

A = {1, 2, 3, 4, 5}
B = {4, 5, 6, 7, 8}

# Union (all elements in either set)
union1 = A | B          # {1, 2, 3, 4, 5, 6, 7, 8}
union2 = A.union(B)     # Same result

# Intersection (elements in both sets)
intersection1 = A & B         # {4, 5}
intersection2 = A.intersection(B)  # Same result

# Difference (elements in A but not in B)
difference1 = A - B         # {1, 2, 3}
difference2 = A.difference(B)  # Same result

# Symmetric difference (elements in either set, but not both)
sym_diff1 = A ^ B                    # {1, 2, 3, 6, 7, 8}
sym_diff2 = A.symmetric_difference(B)  # Same result

# Update operations (modify original set)
A |= B   # A = A.union(B)
A &= B   # A = A.intersection(B)
A -= B   # A = A.difference(B)
A ^= B   # A = A.symmetric_difference(B)

# =============================================================================
# SET RELATIONSHIPS
# =============================================================================

A = {1, 2, 3}
B = {1, 2, 3, 4, 5}
C = {4, 5, 6}

# Subset and superset
is_subset = A <= B      # True (A is subset of B)
is_subset2 = A.issubset(B)  # Same result

is_superset = B >= A    # True (B is superset of A)
is_superset2 = B.issuperset(A)  # Same result

# Proper subset/superset (strict)
is_proper_subset = A < B   # True
is_proper_superset = B > A # True

# Disjoint sets (no common elements)
are_disjoint = A.isdisjoint(C)  # True

# =============================================================================
# FROZEN SETS (IMMUTABLE SETS)
# =============================================================================

# Immutable version of set
frozen = frozenset([1, 2, 3, 4, 5])

# Can be used as dictionary keys or set elements
dict_with_frozen_keys = {frozen: "value"}

set_of_sets = {frozenset([1, 2]), frozenset([3, 4])}

# All set operations work, but return new frozenset
frozen1 = frozenset([1, 2, 3])
frozen2 = frozenset([3, 4, 5])
union_frozen = frozen1 | frozen2  # frozenset({1, 2, 3, 4, 5})

# =============================================================================
# TUPLE CREATION AND BASICS
# =============================================================================

# Empty tuple
empty_tuple = ()
empty_tuple2 = tuple()

# Single element tuple (note the comma!)
single = (5,)  # Without comma, it's just parentheses around 5
single2 = 5,   # Comma makes it a tuple

# Multiple elements
coordinates = (3, 4)
person = ("Alice", 30, "Engineer")

# From list
list_data = [1, 2, 3, 4]
tuple_data = tuple(list_data)  # (1, 2, 3, 4)

# Nested tuples
nested = ((1, 2), (3, 4), (5, 6))

# =============================================================================
# TUPLE OPERATIONS
# =============================================================================

point = (3, 4, 5)

# Indexing (same as lists)
x = point[0]   # 3
y = point[1]   # 4
z = point[-1]  # 5 (last element)

# Slicing
first_two = point[:2]   # (3, 4)
last_two = point[1:]    # (4, 5)

# Unpacking
x, y, z = point  # x=3, y=4, z=5

# Partial unpacking with *
first, *rest = (1, 2, 3, 4, 5)  # first=1, rest=[2, 3, 4, 5]
*beginning, last = (1, 2, 3, 4, 5)  # beginning=[1, 2, 3, 4], last=5
first, *middle, last = (1, 2, 3, 4, 5)  # first=1, middle=[2, 3, 4], last=5

# Concatenation
tuple1 = (1, 2)
tuple2 = (3, 4)
combined = tuple1 + tuple2  # (1, 2, 3, 4)

# Repetition
repeated = (1, 2) * 3  # (1, 2, 1, 2, 1, 2)

# Length and membership
length = len(point)     # 3
exists = 3 in point     # True

# =============================================================================
# TUPLE METHODS
# =============================================================================

data = (1, 2, 3, 2, 4, 2, 5)

# Count occurrences
count_2 = data.count(2)  # 3

# Find index of first occurrence
index_3 = data.index(3)  # 2
# index_10 = data.index(10)  # ValueError: not found

# Find with start position
index_2_after_pos_3 = data.index(2, 3)  # 5 (first 2 after index 3)

# =============================================================================
# NAMED TUPLES
# =============================================================================

from collections import namedtuple

# Define named tuple class
Point = namedtuple('Point', ['x', 'y'])
Person = namedtuple('Person', 'name age occupation')  # Can use string

# Create instances
p1 = Point(3, 4)
p2 = Point(x=1, y=2)

person1 = Person("Alice", 30, "Engineer")

# Access by name or index
print(p1.x)      # 3
print(p1[0])     # 3 (still works like regular tuple)

print(person1.name)        # "Alice"
print(person1.occupation)  # "Engineer"

# Named tuples are immutable
# person1.age = 31  # AttributeError

# Convert to dictionary
person_dict = person1._asdict()  # {'name': 'Alice', 'age': 30, 'occupation': 'Engineer'}

# Create new instance with some fields changed
person2 = person1._replace(age=31)  # Person(name='Alice', age=31, occupation='Engineer')

# Get field names
print(Person._fields)  # ('name', 'age', 'occupation')

# =============================================================================
# COMMON ALGORITHMS WITH SETS AND TUPLES
# =============================================================================

def remove_duplicates_preserve_order(lst):
    """Remove duplicates while preserving order"""
    seen = set()
    result = []
    for item in lst:
        if item not in seen:
            seen.add(item)
            result.append(item)
    return result

def find_common_elements(list1, list2):
    """Find common elements between two lists"""
    return list(set(list1) & set(list2))

def find_unique_elements(list1, list2):
    """Find elements unique to each list"""
    set1, set2 = set(list1), set(list2)
    unique_to_list1 = list(set1 - set2)
    unique_to_list2 = list(set2 - set1)
    return unique_to_list1, unique_to_list2

def is_subset_sum(numbers, target):
    """Check if any subset sums to target using dynamic programming"""
    possible_sums = {0}  # Start with sum 0
    
    for num in numbers:
        # Create new sums by adding current number to existing sums
        new_sums = {existing_sum + num for existing_sum in possible_sums}
        possible_sums |= new_sums
        
        if target in possible_sums:
            return True
    
    return target in possible_sums

def group_coordinates(points):
    """Group points by quadrant"""
    from collections import defaultdict
    
    quadrants = defaultdict(list)
    
    for x, y in points:
        if x >= 0 and y >= 0:
            quadrant = "I"
        elif x < 0 and y >= 0:
            quadrant = "II"
        elif x < 0 and y < 0:
            quadrant = "III"
        else:
            quadrant = "IV"
        
        quadrants[quadrant].append((x, y))
    
    return dict(quadrants)

# =============================================================================
# PROGRAMMING PATTERNS WITH SETS
# =============================================================================

def has_duplicates(lst):
    """Check if list has duplicates - O(n) time"""
    return len(lst) != len(set(lst))

def find_missing_number(nums, n):
    """Find missing number from 1 to n"""
    expected = set(range(1, n + 1))
    actual = set(nums)
    missing = expected - actual
    return list(missing)

def find_single_number(nums):
    """Find number that appears only once (others appear twice)"""
    # Using XOR property: a ^ a = 0, a ^ 0 = a
    result = 0
    for num in nums:
        result ^= num
    return result

def intersection_of_arrays(arrays):
    """Find intersection of multiple arrays"""
    if not arrays:
        return []
    
    result = set(arrays[0])
    for array in arrays[1:]:
        result &= set(array)
    
    return list(result)

def longest_consecutive_sequence(nums):
    """Find length of longest consecutive sequence"""
    num_set = set(nums)
    longest = 0
    
    for num in num_set:
        # Only start counting from the beginning of a sequence
        if num - 1 not in num_set:
            current_num = num
            current_length = 1
            
            while current_num + 1 in num_set:
                current_num += 1
                current_length += 1
            
            longest = max(longest, current_length)
    
    return longest

# =============================================================================
# TUPLE AS DICTIONARY KEYS
# =============================================================================

# Tuples are hashable, so can be used as dict keys
coordinate_values = {
    (0, 0): "origin",
    (1, 0): "right",
    (0, 1): "up",
    (-1, 0): "left",
    (0, -1): "down"
}

# Useful for memoization
def fibonacci_memo():
    cache = {}
    
    def fib(n):
        if n in cache:
            return cache[n]
        
        if n < 2:
            return n
        
        result = fib(n-1) + fib(n-2)
        cache[n] = result
        return result
    
    return fib

# Multi-dimensional memoization
def edit_distance_memo():
    cache = {}
    
    def edit_distance(s1, s2, i, j):
        key = (i, j)  # Tuple as cache key
        if key in cache:
            return cache[key]
        
        # Base cases
        if i == 0:
            return j
        if j == 0:
            return i
        
        # Recursive cases
        if s1[i-1] == s2[j-1]:
            result = edit_distance(s1, s2, i-1, j-1)
        else:
            insert = edit_distance(s1, s2, i, j-1) + 1
            delete = edit_distance(s1, s2, i-1, j) + 1
            replace = edit_distance(s1, s2, i-1, j-1) + 1
            result = min(insert, delete, replace)
        
        cache[key] = result
        return result
    
    return edit_distance

# =============================================================================
# PERFORMANCE CONSIDERATIONS
# =============================================================================

"""
Set Performance:
- Membership testing: O(1) average, O(n) worst case
- Add/Remove: O(1) average, O(n) worst case
- Set operations (union, intersection): O(len(set1) + len(set2))

Tuple Performance:
- Access by index: O(1)
- Slicing: O(k) where k is slice length
- Concatenation: O(n + m) where n, m are lengths
- Comparison: O(n) where n is length

Memory:
- Sets: More memory overhead than lists
- Tuples: Less memory overhead than lists (immutable)
- Named tuples: Slight overhead over regular tuples
"""

# When to use sets vs lists
large_data = list(range(100000))

# Fast membership testing with set
data_set = set(large_data)
# O(1) - very fast
exists = 50000 in data_set

# Slow membership testing with list
# O(n) - slow for large lists
exists_slow = 50000 in large_data

# Use tuples for:
# - Immutable sequences
# - Dictionary keys
# - Multiple return values
# - Coordinates, RGB values, etc.

# Use sets for:
# - Removing duplicates
# - Fast membership testing
# - Mathematical set operations
# - Finding unique elements

# =============================================================================
# COMMON GOTCHAS
# =============================================================================

# 1. Empty set vs empty dict
empty_dict = {}      # This is a dictionary!
empty_set = set()    # This is a set

# 2. Single element tuple needs comma
not_tuple = (5)      # This is just an integer 5
is_tuple = (5,)      # This is a tuple with one element

# 3. Sets are unordered (though insertion order preserved in Python 3.7+)
s = {3, 1, 4, 1, 5}  # Duplicates removed: {1, 3, 4, 5}
# Don't rely on order for compatibility

# 4. Tuples with mutable elements
tuple_with_list = ([1, 2], [3, 4])
tuple_with_list[0].append(3)  # This works! Tuple itself unchanged
# tuple_with_list[0] = [1, 2, 3]  # This would fail

# 5. Set elements must be hashable
# valid_set = {1, 2, 3, "hello", (1, 2)}  # OK
# invalid_set = {1, 2, [1, 2]}  # Error: lists are not hashable

# =============================================================================
# ADVANCED PATTERNS
# =============================================================================

def powerset(iterable):
    """Generate all possible subsets"""
    from itertools import combinations
    s = list(iterable)
    return [set(combinations(s, r)) for r in range(len(s) + 1)]

def cartesian_product_tuples(set1, set2):
    """Generate Cartesian product as tuples"""
    return [(a, b) for a in set1 for b in set2]

def find_all_pairs_with_sum(numbers, target_sum):
    """Find all pairs that sum to target"""
    seen = set()
    pairs = set()
    
    for num in numbers:
        complement = target_sum - num
        if complement in seen:
            # Use tuple to ensure hashable pair
            pair = tuple(sorted([num, complement]))
            pairs.add(pair)
        seen.add(num)
    
    return list(pairs)

# Example usage
numbers = [2, 7, 11, 15, 3, 6]
pairs = find_all_pairs_with_sum(numbers, 9)  # [(2, 7), (3, 6)]