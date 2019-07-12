# Game status categories
# Change the values as you see fit
STATUS_WIN = "win"
STATUS_LOSE = "lose"
STATUS_ONGOING = "ongoing"


class Hangman(object):
    def __init__(self, word):
        self.word = word
        self.masked_word = "_" * len(word)
        self.guesses = set()
        self.remaining_guesses = 9
        self.status = STATUS_ONGOING

    def guess(self, char):
        if self.status != STATUS_ONGOING:
            raise ValueError("Unable to guess on a finished game")

        found = False
        tmp = list(self.masked_word)
        for i in range(0, len(self.word)):
            if self.word[i] == char:
                tmp[i] = char
                found = True

        self.masked_word = "".join(tmp)

        if not found or char in self.guesses:
            self.remaining_guesses -= 1

        self.guesses.add(char)

        if "_" not in self.masked_word:
            self.status = STATUS_WIN
        elif self.remaining_guesses < 0:
            self.status = STATUS_LOSE

    def get_masked_word(self):
        return self.masked_word

    def get_status(self):
        return self.status
