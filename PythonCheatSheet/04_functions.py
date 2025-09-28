"""
Python Functions Cheat Sheet
===========================
Function definitions, lambdas, decorators, and advanced patterns
"""

# =============================================================================
# BASIC FUNCTION DEFINITIONS
# =============================================================================

def greet(name):
    """Basic function with docstring"""
    return f"Hello, {name}!"

# Function with default arguments
def greet_with_title(name, title="Mr."):
    return f"Hello, {title} {name}!"

# Function with multiple return values
def divide_and_remainder(a, b):
    return a // b, a % b

# Unpacking return values
quotient, remainder = divide_and_remainder(17, 5)

# =============================================================================
# PARAMETER TYPES
# =============================================================================

# Positional arguments
def add(a, b, c):
    return a + b + c

result = add(1, 2, 3)

# Keyword arguments
def create_person(name, age, city="Unknown"):
    return {"name": name, "age": age, "city": city}

person = create_person(name="Alice", age=30)
person2 = create_person("Bob", city="NYC", age=25)  # Mixed order OK

# *args - Variable number of positional arguments
def sum_all(*args):
    return sum(args)

print(sum_all(1, 2, 3, 4, 5))  # 15

# **kwargs - Variable number of keyword arguments
def print_info(**kwargs):
    for key, value in kwargs.items():
        print(f"{key}: {value}")

print_info(name="Alice", age=30, city="NYC")

# Combining all parameter types
def complex_function(required, default="default", *args, **kwargs):
    print(f"Required: {required}")
    print(f"Default: {default}")
    print(f"Args: {args}")
    print(f"Kwargs: {kwargs}")

complex_function("req", "custom", 1, 2, 3, extra="value")

# =============================================================================
# LAMBDA FUNCTIONS
# =============================================================================

# Basic lambda
square = lambda x: x ** 2
print(square(5))  # 25

# Lambda with multiple arguments
multiply = lambda x, y: x * y
print(multiply(3, 4))  # 12

# Lambda in higher-order functions
numbers = [1, 2, 3, 4, 5]
squared = list(map(lambda x: x ** 2, numbers))
evens = list(filter(lambda x: x % 2 == 0, numbers))
total = reduce(lambda x, y: x + y, numbers)

# Lambda for sorting
students = [("Alice", 85), ("Bob", 90), ("Charlie", 78)]
students.sort(key=lambda student: student[1])  # Sort by grade

# =============================================================================
# HIGHER-ORDER FUNCTIONS
# =============================================================================

# Functions that take other functions as arguments
def apply_twice(func, x):
    return func(func(x))

def double(x):
    return x * 2

result = apply_twice(double, 5)  # 20

# Functions that return functions
def make_multiplier(factor):
    def multiplier(x):
        return x * factor
    return multiplier

double_func = make_multiplier(2)
triple_func = make_multiplier(3)

print(double_func(5))  # 10
print(triple_func(5))  # 15

# =============================================================================
# DECORATORS
# =============================================================================

# Simple decorator
def timer_decorator(func):
    import time
    def wrapper(*args, **kwargs):
        start_time = time.time()
        result = func(*args, **kwargs)
        end_time = time.time()
        print(f"{func.__name__} took {end_time - start_time:.4f} seconds")
        return result
    return wrapper

@timer_decorator
def slow_function():
    import time
    time.sleep(1)
    return "Done!"

# Decorator with arguments
def repeat(times):
    def decorator(func):
        def wrapper(*args, **kwargs):
            for _ in range(times):
                result = func(*args, **kwargs)
            return result
        return wrapper
    return decorator

@repeat(3)
def say_hello():
    print("Hello!")

# Multiple decorators
def uppercase_decorator(func):
    def wrapper(*args, **kwargs):
        result = func(*args, **kwargs)
        return result.upper()
    return wrapper

def exclamation_decorator(func):
    def wrapper(*args, **kwargs):
        result = func(*args, **kwargs)
        return result + "!"
    return wrapper

@exclamation_decorator
@uppercase_decorator
def greet_loudly(name):
    return f"hello {name}"

print(greet_loudly("alice"))  # "HELLO ALICE!"

# Class-based decorator
class CountCalls:
    def __init__(self, func):
        self.func = func
        self.count = 0
    
    def __call__(self, *args, **kwargs):
        self.count += 1
        print(f"{self.func.__name__} has been called {self.count} times")
        return self.func(*args, **kwargs)

@CountCalls
def say_hi():
    print("Hi!")

# =============================================================================
# BUILT-IN HIGHER-ORDER FUNCTIONS
# =============================================================================

from functools import reduce

numbers = [1, 2, 3, 4, 5]

# map() - apply function to each element
squared = list(map(lambda x: x**2, numbers))  # [1, 4, 9, 16, 25]

# filter() - filter elements based on condition
evens = list(filter(lambda x: x % 2 == 0, numbers))  # [2, 4]

# reduce() - reduce list to single value
product = reduce(lambda x, y: x * y, numbers)  # 120

# zip() - combine iterables
names = ["Alice", "Bob", "Charlie"]
ages = [25, 30, 35]
combined = list(zip(names, ages))  # [('Alice', 25), ('Bob', 30), ('Charlie', 35)]

# enumerate() - add index to iterable
indexed = list(enumerate(names))  # [(0, 'Alice'), (1, 'Bob'), (2, 'Charlie')]

# sorted() with key function
words = ["banana", "apple", "cherry"]
by_length = sorted(words, key=len)  # ['apple', 'banana', 'cherry']
by_last_letter = sorted(words, key=lambda x: x[-1])  # ['banana', 'apple', 'cherry']

# =============================================================================
# SCOPE AND CLOSURES
# =============================================================================

# Global vs Local scope
global_var = "I'm global"

def test_scope():
    local_var = "I'm local"
    print(global_var)  # Can access global
    print(local_var)   # Can access local

# Modifying global variables
counter = 0

def increment():
    global counter
    counter += 1

# Closure example
def make_counter():
    count = 0
    
    def counter():
        nonlocal count
        count += 1
        return count
    
    return counter

counter1 = make_counter()
counter2 = make_counter()

print(counter1())  # 1
print(counter1())  # 2
print(counter2())  # 1 (independent counter)

# =============================================================================
# FUNCTION ANNOTATIONS (TYPE HINTS)
# =============================================================================

def add_numbers(a: int, b: int) -> int:
    """Add two integers and return the result"""
    return a + b

from typing import List, Dict, Optional, Union

def process_list(items: List[int]) -> Dict[str, int]:
    return {
        "sum": sum(items),
        "count": len(items),
        "max": max(items) if items else 0
    }

def find_user(user_id: int) -> Optional[Dict[str, Union[str, int]]]:
    # Returns user dict or None if not found
    users_db = {1: {"name": "Alice", "age": 30}}
    return users_db.get(user_id)

# =============================================================================
# COMMON FUNCTION PATTERNS FOR DEVELOPMENT
# =============================================================================

# Memoization (caching function results)
def memoize(func):
    cache = {}
    def wrapper(*args):
        if args in cache:
            return cache[args]
        result = func(*args)
        cache[args] = result
        return result
    return wrapper

@memoize
def fibonacci(n):
    if n < 2:
        return n
    return fibonacci(n-1) + fibonacci(n-2)

# Using functools.lru_cache (built-in memoization)
from functools import lru_cache

@lru_cache(maxsize=128)
def fibonacci_cached(n):
    if n < 2:
        return n
    return fibonacci_cached(n-1) + fibonacci_cached(n-2)

# Partial application
from functools import partial

def multiply(x, y):
    return x * y

double = partial(multiply, 2)  # Fix first argument to 2
print(double(5))  # 10

# Curry-like behavior
def curry_add(x):
    def add_y(y):
        return x + y
    return add_y

add_5 = curry_add(5)
print(add_5(3))  # 8

# Generator functions
def fibonacci_generator():
    a, b = 0, 1
    while True:
        yield a
        a, b = b, a + b

# Usage
fib_gen = fibonacci_generator()
first_10_fibs = [next(fib_gen) for _ in range(10)]

# Factory pattern using functions
def create_validator(min_length=0, max_length=float('inf')):
    def validate(text):
        return min_length <= len(text) <= max_length
    return validate

email_validator = create_validator(5, 50)
password_validator = create_validator(8, 20)

# =============================================================================
# ERROR HANDLING IN FUNCTIONS
# =============================================================================

def safe_divide(a, b):
    """Division with error handling"""
    try:
        return a / b
    except ZeroDivisionError:
        return float('inf')
    except TypeError:
        raise ValueError("Arguments must be numbers")

def validate_and_process(data):
    """Input validation pattern"""
    if not isinstance(data, list):
        raise TypeError("Data must be a list")
    
    if not data:
        raise ValueError("Data cannot be empty")
    
    # Process data here
    return [item * 2 for item in data]

# Using assertions for debugging
def factorial(n):
    assert n >= 0, "Factorial is not defined for negative numbers"
    assert isinstance(n, int), "Factorial requires an integer"
    
    if n <= 1:
        return 1
    return n * factorial(n - 1)