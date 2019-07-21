import re


def parse(markdown):
    res = ""
    in_list = False
    in_list_append = False
    for line in markdown.split("\n"):
        line = apply_heading(line)
        m = re.match(r"\* (.*)", line)
        if m:
            cur = m.group(1)
            cur = apply_bold(cur)
            cur = apply_italic(cur)

            line = wrap_tag("li", cur)

            if not in_list:
                in_list = True

                line = "<ul>" + line
        else:
            if in_list:
                in_list_append = True
                in_list = False

        m = re.match("<h|<ul|<p|<li", line)
        if not m:
            line = wrap_tag("p", line)
        line = apply_bold(line)
        line = apply_italic(line)
        if in_list_append:
            line = "</ul>" + line
            in_list_append = False
        res += line
    if in_list:
        res += "</ul>"
    return res


def apply_heading(value):
    match = re.match("(#{1,6}) (.*)", value)
    if match is None:
        return value

    length = str(len(match.group(1)))
    return wrap_tag("h" + length, match.group(2))


def apply_bold(value):
    match = re.match("(.*)__(.*)__(.*)", value)
    if not match:
        return value

    return match.group(1) + wrap_tag("strong", match.group(2)) + match.group(3)


def apply_italic(value):
    match = re.match("(.*)_(.*)_(.*)", value)
    if not match:
        return value

    return match.group(1) + wrap_tag("em", match.group(2)) + match.group(3)


def wrap_tag(tag, text):
    return "<{}>{}</{}>".format(tag, text, tag)