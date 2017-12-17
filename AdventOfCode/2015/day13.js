/*
--- Day 13: Knights of the Dinner Table ---
In years past, the holiday feast with your family hasn't gone so well. Not everyone gets along! This year, you resolve, will be different. You're going to find the optimal seating arrangement and avoid all those awkward conversations.

You start by writing up a list of everyone invited and the amount their happiness would increase or decrease if they were to find themselves sitting next to each other person. You have a circular table that will be just big enough to fit everyone comfortably, and so each person will have exactly two neighbors.

For example, suppose you have only four attendees planned, and you calculate their potential happiness as follows:

Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
Then, if you seat Alice next to David, Alice would lose 2 happiness units (because David talks so much), but David would gain 46 happiness units (because Alice is such a good listener), for a total change of 44.

If you continue around the table, you could then seat Bob next to Alice (Bob gains 83, Alice gains 54). Finally, seat Carol, who sits next to Bob (Carol gains 60, Bob loses 7) and David (Carol gains 55, David gains 41). The arrangement looks like this:

     +41 +46
+55   David    -2
Carol       Alice
+60    Bob    +54
     -7  +83
After trying every other seating arrangement in this hypothetical scenario, you find that this one is the most optimal, with a total change in happiness of 330.

What is the total change in happiness for the optimal seating arrangement of the actual guest list?
 */

const Utils = require('./utils.js');

function part1(input) {
    const regex = /([a-zA-Z]+) would (gain|lose) (\d+) happiness units by sitting next to ([a-zA-Z]+)./;

    let lines = input.split('\n');
    let weights = {};
    let people = new Set();

    for (let i = 0; i < lines.length; i++) {
        let match = regex.exec(lines[i]);

        if (!match) {
            throw Error('Invalid line format: ' + lines[i]);
        }

        let weight = parseInt(match[3]);

        if (match[2] === 'lose') {
            weight *= -1;
        }

        people.add(match[1]);
        people.add(match[4]);
        weights[match[1] + '_' + match[4]] = weight;
    }

    let permutations = calcPermutations(Array.from(people.keys()));
    let maxHappiness = Number.MIN_SAFE_INTEGER;

    for (let i = 0; i < permutations.length; i++) {
        let happiness = 0;
        let permutation = permutations[i];

        for (let j = 0; j < permutation.length; j++) {
            let curPerson = permutation[j];
            let leftSide = permutation[(j - 1 + permutation.length) % permutation.length];
            let rightSide = permutation[(j + 1) % permutation.length];

            happiness += weights[curPerson + '_' + leftSide] || 0;
            happiness += weights[curPerson + '_' + rightSide] || 0;
        }

        if (happiness > maxHappiness) {
            maxHappiness = happiness;
        }
    }

    return maxHappiness;
}

function calcPermutations(arr) {
    let result = [];
    for(let i = 0; i < arr.length; i++) {
        let rest = calcPermutations(arr.slice(0, i).concat(arr.slice(i + 1)));

        if (rest.length === 0) {
            result.push([arr[i]]);
        } else {
            for (let j = 0; j < rest.length; j++) {
                result.push([arr[i]].concat(rest[j]));
            }
        }
    }

    return result;
}

function runPart1() {
    Utils.assertAreEqual(330, part1('Alice would gain 54 happiness units by sitting next to Bob.\nAlice would lose 79 happiness units by sitting next to Carol.\nAlice would lose 2 happiness units by sitting next to David.\nBob would gain 83 happiness units by sitting next to Alice.\nBob would lose 7 happiness units by sitting next to Carol.\nBob would lose 63 happiness units by sitting next to David.\nCarol would lose 62 happiness units by sitting next to Alice.\nCarol would gain 60 happiness units by sitting next to Bob.\nCarol would gain 55 happiness units by sitting next to David.\nDavid would gain 46 happiness units by sitting next to Alice.\nDavid would lose 7 happiness units by sitting next to Bob.\nDavid would gain 41 happiness units by sitting next to Carol.'));

    // Answer: 733
    console.log(part1('Alice would gain 2 happiness units by sitting next to Bob.\nAlice would gain 26 happiness units by sitting next to Carol.\nAlice would lose 82 happiness units by sitting next to David.\nAlice would lose 75 happiness units by sitting next to Eric.\nAlice would gain 42 happiness units by sitting next to Frank.\nAlice would gain 38 happiness units by sitting next to George.\nAlice would gain 39 happiness units by sitting next to Mallory.\nBob would gain 40 happiness units by sitting next to Alice.\nBob would lose 61 happiness units by sitting next to Carol.\nBob would lose 15 happiness units by sitting next to David.\nBob would gain 63 happiness units by sitting next to Eric.\nBob would gain 41 happiness units by sitting next to Frank.\nBob would gain 30 happiness units by sitting next to George.\nBob would gain 87 happiness units by sitting next to Mallory.\nCarol would lose 35 happiness units by sitting next to Alice.\nCarol would lose 99 happiness units by sitting next to Bob.\nCarol would lose 51 happiness units by sitting next to David.\nCarol would gain 95 happiness units by sitting next to Eric.\nCarol would gain 90 happiness units by sitting next to Frank.\nCarol would lose 16 happiness units by sitting next to George.\nCarol would gain 94 happiness units by sitting next to Mallory.\nDavid would gain 36 happiness units by sitting next to Alice.\nDavid would lose 18 happiness units by sitting next to Bob.\nDavid would lose 65 happiness units by sitting next to Carol.\nDavid would lose 18 happiness units by sitting next to Eric.\nDavid would lose 22 happiness units by sitting next to Frank.\nDavid would gain 2 happiness units by sitting next to George.\nDavid would gain 42 happiness units by sitting next to Mallory.\nEric would lose 65 happiness units by sitting next to Alice.\nEric would gain 24 happiness units by sitting next to Bob.\nEric would gain 100 happiness units by sitting next to Carol.\nEric would gain 51 happiness units by sitting next to David.\nEric would gain 21 happiness units by sitting next to Frank.\nEric would gain 55 happiness units by sitting next to George.\nEric would lose 44 happiness units by sitting next to Mallory.\nFrank would lose 48 happiness units by sitting next to Alice.\nFrank would gain 91 happiness units by sitting next to Bob.\nFrank would gain 8 happiness units by sitting next to Carol.\nFrank would lose 66 happiness units by sitting next to David.\nFrank would gain 97 happiness units by sitting next to Eric.\nFrank would lose 9 happiness units by sitting next to George.\nFrank would lose 92 happiness units by sitting next to Mallory.\nGeorge would lose 44 happiness units by sitting next to Alice.\nGeorge would lose 25 happiness units by sitting next to Bob.\nGeorge would gain 17 happiness units by sitting next to Carol.\nGeorge would gain 92 happiness units by sitting next to David.\nGeorge would lose 92 happiness units by sitting next to Eric.\nGeorge would gain 18 happiness units by sitting next to Frank.\nGeorge would gain 97 happiness units by sitting next to Mallory.\nMallory would gain 92 happiness units by sitting next to Alice.\nMallory would lose 96 happiness units by sitting next to Bob.\nMallory would lose 51 happiness units by sitting next to Carol.\nMallory would lose 81 happiness units by sitting next to David.\nMallory would gain 31 happiness units by sitting next to Eric.\nMallory would lose 73 happiness units by sitting next to Frank.\nMallory would lose 89 happiness units by sitting next to George.'));
}

runPart1();
