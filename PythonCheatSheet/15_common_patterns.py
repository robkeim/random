"""
Python Common Programming Patterns Cheat Sheet
==============================================
Essential algorithmic patterns frequently used in programming
"""

# =============================================================================
# TWO POINTERS TECHNIQUE
# =============================================================================

def two_sum_sorted(arr, target):
    """
    Find two numbers in sorted array that add up to target
    Time: O(n), Space: O(1)
    """
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

def remove_duplicates_sorted(arr):
    """
    Remove duplicates from sorted array in-place
    Time: O(n), Space: O(1)
    """
    if not arr:
        return 0
    
    write_index = 1
    for read_index in range(1, len(arr)):
        if arr[read_index] != arr[read_index - 1]:
            arr[write_index] = arr[read_index]
            write_index += 1
    
    return write_index

def three_sum(nums):
    """
    Find all unique triplets that sum to zero
    Time: O(n²), Space: O(1)
    """
    nums.sort()
    result = []
    
    for i in range(len(nums) - 2):
        # Skip duplicates for first number
        if i > 0 and nums[i] == nums[i - 1]:
            continue
            
        left, right = i + 1, len(nums) - 1
        
        while left < right:
            total = nums[i] + nums[left] + nums[right]
            
            if total == 0:
                result.append([nums[i], nums[left], nums[right]])
                
                # Skip duplicates
                while left < right and nums[left] == nums[left + 1]:
                    left += 1
                while left < right and nums[right] == nums[right - 1]:
                    right -= 1
                
                left += 1
                right -= 1
            elif total < 0:
                left += 1
            else:
                right -= 1
    
    return result

# =============================================================================
# SLIDING WINDOW TECHNIQUE
# =============================================================================

def max_sum_subarray_size_k(arr, k):
    """
    Find maximum sum of subarray of size k
    Time: O(n), Space: O(1)
    """
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

def longest_substring_without_repeating(s):
    """
    Find length of longest substring without repeating characters
    Time: O(n), Space: O(min(m,n)) where m is charset size
    """
    char_index = {}
    max_length = 0
    start = 0
    
    for end, char in enumerate(s):
        if char in char_index and char_index[char] >= start:
            start = char_index[char] + 1
        
        char_index[char] = end
        max_length = max(max_length, end - start + 1)
    
    return max_length

def min_window_substring(s, t):
    """
    Find minimum window substring containing all characters of t
    Time: O(|s| + |t|), Space: O(|s| + |t|)
    """
    if not s or not t:
        return ""
    
    # Count characters in t
    dict_t = {}
    for char in t:
        dict_t[char] = dict_t.get(char, 0) + 1
    
    required = len(dict_t)
    left, right = 0, 0
    formed = 0
    window_counts = {}
    
    # Answer tuple: (window length, left, right)
    ans = float('inf'), None, None
    
    while right < len(s):
        # Add character from right to window
        char = s[right]
        window_counts[char] = window_counts.get(char, 0) + 1
        
        if char in dict_t and window_counts[char] == dict_t[char]:
            formed += 1
        
        # Contract window until it's no longer valid
        while left <= right and formed == required:
            char = s[left]
            
            # Save smallest window
            if right - left + 1 < ans[0]:
                ans = (right - left + 1, left, right)
            
            # Remove leftmost character
            window_counts[char] -= 1
            if char in dict_t and window_counts[char] < dict_t[char]:
                formed -= 1
            
            left += 1
        
        right += 1
    
    return "" if ans[0] == float('inf') else s[ans[1]:ans[2] + 1]

# =============================================================================
# FAST AND SLOW POINTERS (Floyd's Cycle Detection)
# =============================================================================

class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

def has_cycle(head):
    """
    Detect if linked list has a cycle
    Time: O(n), Space: O(1)
    """
    if not head or not head.next:
        return False
    
    slow = fast = head
    
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        
        if slow == fast:
            return True
    
    return False

def find_cycle_start(head):
    """
    Find the node where cycle begins
    Time: O(n), Space: O(1)
    """
    if not head or not head.next:
        return None
    
    # Find meeting point
    slow = fast = head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow == fast:
            break
    
    if not fast or not fast.next:
        return None  # No cycle
    
    # Find start of cycle
    start = head
    while start != slow:
        start = start.next
        slow = slow.next
    
    return start

def find_middle_node(head):
    """
    Find middle node of linked list
    Time: O(n), Space: O(1)
    """
    if not head:
        return None
    
    slow = fast = head
    
    while fast.next and fast.next.next:
        slow = slow.next
        fast = fast.next.next
    
    return slow

# =============================================================================
# BACKTRACKING
# =============================================================================

def generate_parentheses(n):
    """
    Generate all combinations of well-formed parentheses
    Time: O(4^n / √n), Space: O(4^n / √n)
    """
    def backtrack(current, open_count, close_count):
        if len(current) == 2 * n:
            result.append(current)
            return
        
        if open_count < n:
            backtrack(current + "(", open_count + 1, close_count)
        
        if close_count < open_count:
            backtrack(current + ")", open_count, close_count + 1)
    
    result = []
    backtrack("", 0, 0)
    return result

def permute(nums):
    """
    Generate all permutations of array
    Time: O(n × n!), Space: O(n!)
    """
    def backtrack(current_permutation):
        if len(current_permutation) == len(nums):
            result.append(current_permutation[:])
            return
        
        for num in nums:
            if num not in current_permutation:
                current_permutation.append(num)
                backtrack(current_permutation)
                current_permutation.pop()
    
    result = []
    backtrack([])
    return result

def subsets(nums):
    """
    Generate all possible subsets
    Time: O(n × 2^n), Space: O(n × 2^n)
    """
    def backtrack(start, current_subset):
        result.append(current_subset[:])
        
        for i in range(start, len(nums)):
            current_subset.append(nums[i])
            backtrack(i + 1, current_subset)
            current_subset.pop()
    
    result = []
    backtrack(0, [])
    return result

def word_search(board, word):
    """
    Search for word in 2D board
    Time: O(N × 4^L) where N is cells, L is word length
    Space: O(L)
    """
    def backtrack(row, col, index):
        if index == len(word):
            return True
        
        if (row < 0 or row >= len(board) or 
            col < 0 or col >= len(board[0]) or 
            board[row][col] != word[index]):
            return False
        
        # Mark as visited
        temp = board[row][col]
        board[row][col] = '#'
        
        # Explore all directions
        found = (backtrack(row + 1, col, index + 1) or
                backtrack(row - 1, col, index + 1) or
                backtrack(row, col + 1, index + 1) or
                backtrack(row, col - 1, index + 1))
        
        # Restore original value
        board[row][col] = temp
        
        return found
    
    for i in range(len(board)):
        for j in range(len(board[0])):
            if backtrack(i, j, 0):
                return True
    
    return False

# =============================================================================
# BINARY SEARCH PATTERNS
# =============================================================================

def binary_search(arr, target):
    """
    Standard binary search
    Time: O(log n), Space: O(1)
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

def find_first_occurrence(arr, target):
    """
    Find first occurrence of target in sorted array with duplicates
    Time: O(log n), Space: O(1)
    """
    left, right = 0, len(arr) - 1
    result = -1
    
    while left <= right:
        mid = (left + right) // 2
        
        if arr[mid] == target:
            result = mid
            right = mid - 1  # Continue searching left
        elif arr[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    
    return result

def search_rotated_array(nums, target):
    """
    Search in rotated sorted array
    Time: O(log n), Space: O(1)
    """
    left, right = 0, len(nums) - 1
    
    while left <= right:
        mid = (left + right) // 2
        
        if nums[mid] == target:
            return mid
        
        # Check which half is sorted
        if nums[left] <= nums[mid]:  # Left half is sorted
            if nums[left] <= target < nums[mid]:
                right = mid - 1
            else:
                left = mid + 1
        else:  # Right half is sorted
            if nums[mid] < target <= nums[right]:
                left = mid + 1
            else:
                right = mid - 1
    
    return -1

# =============================================================================
# DYNAMIC PROGRAMMING PATTERNS
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

def coin_change(coins, amount):
    """
    Minimum coins needed to make amount
    Time: O(amount × len(coins)), Space: O(amount)
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
    Length of longest increasing subsequence
    Time: O(n²), Space: O(n)
    """
    if not nums:
        return 0
    
    dp = [1] * len(nums)
    
    for i in range(1, len(nums)):
        for j in range(i):
            if nums[j] < nums[i]:
                dp[i] = max(dp[i], dp[j] + 1)
    
    return max(dp)

# =============================================================================
# TREE TRAVERSAL PATTERNS
# =============================================================================

class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

def inorder_traversal(root):
    """Inorder: left -> root -> right"""
    result = []
    
    def inorder(node):
        if node:
            inorder(node.left)
            result.append(node.val)
            inorder(node.right)
    
    inorder(root)
    return result

def level_order_traversal(root):
    """Level order (BFS) traversal"""
    if not root:
        return []
    
    from collections import deque
    queue = deque([root])
    result = []
    
    while queue:
        level_size = len(queue)
        level_values = []
        
        for _ in range(level_size):
            node = queue.popleft()
            level_values.append(node.val)
            
            if node.left:
                queue.append(node.left)
            if node.right:
                queue.append(node.right)
        
        result.append(level_values)
    
    return result

def validate_bst(root):
    """Check if binary tree is valid BST"""
    def validate(node, min_val, max_val):
        if not node:
            return True
        
        if node.val <= min_val or node.val >= max_val:
            return False
        
        return (validate(node.left, min_val, node.val) and
                validate(node.right, node.val, max_val))
    
    return validate(root, float('-inf'), float('inf'))

# =============================================================================
# QUICK REFERENCE
# =============================================================================

"""
Pattern Recognition Guide:

🎯 Two Pointers:
- Array is sorted
- Need to find pair/triplet with specific sum
- Remove duplicates in-place
- Reverse/rotate operations

🎯 Sliding Window:
- Find optimal subarray/substring
- All subarrays of size k
- Longest/shortest substring with condition

🎯 Fast & Slow Pointers:
- Cycle detection in linked list
- Find middle of linked list
- Palindrome linked list

🎯 Backtracking:
- Generate all combinations/permutations
- Solve puzzles (N-Queens, Sudoku)
- Path finding in grid/tree

🎯 Binary Search:
- Sorted array search
- Find first/last occurrence
- Search in rotated array
- Peak finding

🎯 Dynamic Programming:
- Optimal substructure
- Overlapping subproblems
- Min/max optimization
- Counting problems
"""