class Luhn(object):
    def __init__(self, card_num):
        card_num = str(card_num)[::-1]
        card_num = card_num.replace(" ", "")

        if not card_num.isdigit() or card_num == "0":
            self.is_valid = False
            return

        result = 0

        for i in range(0, len(card_num)):
            if i % 2 == 1:
                double = int(card_num[i]) * 2
                if double > 9:
                    double -= 9
                result += double
            else:
                result += int(card_num[i])

        self.is_valid = result % 10 == 0

    def valid(self):
        return self.is_valid
