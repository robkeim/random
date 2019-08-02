class Matrix(object):
    def __init__(self, matrix_string):
        self.rows = [list(map(int, row.split())) for row in matrix_string.splitlines()]
        self.cols = [[self.rows[j][i] for j in range(0, len(self.rows))] for i in range(0, len(self.rows[0]))]

    def row(self, index):
        return list(self.rows[index - 1])

    def column(self, index):
        return list(self.cols[index - 1])
