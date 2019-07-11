import random

class Robot(object):
    __used_names = set()
    
    def __init__(self):
        self.reset()
    
    def reset(self):
        self.name = self.__get_new_name()
    
    def __get_new_name(self):
        name = self.__get_new_name_helper()
        while name in self.__used_names:
            name = self.__get_new_name_helper()
            
        self.__used_names.add(name)
        
        return name
    
    def __get_new_name_helper(self):
        letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        numbers = "0123456789"
        
        return random.choice(letters) + random.choice(letters) \
            + random.choice(numbers) + random.choice(numbers) + random.choice(numbers)
