class School(object):
    def __init__(self):
        self.enrolled = {}

    def add_student(self, name, grade):
        if grade not in self.enrolled:
            self.enrolled[grade] = [name]
        else:
            self.enrolled[grade].append(name)
            self.enrolled[grade].sort()

    def roster(self):
        keys = list(self.enrolled.keys())
        keys.sort()

        result = []

        for key in keys:
            result += self.enrolled[key]

        return result

    def grade(self, grade_number):
        if grade_number in self.enrolled:
            return self.enrolled[grade_number]
        else:
            return []
