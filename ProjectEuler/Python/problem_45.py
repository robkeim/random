def t_fn(n):
    return n * (n + 1) / 2


def p_fn(n):
    return n * (3 * n - 1) / 2


def h_fn(n):
    return n * (2 * n - 1)


t = 286
p = 165
h = 143

while t_fn(t) != p_fn(p) or t_fn(t) != h_fn(h):
    t += 1

    while p_fn(p) < t_fn(t):
        p += 1

    while h_fn(h) < t_fn(t):
        h += 1

print(t_fn(t))
