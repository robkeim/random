/*
--- Day 4: The Ideal Stocking Stuffer ---

Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically forward-thinking little girls and boys.

To do this, he needs to find MD5 hashes which, in hexadecimal, start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) followed by a number in decimal. To mine AdventCoins, you must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...) that produces such a hash.

For example:
- If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
- If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....

 --- Part Two ---

Now find one that starts with six zeroes.
 */
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

function part2(secretKey) {
    let counter = 0;

    while (true) {
        const hash = Utils.md5(secretKey + counter);

        if (hash.startsWith('000000')) {
            return counter;
        }

        counter++;
    }
}

function runPart2() {
    // Answer: 9962624
    console.log(part2('yzbqklnj'));
}

runPart1();
// This takes a couple of minutes to complete
runPart2();
