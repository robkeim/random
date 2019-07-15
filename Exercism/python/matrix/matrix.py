class Matrix(object):
    def __init__(self, matrix_string):
        self.rows = []

        for row in matrix_string.splitlines():
            self.rows.append(list(map(int, row.split())))

        self.cols = []

        for i in range(0, len(self.rows[0])):
            self.cols.append([])
            for j in range(0, len(self.rows)):
                self.cols[i].append(self.rows[j][i])

    def row(self, index):
        return self.rows[index - 1]

    def column(self, index):
        return self.cols[index - 1]
