# Python Coding Cheat Sheet 🐍

A comprehensive Python reference guide for developers. This repository contains organized, ready-to-reference examples of essential Python concepts, data structures, algorithms, and design patterns commonly used in Python development.

## 📁 Repository Structure

### Core Language Features
- **[01_data_types.py](01_data_types.py)** - Basic data types, type conversion, built-in functions
- **[02_operators.py](02_operators.py)** - Boolean, arithmetic, comparison, bitwise operators
- **[03_control_flow.py](03_control_flow.py)** - If/else, loops, exception handling, comprehensions
- **[04_functions.py](04_functions.py)** - Function definitions, lambdas, decorators, closures

### Data Structures
- **[05_lists_arrays.py](05_lists_arrays.py)** - List operations, comprehensions, common algorithms
- **[06_strings.py](06_strings.py)** - String manipulation, formatting, regular expressions
- **[07_dictionaries.py](07_dictionaries.py)** - Dict operations, defaultdict, Counter, hash tables
- **[08_sets_tuples.py](08_sets_tuples.py)** - Set operations, tuple usage, immutable collections

### Object-Oriented Programming
- **[09_oop_basics.py](09_oop_basics.py)** - Classes, inheritance, polymorphism, special methods
- **[10_design_patterns.py](10_design_patterns.py)** - Common patterns (Singleton, Factory, Observer, etc.)

### Advanced Topics
- **[11_algorithms_complexity.py](11_algorithms_complexity.py)** - Big O notation, sorting, searching algorithms
- **[12_data_structures_advanced.py](12_data_structures_advanced.py)** - Stacks, queues, heaps, trees, graphs
- **[13_recursion.py](13_recursion.py)** - Recursive patterns, memoization, dynamic programming
- **[14_iterators_generators.py](14_iterators_generators.py)** - Iterator protocol, generators, yield

### Concurrency & Performance
- **[17_concurrency.py](17_concurrency.py)** - Threading, multiprocessing, asyncio, locks, GIL

### Programming Essentials
- **[15_common_patterns.py](15_common_patterns.py)** - Two pointers, sliding window, backtracking
- **[16_tips_tricks.py](16_tips_tricks.py)** - Quick solutions, gotchas, best practices

## 🚀 Quick Start

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd PythonCheatSheet
   ```

2. **Browse by topic**: Each file is self-contained with comprehensive examples and explanations.

3. **Run examples**: All code is executable Python - you can run individual files to see examples in action.

## 📖 How to Use This Cheat Sheet

### For Learning and Development
- **Review fundamentals**: Start with `01_data_types.py` through `04_functions.py`
- **Master data structures**: Focus on `05_lists_arrays.py` through `08_sets_tuples.py`
- **Understand OOP**: Study `09_oop_basics.py` and `10_design_patterns.py`
- **Explore advanced topics**: Work through the advanced topics section

### During Development
- **Quick reference**: Use as a reference for syntax and common patterns
- **Pattern recognition**: Find the right approach for your problem
- **Code templates**: Use the examples as starting points for solutions

### Key Features
✅ **Comprehensive coverage** of Python programming topics  
✅ **Executable examples** with clear explanations  
✅ **Common patterns** and useful techniques  
✅ **Performance tips** and best practices  
✅ **Real-world applications** of concepts  

## 🎯 Programming Topics Covered

### Data Structures & Algorithms
- Arrays and Lists (searching, sorting, two pointers)
- Strings (manipulation, pattern matching)
- Hash Tables (dictionaries, sets)
- Stacks and Queues
- Trees and Graphs
- Heaps and Priority Queues

### Programming Concepts
- Object-Oriented Programming
- Functional Programming
- Recursion and Dynamic Programming
- Concurrency and Parallelism (Threading, Multiprocessing, Asyncio)
- Time and Space Complexity Analysis
- Design Patterns

### Python-Specific
- List Comprehensions
- Generators and Iterators
- Decorators and Context Managers
- Exception Handling
- Memory Management
- Global Interpreter Lock (GIL)
- Threading vs Multiprocessing
- Asyncio and Coroutines

## 💡 Key Patterns for Development

### 1. Two Pointers Technique
```python
def two_sum_sorted(arr, target):
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
```

### 2. Sliding Window
```python
def max_sum_subarray(arr, k):
    window_sum = sum(arr[:k])
    max_sum = window_sum
    
    for i in range(1, len(arr) - k + 1):
        window_sum = window_sum - arr[i - 1] + arr[i + k - 1]
        max_sum = max(max_sum, window_sum)
    
    return max_sum
```

### 3. Fast and Slow Pointers (Floyd's Cycle Detection)
```python
def has_cycle(head):
    slow = fast = head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow == fast:
            return True
    return False
```

## 🔧 Python Development Tips

### Best Practices
1. **Write readable code** - be clear and explicit with your syntax
2. **Know the standard library** - collections, itertools, functools
3. **Understand time/space complexity** - be able to analyze your solutions
4. **Use appropriate data structures** - choose the right tool for the job

### During Development
1. **Ask clarifying questions** - understand the requirements fully
2. **Start with a simple solution** - optimize later if needed
3. **Think through your approach** - plan before coding
4. **Test your code** - verify with examples
5. **Consider edge cases** - empty inputs, single elements, etc.

### Common Python Gotchas
- **Mutable default arguments**: Use `None` and create inside function
- **Late binding closures**: Use default arguments to capture variables
- **Integer caching**: Python caches small integers (-5 to 256)
- **List multiplication**: `[[0] * 3] * 3` creates references to same list

## 📚 Additional Resources

### Essential Libraries to Know
- **collections**: defaultdict, Counter, deque
- **itertools**: combinations, permutations, chain
- **functools**: reduce, lru_cache, partial
- **heapq**: heap operations for priority queues
- **bisect**: binary search functions

### Complexity Analysis
| Operation | List | Dict | Set |
|-----------|------|------|-----|
| Access | O(1) | O(1) | - |
| Search | O(n) | O(1) | O(1) |
| Insert | O(1)* | O(1)* | O(1)* |
| Delete | O(n) | O(1)* | O(1)* |

*Average case

## 🤝 Contributing

Feel free to contribute by:
- Adding more examples
- Improving explanations
- Fixing bugs or typos
- Suggesting new patterns or topics

## 📄 License

This project is open source and available under the MIT License.

---

**Happy coding! 🚀**

Remember: Practice makes perfect. The more you work with these patterns, the more natural they'll become in your daily development work.