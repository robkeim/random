class Clock(object):
    def __init__(self, hours, minutes):
        self.hours, self.minutes = divmod((hours * 60 + minutes) % (24 * 60), 60)

    def __repr__(self):
        return "{:02d}:{:02d}".format(self.hours, self.minutes)

    def __eq__(self, other):
        return self.hours == other.hours and self.minutes == other.minutes

    def __add__(self, minutes):
        return Clock(self.hours, self.minutes + minutes)

    def __sub__(self, minutes):
        return Clock(self.hours, self.minutes - minutes)
