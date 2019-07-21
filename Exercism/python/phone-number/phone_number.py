import re


class Phone(object):
    def __init__(self, phone_number):
        phone_number = re.sub("\D", "", phone_number)
        match = re.match("^1?(([2-9][0-9]{2})([2-9][0-9]{2})([0-9]{4}))$", phone_number)

        if not match:
            raise ValueError("Invalid telephone number")

        self.number = match.group(1)
        self.area_code = match.group(2)
        self.pretty_format = "({}) {}-{}".format(match.group(2), match.group(3), match.group(4))

    def pretty(self):
        return self.pretty_format
