from threading import Lock

class BankAccount(object):
    lock = Lock()
    is_open = False
    balance = 0
    
    def __init__(self):
        pass

    def get_balance(self):
        with self.lock:
            if not self.is_open:
                raise ValueError("Invalid operation on an open account")
            balance = self.balance
            return balance

    def open(self):
        with self.lock:
            if self.is_open:
                raise ValueError("Invalid operation on an open account")
            self.is_open = True

    def deposit(self, amount):
        with self.lock:
            if not self.is_open:
                raise ValueError("Invalid operation on a closed account")
                
            if amount < 0:
                raise ValueError("Cannot deposit negative amount")
            self.balance += amount

    def withdraw(self, amount):
        with self.lock:
            if not self.is_open:
                raise ValueError("Invalid operation on a closed account")
                
            if amount > self.balance:
                raise ValueError("Cannot withdraw more than balance")
                
            if amount < 0:
                raise ValueError("Cannot withdraw negative amount")
                
            self.balance -= amount

    def close(self):
        with self.lock:
            if not self.is_open:
                raise ValueError("Invalid operation on a closed account")
            self.is_open = False
            self.balance = 0
