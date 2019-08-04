def is_paired(input_string):
    stack = []

    for char in input_string:
        if char == "[" or char == "{" or char == "(":
            stack.append(char)
        elif char == "]" or char == "}" or char == ")":
            if len(stack) == 0 or stack[-1] != get_opening(char):
                return False
            else:
                stack = stack[:-1]

    return len(stack) == 0


def get_opening(char):
    if char == "]":
        return "["
    elif char == "}":
        return "{"
    elif char == ")":
        return "("
    else:
        raise Exception("Invalid closing bracket")