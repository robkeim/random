/*
--- Day 11: Corporate Policy ---

Santa's previous password expired, and he needs help choosing a new one.

To help him remember his new password after the old one expires, Santa has devised a method of coming up with a password based on the previous one. Corporate policy dictates that passwords must be exactly eight lowercase letters (for security reasons), so he finds his new password by incrementing his old password string repeatedly until it is valid.

Incrementing is just like counting with numbers: xx, xy, xz, ya, yb, and so on. Increase the rightmost letter one step; if it was z, it wraps around to a, and repeat with the next letter to the left until one doesn't wrap around.

Unfortunately for Santa, a new Security-Elf recently started, and he has imposed some additional password requirements:
- Passwords must include one increasing straight of at least three letters, like abc, bcd, cde, and so on, up to xyz. They cannot skip letters; abd doesn't count.
- Passwords may not contain the letters i, o, or l, as these letters can be mistaken for other characters and are therefore confusing.
- Passwords must contain at least two different, non-overlapping pairs of letters, like aa, bb, or zz.

For example:
- hijklmmn meets the first requirement (because it contains the straight hij) but fails the second requirement requirement (because it contains i and l).
- abbceffg meets the third requirement (because it repeats bb and ff) but fails the first requirement.
- abbcegjk fails the third requirement, because it only has one double letter (bb).
- The next password after abcdefgh is abcdffaa.
- The next password after ghijklmn is ghjaabcc, because you eventually skip all the passwords that start with ghi..., since i is not allowed.

Given Santa's current password (your puzzle input), what should his next password be?
 */

const Utils = require('./utils.js');

function isPasswordValid(input) {
    if (input === 'ghjaabcc') {
        input = input;
    }
    // Test for forbidden letters
    if (input.indexOf('i') > -1 || input.indexOf('o') > -1 || input.indexOf('l') > -1) {
        return false;
    }

    // Test for three consecutive letters
    let hasConsecutiveLetters = false;

    for (let i = 0; i < input.length - 2; i++) {
        if ((input.charCodeAt(i + 1) - input.charCodeAt(i) === 1)
            && (input.charCodeAt(i + 2) - input.charCodeAt(i + 1) === 1)) {
            hasConsecutiveLetters = true;
            break;
        }
    }

    if (!hasConsecutiveLetters) {
        return false;
    }

    // Test for two pairs
    let numPairs = 0;

    for (let i = 0; i < input.length - 1; i++)
    {
        if (input[i] === input[i + 1]) {
            numPairs++;

            if (numPairs >= 2) {
                break;
            }

            // Ensure we don't count xxx as two pairs
            i++;
        }
    }

    if (numPairs < 2) {
        return false;
    }

    return true;
}

function incrementPassword(input) {
    let result = (parseInt(input, 36) + 1).toString(36).replace(/0/g, 'a');
    return result;
}

function part1(input) {
    let result = incrementPassword(input);
    let numProcessed = 0;

    while (!isPasswordValid(result)) {
        result = incrementPassword(result);
    }

    return result;
}

function runPart1() {
    Utils.assertAreEqual('abcdffaa', part1('abcdefgh'));
    Utils.assertAreEqual('ghjaabcc', part1('ghijklmn'));

    // Answer: vzbxxyzz
    console.log(part1('vzbxkghb'));
}

runPart1();
