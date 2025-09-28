"""
Python Recursion Cheat Sheet
===========================
Recursive patterns, memoization, and dynamic programming
"""

# =============================================================================
# BASIC RECURSION CONCEPTS
# =============================================================================

def factorial(n):
    """
    Base case: n <= 1
    Recursive case: n * factorial(n-1)
    Time: O(n), Space: O(n) due to call stack
    """
    if n <= 1:  # Base case
        return 1
    return n * factorial(n - 1)  # Recursive case

def fibonacci_naive(n):
    """
    Naive fibonacci - exponential time complexity
    Time: O(2^n), Space: O(n)
    """
    if n <= 1:
        return n
    return fibonacci_naive(n - 1) + fibonacci_naive(n - 2)

def power(base, exp):
    """
    Calculate base^exp recursively
    Time: O(exp), Space: O(exp)
    """
    if exp == 0:
        return 1
    if exp == 1:
        return base
    return base * power(base, exp - 1)

def power_optimized(base, exp):
    """
    Fast exponentiation using divide and conquer
    Time: O(log exp), Space: O(log exp)
    """
    if exp == 0:
        return 1
    if exp == 1:
        return base
    
    if exp % 2 == 0:
        half_power = power_optimized(base, exp // 2)
        return half_power * half_power
    else:
        return base * power_optimized(base, exp - 1)

# =============================================================================
# MEMOIZATION (TOP-DOWN DYNAMIC PROGRAMMING)
# =============================================================================

def fibonacci_memo(n, memo={}):
    """
    Fibonacci with memoization
    Time: O(n), Space: O(n)
    """
    if n in memo:
        return memo[n]
    
    if n <= 1:
        return n
    
    memo[n] = fibonacci_memo(n - 1, memo) + fibonacci_memo(n - 2, memo)
    return memo[n]

# Using functools.lru_cache decorator
from functools import lru_cache

@lru_cache(maxsize=None)
def fibonacci_cached(n):
    """
    Fibonacci with automatic memoization
    Time: O(n), Space: O(n)
    """
    if n <= 1:
        return n
    return fibonacci_cached(n - 1) + fibonacci_cached(n - 2)

def climbing_stairs_memo(n, memo={}):
    """
    Ways to climb n stairs (can take 1 or 2 steps at a time)
    Same as fibonacci sequence
    Time: O(n), Space: O(n)
    """
    if n in memo:
        return memo[n]
    
    if n <= 2:
        return n
    
    memo[n] = climbing_stairs_memo(n - 1, memo) + climbing_stairs_memo(n - 2, memo)
    return memo[n]

def coin_change_memo(coins, amount, memo={}):
    """
    Minimum coins needed to make amount
    Time: O(amount * len(coins)), Space: O(amount)
    """
    if amount in memo:
        return memo[amount]
    
    if amount == 0:
        return 0
    if amount < 0:
        return -1
    
    min_coins = float('inf')
    
    for coin in coins:
        result = coin_change_memo(coins, amount - coin, memo)
        if result != -1:
            min_coins = min(min_coins, result + 1)
    
    memo[amount] = min_coins if min_coins != float('inf') else -1
    return memo[amount]

# =============================================================================
# DYNAMIC PROGRAMMING (BOTTOM-UP)
# =============================================================================

def fibonacci_dp(n):
    """
    Bottom-up fibonacci
    Time: O(n), Space: O(n)
    """
    if n <= 1:
        return n
    
    dp = [0] * (n + 1)
    dp[1] = 1
    
    for i in range(2, n + 1):
        dp[i] = dp[i - 1] + dp[i - 2]
    
    return dp[n]

def fibonacci_dp_optimized(n):
    """
    Space-optimized fibonacci
    Time: O(n), Space: O(1)
    """
    if n <= 1:
        return n
    
    a, b = 0, 1
    
    for _ in range(2, n + 1):
        a, b = b, a + b
    
    return b

def climbing_stairs_dp(n):
    """
    Bottom-up approach for climbing stairs
    Time: O(n), Space: O(1)
    """
    if n <= 2:
        return n
    
    prev2, prev1 = 1, 2
    
    for i in range(3, n + 1):
        current = prev1 + prev2
        prev2, prev1 = prev1, current
    
    return prev1

def coin_change_dp(coins, amount):
    """
    Bottom-up coin change
    Time: O(amount * len(coins)), Space: O(amount)
    """
    dp = [float('inf')] * (amount + 1)
    dp[0] = 0
    
    for i in range(1, amount + 1):
        for coin in coins:
            if coin <= i:
                dp[i] = min(dp[i], dp[i - coin] + 1)
    
    return dp[amount] if dp[amount] != float('inf') else -1

def longest_increasing_subsequence(nums):
    """
    Find length of longest increasing subsequence
    Time: O(n²), Space: O(n)
    """
    if not nums:
        return 0
    
    n = len(nums)
    dp = [1] * n  # dp[i] = length of LIS ending at index i
    
    for i in range(1, n):
        for j in range(i):
            if nums[j] < nums[i]:
                dp[i] = max(dp[i], dp[j] + 1)
    
    return max(dp)

# =============================================================================
# TREE RECURSION
# =============================================================================

class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

def max_depth(root):
    """
    Maximum depth of binary tree
    Time: O(n), Space: O(h) where h is height
    """
    if not root:
        return 0
    
    left_depth = max_depth(root.left)
    right_depth = max_depth(root.right)
    
    return 1 + max(left_depth, right_depth)

def is_same_tree(p, q):
    """
    Check if two binary trees are the same
    Time: O(n), Space: O(h)
    """
    if not p and not q:
        return True
    
    if not p or not q:
        return False
    
    return (p.val == q.val and 
            is_same_tree(p.left, q.left) and 
            is_same_tree(p.right, q.right))

def invert_binary_tree(root):
    """
    Invert/flip a binary tree
    Time: O(n), Space: O(h)
    """
    if not root:
        return None
    
    # Swap left and right subtrees
    root.left, root.right = root.right, root.left
    
    # Recursively invert subtrees
    invert_binary_tree(root.left)
    invert_binary_tree(root.right)
    
    return root

def path_sum(root, target_sum):
    """
    Check if tree has root-to-leaf path with given sum
    Time: O(n), Space: O(h)
    """
    if not root:
        return False
    
    # If leaf node, check if remaining sum equals node value
    if not root.left and not root.right:
        return target_sum == root.val
    
    # Check left and right subtrees with reduced sum
    remaining_sum = target_sum - root.val
    return (path_sum(root.left, remaining_sum) or 
            path_sum(root.right, remaining_sum))

def diameter_of_tree(root):
    """
    Find diameter of binary tree (longest path between any two nodes)
    Time: O(n), Space: O(h)
    """
    def height_and_diameter(node):
        if not node:
            return 0, 0  # height, diameter
        
        left_height, left_diameter = height_and_diameter(node.left)
        right_height, right_diameter = height_and_diameter(node.right)
        
        current_height = 1 + max(left_height, right_height)
        current_diameter = max(
            left_diameter,
            right_diameter,
            left_height + right_height  # Path through current node
        )
        
        return current_height, current_diameter
    
    _, diameter = height_and_diameter(root)
    return diameter

# =============================================================================
# BACKTRACKING (RECURSIVE EXPLORATION)
# =============================================================================

def generate_parentheses(n):
    """
    Generate all combinations of well-formed parentheses
    Time: O(4^n / √n), Space: O(4^n / √n)
    """
    def backtrack(current, open_count, close_count):
        # Base case: we've used all n pairs
        if len(current) == 2 * n:
            result.append(current)
            return
        
        # Add opening parenthesis if we haven't used all n
        if open_count < n:
            backtrack(current + "(", open_count + 1, close_count)
        
        # Add closing parenthesis if it won't make string invalid
        if close_count < open_count:
            backtrack(current + ")", open_count, close_count + 1)
    
    result = []
    backtrack("", 0, 0)
    return result

def permutations(nums):
    """
    Generate all permutations of array
    Time: O(n × n!), Space: O(n!)
    """
    def backtrack(current_perm):
        # Base case: permutation is complete
        if len(current_perm) == len(nums):
            result.append(current_perm[:])  # Make a copy
            return
        
        # Try each remaining number
        for num in nums:
            if num not in current_perm:
                current_perm.append(num)
                backtrack(current_perm)
                current_perm.pop()  # Backtrack
    
    result = []
    backtrack([])
    return result

def combinations(n, k):
    """
    Generate all combinations of k numbers from 1 to n
    Time: O(C(n,k)), Space: O(C(n,k))
    """
    def backtrack(start, current_combo):
        # Base case: combination is complete
        if len(current_combo) == k:
            result.append(current_combo[:])
            return
        
        # Try numbers from start to n
        for i in range(start, n + 1):
            current_combo.append(i)
            backtrack(i + 1, current_combo)
            current_combo.pop()  # Backtrack
    
    result = []
    backtrack(1, [])
    return result

def subsets(nums):
    """
    Generate all possible subsets
    Time: O(n × 2^n), Space: O(n × 2^n)
    """
    def backtrack(start, current_subset):
        # Add current subset to result
        result.append(current_subset[:])
        
        # Try adding each remaining element
        for i in range(start, len(nums)):
            current_subset.append(nums[i])
            backtrack(i + 1, current_subset)
            current_subset.pop()  # Backtrack
    
    result = []
    backtrack(0, [])
    return result

def n_queens(n):
    """
    Solve N-Queens problem
    Time: O(n!), Space: O(n²)
    """
    def is_safe(board, row, col):
        # Check column
        for i in range(row):
            if board[i][col] == 'Q':
                return False
        
        # Check diagonal (top-left to bottom-right)
        for i, j in zip(range(row - 1, -1, -1), range(col - 1, -1, -1)):
            if board[i][j] == 'Q':
                return False
        
        # Check diagonal (top-right to bottom-left)
        for i, j in zip(range(row - 1, -1, -1), range(col + 1, n)):
            if board[i][j] == 'Q':
                return False
        
        return True
    
    def solve(board, row):
        # Base case: all queens placed
        if row == n:
            result.append([''.join(row) for row in board])
            return
        
        # Try placing queen in each column of current row
        for col in range(n):
            if is_safe(board, row, col):
                board[row][col] = 'Q'
                solve(board, row + 1)
                board[row][col] = '.'  # Backtrack
    
    result = []
    board = [['.' for _ in range(n)] for _ in range(n)]
    solve(board, 0)
    return result

# =============================================================================
# STRING RECURSION
# =============================================================================

def reverse_string_recursive(s):
    """
    Reverse string recursively
    Time: O(n), Space: O(n)
    """
    if len(s) <= 1:
        return s
    
    return s[-1] + reverse_string_recursive(s[:-1])

def is_palindrome_recursive(s):
    """
    Check if string is palindrome recursively
    Time: O(n), Space: O(n)
    """
    # Remove non-alphanumeric and convert to lowercase
    clean_s = ''.join(char.lower() for char in s if char.isalnum())
    
    def check_palindrome(string, left, right):
        if left >= right:
            return True
        
        if string[left] != string[right]:
            return False
        
        return check_palindrome(string, left + 1, right - 1)
    
    return check_palindrome(clean_s, 0, len(clean_s) - 1)

def string_permutations(s):
    """
    Generate all permutations of string
    Time: O(n × n!), Space: O(n!)
    """
    def backtrack(current, remaining):
        if not remaining:
            result.append(current)
            return
        
        for i, char in enumerate(remaining):
            backtrack(current + char, remaining[:i] + remaining[i+1:])
    
    result = []
    backtrack("", s)
    return result

# =============================================================================
# DIVIDE AND CONQUER
# =============================================================================

def merge_sort(arr):
    """
    Merge sort using divide and conquer
    Time: O(n log n), Space: O(n)
    """
    if len(arr) <= 1:
        return arr
    
    # Divide
    mid = len(arr) // 2
    left = merge_sort(arr[:mid])
    right = merge_sort(arr[mid:])
    
    # Conquer (merge)
    return merge(left, right)

def merge(left, right):
    """Helper function to merge two sorted arrays"""
    result = []
    i = j = 0
    
    while i < len(left) and j < len(right):
        if left[i] <= right[j]:
            result.append(left[i])
            i += 1
        else:
            result.append(right[j])
            j += 1
    
    # Add remaining elements
    result.extend(left[i:])
    result.extend(right[j:])
    
    return result

def quick_sort(arr):
    """
    Quick sort using divide and conquer
    Time: O(n log n) average, O(n²) worst, Space: O(log n)
    """
    if len(arr) <= 1:
        return arr
    
    pivot = arr[len(arr) // 2]
    left = [x for x in arr if x < pivot]
    middle = [x for x in arr if x == pivot]
    right = [x for x in arr if x > pivot]
    
    return quick_sort(left) + middle + quick_sort(right)

def find_maximum_subarray(arr):
    """
    Find maximum sum subarray using divide and conquer
    Time: O(n log n), Space: O(log n)
    """
    def max_crossing_sum(arr, left, mid, right):
        # Maximum sum ending at mid (going left)
        left_sum = float('-inf')
        current_sum = 0
        for i in range(mid, left - 1, -1):
            current_sum += arr[i]
            left_sum = max(left_sum, current_sum)
        
        # Maximum sum starting at mid+1 (going right)
        right_sum = float('-inf')
        current_sum = 0
        for i in range(mid + 1, right + 1):
            current_sum += arr[i]
            right_sum = max(right_sum, current_sum)
        
        return left_sum + right_sum
    
    def max_subarray_recursive(arr, left, right):
        if left == right:
            return arr[left]
        
        mid = (left + right) // 2
        
        left_max = max_subarray_recursive(arr, left, mid)
        right_max = max_subarray_recursive(arr, mid + 1, right)
        cross_max = max_crossing_sum(arr, left, mid, right)
        
        return max(left_max, right_max, cross_max)
    
    if not arr:
        return 0
    
    return max_subarray_recursive(arr, 0, len(arr) - 1)

# =============================================================================
# TAIL RECURSION OPTIMIZATION
# =============================================================================

def factorial_tail_recursive(n, accumulator=1):
    """
    Tail recursive factorial (Python doesn't optimize this)
    Time: O(n), Space: O(n) in Python, O(1) in languages with TCO
    """
    if n <= 1:
        return accumulator
    return factorial_tail_recursive(n - 1, n * accumulator)

def fibonacci_tail_recursive(n, a=0, b=1):
    """
    Tail recursive fibonacci
    Time: O(n), Space: O(n) in Python
    """
    if n == 0:
        return a
    if n == 1:
        return b
    return fibonacci_tail_recursive(n - 1, b, a + b)

# =============================================================================
# RECURSION PATTERNS FOR PROGRAMMING
# =============================================================================

"""
Common Recursion Patterns:

1. **Linear Recursion**: Function calls itself once
   - Examples: factorial, tree traversal, linked list operations
   - Template: if base_case: return; return f(smaller_problem)

2. **Binary Recursion**: Function calls itself twice
   - Examples: fibonacci, tree problems, divide and conquer
   - Template: if base_case: return; return combine(f(left), f(right))

3. **Tail Recursion**: Recursive call is the last operation
   - Can be optimized to iteration by compilers
   - Template: if base_case: return result; return f(params, updated_accumulator)

4. **Mutual Recursion**: Functions call each other
   - Examples: parsing problems, state machines

5. **Backtracking**: Build solution incrementally, undo choices
   - Examples: permutations, combinations, sudoku, n-queens
   - Template: if complete: add to result; for choice in choices: make_choice; recurse; undo_choice

When to Use Recursion:
✅ Problem has recursive structure (trees, graphs)
✅ Problem can be broken into similar subproblems
✅ Base case is clearly defined
✅ Backtracking is needed

When NOT to Use Recursion:
❌ Simple iteration would be clearer
❌ Risk of stack overflow with deep recursion
❌ Overlapping subproblems without memoization
❌ Performance is critical and iteration is faster

Recursion vs Iteration:
- Recursion: Often cleaner for tree/graph problems
- Iteration: Usually more efficient, avoids stack overflow
- Dynamic Programming: Use when subproblems overlap
"""