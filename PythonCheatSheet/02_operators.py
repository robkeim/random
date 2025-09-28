"""
Python Operators Cheat Sheet
============================
Essential operators and their usage patterns
"""

# =============================================================================
# BOOLEAN OPERATORS
# =============================================================================

# Basic boolean operators
a = True
b = False

print(a and b)  # False
print(a or b)   # True
print(not a)    # False

# Short-circuit evaluation
def expensive_function():
    print("This won't be called")
    return True

# 'and' short-circuits on first False
result = False and expensive_function()  # expensive_function not called

# 'or' short-circuits on first True
result = True or expensive_function()   # expensive_function not called

# =============================================================================
# COMPARISON OPERATORS
# =============================================================================

x, y = 10, 20

print(x == y)   # False (equality)
print(x != y)   # True (not equal)
print(x < y)    # True (less than)
print(x <= y)   # True (less than or equal)
print(x > y)    # False (greater than)
print(x >= y)   # False (greater than or equal)

# Chained comparisons (very useful!)
age = 25
print(18 <= age <= 65)  # True (equivalent to: age >= 18 and age <= 65)

# Identity operators
list1 = [1, 2, 3]
list2 = [1, 2, 3]
list3 = list1

print(list1 == list2)  # True (same content)
print(list1 is list2)  # False (different objects)
print(list1 is list3)  # True (same object)

# Membership operators
numbers = [1, 2, 3, 4, 5]
print(3 in numbers)     # True
print(6 not in numbers) # True

# =============================================================================
# ARITHMETIC OPERATORS
# =============================================================================

a, b = 17, 5

print(a + b)    # 22 (addition)
print(a - b)    # 12 (subtraction)
print(a * b)    # 85 (multiplication)
print(a / b)    # 3.4 (division - always returns float)
print(a // b)   # 3 (floor division)
print(a % b)    # 2 (modulo)
print(a ** b)   # 1419857 (exponentiation)

# Useful modulo applications
def is_even(n):
    return n % 2 == 0

def get_last_digit(n):
    return n % 10

# =============================================================================
# ASSIGNMENT OPERATORS
# =============================================================================

x = 10
x += 5   # x = x + 5, x is now 15
x -= 3   # x = x - 3, x is now 12
x *= 2   # x = x * 2, x is now 24
x /= 4   # x = x / 4, x is now 6.0
x //= 2  # x = x // 2, x is now 3.0
x **= 2  # x = x ** 2, x is now 9.0
x %= 4   # x = x % 4, x is now 1.0

# =============================================================================
# BITWISE OPERATORS
# =============================================================================

a, b = 60, 13  # 60 = 0011 1100, 13 = 0000 1101

print(a & b)   # 12 = 0000 1100 (AND)
print(a | b)   # 61 = 0011 1101 (OR)
print(a ^ b)   # 49 = 0011 0001 (XOR)
print(~a)      # -61 = 1100 0011 (NOT/complement)
print(a << 2)  # 240 = 1111 0000 (left shift)
print(a >> 2)  # 15 = 0000 1111 (right shift)

# Useful bitwise operations
def is_power_of_two(n):
    """Check if n is power of 2 using bit manipulation"""
    return n > 0 and (n & (n - 1)) == 0

def count_set_bits(n):
    """Count number of 1s in binary representation"""
    count = 0
    while n:
        count += n & 1
        n >>= 1
    return count

# Even faster way to count set bits
def count_set_bits_fast(n):
    count = 0
    while n:
        n &= n - 1  # Remove rightmost set bit
        count += 1
    return count

# =============================================================================
# OPERATOR PRECEDENCE (high to low)
# =============================================================================

# 1. () - Parentheses
# 2. ** - Exponentiation
# 3. +x, -x, ~x - Unary operators
# 4. *, /, //, % - Multiplication, division, modulo
# 5. +, - - Addition, subtraction
# 6. <<, >> - Bitwise shifts
# 7. & - Bitwise AND
# 8. ^ - Bitwise XOR
# 9. | - Bitwise OR
# 10. ==, !=, <, <=, >, >=, is, is not, in, not in - Comparisons
# 11. not - Boolean NOT
# 12. and - Boolean AND
# 13. or - Boolean OR

# Example of precedence
result = 2 + 3 * 4  # 14, not 20
result = (2 + 3) * 4  # 20

# =============================================================================
# USEFUL OPERATOR PATTERNS
# =============================================================================

# Swap without temporary variable
a, b = 10, 20
a, b = b, a  # Pythonic way

# XOR swap (less readable, but interesting)
def xor_swap(a, b):
    a ^= b
    b ^= a
    a ^= b
    return a, b

# Check if number is odd/even without modulo
def is_odd_bitwise(n):
    return n & 1 == 1

# Toggle between 0 and 1
def toggle(bit):
    return 1 - bit  # or bit ^ 1

# Check if two numbers have same sign
def same_sign(a, b):
    return (a ^ b) >= 0

# Absolute value using bitwise operations
def abs_bitwise(n):
    mask = n >> 31  # All 1s if negative, all 0s if positive
    return (n ^ mask) - mask

# =============================================================================
# CONDITIONAL EXPRESSIONS (TERNARY OPERATOR)
# =============================================================================

age = 20
status = "adult" if age >= 18 else "minor"

# Multiple conditions
grade = 85
letter = "A" if grade >= 90 else "B" if grade >= 80 else "C" if grade >= 70 else "F"

# In function calls
def max_of_two(a, b):
    return a if a > b else b