"""
Python Lists and Arrays Cheat Sheet
===================================
Essential list operations and programming patterns
"""

# =============================================================================
# LIST CREATION AND INITIALIZATION
# =============================================================================

# Empty list
empty_list = []
empty_list2 = list()

# List with initial values
numbers = [1, 2, 3, 4, 5]
mixed = [1, "hello", 3.14, True]

# List from range
nums = list(range(10))  # [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
evens = list(range(0, 10, 2))  # [0, 2, 4, 6, 8]

# Repeated elements
zeros = [0] * 5  # [0, 0, 0, 0, 0]

# 2D list initialization (be careful!)
# Wrong way (creates references to same list)
matrix_wrong = [[0] * 3] * 3  # Don't do this!

# Right way
matrix = [[0] * 3 for _ in range(3)]

# =============================================================================
# ACCESSING ELEMENTS
# =============================================================================

fruits = ["apple", "banana", "cherry", "date", "elderberry"]

# Positive indexing
print(fruits[0])   # "apple"
print(fruits[2])   # "cherry"

# Negative indexing
print(fruits[-1])  # "elderberry" (last element)
print(fruits[-2])  # "date" (second to last)

# Slicing [start:end:step]
print(fruits[1:4])    # ["banana", "cherry", "date"]
print(fruits[:3])     # ["apple", "banana", "cherry"]
print(fruits[2:])     # ["cherry", "date", "elderberry"]
print(fruits[:])      # Copy of entire list
print(fruits[::2])    # ["apple", "cherry", "elderberry"] (every 2nd)
print(fruits[::-1])   # Reverse the list

# =============================================================================
# MODIFYING LISTS
# =============================================================================

numbers = [1, 2, 3]

# Adding elements
numbers.append(4)           # [1, 2, 3, 4]
numbers.insert(1, 1.5)      # [1, 1.5, 2, 3, 4]
numbers.extend([5, 6])      # [1, 1.5, 2, 3, 4, 5, 6]

# Removing elements
numbers.remove(1.5)         # Remove first occurrence
popped = numbers.pop()      # Remove and return last element
popped_at = numbers.pop(0)  # Remove and return element at index
del numbers[1]              # Remove element at index
numbers.clear()             # Remove all elements

# Modifying elements
fruits[0] = "apricot"       # Change first element
fruits[1:3] = ["blueberry", "coconut"]  # Replace slice

# =============================================================================
# LIST METHODS
# =============================================================================

numbers = [3, 1, 4, 1, 5, 9, 2, 6]

# Sorting
numbers.sort()              # Sort in place: [1, 1, 2, 3, 4, 5, 6, 9]
numbers.sort(reverse=True)  # Reverse sort: [9, 6, 5, 4, 3, 2, 1, 1]

# Create sorted copy (original unchanged)
sorted_nums = sorted(numbers)

# Reversing
numbers.reverse()           # Reverse in place
reversed_nums = numbers[::-1]  # Create reversed copy

# Finding elements
index = numbers.index(5)    # Find index of first occurrence
count = numbers.count(1)    # Count occurrences

# Checking existence
exists = 5 in numbers       # True if 5 is in list
not_exists = 10 not in numbers  # True if 10 is not in list

# =============================================================================
# LIST COMPREHENSIONS
# =============================================================================

# Basic comprehension
squares = [x**2 for x in range(10)]

# With condition
even_squares = [x**2 for x in range(10) if x % 2 == 0]

# Multiple conditions
filtered = [x for x in range(50) if x % 3 == 0 if x % 5 == 0]

# With else clause
pos_neg = [x if x > 0 else 0 for x in [-2, -1, 0, 1, 2]]

# Nested loops
pairs = [(x, y) for x in range(3) for y in range(3)]

# Flattening nested lists
nested = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]
flattened = [item for sublist in nested for item in sublist]

# Processing strings
words = ["hello", "world", "python"]
lengths = [len(word) for word in words]
upper_words = [word.upper() for word in words if len(word) > 4]

# =============================================================================
# COMMON ALGORITHMS ON LISTS
# =============================================================================

# Finding maximum and minimum
numbers = [3, 7, 2, 9, 1, 5]
maximum = max(numbers)
minimum = min(numbers)

# Finding max/min with custom key
words = ["python", "java", "c", "javascript"]
longest = max(words, key=len)
shortest = min(words, key=len)

# Sum and average
total = sum(numbers)
average = sum(numbers) / len(numbers)

# Linear search
def linear_search(arr, target):
    for i, val in enumerate(arr):
        if val == target:
            return i
    return -1

# Binary search (for sorted arrays)
def binary_search(arr, target):
    left, right = 0, len(arr) - 1
    
    while left <= right:
        mid = (left + right) // 2
        if arr[mid] == target:
            return mid
        elif arr[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    
    return -1

# Two pointers technique
def two_sum(arr, target):
    """Find two numbers that add up to target (sorted array)"""
    left, right = 0, len(arr) - 1
    
    while left < right:
        current_sum = arr[left] + arr[right]
        if current_sum == target:
            return [left, right]
        elif current_sum < target:
            left += 1
        else:
            right -= 1
    
    return []

# =============================================================================
# ARRAY-LIKE OPERATIONS
# =============================================================================

# Using array module for memory efficiency
from array import array

# Create typed array
int_array = array('i', [1, 2, 3, 4, 5])  # 'i' for integers

# NumPy arrays (if available)
try:
    import numpy as np
    
    # Create numpy array
    np_array = np.array([1, 2, 3, 4, 5])
    
    # Element-wise operations
    doubled = np_array * 2
    squared = np_array ** 2
    
    # Mathematical operations
    mean = np.mean(np_array)
    std = np.std(np_array)
    
except ImportError:
    print("NumPy not available")

# =============================================================================
# COMMON PROGRAMMING PATTERNS WITH LISTS
# =============================================================================

# Sliding window pattern
def max_sum_subarray_of_size_k(arr, k):
    """Find maximum sum of subarray of size k"""
    if len(arr) < k:
        return None
    
    # Calculate sum of first window
    window_sum = sum(arr[:k])
    max_sum = window_sum
    
    # Slide the window
    for i in range(1, len(arr) - k + 1):
        window_sum = window_sum - arr[i - 1] + arr[i + k - 1]
        max_sum = max(max_sum, window_sum)
    
    return max_sum

# Kadane's algorithm for maximum subarray
def max_subarray_sum(arr):
    """Find maximum sum of contiguous subarray"""
    max_ending_here = max_so_far = arr[0]
    
    for i in range(1, len(arr)):
        max_ending_here = max(arr[i], max_ending_here + arr[i])
        max_so_far = max(max_so_far, max_ending_here)
    
    return max_so_far

# Dutch flag problem (3-way partitioning)
def dutch_flag_partition(arr, pivot):
    """Partition array into elements <pivot, =pivot, >pivot"""
    low = mid = 0
    high = len(arr) - 1
    
    while mid <= high:
        if arr[mid] < pivot:
            arr[low], arr[mid] = arr[mid], arr[low]
            low += 1
            mid += 1
        elif arr[mid] == pivot:
            mid += 1
        else:
            arr[mid], arr[high] = arr[high], arr[mid]
            high -= 1

# Remove duplicates from sorted array
def remove_duplicates(arr):
    """Remove duplicates in-place from sorted array"""
    if not arr:
        return 0
    
    write_index = 1
    
    for read_index in range(1, len(arr)):
        if arr[read_index] != arr[read_index - 1]:
            arr[write_index] = arr[read_index]
            write_index += 1
    
    return write_index

# Rotate array
def rotate_array(arr, k):
    """Rotate array to the right by k steps"""
    if not arr or k == 0:
        return
    
    k = k % len(arr)  # Handle k > len(arr)
    
    # Reverse entire array
    reverse_subarray(arr, 0, len(arr) - 1)
    # Reverse first k elements
    reverse_subarray(arr, 0, k - 1)
    # Reverse remaining elements
    reverse_subarray(arr, k, len(arr) - 1)

def reverse_subarray(arr, start, end):
    """Reverse subarray from start to end"""
    while start < end:
        arr[start], arr[end] = arr[end], arr[start]
        start += 1
        end -= 1

# =============================================================================
# LIST PERFORMANCE TIPS
# =============================================================================

# Efficient list creation
# Good: List comprehension
squares = [x**2 for x in range(1000)]

# Less efficient: Using append in loop
squares = []
for x in range(1000):
    squares.append(x**2)

# Pre-allocate list when size is known
# Good
result = [None] * 1000
for i in range(1000):
    result[i] = compute_value(i)

# Use deque for frequent insertions/deletions at both ends
from collections import deque

# Good for queue operations
queue = deque([1, 2, 3])
queue.appendleft(0)  # Add to front: O(1)
queue.pop()          # Remove from back: O(1)

# Lists are slower for front operations
lst = [1, 2, 3]
lst.insert(0, 0)     # Add to front: O(n)
lst.pop(0)           # Remove from front: O(n)

# =============================================================================
# COMMON GOTCHAS
# =============================================================================

# Shallow copy vs deep copy
original = [[1, 2], [3, 4]]
shallow = original.copy()  # or original[:]
original[0][0] = 99
print(shallow)  # [[99, 2], [3, 4]] - inner list is shared!

# Deep copy
import copy
deep = copy.deepcopy(original)

# Modifying list while iterating
numbers = [1, 2, 3, 4, 5]

# Wrong way
for i, num in enumerate(numbers):
    if num % 2 == 0:
        numbers.remove(num)  # Modifies list during iteration!

# Right way
numbers = [num for num in numbers if num % 2 != 0]

# Or iterate backwards
for i in range(len(numbers) - 1, -1, -1):
    if numbers[i] % 2 == 0:
        del numbers[i]

def compute_value(i):
    return i * 2