/*
--- Day 3: Spiral Memory ---

You come across an experimental new kind of memory stored on an infinite two-dimensional grid.

Each square on the grid is allocated in a spiral pattern starting at a location marked 1 and then counting up while spiraling outward. For example, the first few squares are allocated like this:

17  16  15  14  13
18   5   4   3  12
19   6   1   2  11
20   7   8   9  10
21  22  23---> ...

While this is very space-efficient (no squares are skipped), requested data must be carried back to square 1 (the location of the only access port for this memory system) by programs that can only move up, down, left, or right. They always take the shortest path: the Manhattan Distance between the location of the data and square 1.

For example:
- Data from square 1 is carried 0 steps, since it's at the access port.
- Data from square 12 is carried 3 steps, such as: down, left, left.
- Data from square 23 is carried only 2 steps: up twice.
- Data from square 1024 must be carried 31 steps.

How many steps are required to carry the data from the square identified in your puzzle input all the way to the access port?

 --- Part Two ---

As a stress test on the system, the programs here clear the grid and then store the value 1 in square 1. Then, in the same allocation order as shown above, they store the sum of the values in all adjacent squares, including diagonals.

So, the first few squares' values are chosen as follows:
- Square 1 starts with the value 1.
- Square 2 has only one adjacent filled square (with value 1), so it also stores 1.
- Square 3 has both of the above squares as neighbors and stores the sum of their values, 2.
- Square 4 has all three of the aforementioned squares as neighbors and stores the sum of their values, 4.
- Square 5 only has the first and fourth squares as neighbors, so it gets the value 5.

Once a square is written, its value does not change. Therefore, the first few squares would receive the following values:
147  142  133  122   59
304    5    4    2   57
330   10    1    1   54
351   11   23   25   26
362  747  806--->   ...

What is the first value written that is larger than your puzzle input?
 */

const Utils = require('./utils.js');

// The pattern to calculate the spiral is:
// Right 1, Up 1, Left 2, Down 2, Right 3, Up 3, Left 4, Down 4, Right 5...
function part1(input) {
    let curX = 0;
    let curY = 0;

    let curDir = 'R';
    let numInCurDirection = 1;
    let remainingInCurDirection = numInCurDirection;
    while (--input > 0) {
        if (remainingInCurDirection === 0) {
            switch (curDir) {
                case 'R':
                    curDir = 'U';
                    break;
                case 'U':
                    curDir = 'L';
                    numInCurDirection++;
                    break;
                case 'L':
                    curDir = 'D';
                    break;
                case 'D':
                    curDir = 'R';
                    numInCurDirection++;
                    break;
                default:
                    throw Error('Invalid direction');
            }

            remainingInCurDirection = numInCurDirection;
        }

        switch (curDir) {
            case 'R':
                curX++;
                break;
            case 'U':
                curY++;
                break;
            case 'L':
                curX--;
                break;
            case 'D':
                curY--;
                break;
            default:
                throw Error('Invalid direction');
        }

        remainingInCurDirection--;
    }

    return Math.abs(curX) + Math.abs(curY);
}

function runPart1() {
    Utils.assertAreEqual(0, part1(1));
    Utils.assertAreEqual(3, part1(12));
    Utils.assertAreEqual(2, part1(23));
    Utils.assertAreEqual(31, part1(1024));

    // Answer: 438
    console.log(part1(265149));
}

function part2(input) {
    let curX = 0;
    let curY = 0;

    let squareNum = 2;
    let squares = new Set();
    squares['0_0'] = 1;
    let curDir = 'R';
    let numInCurDirection = 1;
    let remainingInCurDirection = numInCurDirection;
    while (true) {
        if (remainingInCurDirection === 0) {
            switch (curDir) {
                case 'R':
                    curDir = 'U';
                    break;
                case 'U':
                    curDir = 'L';
                    numInCurDirection++;
                    break;
                case 'L':
                    curDir = 'D';
                    break;
                case 'D':
                    curDir = 'R';
                    numInCurDirection++;
                    break;
                default:
                    throw Error('Invalid direction');
            }

            remainingInCurDirection = numInCurDirection;
        }

        switch (curDir) {
            case 'R':
                curX++;
                break;
            case 'U':
                curY++;
                break;
            case 'L':
                curX--;
                break;
            case 'D':
                curY--;
                break;
            default:
                throw Error('Invalid direction');
        }

        remainingInCurDirection--;

        let sumOfNeighbors = 0;

        for (let i = -1; i <= 1; i++) {
            for (let j = -1; j <= 1; j++) {
                const key = (curX + i) + '_' + (curY + j);
                const value = squares[key];

                if (value !== undefined) {
                    sumOfNeighbors += squares[key];
                }
            }
        }

        if (sumOfNeighbors > input) {
            return sumOfNeighbors;
        }

        squares[curX + '_' + curY] = sumOfNeighbors;
        squareNum++;
    }
}

function runPart2() {
    // Answer: 266330
    console.log(part2(265149));
}

runPart1();
runPart2();
