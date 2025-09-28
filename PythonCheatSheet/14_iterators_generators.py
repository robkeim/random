"""
Python Iterators and Generators Cheat Sheet
==========================================
Iterator protocol, generators, yield, and custom iterators for memory-efficient solutions
"""

# =============================================================================
# ITERATOR BASICS
# =============================================================================

# Iterator Protocol: objects that implement __iter__() and __next__()

class CountUp:
    """Simple iterator that counts from start to end"""
    
    def __init__(self, start, end):
        self.current = start
        self.end = end
    
    def __iter__(self):
        return self
    
    def __next__(self):
        if self.current >= self.end:
            raise StopIteration
        else:
            self.current += 1
            return self.current - 1

# Usage
counter = CountUp(0, 3)
for num in counter:
    print(num)  # 0, 1, 2

# Built-in iterators
numbers = [1, 2, 3, 4, 5]
iterator = iter(numbers)
print(next(iterator))  # 1
print(next(iterator))  # 2

# =============================================================================
# GENERATOR FUNCTIONS (YIELD)
# =============================================================================

def count_up_generator(start, end):
    """Generator function using yield"""
    current = start
    while current < end:
        yield current
        current += 1

# Usage
gen = count_up_generator(0, 3)
for num in gen:
    print(num)  # 0, 1, 2

def fibonacci_generator():
    """Infinite fibonacci sequence generator"""
    a, b = 0, 1
    while True:
        yield a
        a, b = b, a + b

# Usage with itertools.islice for finite sequence
import itertools
fib_gen = fibonacci_generator()
first_10_fibs = list(itertools.islice(fib_gen, 10))
print(first_10_fibs)  # [0, 1, 1, 2, 3, 5, 8, 13, 21, 34]

def squares_generator(n):
    """Generate squares of numbers from 0 to n-1"""
    for i in range(n):
        yield i * i

squares = list(squares_generator(5))
print(squares)  # [0, 1, 4, 9, 16]

def even_numbers_generator(max_num):
    """Generate even numbers up to max_num"""
    num = 0
    while num <= max_num:
        yield num
        num += 2

evens = list(even_numbers_generator(10))
print(evens)  # [0, 2, 4, 6, 8, 10]

# =============================================================================
# GENERATOR EXPRESSIONS
# =============================================================================

# Generator expression (similar to list comprehension but lazy)
squares_gen = (x * x for x in range(10))
print(type(squares_gen))  # <class 'generator'>

# Memory efficient - generates values on demand
even_squares = (x * x for x in range(1000000) if x % 2 == 0)
first_even_square = next(even_squares)  # Only calculates first value
print(first_even_square)  # 0

# Generator vs List Comprehension
import sys

# List comprehension - creates all values in memory
list_comp = [x * x for x in range(1000)]
print(f"List size: {sys.getsizeof(list_comp)} bytes")

# Generator expression - lazy evaluation
gen_expr = (x * x for x in range(1000))
print(f"Generator size: {sys.getsizeof(gen_expr)} bytes")

# =============================================================================
# ADVANCED GENERATOR FEATURES
# =============================================================================

def echo_generator():
    """Generator that can receive values via send()"""
    while True:
        received = yield
        if received is not None:
            yield f"Echo: {received}"

# Usage
echo_gen = echo_generator()
next(echo_gen)  # Prime the generator
result = echo_gen.send("Hello")
print(next(echo_gen))  # Echo: Hello

def generator_with_return():
    """Generator that returns a value"""
    yield 1
    yield 2
    yield 3
    return "Generator finished"

# Catching the return value
gen = generator_with_return()
try:
    while True:
        print(next(gen))
except StopIteration as e:
    print(f"Return value: {e.value}")

def generator_with_exception():
    """Generator that handles exceptions"""
    try:
        yield 1
        yield 2
        yield 3
    except ValueError:
        yield "Exception handled"
    finally:
        yield "Cleanup"

# Usage
gen = generator_with_exception()
print(next(gen))  # 1
print(gen.throw(ValueError))  # Exception handled
print(next(gen))  # Cleanup

# =============================================================================
# CUSTOM ITERABLES
# =============================================================================

class Range:
    """Custom implementation of range"""
    
    def __init__(self, start, stop, step=1):
        self.start = start
        self.stop = stop
        self.step = step
    
    def __iter__(self):
        return RangeIterator(self.start, self.stop, self.step)

class RangeIterator:
    """Iterator for Range class"""
    
    def __init__(self, start, stop, step):
        self.current = start
        self.stop = stop
        self.step = step
    
    def __iter__(self):
        return self
    
    def __next__(self):
        if (self.step > 0 and self.current >= self.stop) or \
           (self.step < 0 and self.current <= self.stop):
            raise StopIteration
        
        value = self.current
        self.current += self.step
        return value

# Usage
custom_range = Range(0, 10, 2)
for num in custom_range:
    print(num)  # 0, 2, 4, 6, 8

class ReverseList:
    """Iterable that yields list items in reverse order"""
    
    def __init__(self, data):
        self.data = data
    
    def __iter__(self):
        return ReverseIterator(self.data)

class ReverseIterator:
    def __init__(self, data):
        self.data = data
        self.index = len(data)
    
    def __iter__(self):
        return self
    
    def __next__(self):
        if self.index == 0:
            raise StopIteration
        
        self.index -= 1
        return self.data[self.index]

# Usage
rev_list = ReverseList([1, 2, 3, 4, 5])
for item in rev_list:
    print(item)  # 5, 4, 3, 2, 1

# =============================================================================
# PRACTICAL GENERATOR EXAMPLES
# =============================================================================

def file_reader(filename):
    """Memory-efficient file reading generator"""
    try:
        with open(filename, 'r') as file:
            for line in file:
                yield line.strip()
    except FileNotFoundError:
        print(f"File {filename} not found")

def batch_generator(iterable, batch_size):
    """Generate batches from an iterable"""
    iterator = iter(iterable)
    while True:
        batch = list(itertools.islice(iterator, batch_size))
        if not batch:
            break
        yield batch

# Usage
data = range(23)
for batch in batch_generator(data, 5):
    print(batch)
# [0, 1, 2, 3, 4]
# [5, 6, 7, 8, 9]
# [10, 11, 12, 13, 14]
# [15, 16, 17, 18, 19]
# [20, 21, 22]

def sliding_window(iterable, window_size):
    """Generate sliding windows from an iterable"""
    iterator = iter(iterable)
    window = list(itertools.islice(iterator, window_size))
    
    if len(window) < window_size:
        return
    
    yield tuple(window)
    
    for item in iterator:
        window.pop(0)
        window.append(item)
        yield tuple(window)

# Usage
data = [1, 2, 3, 4, 5, 6, 7]
for window in sliding_window(data, 3):
    print(window)
# (1, 2, 3)
# (2, 3, 4)
# (3, 4, 5)
# (4, 5, 6)
# (5, 6, 7)

def prime_generator():
    """Generate prime numbers using Sieve of Eratosthenes"""
    def sieve():
        yield 2
        primes = []
        candidate = 3
        
        while True:
            is_prime = True
            for prime in primes:
                if prime * prime > candidate:
                    break
                if candidate % prime == 0:
                    is_prime = False
                    break
            
            if is_prime:
                primes.append(candidate)
                yield candidate
            
            candidate += 2
    
    return sieve()

# Usage
prime_gen = prime_generator()
first_10_primes = [next(prime_gen) for _ in range(10)]
print(first_10_primes)  # [2, 3, 5, 7, 11, 13, 17, 19, 23, 29]

def tree_traversal_generator(root):
    """Generator for tree traversal (DFS)"""
    if root is not None:
        yield root.value
        if hasattr(root, 'left'):
            yield from tree_traversal_generator(root.left)
        if hasattr(root, 'right'):
            yield from tree_traversal_generator(root.right)

class TreeNode:
    def __init__(self, value, left=None, right=None):
        self.value = value
        self.left = left
        self.right = right

# Build a simple tree
root = TreeNode(1, TreeNode(2), TreeNode(3, TreeNode(4), TreeNode(5)))

# Usage
for value in tree_traversal_generator(root):
    print(value)  # 1, 2, 3, 4, 5

# =============================================================================
# ITERTOOLS COMBINATIONS WITH GENERATORS
# =============================================================================

import itertools

def fibonacci_pairs():
    """Generate consecutive Fibonacci pairs"""
    fib_gen = fibonacci_generator()
    prev = next(fib_gen)
    
    for current in fib_gen:
        yield (prev, current)
        prev = current

# Usage
fib_pairs = list(itertools.islice(fibonacci_pairs(), 5))
print(fib_pairs)  # [(0, 1), (1, 1), (1, 2), (2, 3), (3, 5)]

def cycle_generator(iterable):
    """Cycle through elements infinitely"""
    saved = []
    for element in iterable:
        yield element
        saved.append(element)
    
    while saved:
        for element in saved:
            yield element

# Usage
colors = ['red', 'green', 'blue']
color_cycle = cycle_generator(colors)
first_10_colors = [next(color_cycle) for _ in range(10)]
print(first_10_colors)  # ['red', 'green', 'blue', 'red', 'green', 'blue', 'red', 'green', 'blue', 'red']

def chain_generators(*generators):
    """Chain multiple generators together"""
    for generator in generators:
        for item in generator:
            yield item

# Usage
gen1 = (x for x in range(3))
gen2 = (x for x in range(3, 6))
gen3 = (x for x in range(6, 9))

chained = chain_generators(gen1, gen2, gen3)
print(list(chained))  # [0, 1, 2, 3, 4, 5, 6, 7, 8]

# =============================================================================
# MEMORY-EFFICIENT DATA PROCESSING
# =============================================================================

def process_large_dataset(filename):
    """Process large dataset without loading into memory"""
    def read_lines():
        with open(filename, 'r') as file:
            for line in file:
                yield line.strip()
    
    def parse_numbers():
        for line in read_lines():
            try:
                yield float(line)
            except ValueError:
                continue  # Skip invalid lines
    
    def filter_positive():
        for number in parse_numbers():
            if number > 0:
                yield number
    
    def compute_squares():
        for number in filter_positive():
            yield number * number
    
    return compute_squares()

def running_average_generator(numbers):
    """Calculate running average without storing all numbers"""
    total = 0
    count = 0
    
    for number in numbers:
        count += 1
        total += number
        yield total / count

# Usage example (conceptual)
# data_stream = process_large_dataset('large_numbers.txt')
# averages = running_average_generator(data_stream)
# for avg in itertools.islice(averages, 10):  # Process first 10
#     print(f"Running average: {avg:.2f}")

def merge_sorted_generators(*generators):
    """Merge multiple sorted generators into one sorted stream"""
    import heapq
    
    # Initialize heap with first element from each generator
    heap = []
    for i, gen in enumerate(generators):
        try:
            first_value = next(gen)
            heapq.heappush(heap, (first_value, i, gen))
        except StopIteration:
            pass
    
    # Merge process
    while heap:
        value, gen_index, gen = heapq.heappop(heap)
        yield value
        
        try:
            next_value = next(gen)
            heapq.heappush(heap, (next_value, gen_index, gen))
        except StopIteration:
            pass

# Usage
gen1 = (x for x in [1, 4, 7, 10])
gen2 = (x for x in [2, 5, 8, 11])
gen3 = (x for x in [3, 6, 9, 12])

merged = merge_sorted_generators(gen1, gen2, gen3)
print(list(merged))  # [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]

# =============================================================================
# ASYNC GENERATORS (PYTHON 3.6+)
# =============================================================================

import asyncio

async def async_counter(max_count):
    """Async generator that yields numbers with delays"""
    count = 0
    while count < max_count:
        await asyncio.sleep(0.1)  # Simulate async operation
        yield count
        count += 1

async def async_fibonacci():
    """Async fibonacci generator"""
    a, b = 0, 1
    while True:
        await asyncio.sleep(0.05)  # Simulate async delay
        yield a
        a, b = b, a + b

# Usage (in async context)
async def demo_async_generators():
    # Async counter
    async for num in async_counter(5):
        print(f"Count: {num}")
    
    # Async fibonacci (first 5 numbers)
    fib_gen = async_fibonacci()
    for _ in range(5):
        value = await fib_gen.__anext__()
        print(f"Fibonacci: {value}")

# To run: asyncio.run(demo_async_generators())

# =============================================================================
# PERFORMANCE CONSIDERATIONS
# =============================================================================

def memory_efficient_sum(numbers):
    """Sum large sequence without storing in memory"""
    return sum(numbers)  # sum() works with any iterable

def memory_efficient_max(numbers):
    """Find max without storing all numbers"""
    max_val = float('-inf')
    for num in numbers:
        if num > max_val:
            max_val = num
    return max_val

# Benchmarking generators vs lists
import time

def benchmark_generators_vs_lists():
    """Compare memory and time performance"""
    
    def list_approach(n):
        return [x * x for x in range(n)]
    
    def generator_approach(n):
        return (x * x for x in range(n))
    
    n = 1000000
    
    # Time list creation
    start = time.time()
    lst = list_approach(n)
    list_time = time.time() - start
    
    # Time generator creation (almost instant)
    start = time.time()
    gen = generator_approach(n)
    gen_time = time.time() - start
    
    print(f"List creation time: {list_time:.4f}s")
    print(f"Generator creation time: {gen_time:.6f}s")
    
    # Memory usage
    print(f"List memory: {sys.getsizeof(lst)} bytes")
    print(f"Generator memory: {sys.getsizeof(gen)} bytes")

# =============================================================================
# PROGRAMMING PATTERNS WITH GENERATORS
# =============================================================================

def two_sum_generator(nums, target):
    """Generate all pairs that sum to target"""
    seen = set()
    for i, num in enumerate(nums):
        complement = target - num
        if complement in seen:
            yield (complement, num)
        seen.add(num)

# Usage
numbers = [2, 7, 11, 15, 3, 4]
pairs = list(two_sum_generator(numbers, 9))
print(pairs)  # [(2, 7), (11, -2), (15, -6), (3, 6)]

def subarray_sum_generator(nums, target):
    """Generate all subarrays with given sum"""
    n = len(nums)
    for i in range(n):
        current_sum = 0
        for j in range(i, n):
            current_sum += nums[j]
            if current_sum == target:
                yield nums[i:j+1]

# Usage
arr = [1, 2, 3, 4, 5]
subarrays = list(subarray_sum_generator(arr, 5))
print(subarrays)  # [[5], [2, 3]]

def permutation_generator(items):
    """Generate all permutations iteratively"""
    if len(items) <= 1:
        yield items
    else:
        for i in range(len(items)):
            for perm in permutation_generator(items[:i] + items[i+1:]):
                yield [items[i]] + perm

# Usage
perms = list(permutation_generator([1, 2, 3]))
print(len(perms))  # 6 permutations

# =============================================================================
# BEST PRACTICES AND PATTERNS
# =============================================================================

"""
Iterator and Generator Best Practices:

1. **Memory Efficiency**: Use generators for large datasets
   ✅ gen = (process(item) for item in large_dataset)
   ❌ lst = [process(item) for item in large_dataset]

2. **Lazy Evaluation**: Only compute values when needed
   - Generators evaluate on-demand
   - Perfect for processing streams or large files

3. **Composition**: Chain generators together
   def pipeline():
       return filter_func(transform_func(source_gen()))

4. **Error Handling**: Use try/except in generators
   def safe_generator():
       try:
           yield risky_operation()
       except SomeError:
           yield default_value

5. **Testing**: Generators are consumed once
   ✅ list(generator()) for testing
   ✅ itertools.tee() for multiple iterations

6. **Performance**: Generators have minimal overhead
   - Faster than creating intermediate lists
   - Use for one-time iteration
   - Consider caching for repeated access

Common Programming Applications:
- File processing without memory constraints
- Stream processing and data pipelines  
- Tree/graph traversal with lazy evaluation
- Infinite sequences (Fibonacci, primes)
- Batch processing of large datasets
- Memory-efficient data transformations

Generator vs Iterator vs Iterable:
- Iterable: Has __iter__() method
- Iterator: Has __iter__() and __next__() methods  
- Generator: Function with yield, automatically creates iterator
"""