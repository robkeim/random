const Utils = require('./utils.js');

function part1(secretKey) {
    let counter = 0;

    while (true) {
        const hash = Utils.md5(secretKey + counter);

        if (hash.startsWith('00000')) {
            return counter;
        }

        counter++;
    }
}

function runPart1() {
    Utils.assertAreEqual(609043, part1('abcdef'));
    Utils.assertAreEqual(1048970, part1('pqrstuv'));

    // Answer: 282749
    console.log(part1('yzbqklnj'));
}

runPart1();
