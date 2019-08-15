def digit_sum(num):
    result = 0

    while num > 0:
        result += num % 10
        num //= 10

    return result


max_sum = 0

for a in range(1, 100):
    for b in range(1, 100):
        value = digit_sum(a ** b)

        if value > max_sum:
            max_sum = value

print(max_sum)
