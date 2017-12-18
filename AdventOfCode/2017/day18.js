/*
--- Day 18: Duet ---
You discover a tablet containing some strange assembly code labeled simply "Duet". Rather than bother the sound card with it, you decide to run the code yourself. Unfortunately, you don't see any documentation, so you're left to figure out what the instructions mean on your own.

It seems like the assembly is meant to operate on a set of registers that are each named with a single letter and that can each hold a single integer. You suppose each register should start with a value of 0.

There aren't that many instructions, so it shouldn't be hard to figure out what they do. Here's what you determine:
- snd X plays a sound with a frequency equal to the value of X.
- set X Y sets register X to the value of Y.
- add X Y increases register X by the value of Y.
- mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
- mod X Y sets register X to the remainder of dividing the value contained in register X by the value of Y (that is, it sets X to the result of X modulo Y).
- rcv X recovers the frequency of the last sound played, but only when the value of X is not zero. (If it is zero, the command does nothing.)
- jgz X Y jumps with an offset of the value of Y, but only if the value of X is greater than zero. (An offset of 2 skips the next instruction, an offset of -1 jumps to the previous instruction, and so on.)

Many of the instructions can take either a register (a single letter) or a number. The value of a register is the integer it contains; the value of a number is that number.

After each jump instruction, the program continues with the instruction to which the jump jumped. After any other instruction, the program continues with the next instruction. Continuing (or jumping) off either end of the program terminates it.

For example:

set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2

The first four instructions set a to 1, add 2 to it, square it, and then set it to itself modulo 5, resulting in a value of 4.
Then, a sound with frequency 4 (the value of a) is played.
After that, a is set to 0, causing the subsequent rcv and jgz instructions to both be skipped (rcv because a is 0, and jgz because a is not greater than 0).
Finally, a is set to 1, causing the next jgz instruction to activate, jumping back two instructions to another jump, which jumps again to the rcv, which ultimately triggers the recover operation.
At the time the recover operation is executed, the frequency of the last sound played is 4.

What is the value of the recovered frequency (the value of the most recently played sound) the first time a rcv instruction is executed with a non-zero value?
 */

const Utils = require('./utils.js');

function part1(input) {
    const letters = /[a-z]/;
    const regex = /(snd|set|add|mul|mod|rcv|jgz) ([a-z]|-?\d+) ?([a-z]|-?\d+)?/;
    let instructions = input.split('\n');
    let registers = {};
    let index = 0;
    let lastSound = undefined;

    while (index >= 0 && index < instructions.length) {
        let match = regex.exec(instructions[index]);

        if (!match) {
            throw Error('Invalid instruction: ' + instructions[index]);
        }

        let needToIncrement = true;

        switch (match[1]) {
            case 'snd':
                if (match[2].match(letters)) {
                    lastSound = registers[match[2]] || 0;
                } else {
                    lastSound = parseInt(match[2]);
                }
                break;
            case 'set':
                if (match[3].match(letters)) {
                    registers[match[2]] = registers[match[3]] || 0;
                } else {
                    registers[match[2]] = parseInt(match[3]);
                }
                break;
            case 'add':
                if (match[3].match(letters)) {
                    registers[match[2]] += registers[match[3]] || 0;
                } else {
                    registers[match[2]] += parseInt(match[3]);
                }
                break;
            case 'mul':
                if (match[3].match(letters)) {
                    registers[match[2]] *= registers[match[3]] || 0;
                } else {
                    registers[match[2]] *= parseInt(match[3]);
                }
                break;
            case 'mod':
                if (match[3].match(letters)) {
                    registers[match[2]] %= registers[match[3]] || 0;
                } else {
                    registers[match[2]] %= parseInt(match[3]);
                }
                break;
            case 'rcv':
                let value = 0;
                if (match[2].match(letters)) {
                    value = registers[match[2]] || 0;
                } else {
                    value = parseInt(match[2]);
                }

                if (value !== 0) {
                    return lastSound;
                }
                break;
            case 'jgz':
                let shouldJump = 0;
                if (match[2].match(letters)) {
                    shouldJump = registers[match[2]] || 0;
                } else {
                    shouldJump = parseInt(match[2]);
                }

                if (shouldJump > 0) {
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

    throw Error('No solution found');
}

function runPart1() {
    Utils.assertAreEqual(4, part1('set a 1\nadd a 2\nmul a a\nmod a 5\nsnd a\nset a 0\nrcv a\njgz a -1\nset a 1\njgz a -2'));

    // Answer: 3188
    console.log(part1('set i 31\nset a 1\nmul p 17\njgz p p\nmul a 2\nadd i -1\njgz i -2\nadd a -1\nset i 127\nset p 680\nmul p 8505\nmod p a\nmul p 129749\nadd p 12345\nmod p a\nset b p\nmod b 10000\nsnd b\nadd i -1\njgz i -9\njgz a 3\nrcv b\njgz b -1\nset f 0\nset i 126\nrcv a\nrcv b\nset p a\nmul p -1\nadd p b\njgz p 4\nsnd a\nset a b\njgz 1 3\nsnd b\nset f 1\nadd i -1\njgz i -11\nsnd a\njgz f -16\njgz a -19'));
}

runPart1();
