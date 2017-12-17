/*
--- Day 9: All in a Single Night ---
Every year, Santa manages to deliver all of his presents in a single night.

This year, however, he has some new locations to visit; his elves have provided him the distances between every pair of locations. He can start and end at any two (different) locations he wants, but he must visit each location exactly once. What is the shortest distance he can travel to achieve this?

For example, given the following distances:
London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141

The possible routes are therefore:
Dublin -> London -> Belfast = 982
London -> Dublin -> Belfast = 605
London -> Belfast -> Dublin = 659
Dublin -> Belfast -> London = 659
Belfast -> Dublin -> London = 605
Belfast -> London -> Dublin = 982

The shortest of these is London -> Dublin -> Belfast = 605, and so the answer is 605 in this example.

What is the distance of the shortest route?

--- Part Two ---
The next year, just to show off, Santa decides to take the route with the longest distance instead.

He can still start and end at any two (different) locations he wants, and he still must visit each location exactly once.

For example, given the distances above, the longest route would be 982 via (for example) Dublin -> London -> Belfast.

What is the distance of the longest route?
 */
const Utils = require('./utils.js');

function part1(input) {
    return calculate(input, false);
}

function part2(input) {
    return calculate(input, true);
}

function calculate(input, useLongest) {
    const regex = /([a-zA-Z]+) to ([a-zA-Z]+) = (\d+)/;
    let lines = input.split('\n');
    let cities = new Set();
    let distances = {};

    for (let i = 0; i < lines.length; i++) {
        let match = regex.exec(lines[i]);

        if (!match) {
            throw Error('Invalid line format: ' + lines[i]);
        }

        let distance = parseInt(match[3]);
        distances[match[1] + '_' + match[2]] = distance;
        distances[match[2] + '_' + match[1]] = distance;
        cities.add(match[1]);
        cities.add(match[2]);
    }

    let permutations = Utils.permutations(Array.from(cities.keys()));
    let result = useLongest ? Number.MIN_SAFE_INTEGER : Number.MAX_SAFE_INTEGER;

    for (let i = 0; i < permutations.length; i++) {
        let distance = 0;

        for (let j = 0; j < permutations[i].length - 1; j++) {
            distance += distances[permutations[i][j] + '_' + permutations[i][j + 1]] || 0;
        }

        if (!useLongest && distance < result) {
            result = distance;
        }

        if (useLongest && distance > result) {
            result = distance;
        }
    }

    return result;
}

function runPart1() {
    Utils.assertAreEqual(605, part1('London to Dublin = 464\nLondon to Belfast = 518\nDublin to Belfast = 141'));

    // Answer: 251
    console.log(part1('Tristram to AlphaCentauri = 34\nTristram to Snowdin = 100\nTristram to Tambi = 63\nTristram to Faerun = 108\nTristram to Norrath = 111\nTristram to Straylight = 89\nTristram to Arbre = 132\nAlphaCentauri to Snowdin = 4\nAlphaCentauri to Tambi = 79\nAlphaCentauri to Faerun = 44\nAlphaCentauri to Norrath = 147\nAlphaCentauri to Straylight = 133\nAlphaCentauri to Arbre = 74\nSnowdin to Tambi = 105\nSnowdin to Faerun = 95\nSnowdin to Norrath = 48\nSnowdin to Straylight = 88\nSnowdin to Arbre = 7\nTambi to Faerun = 68\nTambi to Norrath = 134\nTambi to Straylight = 107\nTambi to Arbre = 40\nFaerun to Norrath = 11\nFaerun to Straylight = 66\nFaerun to Arbre = 144\nNorrath to Straylight = 115\nNorrath to Arbre = 135\nStraylight to Arbre = 127'));
}

function runPart2() {
    Utils.assertAreEqual(982, part2('London to Dublin = 464\nLondon to Belfast = 518\nDublin to Belfast = 141'));

    // Answer: 898
    console.log(part2('Tristram to AlphaCentauri = 34\nTristram to Snowdin = 100\nTristram to Tambi = 63\nTristram to Faerun = 108\nTristram to Norrath = 111\nTristram to Straylight = 89\nTristram to Arbre = 132\nAlphaCentauri to Snowdin = 4\nAlphaCentauri to Tambi = 79\nAlphaCentauri to Faerun = 44\nAlphaCentauri to Norrath = 147\nAlphaCentauri to Straylight = 133\nAlphaCentauri to Arbre = 74\nSnowdin to Tambi = 105\nSnowdin to Faerun = 95\nSnowdin to Norrath = 48\nSnowdin to Straylight = 88\nSnowdin to Arbre = 7\nTambi to Faerun = 68\nTambi to Norrath = 134\nTambi to Straylight = 107\nTambi to Arbre = 40\nFaerun to Norrath = 11\nFaerun to Straylight = 66\nFaerun to Arbre = 144\nNorrath to Straylight = 115\nNorrath to Arbre = 135\nStraylight to Arbre = 127'));
}

runPart1();
runPart2();
