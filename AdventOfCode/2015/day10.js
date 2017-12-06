/*
--- Day 10: Elves Look, Elves Say ---

Today, the Elves are playing a game called look-and-say. They take turns making sequences by reading aloud the previous sequence and using that reading as the next sequence. For example, 211 is read as "one two, two ones", which becomes 1221 (1 2, 2 1s).

Look-and-say sequences are generated iteratively, using the previous value as input for the next step. For each step, take the previous value, and replace each run of digits (like 111) with the number of digits (3) followed by the digit itself (1).

For example:
- 1 becomes 11 (1 copy of digit 1).
- 11 becomes 21 (2 copies of digit 1).
- 21 becomes 1211 (one 2 followed by one 1).
- 1211 becomes 111221 (one 1, one 2, and two 1s).
- 111221 becomes 312211 (three 1s, two 2s, and one 1).

Starting with the digits in your puzzle input, apply this process 40 times. What is the length of the result?

--- Part Two ---

Neat, right? You might also enjoy hearing John Conway talking about this sequence (that's Conway of Conway's Game of Life fame).

Now, starting again with the digits in your puzzle input, apply this process 50 times. What is the length of the new result?
 */

const Utils = require('./utils.js');

function encodeSequence(input) {
    let result = '';

    let prevChar = input[0];
    let total = 0;
    let index = 0;

    while (index < input.length) {
        if (input[index] === prevChar) {
            total++;
        } else {
            result += total + prevChar;
            total = 1;
            prevChar = input[index];
        }

        index++;
    }

    result += total + prevChar;

    return result;
}

function part1(input) {
    let result = input;
    for (let i = 0; i < 40; i++) {
        result = encodeSequence(result);
    }

    return result.length;
}

function runPart1() {
    Utils.assertAreEqual('11', encodeSequence('1'));
    Utils.assertAreEqual('21', encodeSequence('11'));
    Utils.assertAreEqual('1211', encodeSequence('21'));
    Utils.assertAreEqual('111221', encodeSequence('1211'));
    Utils.assertAreEqual('312211', encodeSequence('111221'));

    // Answer: 252594
    console.log(part1('1113222113'));
}

function part2(input) {
    let result = input;
    for (let i = 0; i < 50; i++) {
        result = encodeSequence(result);
    }

    return result.length;
}

function runPart2() {
    // Answer: XXX
    console.log(part2('1113222113'));
}

runPart1();
runPart2();
