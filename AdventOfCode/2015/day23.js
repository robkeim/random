const Utils = require('./utils.js');

function part1(input) {
    const singleArgRegex = /(hlf|tpl|inc|jmp) \+?([\-ab0-9]+)/;
    const doubleArgRegex = /(jie|jio) ([ab]), \+?([\-0-9]+)/;

    let lines = input.split('\n');
    let index = 0;
    let a = 0;
    let b = 0;

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

runPart1();
