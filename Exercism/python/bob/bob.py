def hey(phrase):
    phrase = phrase.strip()

    if len(phrase) == 0:
        return "Fine. Be that way!"

    letters = filter(lambda x: x.isalpha(), phrase)

    if len(letters) > 0 and all(letter.isupper() for letter in letters):
        if phrase.endswith("?"):
            return "Calm down, I know what I'm doing!"
        else:
            return "Whoa, chill out!"

    if phrase.endswith("?"):
        return "Sure."

    return "Whatever."
