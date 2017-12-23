/*
--- Day 23: Coprocessor Conflagration ---
You decide to head directly to the CPU and fix the printer from there. As you get close, you find an experimental coprocessor doing so much work that the local programs are afraid it will halt and catch fire. This would cause serious issues for the rest of the computer, so you head in and see what you can do.

The code it's running seems to be a variant of the kind you saw recently on that tablet. The general functionality seems very similar, but some of the instructions are different:
- set X Y sets register X to the value of Y.
- sub X Y decreases register X by the value of Y.
- mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
- jnz X Y jumps with an offset of the value of Y, but only if the value of X is not zero. (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)

Only the instructions listed above are used. The eight registers here, named a through h, all start at 0.

The coprocessor is currently set to some kind of debug mode, which allows for testing, but prevents it from doing any meaningful work.

If you run the program (your puzzle input), how many times is the mul instruction invoked?
 */

const Utils = require('./utils.js');

function part1(input) {
    const letters = /[a-z]/;
    const regex = /(set|sub|mul|jnz) ([a-h]|-?\d+) ?([a-h]|-?\d+)?/;
    let instructions = input.split('\n');
    let registers = {};
    let index = 0;
    let totalMul = 0;

    while (index >= 0 && index < instructions.length) {
        let match = regex.exec(instructions[index]);

        if (!match) {
            throw Error('Invalid instruction: ' + instructions[index]);
        }

        let needToIncrement = true;

        switch (match[1]) {
            case 'set':
                if (match[3].match(letters)) {
                    registers[match[2]] = registers[match[3]] || 0;
                } else {
                    registers[match[2]] = parseInt(match[3]);
                }
                break;
            case 'sub':
                if (match[3].match(letters)) {
                    registers[match[2]] -= registers[match[3]] || 0;
                } else {
                    registers[match[2]] -= parseInt(match[3]);
                }
                break;
            case 'mul':
                totalMul++;
                if (match[3].match(letters)) {
                    registers[match[2]] *= registers[match[3]] || 0;
                } else {
                    registers[match[2]] *= parseInt(match[3]);
                }
                break;
            case 'jnz':
                let shouldJump = 0;
                if (match[2].match(letters)) {
                    shouldJump = registers[match[2]] || 0;
                } else {
                    shouldJump = parseInt(match[2]);
                }

                if (shouldJump !== 0) {
                    let offset = 0;
                    if (match[3].match(letters)) {
                        offset = registers[match[3]] || 0;
                    } else {
                        offset = parseInt(match[3]);
                    }

                    if (offset !== 0) {
                        needToIncrement = false;
                        index += offset;
                    }
                }
                break;
            default:
                throw Error('Unrecognized instruction: ' + match[1]);
        }

        if (needToIncrement) {
            index++;
        }
    }

    return totalMul;
}

function runPart1() {
    // Answer: 9409
    console.log(part1('set b 99\nset c b\njnz a 2\njnz 1 5\nmul b 100\nsub b -100000\nset c b\nsub c -17000\nset f 1\nset d 2\nset e 2\nset g d\nmul g e\nsub g b\njnz g 2\nset f 0\nsub e -1\nset g e\nsub g b\njnz g -8\nsub d -1\nset g d\nsub g b\njnz g -13\njnz f 2\nsub h -1\nset g b\nsub g c\njnz g 2\njnz 1 3\nsub b -17\njnz 1 -23'));
}

runPart1();
