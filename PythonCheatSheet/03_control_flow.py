"""
Python Control Flow Cheat Sheet
===============================
If/else, loops, exception handling patterns and techniques
"""

# =============================================================================
# IF/ELSE STATEMENTS
# =============================================================================

# Basic if/else
age = 25
if age >= 18:
    print("Adult")
elif age >= 13:
    print("Teenager")
else:
    print("Child")

# Multiple conditions
temperature = 75
humidity = 60

if temperature > 80 and humidity > 70:
    print("Hot and humid")
elif temperature > 80 or humidity > 70:
    print("Either hot or humid")
else:
    print("Pleasant weather")

# Checking for None, empty containers
data = []
if data:  # Pythonic way to check if list is not empty
    print("Data exists")
else:
    print("No data")

# Checking multiple values
value = "apple"
if value in ["apple", "banana", "orange"]:
    print("It's a fruit")

# =============================================================================
# FOR LOOPS
# =============================================================================

# Basic for loop
numbers = [1, 2, 3, 4, 5]
for num in numbers:
    print(num)

# Range-based loops
for i in range(5):          # 0, 1, 2, 3, 4
    print(i)

for i in range(2, 8, 2):    # 2, 4, 6
    print(i)

# Enumerate for index and value
fruits = ["apple", "banana", "orange"]
for index, fruit in enumerate(fruits):
    print(f"{index}: {fruit}")

# Starting enumerate at different number
for index, fruit in enumerate(fruits, 1):  # Start at 1
    print(f"{index}: {fruit}")

# Zip for parallel iteration
names = ["Alice", "Bob", "Charlie"]
ages = [25, 30, 35]
for name, age in zip(names, ages):
    print(f"{name} is {age} years old")

# Reverse iteration
for item in reversed(numbers):
    print(item)

# Sorted iteration
words = ["banana", "apple", "cherry"]
for word in sorted(words):
    print(word)

# Dictionary iteration
person = {"name": "Alice", "age": 30, "city": "New York"}

# Keys only
for key in person:
    print(key)

# Keys explicitly
for key in person.keys():
    print(key)

# Values only
for value in person.values():
    print(value)

# Both keys and values
for key, value in person.items():
    print(f"{key}: {value}")

# =============================================================================
# WHILE LOOPS
# =============================================================================

# Basic while loop
count = 0
while count < 5:
    print(count)
    count += 1

# While with else (executes if loop completes normally)
count = 0
while count < 3:
    print(count)
    count += 1
else:
    print("Loop completed normally")

# Infinite loop with break
while True:
    user_input = input("Enter 'quit' to exit: ")
    if user_input == 'quit':
        break
    print(f"You entered: {user_input}")

# =============================================================================
# BREAK AND CONTINUE
# =============================================================================

# Break - exit loop completely
for i in range(10):
    if i == 5:
        break
    print(i)  # Prints 0, 1, 2, 3, 4

# Continue - skip current iteration
for i in range(10):
    if i % 2 == 0:
        continue
    print(i)  # Prints 1, 3, 5, 7, 9

# Nested loops with labeled break (using functions)
def find_in_matrix(matrix, target):
    for i, row in enumerate(matrix):
        for j, val in enumerate(row):
            if val == target:
                return i, j  # "Break" out of both loops
    return None

# =============================================================================
# EXCEPTION HANDLING
# =============================================================================

# Basic try/except
try:
    result = 10 / 0
except ZeroDivisionError:
    print("Cannot divide by zero!")

# Multiple exceptions
try:
    num = int(input("Enter a number: "))
    result = 10 / num
except ValueError:
    print("Invalid number format")
except ZeroDivisionError:
    print("Cannot divide by zero")

# Multiple exceptions in one except
try:
    # some code
    pass
except (ValueError, TypeError) as e:
    print(f"Error: {e}")

# Generic exception handler
try:
    # risky code
    pass
except Exception as e:
    print(f"Unexpected error: {e}")

# Try/except/else/finally
try:
    file = open("data.txt", "r")
except FileNotFoundError:
    print("File not found")
else:
    # Executes only if no exception occurred
    content = file.read()
    print("File read successfully")
finally:
    # Always executes
    try:
        file.close()
    except NameError:
        pass

# Raising exceptions
def validate_age(age):
    if age < 0:
        raise ValueError("Age cannot be negative")
    if age > 150:
        raise ValueError("Age seems unrealistic")
    return True

# Custom exceptions
class CustomError(Exception):
    pass

def risky_function():
    raise CustomError("Something went wrong")

try:
    risky_function()
except CustomError as e:
    print(f"Custom error: {e}")

# =============================================================================
# LIST COMPREHENSIONS (Advanced Control Flow)
# =============================================================================

# Basic list comprehension
squares = [x**2 for x in range(10)]

# With condition
even_squares = [x**2 for x in range(10) if x % 2 == 0]

# Multiple conditions
filtered = [x for x in range(20) if x % 2 == 0 if x % 3 == 0]

# Nested loops in comprehension
matrix = [[i+j for j in range(3)] for i in range(3)]

# Flattening nested lists
nested = [[1, 2], [3, 4], [5, 6]]
flattened = [item for sublist in nested for item in sublist]

# Dictionary comprehension
word_lengths = {word: len(word) for word in ["apple", "banana", "cherry"]}

# Set comprehension
unique_lengths = {len(word) for word in ["apple", "banana", "cherry", "date"]}

# =============================================================================
# USEFUL CONTROL FLOW PATTERNS
# =============================================================================

# Early return pattern
def process_data(data):
    if not data:
        return None
    
    if len(data) < 2:
        return data[0]
    
    # Main logic here
    return sum(data) / len(data)

# Using any() and all() for complex conditions
def has_positive_number(numbers):
    return any(num > 0 for num in numbers)

def all_positive(numbers):
    return all(num > 0 for num in numbers)

# Loop else pattern (rarely used but good to know)
def find_prime_factor(n):
    for i in range(2, int(n**0.5) + 1):
        if n % i == 0:
            return i
    else:
        return n  # n is prime

# Sentinel value pattern
def read_until_sentinel(sentinel="STOP"):
    values = []
    while True:
        value = input("Enter value (or STOP): ")
        if value == sentinel:
            break
        values.append(value)
    return values

# State machine pattern
def simple_state_machine():
    state = "start"
    
    while state != "end":
        if state == "start":
            print("Starting...")
            state = "processing"
        elif state == "processing":
            print("Processing...")
            state = "end"
    
    print("Finished!")

# =============================================================================
# PERFORMANCE TIPS
# =============================================================================

# Use enumerate instead of range(len())
# Bad
items = ["a", "b", "c"]
for i in range(len(items)):
    print(f"{i}: {items[i]}")

# Good
for i, item in enumerate(items):
    print(f"{i}: {item}")

# Use zip for parallel iteration instead of indexing
# Bad
names = ["Alice", "Bob"]
ages = [25, 30]
for i in range(len(names)):
    print(f"{names[i]} is {ages[i]} years old")

# Good
for name, age in zip(names, ages):
    print(f"{name} is {age} years old")