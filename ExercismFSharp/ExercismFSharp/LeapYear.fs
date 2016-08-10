module LeapYear

let isLeapYear year =
    year % 4 = 0 && ((not (year % 100 = 0)) || year % 400 = 0)
