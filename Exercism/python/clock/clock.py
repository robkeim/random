class Clock(object):
    def __init__(self, hours, minutes):
        hours += minutes // 60
        minutes = minutes % 60

        hours %= 24

        if hours < 0:
            hours += 24

        self.hours = hours
        self.minutes = minutes

    def __repr__(self):
        return "{:02d}:{:02d}".format(self.hours, self.minutes)

    def __eq__(self, other):
        return self.hours == other.hours and self.minutes == other.minutes

    def __add__(self, minutes):
        return Clock(self.hours, self.minutes + minutes)

    def __sub__(self, minutes):
        return Clock(self.hours, self.minutes - minutes)
