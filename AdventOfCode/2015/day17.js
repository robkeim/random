/*
--- Day 17: No Such Thing as Too Much ---

The elves bought too much eggnog again - 150 liters this time. To fit it all into your refrigerator, you'll need to move it into smaller containers. You take an inventory of the capacities of the available containers.

For example, suppose you have containers of size 20, 15, 10, 5, and 5 liters. If you need to store 25 liters, there are four ways to do it:
- 15 and 10
- 20 and 5 (the first 5)
- 20 and 5 (the second 5)
- 15, 5, and 5

Filling all containers entirely, how many different combinations of containers can exactly fit all 150 liters of eggnog?

--- Part Two ---

While playing with all the containers in the kitchen, another load of eggnog arrives! The shipping and receiving department is requesting as many containers as you can spare.

Find the minimum number of containers that can exactly fit all 150 liters of eggnog. How many different ways can you fill that number of containers and still hold exactly 150 litres?

In the example above, the minimum number of containers was two. There were three ways to use that many containers, and so the answer there would be 3.
 */

const Utils = require('./utils.js');

function part1(numLiters, input) {
    const jars = input.split('\n').map(v => parseInt(v));

    let combinations = getCombinations(jars.length);
    let result = 0;

    for (let i = 0; i < combinations.length; i++) {
        let total = 0;

        for (let j = 0; j < jars.length; j++) {
            if (combinations[i][j]) {
                total += jars[j];
            }
        }

        if (total === numLiters) {
            result++;
        }
    }

    return result;
}

function getCombinations(numDigits) {
    let combinations = [];
    for(let i = 0; i < (1 << numDigits); i++) {
        let combination = [];
        for(let j = 0; j < numDigits; j++) {
            combination.push(i & (1 << j) ? 1 : 0);
        }
        combinations.push(combination);
    }
    return combinations;
}

function runPart1() {
    Utils.assertAreEqual(4, part1(25, '20\n15\n10\n5\n5'));

    // Answer: 654
    console.log(part1(150, '50\n44\n11\n49\n42\n46\n18\n32\n26\n40\n21\n7\n18\n43\n10\n47\n36\n24\n22\n40'));
}

function part2(numLiters, input) {
    const jars = input.split('\n').map(v => parseInt(v));

    let combinations = getCombinations(jars.length);
    let result = 0;
    let minNumJars = Number.MAX_SAFE_INTEGER;

    for (let i = 0; i < combinations.length; i++) {
        let total = 0;

        for (let j = 0; j < jars.length; j++) {
            if (combinations[i][j]) {
                total += jars[j];
            }
        }

        if (total === numLiters) {
            let numJars = combinations[i].filter(v => v === 1).length;

            if (numJars < minNumJars) {
                minNumJars = numJars;
                result = 1;
            } else if (numJars === minNumJars) {
                result++;
            }
        }
    }

    return result;
}

function runPart2() {
    Utils.assertAreEqual(3, part2(25, '20\n15\n10\n5\n5'));

    // Answer: 57
    console.log(part2(150, '50\n44\n11\n49\n42\n46\n18\n32\n26\n40\n21\n7\n18\n43\n10\n47\n36\n24\n22\n40'));
}

runPart1();
runPart2();
