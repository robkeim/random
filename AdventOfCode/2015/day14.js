/*
--- Day 14: Reindeer Olympics ---

This year is the Reindeer Olympics! Reindeer can fly at high speeds, but must rest occasionally to recover their energy. Santa would like to know which of his reindeer is fastest, and so he has them race.

Reindeer can only either be flying (always at their top speed) or resting (not moving at all), and always spend whole seconds in either state.

For example, suppose you have the following Reindeer:
- Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
- Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
- After one second, Comet has gone 14 km, while Dancer has gone 16 km. After ten seconds, Comet has gone 140 km, while Dancer has gone 160 km. On the eleventh second, Comet begins resting (staying at 140 km), and Dancer continues on for a total distance of 176 km. On the 12th second, both reindeer are resting. They continue to rest until the 138th second, when Comet flies for another ten seconds. On the 174th second, Dancer flies for another 11 seconds.

In this example, after the 1000th second, both reindeer are resting, and Comet is in the lead at 1120 km (poor Dancer has only gotten 1056 km by that point). So, in this situation, Comet would win (if the race ended at 1000 seconds).

Given the descriptions of each reindeer (in your puzzle input), after exactly 2503 seconds, what distance has the winning reindeer traveled?
 */

const Utils = require('./utils.js');

function part1(totalTime, input) {
    const lineRegex = /[^ ]+ can fly (\d+) km\/s for (\d+) seconds, but then must rest for (\d+) seconds\./;
    let maxDistance = -1;

    input.split('\n').forEach(line => {
       let match = lineRegex.exec(line);

       if (!match) {
           throw Error('Invalid line format: ' + line);
       }

       let speed = parseInt(match[1]);
       let flyingDuration = parseInt(match[2]);
       let restingDuration = parseInt(match[3]);

       let timePerCycle = flyingDuration + restingDuration;
       let distance = Math.floor(totalTime / timePerCycle) * (speed * flyingDuration);

       let remainingTime = Math.min(totalTime % timePerCycle, flyingDuration);
       distance += remainingTime * speed;

       if (distance > maxDistance) {
           maxDistance = distance;
       }
    });

    return maxDistance;
}

function runPart1() {
    Utils.assertAreEqual(1120, part1(1000, 'Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.\nDancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.'));
    Utils.assertAreEqual(10, part1(1, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(20, part1(2, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(20, part1(3, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(20, part1(4, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(20, part1(5, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(30, part1(6, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(40, part1(7, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(40, part1(8, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(40, part1(9, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(40, part1(10, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));
    Utils.assertAreEqual(50, part1(11, 'Comet can fly 10 km/s for 2 seconds, but then must rest for 3 seconds.'));

    // Answer: 2696 (2640 is too low)
    console.log(part1(2503, 'Rudolph can fly 22 km/s for 8 seconds, but then must rest for 165 seconds.\nCupid can fly 8 km/s for 17 seconds, but then must rest for 114 seconds.\nPrancer can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.\nDonner can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.\nDasher can fly 11 km/s for 12 seconds, but then must rest for 125 seconds.\nComet can fly 21 km/s for 6 seconds, but then must rest for 121 seconds.\nBlitzen can fly 18 km/s for 3 seconds, but then must rest for 50 seconds.\nVixen can fly 20 km/s for 4 seconds, but then must rest for 75 seconds.\nDancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.'));
}

runPart1();
