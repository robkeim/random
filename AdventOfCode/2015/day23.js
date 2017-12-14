/*
--- Day 23: Opening the Turing Lock ---

Little Jane Marie just got her very first computer for Christmas from some unknown benefactor. It comes with instructions and an example program, but the computer itself seems to be malfunctioning. She's curious what the program does, and would like you to help her run it.

The manual explains that the computer supports two registers and six instructions (truly, it goes on to remind the reader, a state-of-the-art technology). The registers are named a and b, can hold any non-negative integer, and begin with a value of 0. The instructions are as follows:
= hlf r sets register r to half its current value, then continues with the next instruction.
= tpl r sets register r to triple its current value, then continues with the next instruction.
= inc r increments register r, adding 1 to it, then continues with the next instruction.
= jmp offset is a jump; it continues with the instruction offset away relative to itself.
= jie r, offset is like jmp, but only jumps if register r is even ("jump if even").
= jio r, offset is like jmp, but only jumps if register r is 1 ("jump if one", not odd).

All three jump instructions work with an offset relative to that instruction. The offset is always written with a prefix + or - to indicate the direction of the jump (forward or backward, respectively). For example, jmp +1 would simply continue with the next instruction, while jmp +0 would continuously jump back to itself forever.

The program exits when it tries to run an instruction beyond the ones defined.

For example, this program sets a to 2, because the jio instruction causes it to skip the tpl instruction:

inc a
jio a, +2
tpl a
inc a
What is the value in register b when the program in your puzzle input is finished executing?

--- Part Two ---

The unknown benefactor is very thankful for releasi-- er, helping little Jane Marie with her computer. Definitely not to distract you, what is the value in register b after the program is finished executing if register a starts as 1 instead?
 */

const Utils = require('./utils.js');

function part1(input) {
    return execute(input, 0, 0);
}

function part2(input) {
    return execute(input, 1, 0);
}

function execute(input, a, b) {
    const singleArgRegex = /(hlf|tpl|inc|jmp) \+?([\-ab0-9]+)/;
    const doubleArgRegex = /(jie|jio) ([ab]), \+?([\-0-9]+)/;

    let lines = input.split('\n');
    let index = 0;

    while (index < lines.length) {
        let match = singleArgRegex.exec(lines[index]);

        if (!match) {
            match = doubleArgRegex.exec(lines[index]);
        }

        if (!match) {
            throw Error('Invalid line format: ' + lines[index]);
        }

        let needToIncrement = true;
        let value = 0;

        switch (match[1]) {
            case 'hlf':
                if (match[2] === 'a') {
                    a /= 2;
                } else if (match[2] === 'b') {
                    b /= 2;
                } else {
                    throw Error('Invalid line: ' + lines[index]);
                }
                break;
            case 'tpl':
                if (match[2] === 'a') {
                    a *= 3;
                } else if (match[2] === 'b') {
                    b *= 3;
                } else {
                    throw Error('Invalid line: ' + lines[index]);
                }
                break;
            case 'inc':
                if (match[2] === 'a') {
                    a++;
                } else if (match[2] === 'b') {
                    b++;
                } else {
                    throw Error('Invalid line: ' + lines[index]);
                }
                break;
            case 'jmp':
                needToIncrement = false;
                index += parseInt(match[2]);
                break;
            case 'jio':
                if (match[2] === 'a') {
                    value = a;
                } else if (match[2] === 'b') {
                    value = b;
                } else {
                    throw Error('Invalid line: ' + lines[index]);
                }

                if (value === 1) {
                    needToIncrement = false;
                    index += parseInt(match[3]);
                }
                break;
            case 'jie':
                if (match[2] === 'a') {
                    value = a;
                } else if (match[2] === 'b') {
                    value = b;
                } else {
                    throw Error('Invalid line: ' + lines[index]);
                }

                if (value % 2 === 0) {
                    needToIncrement = false;
                    index += parseInt(match[3]);
                }
                break;
            default:
                throw Error('Invalid instruction: ' + match[1]);
        }

        if (needToIncrement) {
            index++;
        }
    }

    return b;
}

function runPart1() {
    Utils.assertAreEqual(2, part1('inc b\njio b, +2\ntpl b\ninc b'));

    // Answer: 170
    console.log(part1('jio a, +16\ninc a\ninc a\ntpl a\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ntpl a\ntpl a\ninc a\njmp +23\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ntpl a\ntpl a\ninc a\njio a, +8\ninc b\njie a, +4\ntpl a\ninc a\njmp +2\nhlf a\njmp -7'));
}

function runPart2() {
    // Answer: 247
    console.log(part2('jio a, +16\ninc a\ninc a\ntpl a\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ntpl a\ntpl a\ninc a\njmp +23\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ntpl a\ntpl a\ninc a\njio a, +8\ninc b\njie a, +4\ntpl a\ninc a\njmp +2\nhlf a\njmp -7'));
}

runPart1();
runPart2();
