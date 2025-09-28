"""
Python Concurrency Cheat Sheet
==============================
Threading, multiprocessing, asyncio, locks, GIL, and concurrent programming patterns
"""

import threading
import multiprocessing
import asyncio
import time
import queue
import concurrent.futures
from threading import Lock, RLock, Semaphore, Condition, Event, Barrier
from multiprocessing import Process, Queue, Pipe, Manager, Pool
import os

# =============================================================================
# GLOBAL INTERPRETER LOCK (GIL)
# =============================================================================

"""
The GIL Explained:
- Python's Global Interpreter Lock prevents multiple threads from executing 
  Python bytecode simultaneously
- Only ONE thread can execute Python code at a time
- GIL is released during I/O operations and C extensions
- Makes threading good for I/O-bound tasks but not CPU-bound tasks

When to use what:
- Threading: I/O-bound tasks (file operations, network requests, database calls)
- Multiprocessing: CPU-bound tasks (calculations, data processing)
- Asyncio: I/O-bound tasks with many concurrent operations
"""

def demonstrate_gil():
    """Demonstrate GIL limitations with CPU-bound task"""
    import time
    
    def cpu_bound_task(n):
        """CPU-intensive task"""
        result = 0
        for i in range(n):
            result += i * i
        return result
    
    # Sequential execution
    start_time = time.time()
    results = [cpu_bound_task(1000000) for _ in range(4)]
    sequential_time = time.time() - start_time
    
    # Multi-threaded execution (limited by GIL)
    start_time = time.time()
    threads = []
    results_threaded = []
    
    def worker(n, results_list, index):
        results_list[index] = cpu_bound_task(n)
    
    results_threaded = [None] * 4
    for i in range(4):
        thread = threading.Thread(target=worker, args=(1000000, results_threaded, i))
        threads.append(thread)
        thread.start()
    
    for thread in threads:
        thread.join()
    
    threaded_time = time.time() - start_time
    
    print(f"Sequential time: {sequential_time:.2f}s")
    print(f"Threaded time: {threaded_time:.2f}s")
    print(f"Threading speedup: {sequential_time/threaded_time:.2f}x")

# =============================================================================
# THREADING
# =============================================================================

def basic_threading():
    """Basic thread creation and management"""
    
    def worker(name, delay):
        """Simple worker function"""
        for i in range(3):
            print(f"Worker {name}: Task {i+1}")
            time.sleep(delay)
    
    # Create and start threads
    thread1 = threading.Thread(target=worker, args=("A", 0.5))
    thread2 = threading.Thread(target=worker, args=("B", 0.3))
    
    thread1.start()
    thread2.start()
    
    # Wait for threads to complete
    thread1.join()
    thread2.join()
    
    print("All threads completed")

def thread_with_return_value():
    """Get return values from threads"""
    import queue
    
    def worker(name, result_queue):
        """Worker that puts result in queue"""
        result = f"Result from {name}"
        time.sleep(1)
        result_queue.put(result)
    
    # Create queue to collect results
    result_queue = queue.Queue()
    
    # Start threads
    threads = []
    for i in range(3):
        thread = threading.Thread(target=worker, args=(f"Thread-{i}", result_queue))
        threads.append(thread)
        thread.start()
    
    # Wait for all threads
    for thread in threads:
        thread.join()
    
    # Collect results
    while not result_queue.empty():
        print(result_queue.get())

# Thread-safe counter example
class ThreadSafeCounter:
    """Thread-safe counter using locks"""
    
    def __init__(self):
        self._value = 0
        self._lock = threading.Lock()
    
    def increment(self):
        with self._lock:
            self._value += 1
    
    def get_value(self):
        with self._lock:
            return self._value

def demonstrate_thread_safety():
    """Show why locks are necessary"""
    
    # Unsafe counter
    class UnsafeCounter:
        def __init__(self):
            self.value = 0
        
        def increment(self):
            self.value += 1
    
    def increment_counter(counter, iterations):
        for _ in range(iterations):
            counter.increment()
    
    # Test unsafe counter
    unsafe_counter = UnsafeCounter()
    threads = []
    
    for _ in range(5):
        thread = threading.Thread(target=increment_counter, args=(unsafe_counter, 1000))
        threads.append(thread)
        thread.start()
    
    for thread in threads:
        thread.join()
    
    print(f"Unsafe counter final value: {unsafe_counter.value} (expected: 5000)")
    
    # Test safe counter
    safe_counter = ThreadSafeCounter()
    threads = []
    
    for _ in range(5):
        thread = threading.Thread(target=increment_counter, args=(safe_counter, 1000))
        threads.append(thread)
        thread.start()
    
    for thread in threads:
        thread.join()
    
    print(f"Safe counter final value: {safe_counter.get_value()} (expected: 5000)")

# =============================================================================
# THREAD SYNCHRONIZATION PRIMITIVES
# =============================================================================

def demonstrate_locks():
    """Different types of locks"""
    
    # Basic Lock
    basic_lock = Lock()
    
    def worker_with_lock(name, shared_resource, lock):
        with lock:
            print(f"{name} acquired lock")
            shared_resource.append(name)
            time.sleep(0.1)
            print(f"{name} released lock")
    
    shared_list = []
    threads = []
    
    for i in range(3):
        thread = threading.Thread(target=worker_with_lock, 
                                 args=(f"Thread-{i}", shared_list, basic_lock))
        threads.append(thread)
        thread.start()
    
    for thread in threads:
        thread.join()
    
    print(f"Shared list: {shared_list}")

def demonstrate_rlock():
    """Reentrant Lock - can be acquired multiple times by same thread"""
    
    rlock = RLock()
    
    def recursive_function(n, rlock):
        with rlock:
            print(f"Acquired lock at level {n}")
            if n > 0:
                recursive_function(n - 1, rlock)  # Reacquire same lock
            print(f"Released lock at level {n}")
    
    thread = threading.Thread(target=recursive_function, args=(3, rlock))
    thread.start()
    thread.join()

def demonstrate_semaphore():
    """Semaphore - limits number of threads accessing resource"""
    
    # Allow maximum 2 threads to access resource simultaneously
    semaphore = Semaphore(2)
    
    def worker_with_semaphore(name):
        with semaphore:
            print(f"{name} acquired semaphore")
            time.sleep(2)
            print(f"{name} released semaphore")
    
    threads = []
    for i in range(5):
        thread = threading.Thread(target=worker_with_semaphore, args=(f"Worker-{i}",))
        threads.append(thread)
        thread.start()
    
    for thread in threads:
        thread.join()

def demonstrate_condition():
    """Condition variable - wait for specific condition"""
    
    items = []
    condition = Condition()
    
    def consumer(name):
        with condition:
            while not items:  # Wait until items available
                print(f"{name} waiting for items...")
                condition.wait()
            item = items.pop(0)
            print(f"{name} consumed {item}")
    
    def producer():
        for i in range(3):
            with condition:
                item = f"item-{i}"
                items.append(item)
                print(f"Produced {item}")
                condition.notify()  # Wake up one waiting thread
                time.sleep(0.5)
    
    # Start consumer threads
    consumers = []
    for i in range(2):
        thread = threading.Thread(target=consumer, args=(f"Consumer-{i}",))
        consumers.append(thread)
        thread.start()
    
    # Start producer
    producer_thread = threading.Thread(target=producer)
    producer_thread.start()
    
    # Wait for completion
    producer_thread.join()
    for consumer in consumers:
        consumer.join()

def demonstrate_event():
    """Event - simple signaling between threads"""
    
    event = Event()
    
    def waiter(name):
        print(f"{name} waiting for event...")
        event.wait()
        print(f"{name} received event!")
    
    def setter():
        time.sleep(2)
        print("Setting event...")
        event.set()
    
    # Start waiters
    waiters = []
    for i in range(3):
        thread = threading.Thread(target=waiter, args=(f"Waiter-{i}",))
        waiters.append(thread)
        thread.start()
    
    # Start setter
    setter_thread = threading.Thread(target=setter)
    setter_thread.start()
    
    # Wait for completion
    setter_thread.join()
    for waiter in waiters:
        waiter.join()

# =============================================================================
# MULTIPROCESSING
# =============================================================================

def cpu_intensive_task(n):
    """CPU-intensive task for multiprocessing example"""
    result = 0
    for i in range(n):
        result += i * i
    return result

def demonstrate_multiprocessing():
    """Basic multiprocessing example"""
    
    def worker(name, n):
        print(f"Process {name} starting...")
        result = cpu_intensive_task(n)
        print(f"Process {name} finished with result: {result}")
    
    # Create and start processes
    processes = []
    for i in range(4):
        process = Process(target=worker, args=(f"Process-{i}", 1000000))
        processes.append(process)
        process.start()
    
    # Wait for all processes to complete
    for process in processes:
        process.join()
    
    print("All processes completed")

def multiprocessing_with_return_values():
    """Get return values from processes using Queue"""
    
    def worker(name, n, result_queue):
        result = cpu_intensive_task(n)
        result_queue.put((name, result))
    
    # Create queue for results
    result_queue = Queue()
    
    # Start processes
    processes = []
    for i in range(4):
        process = Process(target=worker, args=(f"Process-{i}", 500000, result_queue))
        processes.append(process)
        process.start()
    
    # Wait for processes
    for process in processes:
        process.join()
    
    # Collect results
    while not result_queue.empty():
        name, result = result_queue.get()
        print(f"{name}: {result}")

def demonstrate_process_pool():
    """Using process pool for parallel execution"""
    
    # Using Pool class
    with Pool(processes=4) as pool:
        # Map function to multiple arguments
        results = pool.map(cpu_intensive_task, [100000, 200000, 300000, 400000])
        print(f"Pool results: {results}")
    
    # Using ProcessPoolExecutor (recommended)
    with concurrent.futures.ProcessPoolExecutor(max_workers=4) as executor:
        futures = [executor.submit(cpu_intensive_task, n) 
                  for n in [100000, 200000, 300000, 400000]]
        
        for future in concurrent.futures.as_completed(futures):
            result = future.result()
            print(f"Future result: {result}")

def demonstrate_shared_memory():
    """Sharing data between processes"""
    
    def worker(shared_list, shared_value, lock, name):
        with lock:
            temp = shared_value.value
            temp += 1
            shared_value.value = temp
            shared_list.append(temp)
            print(f"{name} incremented to {temp}")
    
    # Create shared objects
    manager = Manager()
    shared_list = manager.list()
    shared_value = manager.Value('i', 0)  # 'i' for integer
    lock = manager.Lock()
    
    # Start processes
    processes = []
    for i in range(5):
        process = Process(target=worker, 
                         args=(shared_list, shared_value, lock, f"Process-{i}"))
        processes.append(process)
        process.start()
    
    # Wait for completion
    for process in processes:
        process.join()
    
    print(f"Final shared list: {list(shared_list)}")
    print(f"Final shared value: {shared_value.value}")

# =============================================================================
# ASYNCIO (ASYNCHRONOUS PROGRAMMING)
# =============================================================================

async def async_task(name, delay):
    """Simple async task"""
    print(f"{name} starting...")
    await asyncio.sleep(delay)  # Non-blocking sleep
    print(f"{name} completed after {delay} seconds")
    return f"Result from {name}"

async def demonstrate_basic_asyncio():
    """Basic asyncio usage"""
    
    # Run tasks concurrently
    tasks = [
        async_task("Task-1", 2),
        async_task("Task-2", 1),
        async_task("Task-3", 3)
    ]
    
    results = await asyncio.gather(*tasks)
    print(f"All results: {results}")

async def async_http_requests():
    """Simulate async HTTP requests"""
    
    async def fetch_data(url, delay):
        print(f"Fetching {url}...")
        await asyncio.sleep(delay)  # Simulate network delay
        return f"Data from {url}"
    
    # Concurrent requests
    urls = [("http://api1.com", 1), ("http://api2.com", 2), ("http://api3.com", 1.5)]
    
    tasks = [fetch_data(url, delay) for url, delay in urls]
    results = await asyncio.gather(*tasks)
    
    for result in results:
        print(result)

async def demonstrate_async_context_manager():
    """Async context managers"""
    
    class AsyncResource:
        async def __aenter__(self):
            print("Acquiring async resource...")
            await asyncio.sleep(0.1)
            return self
        
        async def __aexit__(self, exc_type, exc_val, exc_tb):
            print("Releasing async resource...")
            await asyncio.sleep(0.1)
        
        async def do_work(self):
            print("Doing async work...")
            await asyncio.sleep(0.5)
    
    async with AsyncResource() as resource:
        await resource.do_work()

async def demonstrate_async_generators():
    """Async generators"""
    
    async def async_range(n):
        for i in range(n):
            await asyncio.sleep(0.1)  # Simulate async operation
            yield i
    
    async for value in async_range(5):
        print(f"Async generated: {value}")

# =============================================================================
# CONCURRENT.FUTURES
# =============================================================================

def demonstrate_thread_pool_executor():
    """Using ThreadPoolExecutor for I/O-bound tasks"""
    
    def io_task(name, delay):
        time.sleep(delay)  # Simulate I/O
        return f"Completed {name} after {delay}s"
    
    # Using ThreadPoolExecutor
    with concurrent.futures.ThreadPoolExecutor(max_workers=3) as executor:
        # Submit individual tasks
        futures = {
            executor.submit(io_task, f"Task-{i}", i): i 
            for i in range(1, 4)
        }
        
        # Process completed tasks
        for future in concurrent.futures.as_completed(futures):
            result = future.result()
            print(result)

def demonstrate_process_pool_executor():
    """Using ProcessPoolExecutor for CPU-bound tasks"""
    
    with concurrent.futures.ProcessPoolExecutor(max_workers=4) as executor:
        # Submit tasks
        futures = [
            executor.submit(cpu_intensive_task, n) 
            for n in [100000, 200000, 300000, 400000]
        ]
        
        # Get results
        for i, future in enumerate(concurrent.futures.as_completed(futures)):
            result = future.result()
            print(f"CPU task {i}: {result}")

# =============================================================================
# PRODUCER-CONSUMER PATTERNS
# =============================================================================

def demonstrate_producer_consumer():
    """Classic producer-consumer with queue"""
    
    import random
    
    def producer(name, q, num_items):
        for i in range(num_items):
            item = f"{name}-item-{i}"
            q.put(item)
            print(f"Producer {name} produced {item}")
            time.sleep(random.uniform(0.1, 0.5))
        
        # Signal completion
        q.put(None)
    
    def consumer(name, q):
        while True:
            item = q.get()
            if item is None:
                q.task_done()
                break
            
            print(f"Consumer {name} consumed {item}")
            time.sleep(random.uniform(0.2, 0.8))
            q.task_done()
    
    # Create queue
    task_queue = queue.Queue(maxsize=5)  # Limited size queue
    
    # Start producer and consumer threads
    producer_thread = threading.Thread(target=producer, args=("P1", task_queue, 10))
    consumer_threads = [
        threading.Thread(target=consumer, args=(f"C{i}", task_queue))
        for i in range(1, 3)
    ]
    
    producer_thread.start()
    for consumer_thread in consumer_threads:
        consumer_thread.start()
    
    # Wait for producer to finish
    producer_thread.join()
    
    # Wait for all tasks to be processed
    task_queue.join()
    
    # Stop consumers
    for _ in consumer_threads:
        task_queue.put(None)
    
    for consumer_thread in consumer_threads:
        consumer_thread.join()

# =============================================================================
# DEADLOCK PREVENTION
# =============================================================================

def demonstrate_deadlock_scenario():
    """Show potential deadlock and how to prevent it"""
    
    lock1 = threading.Lock()
    lock2 = threading.Lock()
    
    def worker1():
        with lock1:
            print("Worker 1 acquired lock1")
            time.sleep(0.1)
            with lock2:
                print("Worker 1 acquired lock2")
    
    def worker2():
        with lock2:
            print("Worker 2 acquired lock2")  
            time.sleep(0.1)
            with lock1:
                print("Worker 2 acquired lock1")
    
    # This could potentially deadlock
    # thread1 = threading.Thread(target=worker1)
    # thread2 = threading.Thread(target=worker2)
    
    print("Potential deadlock scenario (commented out)")

def prevent_deadlock_with_timeout():
    """Prevent deadlock using lock timeout"""
    
    lock1 = threading.Lock()
    lock2 = threading.Lock()
    
    def safe_worker1():
        if lock1.acquire(timeout=1):
            try:
                print("Worker 1 acquired lock1")
                if lock2.acquire(timeout=1):
                    try:
                        print("Worker 1 acquired lock2")
                    finally:
                        lock2.release()
                else:
                    print("Worker 1 timeout on lock2")
            finally:
                lock1.release()
        else:
            print("Worker 1 timeout on lock1")
    
    def safe_worker2():
        if lock1.acquire(timeout=1):  # Same order as worker1
            try:
                print("Worker 2 acquired lock1")
                if lock2.acquire(timeout=1):
                    try:
                        print("Worker 2 acquired lock2")
                    finally:
                        lock2.release()
                else:
                    print("Worker 2 timeout on lock2")
            finally:
                lock1.release()
        else:
            print("Worker 2 timeout on lock1")
    
    thread1 = threading.Thread(target=safe_worker1)
    thread2 = threading.Thread(target=safe_worker2)
    
    thread1.start()
    thread2.start()
    
    thread1.join()
    thread2.join()

# =============================================================================
# PERFORMANCE COMPARISON
# =============================================================================

def performance_comparison():
    """Compare threading vs multiprocessing vs asyncio performance"""
    
    def cpu_bound_task(n):
        """CPU intensive task"""
        return sum(i * i for i in range(n))
    
    def io_bound_task(delay):
        """I/O intensive task"""
        time.sleep(delay)
        return f"IO task completed after {delay}s"
    
    async def async_io_task(delay):
        """Async I/O task"""
        await asyncio.sleep(delay)
        return f"Async IO task completed after {delay}s"
    
    # Test data
    cpu_tasks = [100000] * 4
    io_tasks = [0.5] * 4
    
    # Sequential CPU tasks
    start = time.time()
    [cpu_bound_task(n) for n in cpu_tasks]
    sequential_cpu_time = time.time() - start
    
    # Multiprocessing CPU tasks
    start = time.time()
    with concurrent.futures.ProcessPoolExecutor(max_workers=4) as executor:
        list(executor.map(cpu_bound_task, cpu_tasks))
    multiprocessing_time = time.time() - start
    
    # Sequential I/O tasks
    start = time.time()
    [io_bound_task(delay) for delay in io_tasks]
    sequential_io_time = time.time() - start
    
    # Threading I/O tasks
    start = time.time()
    with concurrent.futures.ThreadPoolExecutor(max_workers=4) as executor:
        list(executor.map(io_bound_task, io_tasks))
    threading_time = time.time() - start
    
    # Asyncio I/O tasks
    start = time.time()
    async def run_async_tasks():
        tasks = [async_io_task(delay) for delay in io_tasks]
        await asyncio.gather(*tasks)
    
    asyncio.run(run_async_tasks())
    asyncio_time = time.time() - start
    
    print(f"CPU Tasks:")
    print(f"  Sequential: {sequential_cpu_time:.2f}s")
    print(f"  Multiprocessing: {multiprocessing_time:.2f}s")
    print(f"  Speedup: {sequential_cpu_time/multiprocessing_time:.2f}x")
    
    print(f"\nI/O Tasks:")
    print(f"  Sequential: {sequential_io_time:.2f}s")
    print(f"  Threading: {threading_time:.2f}s")
    print(f"  Asyncio: {asyncio_time:.2f}s")

# =============================================================================
# PROGRAMMING PATTERNS AND BEST PRACTICES
# =============================================================================

"""
Common Concurrency Programming Questions:

1. **Explain the GIL and its implications**
   - Global Interpreter Lock prevents true parallelism for CPU-bound tasks
   - Use multiprocessing for CPU-bound, threading for I/O-bound
   - GIL is released during I/O operations

2. **When would you use threading vs multiprocessing vs asyncio?**
   - Threading: I/O-bound tasks, shared memory, lighter overhead
   - Multiprocessing: CPU-bound tasks, true parallelism, isolated memory
   - Asyncio: I/O-bound tasks with many concurrent operations, single-threaded

3. **How to prevent race conditions?**
   - Use locks, semaphores, or other synchronization primitives
   - Atomic operations, thread-safe data structures
   - Immutable data, message passing

4. **What is a deadlock and how to prevent it?**
   - Two or more threads waiting for each other's resources
   - Prevention: consistent lock ordering, timeouts, avoid nested locks

5. **Explain different types of locks**
   - Lock: Basic mutual exclusion
   - RLock: Reentrant lock (same thread can acquire multiple times)
   - Semaphore: Allow N threads to access resource
   - Condition: Wait for specific condition

Best Practices:
✅ Use context managers (with statement) for locks
✅ Keep critical sections small
✅ Use thread-safe data structures when possible
✅ Choose right concurrency model for the task
✅ Handle exceptions in concurrent code
✅ Use timeouts to prevent indefinite blocking
✅ Test concurrent code thoroughly

Common Pitfalls:
❌ Using threading for CPU-bound tasks (GIL limitation)
❌ Sharing mutable state without synchronization
❌ Creating too many threads/processes (overhead)
❌ Not handling exceptions in worker threads
❌ Deadlocks from incorrect lock ordering
❌ Race conditions in shared resources
"""

# =============================================================================
# EXAMPLE USAGE
# =============================================================================

if __name__ == "__main__":
    print("Python Concurrency Examples")
    print("=" * 40)
    
    # Run examples (uncomment to test)
    # demonstrate_gil()
    # basic_threading()
    # demonstrate_thread_safety()
    # demonstrate_locks()
    # demonstrate_multiprocessing()
    # asyncio.run(demonstrate_basic_asyncio())
    # performance_comparison()
    
    print("Choose specific functions to run based on your needs!")