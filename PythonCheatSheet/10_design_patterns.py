"""
Python Design Patterns Cheat Sheet
===================================
Common design patterns for development
"""

# =============================================================================
# CREATIONAL PATTERNS
# =============================================================================

# 1. SINGLETON PATTERN
class Singleton:
    """Ensures only one instance of a class exists"""
    
    _instance = None
    _initialized = False
    
    def __new__(cls):
        if cls._instance is None:
            cls._instance = super().__new__(cls)
        return cls._instance
    
    def __init__(self):
        if not self._initialized:
            self.data = "Singleton data"
            self._initialized = True

# Thread-safe singleton
import threading

class ThreadSafeSingleton:
    _instance = None
    _lock = threading.Lock()
    
    def __new__(cls):
        if cls._instance is None:
            with cls._lock:
                if cls._instance is None:  # Double-checked locking
                    cls._instance = super().__new__(cls)
        return cls._instance

# 2. FACTORY PATTERN
class Animal:
    def make_sound(self):
        pass

class Dog(Animal):
    def make_sound(self):
        return "Woof!"

class Cat(Animal):
    def make_sound(self):
        return "Meow!"

class Bird(Animal):
    def make_sound(self):
        return "Tweet!"

class AnimalFactory:
    """Factory to create different animals"""
    
    @staticmethod
    def create_animal(animal_type):
        animals = {
            'dog': Dog,
            'cat': Cat,
            'bird': Bird
        }
        
        animal_class = animals.get(animal_type.lower())
        if animal_class:
            return animal_class()
        else:
            raise ValueError(f"Unknown animal type: {animal_type}")

# Usage
factory = AnimalFactory()
dog = factory.create_animal('dog')
print(dog.make_sound())  # "Woof!"

# 3. ABSTRACT FACTORY PATTERN
class GUIFactory:
    """Abstract factory for creating UI components"""
    
    def create_button(self):
        pass
    
    def create_window(self):
        pass

class WindowsFactory(GUIFactory):
    def create_button(self):
        return WindowsButton()
    
    def create_window(self):
        return WindowsWindow()

class MacFactory(GUIFactory):
    def create_button(self):
        return MacButton()
    
    def create_window(self):
        return MacWindow()

class Button:
    def click(self):
        pass

class WindowsButton(Button):
    def click(self):
        return "Windows button clicked"

class MacButton(Button):
    def click(self):
        return "Mac button clicked"

class Window:
    def render(self):
        pass

class WindowsWindow(Window):
    def render(self):
        return "Windows window rendered"

class MacWindow(Window):
    def render(self):
        return "Mac window rendered"

# 4. BUILDER PATTERN
class Pizza:
    def __init__(self):
        self.size = None
        self.crust = None
        self.toppings = []
    
    def __str__(self):
        return f"{self.size} pizza with {self.crust} crust and toppings: {', '.join(self.toppings)}"

class PizzaBuilder:
    def __init__(self):
        self.pizza = Pizza()
    
    def set_size(self, size):
        self.pizza.size = size
        return self  # Return self for method chaining
    
    def set_crust(self, crust):
        self.pizza.crust = crust
        return self
    
    def add_topping(self, topping):
        self.pizza.toppings.append(topping)
        return self
    
    def build(self):
        return self.pizza

# Usage
pizza = (PizzaBuilder()
         .set_size("Large")
         .set_crust("Thin")
         .add_topping("Pepperoni")
         .add_topping("Mushrooms")
         .build())

# =============================================================================
# STRUCTURAL PATTERNS
# =============================================================================

# 1. ADAPTER PATTERN
class EuropeanSocket:
    """European socket (different interface)"""
    def voltage(self):
        return 230
    
    def live(self):
        return 1
    
    def neutral(self):
        return -1
    
    def earth(self):
        return 0

class USASocket:
    """USA socket (different interface)"""
    def voltage(self):
        return 120
    
    def live(self):
        return 1
    
    def neutral(self):
        return -1

class EuropeanSocketAdapter:
    """Adapter to make European socket work with USA interface"""
    def __init__(self, socket):
        self.socket = socket
    
    def voltage(self):
        return 110  # Convert voltage
    
    def live(self):
        return self.socket.live()
    
    def neutral(self):
        return self.socket.neutral()

# 2. DECORATOR PATTERN
class Coffee:
    def cost(self):
        return 2.0
    
    def description(self):
        return "Simple coffee"

class CoffeeDecorator:
    """Base decorator"""
    def __init__(self, coffee):
        self._coffee = coffee
    
    def cost(self):
        return self._coffee.cost()
    
    def description(self):
        return self._coffee.description()

class MilkDecorator(CoffeeDecorator):
    def cost(self):
        return self._coffee.cost() + 0.5
    
    def description(self):
        return self._coffee.description() + ", milk"

class SugarDecorator(CoffeeDecorator):
    def cost(self):
        return self._coffee.cost() + 0.2
    
    def description(self):
        return self._coffee.description() + ", sugar"

# Usage
coffee = Coffee()
coffee_with_milk = MilkDecorator(coffee)
coffee_with_milk_and_sugar = SugarDecorator(coffee_with_milk)
print(f"{coffee_with_milk_and_sugar.description()}: ${coffee_with_milk_and_sugar.cost()}")

# 3. FACADE PATTERN
class CPU:
    def freeze(self):
        print("CPU frozen")
    
    def jump(self, position):
        print(f"CPU jumped to {position}")
    
    def execute(self):
        print("CPU executing")

class Memory:
    def load(self, position, data):
        print(f"Memory loaded {data} at {position}")

class HardDrive:
    def read(self, lba, size):
        return f"Data from sector {lba}"

class ComputerFacade:
    """Simplified interface to complex subsystem"""
    def __init__(self):
        self.cpu = CPU()
        self.memory = Memory()
        self.hard_drive = HardDrive()
    
    def start(self):
        """Simplified start process"""
        self.cpu.freeze()
        boot_data = self.hard_drive.read(0, 1024)
        self.memory.load(0, boot_data)
        self.cpu.jump(0)
        self.cpu.execute()
        print("Computer started!")

# =============================================================================
# BEHAVIORAL PATTERNS
# =============================================================================

# 1. OBSERVER PATTERN
class Subject:
    """Subject that notifies observers"""
    def __init__(self):
        self._observers = []
        self._state = None
    
    def attach(self, observer):
        self._observers.append(observer)
    
    def detach(self, observer):
        self._observers.remove(observer)
    
    def notify(self):
        for observer in self._observers:
            observer.update(self._state)
    
    def set_state(self, state):
        self._state = state
        self.notify()

class Observer:
    """Observer interface"""
    def update(self, state):
        pass

class ConcreteObserver(Observer):
    def __init__(self, name):
        self.name = name
    
    def update(self, state):
        print(f"{self.name} received update: {state}")

# Usage
subject = Subject()
observer1 = ConcreteObserver("Observer 1")
observer2 = ConcreteObserver("Observer 2")

subject.attach(observer1)
subject.attach(observer2)
subject.set_state("New State")

# 2. STRATEGY PATTERN
class PaymentStrategy:
    """Strategy interface"""
    def pay(self, amount):
        pass

class CreditCardPayment(PaymentStrategy):
    def __init__(self, card_number):
        self.card_number = card_number
    
    def pay(self, amount):
        return f"Paid ${amount} using Credit Card {self.card_number[-4:]}"

class PayPalPayment(PaymentStrategy):
    def __init__(self, email):
        self.email = email
    
    def pay(self, amount):
        return f"Paid ${amount} using PayPal account {self.email}"

class ShoppingCart:
    def __init__(self):
        self.items = []
        self.payment_strategy = None
    
    def add_item(self, item):
        self.items.append(item)
    
    def set_payment_strategy(self, strategy):
        self.payment_strategy = strategy
    
    def checkout(self):
        total = sum(item['price'] for item in self.items)
        if self.payment_strategy:
            return self.payment_strategy.pay(total)
        else:
            return "No payment method selected"

# Usage
cart = ShoppingCart()
cart.add_item({'name': 'Book', 'price': 20})
cart.add_item({'name': 'Pen', 'price': 5})

cart.set_payment_strategy(CreditCardPayment("1234567890123456"))
print(cart.checkout())

# 3. COMMAND PATTERN
class Command:
    """Command interface"""
    def execute(self):
        pass
    
    def undo(self):
        pass

class Light:
    def __init__(self):
        self.is_on = False
    
    def turn_on(self):
        self.is_on = True
        return "Light is ON"
    
    def turn_off(self):
        self.is_on = False
        return "Light is OFF"

class LightOnCommand(Command):
    def __init__(self, light):
        self.light = light
    
    def execute(self):
        return self.light.turn_on()
    
    def undo(self):
        return self.light.turn_off()

class LightOffCommand(Command):
    def __init__(self, light):
        self.light = light
    
    def execute(self):
        return self.light.turn_off()
    
    def undo(self):
        return self.light.turn_on()

class RemoteControl:
    def __init__(self):
        self.command = None
        self.last_command = None
    
    def set_command(self, command):
        self.command = command
    
    def press_button(self):
        if self.command:
            result = self.command.execute()
            self.last_command = self.command
            return result
    
    def press_undo(self):
        if self.last_command:
            return self.last_command.undo()

# Usage
light = Light()
light_on = LightOnCommand(light)
light_off = LightOffCommand(light)
remote = RemoteControl()

remote.set_command(light_on)
print(remote.press_button())  # "Light is ON"
print(remote.press_undo())    # "Light is OFF"

# 4. TEMPLATE METHOD PATTERN
class DataProcessor:
    """Template method pattern"""
    
    def process(self):
        """Template method defining the algorithm structure"""
        self.load_data()
        self.process_data()
        self.save_data()
    
    def load_data(self):
        """Abstract method - must be implemented by subclasses"""
        raise NotImplementedError
    
    def process_data(self):
        """Abstract method - must be implemented by subclasses"""
        raise NotImplementedError
    
    def save_data(self):
        """Concrete method with default implementation"""
        print("Data saved to default location")

class CSVProcessor(DataProcessor):
    def load_data(self):
        print("Loading CSV data")
    
    def process_data(self):
        print("Processing CSV data")

class JSONProcessor(DataProcessor):
    def load_data(self):
        print("Loading JSON data")
    
    def process_data(self):
        print("Processing JSON data")
    
    def save_data(self):
        print("Saving JSON data with custom format")

# =============================================================================
# COMMON PROGRAMMING PATTERNS
# =============================================================================

# 1. NULL OBJECT PATTERN
class Logger:
    def log(self, message):
        pass

class FileLogger(Logger):
    def __init__(self, filename):
        self.filename = filename
    
    def log(self, message):
        print(f"Logging to {self.filename}: {message}")

class NullLogger(Logger):
    """Null object - does nothing"""
    def log(self, message):
        pass  # Do nothing

class Application:
    def __init__(self, logger=None):
        self.logger = logger or NullLogger()
    
    def do_something(self):
        self.logger.log("Something happened")

# 2. FLUENT INTERFACE (METHOD CHAINING)
class QueryBuilder:
    def __init__(self):
        self.query = ""
    
    def select(self, fields):
        self.query += f"SELECT {fields} "
        return self
    
    def from_table(self, table):
        self.query += f"FROM {table} "
        return self
    
    def where(self, condition):
        self.query += f"WHERE {condition} "
        return self
    
    def order_by(self, field):
        self.query += f"ORDER BY {field} "
        return self
    
    def build(self):
        return self.query.strip()

# Usage
query = (QueryBuilder()
         .select("name, email")
         .from_table("users")
         .where("age > 18")
         .order_by("name")
         .build())

# 3. REGISTRY PATTERN
class ComponentRegistry:
    """Registry for managing components"""
    
    _registry = {}
    
    @classmethod
    def register(cls, name, component):
        cls._registry[name] = component
    
    @classmethod
    def get(cls, name):
        return cls._registry.get(name)
    
    @classmethod
    def list_all(cls):
        return list(cls._registry.keys())

# Decorator for automatic registration
def register_component(name):
    def decorator(cls):
        ComponentRegistry.register(name, cls)
        return cls
    return decorator

@register_component("button")
class Button:
    def render(self):
        return "<button>Click me</button>"

@register_component("input")
class Input:
    def render(self):
        return "<input type='text' />"

# Usage
button_class = ComponentRegistry.get("button")
if button_class:
    button = button_class()
    print(button.render())

# =============================================================================
# DESIGN PATTERN BEST PRACTICES
# =============================================================================

"""
1. Don't overuse patterns - use them when they solve actual problems
2. Prefer composition over inheritance
3. Program to interfaces, not implementations
4. Single Responsibility Principle - each class should have one reason to change
5. Open/Closed Principle - open for extension, closed for modification
6. Dependency Inversion - depend on abstractions, not concretions

When to use each pattern:
- Singleton: When exactly one instance is needed (database connections, loggers)
- Factory: When object creation logic is complex or varies
- Observer: When changes to one object require updating multiple objects
- Strategy: When you have multiple ways to perform a task
- Decorator: When you need to add behavior to objects dynamically
- Command: When you need to queue, log, or undo operations
- Template Method: When you have a common algorithm structure with varying steps
"""