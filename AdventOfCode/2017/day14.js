/*
--- Day 14: Disk Defragmentation ---

Suddenly, a scheduled job activates the system's disk defragmenter. Were the situation different, you might sit and watch it for a while, but today, you just don't have that kind of time. It's soaking up valuable system resources that are needed elsewhere, and so the only option is to help it finish its task as soon as possible.

The disk in question consists of a 128x128 grid; each square of the grid is either free or used. On this disk, the state of the grid is tracked by the bits in a sequence of knot hashes.

A total of 128 knot hashes are calculated, each corresponding to a single row in the grid; each hash contains 128 bits which correspond to individual grid squares. Each bit of a hash indicates whether that square is free (0) or used (1).

The hash inputs are a key string (your puzzle input), a dash, and a number from 0 to 127 corresponding to the row. For example, if your key string were flqrgnkx, then the first row would be given by the bits of the knot hash of flqrgnkx-0, the second row from the bits of the knot hash of flqrgnkx-1, and so on until the last row, flqrgnkx-127.

The output of a knot hash is traditionally represented by 32 hexadecimal digits; each of these digits correspond to 4 bits, for a total of 4 * 32 = 128 bits. To convert to bits, turn each hexadecimal digit to its equivalent binary value, high-bit first: 0 becomes 0000, 1 becomes 0001, e becomes 1110, f becomes 1111, and so on; a hash that begins with a0c2017... in hexadecimal would begin with 10100000110000100000000101110000... in binary.

Continuing this process, the first 8 rows and columns for key flqrgnkx appear as follows, using # to denote used squares, and . to denote free ones:

##.#.#..-->
.#.#.#.#
....#.#.
#.#.##.#
.##.#...
##..#..#
.#...#..
##.#.##.-->
|      |
V      V

In this example, 8108 squares are used across the entire 128x128 grid.

Given your actual key string, how many squares are used?
 */

const Utils = require('./utils.js');

function part1(input) {
    let numUsedSquares = 0;

    for (let i = 0; i < 128; i++) {
        let hash = knotHash(input + '-' + i);

        let line = hash.split('').map(c => hexToBin(c)).join('');

        numUsedSquares += line.split('').filter(c => c === '1').length;
    }

    return numUsedSquares;
}

function hexToBin(input) {
    if (input.length !== 1) {
        throw Error('Invalid input: ' + input);
    }

    switch (input) {
        case '0':
            return '0000';
        case '1':
            return '0001';
        case '2':
            return '0010';
        case '3':
            return '0011';
        case '4':
            return '0100';
        case '5':
            return '0101';
        case '6':
            return '0110';
        case '7':
            return '0111';
        case '8':
            return '1000';
        case '9':
            return '1001';
        case 'a':
            return '1010';
        case 'b':
            return '1011';
        case 'c':
            return '1100';
        case 'd':
            return '1101';
        case 'e':
            return '1110';
        case 'f':
            return '1111';
        default:
            throw Error('Invalid input: ' + input);
    }
}

function knotHash(input) {
    let list = [];

    for (let i = 0; i < 256; i++) {
        list.push(i);
    }

    let lengths = input
        .split('')
        .map(c => c.charCodeAt(0))
        .concat([17, 31, 73, 47, 23]);

    let curPos = 0;
    let skipSize = 0;

    for (let i = 0; i < 64; i++) {
        let result = applyOneRound(list, lengths, curPos, skipSize);
        list = result.list;
        curPos = result.curPos;
        skipSize = result.skipSize;
    }

    let blocksToHash = [];

    for (let i = 0; i < 256; i += 16) {
        blocksToHash.push(list.slice(i, i + 16));
    }

    let hashes = blocksToHash.map(block => {
        return block.reduce((acc, cur) => acc ^ cur);
    });

    let hexHashes = hashes.map(hash => {
        // Ensure that each hex has a leading 0 if needed
        return ('0' + hash.toString(16)).slice(-2);
    });

    return hexHashes.join('');
}

function applyOneRound(list, lengths, curPos, skipSize) {
    for (let i = 0; i < lengths.length; i++) {
        let curLength = lengths[i];
        list = reverse(list, curPos, curLength);

        curPos += curLength + skipSize;
        skipSize++;

        curPos %= list.length;
    }

    return {
        list: list,
        curPos: curPos,
        skipSize: skipSize,
    };
}

function reverse(arr, start, length) {
    if (start < 0 || length < 0 || length > arr.length) {
        throw Error('Invalid request for array');
    }

    let reversedArr = [];

    for (let i = 0; i < length; i++) {
        reversedArr.push(arr[(start + i) % arr.length]);
    }

    reversedArr.reverse();

    for (let i = 0; i < length; i++) {
        arr[(start + i) % arr.length] = reversedArr[i];
    }

    return arr;
}

function runPart1() {
    Utils.assertAreEqual(8108, part1('flqrgnkx'));

    // Answer: 8140
    console.log(part1('jxqlasbh'));
}

function part2(input) {
    let usedSpaces = new Set();

    for (let y = 0; y < 128; y++) {
        let hash = knotHash(input + '-' + y);

        let line = hash.split('').map(c => hexToBin(c)).join('');

        for (let x = 0; x < 128; x++) {
            if (line[x] === '1') {
                usedSpaces.add(x + '_' + y);
            }
        }
    }

    let numGroups = 0;

    while (usedSpaces.size > 0) {
        numGroups++;
        let toProcess = [];
        let value = usedSpaces.keys().next().value;
        toProcess.push(value);
        usedSpaces.delete(value);

        while (toProcess.length > 0) {
            value = toProcess.pop();
            let split = value.split('_');
            let x = parseInt(split[0]);
            let y = parseInt(split[1]);
            let neighbors = [
                (x + 1) + '_' + y,
                (x - 1) + '_' + y,
                x + '_' + (y + 1),
                x + '_' + (y - 1),
            ];

            for (let i = 0; i < neighbors.length; i++) {
                if (usedSpaces.has(neighbors[i])) {
                    toProcess.push(neighbors[i]);
                    usedSpaces.delete(neighbors[i]);
                }
            }
        }
    }

    return numGroups;
}

function runPart2() {
    Utils.assertAreEqual(1242, part2('flqrgnkx'));

    // Answer: 1182
    console.log(part2('jxqlasbh'));
}

runPart1();
runPart2();
