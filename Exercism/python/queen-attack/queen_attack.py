class Queen(object):
    def __init__(self, row, column):
        if row < 0 or row > 7 or column < 0 or column > 7:
            raise ValueError("Invalid position for queen")

        self.row = row
        self.column = column

    def can_attack(self, another_queen):
        if self.row == another_queen.row and self.column == another_queen.column:
            raise ValueError("Queens cannot be on top of each other")

        row_diff = abs(self.row - another_queen.row)
        column_diff = abs(self.column - another_queen.column)

        return row_diff == 0 or column_diff == 0 or row_diff == column_diff
