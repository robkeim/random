"""
Python OOP Basics Cheat Sheet
=============================
Classes, inheritance, polymorphism, and OOP concepts for development
"""

# =============================================================================
# BASIC CLASS DEFINITION
# =============================================================================

class Person:
    """Basic class with attributes and methods"""
    
    # Class variable (shared by all instances)
    species = "Homo sapiens"
    
    def __init__(self, name, age):
        """Constructor method"""
        self.name = name        # Instance variable
        self.age = age         # Instance variable
    
    def introduce(self):
        """Instance method"""
        return f"Hi, I'm {self.name} and I'm {self.age} years old"
    
    def have_birthday(self):
        """Method that modifies instance state"""
        self.age += 1
        return f"Happy birthday! {self.name} is now {self.age}"
    
    @classmethod
    def from_string(cls, person_str):
        """Class method - alternative constructor"""
        name, age = person_str.split('-')
        return cls(name, int(age))
    
    @staticmethod
    def is_adult(age):
        """Static method - utility function"""
        return age >= 18
    
    def __str__(self):
        """String representation for print()"""
        return f"Person(name='{self.name}', age={self.age})"
    
    def __repr__(self):
        """Official string representation"""
        return f"Person('{self.name}', {self.age})"

# Usage
person1 = Person("Alice", 30)
person2 = Person.from_string("Bob-25")
print(person1.introduce())
print(Person.is_adult(16))

# =============================================================================
# INHERITANCE
# =============================================================================

class Employee(Person):
    """Employee inherits from Person"""
    
    def __init__(self, name, age, employee_id, salary):
        super().__init__(name, age)  # Call parent constructor
        self.employee_id = employee_id
        self.salary = salary
    
    def introduce(self):
        """Override parent method"""
        base_intro = super().introduce()
        return f"{base_intro}. I work here as employee #{self.employee_id}"
    
    def get_annual_salary(self):
        """New method specific to Employee"""
        return self.salary * 12

class Manager(Employee):
    """Manager inherits from Employee"""
    
    def __init__(self, name, age, employee_id, salary, team_size):
        super().__init__(name, age, employee_id, salary)
        self.team_size = team_size
    
    def introduce(self):
        """Override again"""
        base_intro = super().introduce()
        return f"{base_intro} and I manage a team of {self.team_size} people"

# Multiple inheritance
class Consultant(Employee):
    def __init__(self, name, age, employee_id, hourly_rate):
        super().__init__(name, age, employee_id, 0)  # Salary is 0
        self.hourly_rate = hourly_rate

# =============================================================================
# POLYMORPHISM
# =============================================================================

def process_people(people_list):
    """Demonstrates polymorphism - same method name, different behavior"""
    for person in people_list:
        print(person.introduce())  # Each class implements introduce() differently

people = [
    Person("Charlie", 22),
    Employee("Diana", 28, "E001", 5000),
    Manager("Eve", 35, "M001", 8000, 5)
]

process_people(people)

# =============================================================================
# SPECIAL METHODS (MAGIC METHODS)
# =============================================================================

class BankAccount:
    """Class demonstrating special methods"""
    
    def __init__(self, owner, balance=0):
        self.owner = owner
        self.balance = balance
    
    def __str__(self):
        """Human-readable string representation"""
        return f"{self.owner}'s account: ${self.balance}"
    
    def __repr__(self):
        """Technical string representation"""
        return f"BankAccount('{self.owner}', {self.balance})"
    
    def __len__(self):
        """Define length (number of transactions, for example)"""
        return int(self.balance)  # Simplified
    
    def __eq__(self, other):
        """Define equality comparison"""
        return self.balance == other.balance
    
    def __lt__(self, other):
        """Define less than comparison"""
        return self.balance < other.balance
    
    def __add__(self, amount):
        """Define addition operation"""
        return BankAccount(self.owner, self.balance + amount)
    
    def __iadd__(self, amount):
        """Define in-place addition"""
        self.balance += amount
        return self
    
    def __getitem__(self, key):
        """Make object subscriptable"""
        if key == 'owner':
            return self.owner
        elif key == 'balance':
            return self.balance
        else:
            raise KeyError(key)
    
    def __setitem__(self, key, value):
        """Allow item assignment"""
        if key == 'owner':
            self.owner = value
        elif key == 'balance':
            self.balance = value
        else:
            raise KeyError(key)
    
    def __call__(self, amount):
        """Make object callable"""
        self.balance += amount
        return self.balance

# Usage of special methods
account1 = BankAccount("Alice", 1000)
account2 = BankAccount("Bob", 1500)

print(len(account1))        # 1000
print(account1 == account2) # False
print(account1 < account2)  # True
account3 = account1 + 500   # Creates new account
account1 += 200             # Modifies existing account
print(account1['balance'])  # 1200
account1('deposit', 100)    # Callable

# =============================================================================
# PROPERTY DECORATORS
# =============================================================================

class Circle:
    """Demonstrates property decorators"""
    
    def __init__(self, radius):
        self._radius = radius  # Private attribute convention
    
    @property
    def radius(self):
        """Getter for radius"""
        return self._radius
    
    @radius.setter
    def radius(self, value):
        """Setter for radius with validation"""
        if value <= 0:
            raise ValueError("Radius must be positive")
        self._radius = value
    
    @property
    def area(self):
        """Computed property"""
        return 3.14159 * self._radius ** 2
    
    @property
    def diameter(self):
        """Another computed property"""
        return 2 * self._radius

# Usage
circle = Circle(5)
print(circle.radius)    # 5
print(circle.area)      # 78.53975
circle.radius = 7       # Calls setter
print(circle.diameter)  # 14

# =============================================================================
# CLASS DECORATORS AND DESCRIPTORS
# =============================================================================

class ValidatedAttribute:
    """Descriptor for validated attributes"""
    
    def __init__(self, name, validator):
        self.name = name
        self.validator = validator
    
    def __get__(self, obj, objtype=None):
        if obj is None:
            return self
        return obj.__dict__.get(self.name)
    
    def __set__(self, obj, value):
        if not self.validator(value):
            raise ValueError(f"Invalid value for {self.name}: {value}")
        obj.__dict__[self.name] = value

def positive_number(value):
    return isinstance(value, (int, float)) and value > 0

def non_empty_string(value):
    return isinstance(value, str) and len(value.strip()) > 0

class Product:
    """Using descriptors for validation"""
    
    name = ValidatedAttribute('name', non_empty_string)
    price = ValidatedAttribute('price', positive_number)
    
    def __init__(self, name, price):
        self.name = name
        self.price = price

# =============================================================================
# ABSTRACT BASE CLASSES
# =============================================================================

from abc import ABC, abstractmethod

class Shape(ABC):
    """Abstract base class"""
    
    @abstractmethod
    def area(self):
        """All shapes must implement area calculation"""
        pass
    
    @abstractmethod
    def perimeter(self):
        """All shapes must implement perimeter calculation"""
        pass
    
    def description(self):
        """Concrete method that can be inherited"""
        return f"This shape has area {self.area()} and perimeter {self.perimeter()}"

class Rectangle(Shape):
    """Concrete implementation of Shape"""
    
    def __init__(self, width, height):
        self.width = width
        self.height = height
    
    def area(self):
        return self.width * self.height
    
    def perimeter(self):
        return 2 * (self.width + self.height)

class Square(Rectangle):
    """Square is a special case of Rectangle"""
    
    def __init__(self, side):
        super().__init__(side, side)

# =============================================================================
# COMPOSITION VS INHERITANCE
# =============================================================================

class Engine:
    """Component class"""
    
    def __init__(self, horsepower):
        self.horsepower = horsepower
    
    def start(self):
        return "Engine started"
    
    def stop(self):
        return "Engine stopped"

class Car:
    """Composition - Car HAS an Engine"""
    
    def __init__(self, make, model, horsepower):
        self.make = make
        self.model = model
        self.engine = Engine(horsepower)  # Composition
    
    def start(self):
        return f"{self.make} {self.model}: {self.engine.start()}"
    
    def stop(self):
        return f"{self.make} {self.model}: {self.engine.stop()}"

# =============================================================================
# MIXINS
# =============================================================================

class TimestampMixin:
    """Mixin to add timestamp functionality"""
    
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        from datetime import datetime
        self.created_at = datetime.now()
    
    def age(self):
        from datetime import datetime
        return datetime.now() - self.created_at

class SerializableMixin:
    """Mixin to add serialization functionality"""
    
    def to_dict(self):
        """Convert object to dictionary"""
        return {k: v for k, v in self.__dict__.items() 
                if not k.startswith('_')}
    
    @classmethod
    def from_dict(cls, data):
        """Create object from dictionary"""
        return cls(**data)

class Document(TimestampMixin, SerializableMixin):
    """Class using multiple mixins"""
    
    def __init__(self, title, content):
        super().__init__()
        self.title = title
        self.content = content

# =============================================================================
# COMMON OOP PATTERNS FOR DEVELOPMENT
# =============================================================================

# Singleton pattern
class Singleton:
    """Ensure only one instance exists"""
    
    _instance = None
    
    def __new__(cls):
        if cls._instance is None:
            cls._instance = super().__new__(cls)
        return cls._instance

# Factory pattern
class AnimalFactory:
    """Factory to create different animals"""
    
    @staticmethod
    def create_animal(animal_type, name):
        if animal_type == "dog":
            return Dog(name)
        elif animal_type == "cat":
            return Cat(name)
        else:
            raise ValueError(f"Unknown animal type: {animal_type}")

class Animal:
    def __init__(self, name):
        self.name = name
    
    def make_sound(self):
        pass

class Dog(Animal):
    def make_sound(self):
        return f"{self.name} says Woof!"

class Cat(Animal):
    def make_sound(self):
        return f"{self.name} says Meow!"

# Observer pattern
class Subject:
    """Subject being observed"""
    
    def __init__(self):
        self._observers = []
    
    def attach(self, observer):
        self._observers.append(observer)
    
    def detach(self, observer):
        self._observers.remove(observer)
    
    def notify(self, message):
        for observer in self._observers:
            observer.update(message)

class Observer:
    """Observer interface"""
    
    def __init__(self, name):
        self.name = name
    
    def update(self, message):
        print(f"{self.name} received: {message}")

# =============================================================================
# BEST PRACTICES AND CONVENTIONS
# =============================================================================

class BestPracticesExample:
    """Demonstrates Python OOP best practices"""
    
    # Class constants
    MAX_ITEMS = 100
    
    def __init__(self, name):
        # Public attribute
        self.name = name
        
        # Protected attribute (convention: don't use outside class hierarchy)
        self._internal_id = id(self)
        
        # Private attribute (name mangling)
        self.__secret = "secret_value"
    
    def public_method(self):
        """Public method - part of the API"""
        return self._protected_method()
    
    def _protected_method(self):
        """Protected method - for internal use and subclasses"""
        return self.__private_method()
    
    def __private_method(self):
        """Private method - name mangled to _ClassName__method_name"""
        return "private result"
    
    def __repr__(self):
        """Always implement __repr__ for debugging"""
        return f"{self.__class__.__name__}(name='{self.name}')"
    
    def __eq__(self, other):
        """Implement equality if objects should be comparable"""
        if not isinstance(other, self.__class__):
            return NotImplemented
        return self.name == other.name

# =============================================================================
# METACLASSES (ADVANCED)
# =============================================================================

class SingletonMeta(type):
    """Metaclass for Singleton pattern"""
    
    _instances = {}
    
    def __call__(cls, *args, **kwargs):
        if cls not in cls._instances:
            cls._instances[cls] = super().__call__(*args, **kwargs)
        return cls._instances[cls]

class DatabaseConnection(metaclass=SingletonMeta):
    """Singleton using metaclass"""
    
    def __init__(self):
        self.connection = "Connected to database"

# Both instances will be the same object
db1 = DatabaseConnection()
db2 = DatabaseConnection()
print(db1 is db2)  # True