"""
Python Advanced Data Structures Cheat Sheet
===========================================
Stacks, queues, heaps, trees, graphs, and advanced collections
"""

# =============================================================================
# STACKS (LIFO - Last In, First Out)
# =============================================================================

# Using list as stack
class ListStack:
    def __init__(self):
        self.items = []
    
    def push(self, item):
        self.items.append(item)  # O(1)
    
    def pop(self):
        if self.is_empty():
            raise IndexError("Stack is empty")
        return self.items.pop()  # O(1)
    
    def peek(self):
        if self.is_empty():
            raise IndexError("Stack is empty")
        return self.items[-1]  # O(1)
    
    def is_empty(self):
        return len(self.items) == 0
    
    def size(self):
        return len(self.items)

# Stack applications
def balanced_parentheses(s):
    """Check if parentheses are balanced using stack"""
    stack = []
    mapping = {')': '(', '}': '{', ']': '['}
    
    for char in s:
        if char in mapping:  # Closing bracket
            if not stack or stack.pop() != mapping[char]:
                return False
        else:  # Opening bracket or other character
            if char in '({[':
                stack.append(char)
    
    return len(stack) == 0

def evaluate_postfix(expression):
    """Evaluate postfix expression using stack"""
    stack = []
    operators = {'+', '-', '*', '/'}
    
    for token in expression.split():
        if token in operators:
            if len(stack) < 2:
                raise ValueError("Invalid expression")
            
            b = stack.pop()
            a = stack.pop()
            
            if token == '+':
                result = a + b
            elif token == '-':
                result = a - b
            elif token == '*':
                result = a * b
            elif token == '/':
                result = a / b
            
            stack.append(result)
        else:
            stack.append(float(token))
    
    if len(stack) != 1:
        raise ValueError("Invalid expression")
    
    return stack[0]

# =============================================================================
# QUEUES (FIFO - First In, First Out)
# =============================================================================

from collections import deque

class Queue:
    def __init__(self):
        self.items = deque()  # More efficient than list for queue operations
    
    def enqueue(self, item):
        self.items.append(item)  # O(1)
    
    def dequeue(self):
        if self.is_empty():
            raise IndexError("Queue is empty")
        return self.items.popleft()  # O(1)
    
    def front(self):
        if self.is_empty():
            raise IndexError("Queue is empty")
        return self.items[0]
    
    def is_empty(self):
        return len(self.items) == 0
    
    def size(self):
        return len(self.items)

# Circular queue implementation
class CircularQueue:
    def __init__(self, capacity):
        self.capacity = capacity
        self.queue = [None] * capacity
        self.front_idx = 0
        self.rear_idx = -1
        self.count = 0
    
    def enqueue(self, item):
        if self.is_full():
            raise OverflowError("Queue is full")
        
        self.rear_idx = (self.rear_idx + 1) % self.capacity
        self.queue[self.rear_idx] = item
        self.count += 1
    
    def dequeue(self):
        if self.is_empty():
            raise IndexError("Queue is empty")
        
        item = self.queue[self.front_idx]
        self.queue[self.front_idx] = None
        self.front_idx = (self.front_idx + 1) % self.capacity
        self.count -= 1
        return item
    
    def is_empty(self):
        return self.count == 0
    
    def is_full(self):
        return self.count == self.capacity

# Priority Queue using heapq
import heapq

class PriorityQueue:
    def __init__(self):
        self.heap = []
        self.index = 0  # To handle tie-breaking
    
    def push(self, item, priority):
        # Python's heapq is a min-heap, so negate priority for max-heap behavior
        heapq.heappush(self.heap, (priority, self.index, item))
        self.index += 1
    
    def pop(self):
        if not self.heap:
            raise IndexError("Priority queue is empty")
        
        priority, _, item = heapq.heappop(self.heap)
        return item, priority
    
    def peek(self):
        if not self.heap:
            raise IndexError("Priority queue is empty")
        
        priority, _, item = self.heap[0]
        return item, priority
    
    def is_empty(self):
        return len(self.heap) == 0

# =============================================================================
# HEAPS (BINARY HEAP)
# =============================================================================

class MinHeap:
    def __init__(self):
        self.heap = []
    
    def parent(self, i):
        return (i - 1) // 2
    
    def left_child(self, i):
        return 2 * i + 1
    
    def right_child(self, i):
        return 2 * i + 2
    
    def insert(self, item):
        self.heap.append(item)
        self._heapify_up(len(self.heap) - 1)
    
    def extract_min(self):
        if not self.heap:
            raise IndexError("Heap is empty")
        
        if len(self.heap) == 1:
            return self.heap.pop()
        
        # Replace root with last element
        min_item = self.heap[0]
        self.heap[0] = self.heap.pop()
        self._heapify_down(0)
        
        return min_item
    
    def peek(self):
        if not self.heap:
            raise IndexError("Heap is empty")
        return self.heap[0]
    
    def _heapify_up(self, i):
        parent_idx = self.parent(i)
        
        if i > 0 and self.heap[i] < self.heap[parent_idx]:
            self.heap[i], self.heap[parent_idx] = self.heap[parent_idx], self.heap[i]
            self._heapify_up(parent_idx)
    
    def _heapify_down(self, i):
        left = self.left_child(i)
        right = self.right_child(i)
        smallest = i
        
        if left < len(self.heap) and self.heap[left] < self.heap[smallest]:
            smallest = left
        
        if right < len(self.heap) and self.heap[right] < self.heap[smallest]:
            smallest = right
        
        if smallest != i:
            self.heap[i], self.heap[smallest] = self.heap[smallest], self.heap[i]
            self._heapify_down(smallest)
    
    def size(self):
        return len(self.heap)
    
    def is_empty(self):
        return len(self.heap) == 0

# Heap applications
def find_k_largest_elements(arr, k):
    """Find k largest elements using min-heap"""
    if k <= 0:
        return []
    
    # Use min-heap of size k
    heap = []
    
    for num in arr:
        if len(heap) < k:
            heapq.heappush(heap, num)
        elif num > heap[0]:
            heapq.heappop(heap)
            heapq.heappush(heap, num)
    
    return sorted(heap, reverse=True)

def merge_k_sorted_lists(lists):
    """Merge k sorted lists using heap"""
    heap = []
    result = []
    
    # Initialize heap with first element from each list
    for i, lst in enumerate(lists):
        if lst:
            heapq.heappush(heap, (lst[0], i, 0))  # (value, list_index, element_index)
    
    while heap:
        val, list_idx, elem_idx = heapq.heappop(heap)
        result.append(val)
        
        # Add next element from the same list
        if elem_idx + 1 < len(lists[list_idx]):
            next_val = lists[list_idx][elem_idx + 1]
            heapq.heappush(heap, (next_val, list_idx, elem_idx + 1))
    
    return result

# =============================================================================
# TREES
# =============================================================================

class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

class BinarySearchTree:
    def __init__(self):
        self.root = None
    
    def insert(self, val):
        self.root = self._insert_recursive(self.root, val)
    
    def _insert_recursive(self, node, val):
        if not node:
            return TreeNode(val)
        
        if val < node.val:
            node.left = self._insert_recursive(node.left, val)
        elif val > node.val:
            node.right = self._insert_recursive(node.right, val)
        
        return node
    
    def search(self, val):
        return self._search_recursive(self.root, val)
    
    def _search_recursive(self, node, val):
        if not node or node.val == val:
            return node
        
        if val < node.val:
            return self._search_recursive(node.left, val)
        else:
            return self._search_recursive(node.right, val)
    
    def delete(self, val):
        self.root = self._delete_recursive(self.root, val)
    
    def _delete_recursive(self, node, val):
        if not node:
            return node
        
        if val < node.val:
            node.left = self._delete_recursive(node.left, val)
        elif val > node.val:
            node.right = self._delete_recursive(node.right, val)
        else:
            # Node to be deleted found
            if not node.left:
                return node.right
            elif not node.right:
                return node.left
            else:
                # Node with two children
                successor = self._find_min(node.right)
                node.val = successor.val
                node.right = self._delete_recursive(node.right, successor.val)
        
        return node
    
    def _find_min(self, node):
        while node.left:
            node = node.left
        return node
    
    def inorder_traversal(self):
        result = []
        self._inorder_recursive(self.root, result)
        return result
    
    def _inorder_recursive(self, node, result):
        if node:
            self._inorder_recursive(node.left, result)
            result.append(node.val)
            self._inorder_recursive(node.right, result)

# Tree traversal algorithms
def preorder_traversal(root):
    """Root -> Left -> Right"""
    if not root:
        return []
    
    result = []
    stack = [root]
    
    while stack:
        node = stack.pop()
        result.append(node.val)
        
        if node.right:
            stack.append(node.right)
        if node.left:
            stack.append(node.left)
    
    return result

def inorder_traversal(root):
    """Left -> Root -> Right"""
    result = []
    stack = []
    current = root
    
    while stack or current:
        while current:
            stack.append(current)
            current = current.left
        
        current = stack.pop()
        result.append(current.val)
        current = current.right
    
    return result

def postorder_traversal(root):
    """Left -> Right -> Root"""
    if not root:
        return []
    
    result = []
    stack = [root]
    
    while stack:
        node = stack.pop()
        result.append(node.val)
        
        if node.left:
            stack.append(node.left)
        if node.right:
            stack.append(node.right)
    
    return result[::-1]  # Reverse to get correct postorder

def level_order_traversal(root):
    """Breadth-first traversal"""
    if not root:
        return []
    
    result = []
    queue = deque([root])
    
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

# =============================================================================
# GRAPHS
# =============================================================================

class Graph:
    def __init__(self, directed=False):
        self.graph = {}
        self.directed = directed
    
    def add_vertex(self, vertex):
        if vertex not in self.graph:
            self.graph[vertex] = []
    
    def add_edge(self, u, v, weight=1):
        self.add_vertex(u)
        self.add_vertex(v)
        
        self.graph[u].append((v, weight))
        
        if not self.directed:
            self.graph[v].append((u, weight))
    
    def get_vertices(self):
        return list(self.graph.keys())
    
    def get_edges(self):
        edges = []
        for u in self.graph:
            for v, weight in self.graph[u]:
                if self.directed or u <= v:  # Avoid duplicates for undirected
                    edges.append((u, v, weight))
        return edges
    
    def get_neighbors(self, vertex):
        return self.graph.get(vertex, [])

# Graph traversal algorithms
def dfs(graph, start, visited=None):
    """Depth-First Search"""
    if visited is None:
        visited = set()
    
    visited.add(start)
    result = [start]
    
    for neighbor, _ in graph.get_neighbors(start):
        if neighbor not in visited:
            result.extend(dfs(graph, neighbor, visited))
    
    return result

def dfs_iterative(graph, start):
    """Iterative DFS using stack"""
    visited = set()
    result = []
    stack = [start]
    
    while stack:
        vertex = stack.pop()
        
        if vertex not in visited:
            visited.add(vertex)
            result.append(vertex)
            
            # Add neighbors to stack
            for neighbor, _ in graph.get_neighbors(vertex):
                if neighbor not in visited:
                    stack.append(neighbor)
    
    return result

def bfs(graph, start):
    """Breadth-First Search"""
    visited = set()
    result = []
    queue = deque([start])
    visited.add(start)
    
    while queue:
        vertex = queue.popleft()
        result.append(vertex)
        
        for neighbor, _ in graph.get_neighbors(vertex):
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append(neighbor)
    
    return result

def has_path(graph, start, end):
    """Check if path exists between two vertices"""
    visited = set()
    queue = deque([start])
    visited.add(start)
    
    while queue:
        vertex = queue.popleft()
        
        if vertex == end:
            return True
        
        for neighbor, _ in graph.get_neighbors(vertex):
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append(neighbor)
    
    return False

def shortest_path_bfs(graph, start, end):
    """Find shortest path using BFS (unweighted graph)"""
    if start == end:
        return [start]
    
    visited = set()
    queue = deque([(start, [start])])
    visited.add(start)
    
    while queue:
        vertex, path = queue.popleft()
        
        for neighbor, _ in graph.get_neighbors(vertex):
            if neighbor == end:
                return path + [neighbor]
            
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append((neighbor, path + [neighbor]))
    
    return []  # No path found

# =============================================================================
# TRIE (PREFIX TREE)
# =============================================================================

class TrieNode:
    def __init__(self):
        self.children = {}
        self.is_end_of_word = False

class Trie:
    def __init__(self):
        self.root = TrieNode()
    
    def insert(self, word):
        node = self.root
        
        for char in word:
            if char not in node.children:
                node.children[char] = TrieNode()
            node = node.children[char]
        
        node.is_end_of_word = True
    
    def search(self, word):
        node = self.root
        
        for char in word:
            if char not in node.children:
                return False
            node = node.children[char]
        
        return node.is_end_of_word
    
    def starts_with(self, prefix):
        node = self.root
        
        for char in prefix:
            if char not in node.children:
                return False
            node = node.children[char]
        
        return True
    
    def get_all_words_with_prefix(self, prefix):
        """Get all words that start with given prefix"""
        node = self.root
        
        # Navigate to prefix end
        for char in prefix:
            if char not in node.children:
                return []
            node = node.children[char]
        
        # Collect all words from this point
        words = []
        self._collect_words(node, prefix, words)
        return words
    
    def _collect_words(self, node, current_word, words):
        if node.is_end_of_word:
            words.append(current_word)
        
        for char, child_node in node.children.items():
            self._collect_words(child_node, current_word + char, words)

# =============================================================================
# UNION-FIND (DISJOINT SET)
# =============================================================================

class UnionFind:
    def __init__(self, n):
        self.parent = list(range(n))
        self.rank = [0] * n
        self.components = n
    
    def find(self, x):
        """Find root with path compression"""
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])
        return self.parent[x]
    
    def union(self, x, y):
        """Union by rank"""
        root_x = self.find(x)
        root_y = self.find(y)
        
        if root_x == root_y:
            return False  # Already in same component
        
        # Union by rank
        if self.rank[root_x] < self.rank[root_y]:
            self.parent[root_x] = root_y
        elif self.rank[root_x] > self.rank[root_y]:
            self.parent[root_y] = root_x
        else:
            self.parent[root_y] = root_x
            self.rank[root_x] += 1
        
        self.components -= 1
        return True
    
    def connected(self, x, y):
        return self.find(x) == self.find(y)
    
    def get_components(self):
        return self.components

# Union-Find applications
def number_of_islands(grid):
    """Count number of islands using Union-Find"""
    if not grid or not grid[0]:
        return 0
    
    rows, cols = len(grid), len(grid[0])
    uf = UnionFind(rows * cols)
    water_cells = 0
    
    def get_index(r, c):
        return r * cols + c
    
    directions = [(0, 1), (1, 0), (0, -1), (-1, 0)]
    
    for i in range(rows):
        for j in range(cols):
            if grid[i][j] == '0':
                water_cells += 1
            else:
                for di, dj in directions:
                    ni, nj = i + di, j + dj
                    if (0 <= ni < rows and 0 <= nj < cols and 
                        grid[ni][nj] == '1'):
                        uf.union(get_index(i, j), get_index(ni, nj))
    
    return uf.get_components() - water_cells

# =============================================================================
# ADVANCED DATA STRUCTURE APPLICATIONS
# =============================================================================

def sliding_window_maximum(nums, k):
    """Find maximum in each sliding window using deque"""
    from collections import deque
    
    dq = deque()  # Store indices
    result = []
    
    for i, num in enumerate(nums):
        # Remove indices that are out of current window
        while dq and dq[0] < i - k + 1:
            dq.popleft()
        
        # Remove indices whose corresponding values are smaller than current
        while dq and nums[dq[-1]] < num:
            dq.pop()
        
        dq.append(i)
        
        # Add result when window is of size k
        if i >= k - 1:
            result.append(nums[dq[0]])
    
    return result

def median_from_data_stream():
    """Find median from data stream using two heaps"""
    import heapq
    
    class MedianFinder:
        def __init__(self):
            self.max_heap = []  # For smaller half (negate values for max-heap)
            self.min_heap = []  # For larger half
        
        def add_num(self, num):
            # Add to max_heap first
            heapq.heappush(self.max_heap, -num)
            
            # Balance: move max from max_heap to min_heap
            heapq.heappush(self.min_heap, -heapq.heappop(self.max_heap))
            
            # Balance sizes: max_heap can have at most 1 more element
            if len(self.max_heap) < len(self.min_heap):
                heapq.heappush(self.max_heap, -heapq.heappop(self.min_heap))
        
        def find_median(self):
            if len(self.max_heap) > len(self.min_heap):
                return -self.max_heap[0]
            else:
                return (-self.max_heap[0] + self.min_heap[0]) / 2.0
    
    return MedianFinder

def lru_cache_implementation():
    """LRU Cache using doubly linked list + hash map"""
    
    class Node:
        def __init__(self, key=0, val=0):
            self.key = key
            self.val = val
            self.prev = None
            self.next = None
    
    class LRUCache:
        def __init__(self, capacity):
            self.capacity = capacity
            self.cache = {}
            
            # Dummy head and tail
            self.head = Node()
            self.tail = Node()
            self.head.next = self.tail
            self.tail.prev = self.head
        
        def _add_node(self, node):
            """Add node right after head"""
            node.prev = self.head
            node.next = self.head.next
            
            self.head.next.prev = node
            self.head.next = node
        
        def _remove_node(self, node):
            """Remove an existing node"""
            prev_node = node.prev
            new_node = node.next
            
            prev_node.next = new_node
            new_node.prev = prev_node
        
        def _move_to_head(self, node):
            """Move node to head (mark as recently used)"""
            self._remove_node(node)
            self._add_node(node)
        
        def _pop_tail(self):
            """Remove last node before tail"""
            last_node = self.tail.prev
            self._remove_node(last_node)
            return last_node
        
        def get(self, key):
            node = self.cache.get(key)
            
            if node:
                self._move_to_head(node)
                return node.val
            
            return -1
        
        def put(self, key, value):
            node = self.cache.get(key)
            
            if node:
                # Update existing node
                node.val = value
                self._move_to_head(node)
            else:
                # Add new node
                new_node = Node(key, value)
                
                if len(self.cache) >= self.capacity:
                    # Remove LRU node
                    tail = self._pop_tail()
                    del self.cache[tail.key]
                
                self.cache[key] = new_node
                self._add_node(new_node)
    
    return LRUCache