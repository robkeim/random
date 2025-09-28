"""
Python Algorithms & Complexity Cheat Sheet
==========================================
Big O notation, sorting, searching, and algorithmic analysis
"""

# =============================================================================
# BIG O NOTATION - TIME COMPLEXITY
# =============================================================================

"""
Common Time Complexities (from fastest to slowest):

O(1)        - Constant time
O(log n)    - Logarithmic time
O(n)        - Linear time
O(n log n)  - Linearithmic time
O(n²)       - Quadratic time
O(2^n)      - Exponential time
O(n!)       - Factorial time

Examples:
- O(1): Array access, hash table lookup, stack push/pop
- O(log n): Binary search, balanced tree operations
- O(n): Linear search, array traversal
- O(n log n): Merge sort, heap sort, fast Fourier transform
- O(n²): Bubble sort, insertion sort, selection sort
- O(2^n): Recursive fibonacci, subset generation
- O(n!): Traveling salesman brute force, permutation generation
"""

def constant_time_example(arr, index):
    """O(1) - Constant time"""
    return arr[index]  # Direct array access

def logarithmic_time_example(arr, target):
    """O(log n) - Binary search"""
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

def linear_time_example(arr, target):
    """O(n) - Linear search"""
    for i, value in enumerate(arr):
        if value == target:
            return i
    return -1

def linearithmic_time_example(arr):
    """O(n log n) - Merge sort"""
    if len(arr) <= 1:
        return arr
    
    mid = len(arr) // 2
    left = linearithmic_time_example(arr[:mid])
    right = linearithmic_time_example(arr[mid:])
    
    return merge(left, right)

def merge(left, right):
    """Helper function for merge sort"""
    result = []
    i = j = 0
    
    while i < len(left) and j < len(right):
        if left[i] <= right[j]:
            result.append(left[i])
            i += 1
        else:
            result.append(right[j])
            j += 1
    
    result.extend(left[i:])
    result.extend(right[j:])
    return result

def quadratic_time_example(arr):
    """O(n²) - Bubble sort"""
    n = len(arr)
    arr = arr.copy()  # Don't modify original
    
    for i in range(n):
        for j in range(0, n - i - 1):
            if arr[j] > arr[j + 1]:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
    
    return arr

def exponential_time_example(n):
    """O(2^n) - Naive fibonacci"""
    if n <= 1:
        return n
    return exponential_time_example(n - 1) + exponential_time_example(n - 2)

# =============================================================================
# SPACE COMPLEXITY
# =============================================================================

def constant_space(arr):
    """O(1) space - only uses fixed amount of extra space"""
    total = 0
    for num in arr:
        total += num
    return total

def linear_space(arr):
    """O(n) space - creates copy of input"""
    return arr.copy()

def recursive_space_example(n):
    """O(n) space due to recursion stack"""
    if n <= 0:
        return 0
    return n + recursive_space_example(n - 1)

# =============================================================================
# SORTING ALGORITHMS
# =============================================================================

def bubble_sort(arr):
    """
    Bubble Sort: O(n²) time, O(1) space
    Stable: Yes, In-place: Yes
    """
    arr = arr.copy()
    n = len(arr)
    
    for i in range(n):
        swapped = False
        for j in range(0, n - i - 1):
            if arr[j] > arr[j + 1]:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
                swapped = True
        
        if not swapped:  # Optimization: stop if no swaps
            break
    
    return arr

def selection_sort(arr):
    """
    Selection Sort: O(n²) time, O(1) space
    Stable: No, In-place: Yes
    """
    arr = arr.copy()
    n = len(arr)
    
    for i in range(n):
        min_idx = i
        for j in range(i + 1, n):
            if arr[j] < arr[min_idx]:
                min_idx = j
        
        arr[i], arr[min_idx] = arr[min_idx], arr[i]
    
    return arr

def insertion_sort(arr):
    """
    Insertion Sort: O(n²) time, O(1) space
    Stable: Yes, In-place: Yes
    Best for small arrays or nearly sorted arrays
    """
    arr = arr.copy()
    
    for i in range(1, len(arr)):
        key = arr[i]
        j = i - 1
        
        while j >= 0 and arr[j] > key:
            arr[j + 1] = arr[j]
            j -= 1
        
        arr[j + 1] = key
    
    return arr

def merge_sort(arr):
    """
    Merge Sort: O(n log n) time, O(n) space
    Stable: Yes, In-place: No
    """
    if len(arr) <= 1:
        return arr
    
    mid = len(arr) // 2
    left = merge_sort(arr[:mid])
    right = merge_sort(arr[mid:])
    
    return merge(left, right)

def quick_sort(arr):
    """
    Quick Sort: O(n log n) average, O(n²) worst case time, O(log n) space
    Stable: No, In-place: Yes (with modifications)
    """
    if len(arr) <= 1:
        return arr
    
    pivot = arr[len(arr) // 2]
    left = [x for x in arr if x < pivot]
    middle = [x for x in arr if x == pivot]
    right = [x for x in arr if x > pivot]
    
    return quick_sort(left) + middle + quick_sort(right)

def heap_sort(arr):
    """
    Heap Sort: O(n log n) time, O(1) space
    Stable: No, In-place: Yes
    """
    def heapify(arr, n, i):
        largest = i
        left = 2 * i + 1
        right = 2 * i + 2
        
        if left < n and arr[left] > arr[largest]:
            largest = left
        
        if right < n and arr[right] > arr[largest]:
            largest = right
        
        if largest != i:
            arr[i], arr[largest] = arr[largest], arr[i]
            heapify(arr, n, largest)
    
    arr = arr.copy()
    n = len(arr)
    
    # Build max heap
    for i in range(n // 2 - 1, -1, -1):
        heapify(arr, n, i)
    
    # Extract elements one by one
    for i in range(n - 1, 0, -1):
        arr[0], arr[i] = arr[i], arr[0]
        heapify(arr, i, 0)
    
    return arr

# =============================================================================
# SEARCHING ALGORITHMS
# =============================================================================

def linear_search(arr, target):
    """
    Linear Search: O(n) time, O(1) space
    Works on unsorted arrays
    """
    for i, value in enumerate(arr):
        if value == target:
            return i
    return -1

def binary_search(arr, target):
    """
    Binary Search: O(log n) time, O(1) space
    Requires sorted array
    """
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

def binary_search_recursive(arr, target, left=0, right=None):
    """
    Recursive Binary Search: O(log n) time, O(log n) space
    """
    if right is None:
        right = len(arr) - 1
    
    if left > right:
        return -1
    
    mid = (left + right) // 2
    
    if arr[mid] == target:
        return mid
    elif arr[mid] < target:
        return binary_search_recursive(arr, target, mid + 1, right)
    else:
        return binary_search_recursive(arr, target, left, mid - 1)

def jump_search(arr, target):
    """
    Jump Search: O(√n) time, O(1) space
    Requires sorted array
    """
    import math
    
    n = len(arr)
    step = int(math.sqrt(n))
    prev = 0
    
    # Find block where target may be present
    while arr[min(step, n) - 1] < target:
        prev = step
        step += int(math.sqrt(n))
        if prev >= n:
            return -1
    
    # Linear search in the block
    while arr[prev] < target:
        prev += 1
        if prev == min(step, n):
            return -1
    
    if arr[prev] == target:
        return prev
    
    return -1

def interpolation_search(arr, target):
    """
    Interpolation Search: O(log log n) average, O(n) worst case
    Works best on uniformly distributed sorted arrays
    """
    low = 0
    high = len(arr) - 1
    
    while low <= high and target >= arr[low] and target <= arr[high]:
        if low == high:
            if arr[low] == target:
                return low
            return -1
        
        # Estimate position
        pos = low + int(((float(target - arr[low]) / 
                         (arr[high] - arr[low])) * (high - low)))
        
        if arr[pos] == target:
            return pos
        
        if arr[pos] < target:
            low = pos + 1
        else:
            high = pos - 1
    
    return -1

# =============================================================================
# ALGORITHM ANALYSIS EXAMPLES
# =============================================================================

def analyze_nested_loops():
    """Examples of different complexity patterns"""
    
    # O(n) - Single loop
    def single_loop(n):
        count = 0
        for i in range(n):
            count += 1
        return count
    
    # O(n²) - Nested loops, both depend on n
    def nested_loops_n_squared(n):
        count = 0
        for i in range(n):
            for j in range(n):
                count += 1
        return count
    
    # O(n²) - Triangle pattern
    def triangle_pattern(n):
        count = 0
        for i in range(n):
            for j in range(i):
                count += 1
        return count  # Actually n(n-1)/2, still O(n²)
    
    # O(n log n) - Outer loop n, inner loop log n
    def n_log_n_pattern(n):
        count = 0
        for i in range(n):
            j = 1
            while j < n:
                count += 1
                j *= 2
        return count
    
    # O(log n) - Dividing by 2 each iteration
    def logarithmic_pattern(n):
        count = 0
        while n > 1:
            count += 1
            n //= 2
        return count

def space_complexity_examples():
    """Examples of different space complexities"""
    
    # O(1) space - constant extra space
    def constant_space_sum(arr):
        total = 0
        for num in arr:
            total += num
        return total
    
    # O(n) space - creating new array
    def linear_space_double(arr):
        return [x * 2 for x in arr]
    
    # O(n) space - recursion depth
    def recursive_factorial(n):
        if n <= 1:
            return 1
        return n * recursive_factorial(n - 1)
    
    # O(1) space - iterative version
    def iterative_factorial(n):
        result = 1
        for i in range(2, n + 1):
            result *= i
        return result

# =============================================================================
# AMORTIZED ANALYSIS
# =============================================================================

class DynamicArray:
    """
    Example of amortized analysis
    Append is O(1) amortized, even though sometimes it's O(n)
    """
    def __init__(self):
        self.data = [None] * 1
        self.size = 0
        self.capacity = 1
    
    def append(self, item):
        if self.size == self.capacity:
            # Resize: O(n) operation
            self._resize()
        
        self.data[self.size] = item
        self.size += 1
    
    def _resize(self):
        """Double the capacity"""
        self.capacity *= 2
        new_data = [None] * self.capacity
        
        for i in range(self.size):
            new_data[i] = self.data[i]
        
        self.data = new_data
    
    def get(self, index):
        if 0 <= index < self.size:
            return self.data[index]
        raise IndexError("Index out of range")

# =============================================================================
# COMPLEXITY COMPARISON
# =============================================================================

def compare_sorting_algorithms():
    """
    Sorting Algorithm Comparison:
    
    Algorithm    | Best Case  | Average    | Worst Case | Space    | Stable
    -------------|------------|------------|------------|----------|--------
    Bubble Sort  | O(n)       | O(n²)      | O(n²)      | O(1)     | Yes
    Selection    | O(n²)      | O(n²)      | O(n²)      | O(1)     | No
    Insertion    | O(n)       | O(n²)      | O(n²)      | O(1)     | Yes
    Merge Sort   | O(n log n) | O(n log n) | O(n log n) | O(n)     | Yes
    Quick Sort   | O(n log n) | O(n log n) | O(n²)      | O(log n) | No
    Heap Sort    | O(n log n) | O(n log n) | O(n log n) | O(1)     | No
    
    When to use:
    - Bubble/Selection/Insertion: Small arrays (< 50 elements)
    - Merge Sort: When stability is required
    - Quick Sort: General purpose (fastest on average)
    - Heap Sort: When consistent O(n log n) is needed
    """
    pass

def data_structure_complexities():
    """
    Data Structure Time Complexities:
    
    Structure    | Access | Search | Insert | Delete | Space
    -------------|--------|--------|--------|--------|-------
    Array        | O(1)   | O(n)   | O(n)   | O(n)   | O(n)
    Stack        | O(n)   | O(n)   | O(1)   | O(1)   | O(n)
    Queue        | O(n)   | O(n)   | O(1)   | O(1)   | O(n)
    Linked List  | O(n)   | O(n)   | O(1)   | O(1)   | O(n)
    Hash Table   | N/A    | O(1)   | O(1)   | O(1)   | O(n)
    Binary Tree  | O(n)   | O(n)   | O(n)   | O(n)   | O(n)
    BST          | O(log n)| O(log n)| O(log n)| O(log n)| O(n)
    AVL Tree     | O(log n)| O(log n)| O(log n)| O(log n)| O(n)
    B-Tree       | O(log n)| O(log n)| O(log n)| O(log n)| O(n)
    Red-Black    | O(log n)| O(log n)| O(log n)| O(log n)| O(n)
    Heap         | O(1)   | O(n)   | O(log n)| O(log n)| O(n)
    Trie         | O(m)   | O(m)   | O(m)   | O(m)   | O(ALPHABET_SIZE * N * M)
    
    Where:
    - n = number of elements
    - m = length of string (for Trie)
    """
    pass

# =============================================================================
# PRACTICAL COMPLEXITY ANALYSIS
# =============================================================================

def practical_examples():
    """Real-world complexity analysis examples"""
    
    # Example 1: Finding duplicates
    def has_duplicates_naive(arr):
        """O(n²) - compare every pair"""
        for i in range(len(arr)):
            for j in range(i + 1, len(arr)):
                if arr[i] == arr[j]:
                    return True
        return False
    
    def has_duplicates_optimized(arr):
        """O(n) - use set for O(1) lookups"""
        seen = set()
        for item in arr:
            if item in seen:
                return True
            seen.add(item)
        return False
    
    # Example 2: Two sum problem
    def two_sum_naive(arr, target):
        """O(n²) - brute force"""
        for i in range(len(arr)):
            for j in range(i + 1, len(arr)):
                if arr[i] + arr[j] == target:
                    return [i, j]
        return []
    
    def two_sum_optimized(arr, target):
        """O(n) - use hash map"""
        num_to_index = {}
        for i, num in enumerate(arr):
            complement = target - num
            if complement in num_to_index:
                return [num_to_index[complement], i]
            num_to_index[num] = i
        return []

# =============================================================================
# TIPS FOR COMPLEXITY ANALYSIS
# =============================================================================

"""
Rules for Analyzing Time Complexity:

1. Drop constants: O(2n) → O(n)
2. Drop non-dominant terms: O(n² + n) → O(n²)
3. Different inputs use different variables: O(a + b), not O(n)
4. Nested loops usually multiply complexities
5. Sequential operations add complexities

Common Patterns:
- If you can divide the problem in half: O(log n)
- If you process each element once: O(n)
- If you process each pair: O(n²)
- If you make recursive calls on subproblems: analyze recursion depth

Space Complexity:
- Input space doesn't count (unless specified)
- Count auxiliary space used by algorithm
- Recursion uses O(depth) space for call stack

Best Practices:
1. Always consider worst-case unless specified
2. Consider both time and space complexity
3. Real constants matter in practice
4. Profile your code for actual performance
5. Consider average case for practical applications
"""

# =============================================================================
# COMPLEXITY QUIZ EXAMPLES
# =============================================================================

def complexity_quiz():
    """Test your understanding"""
    
    # What's the complexity?
    def mystery1(n):
        for i in range(n):
            for j in range(i):
                print(f"{i}, {j}")
    # Answer: O(n²) - triangle pattern, still quadratic
    
    def mystery2(arr):
        n = len(arr)
        for i in range(n):
            j = i
            while j > 0 and arr[j] < arr[j-1]:
                arr[j], arr[j-1] = arr[j-1], arr[j]
                j -= 1
    # Answer: O(n²) - this is insertion sort
    
    def mystery3(n):
        if n <= 1:
            return n
        return mystery3(n-1) + mystery3(n-2)
    # Answer: O(2^n) - naive fibonacci
    
    def mystery4(arr, target):
        for i in range(len(arr)):
            if arr[i] == target:
                return i
        return -1
    # Answer: O(n) - linear search
    
    def mystery5(n):
        count = 0
        i = 1
        while i < n:
            count += 1
            i *= 2
        return count
    # Answer: O(log n) - doubling pattern