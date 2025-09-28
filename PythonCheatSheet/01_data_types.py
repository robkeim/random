"""
Python Data Types Cheat Sheet
=============================
Essential data types and type operations for programming
"""

# =============================================================================
# BASIC DATA TYPES
# =============================================================================

# Integers
x = 42
y = -17
z = 0

# Check if integer
print(isinstance(x, int))  # True

# Integer operations
print(x // y)  # Floor division: -3
print(x % y)   # Modulo: 8
print(x ** 2)  # Exponentiation: 1764
print(divmod(x, y))  # (quotient, remainder): (-3, 8)

# =============================================================================
# FLOATS
# =============================================================================

a = 3.14
b = -2.5
c = 1e6  # Scientific notation: 1000000.0

# Float precision issues
print(0.1 + 0.2 == 0.3)  # False!
print(abs(0.1 + 0.2 - 0.3) < 1e-10)  # True (better comparison)

# Infinity and NaN
import math
inf = float('inf')
neg_inf = float('-inf')
nan = float('nan')

print(math.isinf(inf))    # True
print(math.isnan(nan))    # True

# =============================================================================
# BOOLEANS
# =============================================================================

flag = True
other_flag = False

# Falsy values in Python
falsy_values = [False, None, 0, 0.0, '', [], {}, set()]
for val in falsy_values:
    print(f"{val} is falsy: {not val}")

# Boolean operations (short-circuiting)
result = True or expensive_operation()  # expensive_operation() not called
result = False and expensive_operation()  # expensive_operation() not called

# =============================================================================
# NONE TYPE
# =============================================================================

nothing = None
print(nothing is None)  # True (use 'is', not ==)
print(nothing == None)  # True but not recommended

# Common None patterns
def get_value(key, default=None):
    return data.get(key, default)

# =============================================================================
# TYPE CONVERSION
# =============================================================================

# String to number
num_str = "123"
num = int(num_str)
float_num = float(num_str)

# Number to string
str_num = str(123)

# With error handling
try:
    num = int("abc")
except ValueError:
    print("Invalid number format")

# =============================================================================
# TYPE CHECKING
# =============================================================================

value = 42

# Check specific type
print(type(value))  # <class 'int'>
print(type(value) == int)  # True

# Better: use isinstance (handles inheritance)
print(isinstance(value, int))  # True
print(isinstance(value, (int, float)))  # True (multiple types)

# =============================================================================
# USEFUL BUILT-IN FUNCTIONS
# =============================================================================

numbers = [1, 2, 3, 4, 5]

print(len(numbers))     # 5
print(sum(numbers))     # 15
print(min(numbers))     # 1
print(max(numbers))     # 5
print(sorted(numbers, reverse=True))  # [5, 4, 3, 2, 1]

# All/any for boolean checks
print(all([True, True, True]))   # True
print(any([False, False, True])) # True

# Range for loops
print(list(range(5)))        # [0, 1, 2, 3, 4]
print(list(range(2, 8, 2)))  # [2, 4, 6]

# Enumerate for index/value pairs
for i, val in enumerate(['a', 'b', 'c']):
    print(f"Index {i}: {val}")

# Zip for parallel iteration
names = ['Alice', 'Bob', 'Charlie']
ages = [25, 30, 35]
for name, age in zip(names, ages):
    print(f"{name} is {age} years old")