def hey(phrase):
    phrase = phrase.strip()

    if not phrase:
        return "Fine. Be that way!"

    if str.isupper(phrase):
        if phrase.endswith("?"):
            return "Calm down, I know what I'm doing!"
        else:
            return "Whoa, chill out!"

    if phrase.endswith("?"):
        return "Sure."

    return "Whatever."
