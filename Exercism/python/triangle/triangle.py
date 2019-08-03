def equilateral(sides):
    return sides[0] > 0 and sides[0] == sides[1] and sides[1] == sides[2]


def isosceles(sides):
    return test_triangle_inequality(sides) and (sides[0] == sides[1] or sides[0] == sides[2] or sides[1] == sides[2])


def scalene(sides):
    return test_triangle_inequality(sides) and not isosceles(sides)


def test_triangle_inequality(sides):
    max_side = max(sides)
    return max_side <= sum(sides) - max_side