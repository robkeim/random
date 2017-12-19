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

--- Part Two ---
As you congratulate yourself for a job well done, you notice that the documentation has been on the back of the tablet this entire time. While you actually got most of the instructions correct, there are a few key differences. This assembly code isn't about sound at all - it's meant to be run twice at the same time.

Each running copy of the program has its own set of registers and follows the code independently - in fact, the programs don't even necessarily run at the same speed. To coordinate, they use the send (snd) and receive (rcv) instructions:

snd X sends the value of X to the other program. These values wait in a queue until that program is ready to receive them. Each program has its own message queue, so a program can never receive a message it sent.
rcv X receives the next value and stores it in register X. If no values are in the queue, the program waits for a value to be sent to it. Programs do not continue to the next instruction until they have received a value. Values are received in the order they are sent.
Each program also has its own program ID (one 0 and the other 1); the register p should begin with this value.

For example:
snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d

Both programs begin by sending three values to the other. Program 0 sends 1, 2, 0; program 1 sends 1, 2, 1. Then, each program receives a value (both 1) and stores it in a, receives another value (both 2) and stores it in b, and then each receives the program ID of the other program (program 0 receives 1; program 1 receives 0) and stores it in c. Each program now sees a different value in its own copy of register c.

Finally, both programs try to rcv a fourth time, but no data is waiting for either of them, and they reach a deadlock. When this happens, both programs terminate.

It should be noted that it would be equally valid for the programs to run at different speeds; for example, program 0 might have sent all three values and then stopped at the first rcv before program 1 executed even its first instruction.

Once both of your programs have terminated (regardless of what caused them to do so), how many times did program 1 send a value?
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

class Machine {
    constructor(input, num) {
        this.letters = /[a-z]/;
        this.regex = /(snd|set|add|mul|mod|rcv|jgz) ([a-z]|-?\d+) ?([a-z]|-?\d+)?/;
        this.instructions = input.split('\n');
        this.registers = {};
        this.registers['p'] = num;
        this.index = 0;
        this.receiveQueue = [];
        this.numSends = 0;
    }

    setSendFunction(fn) {
        this.sendFn = fn;
    }

    receiveValue(value) {
        this.receiveQueue.push(value);
    }

    get totalSends() {
        return this.numSends;
    }

    get executeOneCycle() {
        if (this.index < 0 || this.index >= this.instructions.length) {
            return false;
        }

        let match = this.regex.exec(this.instructions[this.index]);

        if (!match) {
            throw Error('Invalid instruction: ' + this.instructions[this.index]);
        }

        let needToIncrement = true;

        switch (match[1]) {
            case 'snd':
                this.numSends++;
                let valueToSend;
                if (match[2].match(this.letters)) {
                    valueToSend = this.registers[match[2]] || 0;
                } else {
                    valueToSend = parseInt(match[2]);
                }
                this.sendFn(valueToSend);
                break;
            case 'set':
                if (match[3].match(this.letters)) {
                    this.registers[match[2]] = this.registers[match[3]] || 0;
                } else {
                    this.registers[match[2]] = parseInt(match[3]);
                }
                break;
            case 'add':
                if (match[3].match(this.letters)) {
                    this.registers[match[2]] += this.registers[match[3]] || 0;
                } else {
                    this.registers[match[2]] += parseInt(match[3]);
                }
                break;
            case 'mul':
                if (match[3].match(this.letters)) {
                    this.registers[match[2]] *= this.registers[match[3]] || 0;
                } else {
                    this.registers[match[2]] *= parseInt(match[3]);
                }
                break;
            case 'mod':
                if (match[3].match(this.letters)) {
                    this.registers[match[2]] %= this.registers[match[3]] || 0;
                } else {
                    this.registers[match[2]] %= parseInt(match[3]);
                }
                break;
            case 'rcv':
                if (this.receiveQueue.length === 0) {
                    return false;
                }

                this.registers[match[2]] = this.receiveQueue.shift();
                break;
            case 'jgz':
                let shouldJump = 0;
                if (match[2].match(this.letters)) {
                    shouldJump = this.registers[match[2]] || 0;
                } else {
                    shouldJump = parseInt(match[2]);
                }

                if (shouldJump > 0) {
                    let offset = 0;
                    if (match[3].match(this.letters)) {
                        offset = this.registers[match[3]] || 0;
                    } else {
                        offset = parseInt(match[3]);
                    }

                    if (offset !== 0) {
                        needToIncrement = false;
                        this.index += offset;
                    }
                }
                break;
            default:
                throw Error('Unrecognized instruction: ' + match[1]);
        }

        if (needToIncrement) {
            this.index++;
        }

        return true;
    }
}

function part2(input) {
    let program1 = new Machine(input, 0);
    let program2 = new Machine(input, 1);
    program1.setSendFunction(program2.receiveValue);
    program2.setSendFunction(program1.receiveValue);

    let result = true;

    while (result) {
        result = program1.executeOneCycle || program2.executeOneCycle;
    }

    return program1.numSends;
}

function runPart2() {
    Utils.assertAreEqual(3, part2('snd 1\nsnd 2\nsnd p\nrcv a\nrcv b\nrcv c\nrcv d'));

    // Answer: 7112
    console.log(part2('set i 31\nset a 1\nmul p 17\njgz p p\nmul a 2\nadd i -1\njgz i -2\nadd a -1\nset i 127\nset p 680\nmul p 8505\nmod p a\nmul p 129749\nadd p 12345\nmod p a\nset b p\nmod b 10000\nsnd b\nadd i -1\njgz i -9\njgz a 3\nrcv b\njgz b -1\nset f 0\nset i 126\nrcv a\nrcv b\nset p a\nmul p -1\nadd p b\njgz p 4\nsnd a\nset a b\njgz 1 3\nsnd b\nset f 1\nadd i -1\njgz i -11\nsnd a\njgz f -16\njgz a -19'));
}

runPart1();
runPart2();
