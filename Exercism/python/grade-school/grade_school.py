import collections


class School(object):
    def __init__(self):
        self.enrolled = collections.defaultdict(list)

    def add_student(self, name, grade):
        self.enrolled[grade].append(name)
        self.enrolled[grade].sort()

    def roster(self):
        return [name
                for grade, names in sorted(self.enrolled.items())
                for name in sorted(names)
            ]

    def grade(self, grade_number):
        return self.enrolled.get(grade_number, [])
