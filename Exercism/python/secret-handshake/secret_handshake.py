def commands(number):
    codes = [
        (1, "wink"),
        (2, "double blink"),
        (4, "close your eyes"),
        (8, "jump"),
    ]

    result = []

    for value, action in codes:
        if number & value != 0:
            result.append(action)

    if number & 16 != 0:
        result.reverse()

    return result


def secret_code(actions):
    codes = {
        "wink": 1,
        "double blink": 2,
        "close your eyes": 4,
        "jump": 8
    }

    result = 0

    for action in actions:
        result |= codes[action]

    reverse = False
    for i in range(0, len(actions) - 1):
        if codes[actions[i]] > codes[actions[i + 1]]:
            reverse = True
            break

    if reverse:
        result |= 16

    return result
