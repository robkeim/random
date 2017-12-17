const Utils = require('./utils.js');

function part1(numSteps) {
    let buffer = [0];
    let curIndex = 0;

    for (let i = 1; i <= 2017; i++) {
        curIndex = (curIndex + numSteps) % buffer.length + 1;
        buffer.splice(curIndex, 0, i);
    }

    return buffer[(curIndex + 1) % buffer.length];
}

function part2(numSteps) {
    let bufferSize = 1;
    let curIndex = 0;
    let result = 0;

    for (let i = 1; i <= 50000000; i++) {
        curIndex = (curIndex + numSteps) % bufferSize + 1;

        if (curIndex === 1) {
            result = i;
        }

        bufferSize++;
    }

    return result;
}

function runPart1() {
    Utils.assertAreEqual(638, part1(3));

    // Answer: 1561
    console.log(part1(382));
}

function runPart2() {
    // Answer: 33454823
    console.log(part2(382));
}

runPart1();
runPart2();
