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
 */

const Utils = require('./utils.js');

// The pattern to calculate the spiral is:
// Right 1, Up 1, Left 2, Down 2, Right 3, Up 3, Left 4, Down 4, Right 5...
function part1(input) {
    let curX = 0;
    let curY = 0;

    let curDir = 'R';
    let curNum = 1;
    let remaining = curNum;
    while (--input > 0) {
        if (remaining === 0) {
            switch (curDir) {
                case 'R':
                    curDir = 'U';
                    break;
                case 'U':
                    curDir = 'L';
                    curNum++;
                    break;
                case 'L':
                    curDir = 'D';
                    break;
                case 'D':
                    curDir = 'R';
                    curNum++;
                    break;
                default:
                    throw Error('Invalid direction');
            }

            remaining = curNum;
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

        remaining--;
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

runPart1();
