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

 --- Part Two ---

Seeing how reindeer move in bursts, Santa decides he's not pleased with the old scoring system.

Instead, at the end of each second, he awards one point to the reindeer currently in the lead. (If there are multiple reindeer tied for the lead, they each get one point.) He keeps the traditional 2503 second time limit, of course, as doing otherwise would be entirely ridiculous.

Given the example reindeer from above, after the first second, Dancer is in the lead and gets one point. He stays in the lead until several seconds into Comet's second burst: after the 140th second, Comet pulls into the lead and gets his first point. Of course, since Dancer had been in the lead for the 139 seconds before that, he has accumulated 139 points by the 140th second.

After the 1000th second, Dancer has accumulated 689 points, while poor Comet, our old champion, only has 312. So, with the new scoring system, Dancer would win (if the race ended at 1000 seconds).

Again given the descriptions of each reindeer (in your puzzle input), after exactly 2503 seconds, how many points does the winning reindeer have?
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

    // Answer: 2696
    console.log(part1(2503, 'Rudolph can fly 22 km/s for 8 seconds, but then must rest for 165 seconds.\nCupid can fly 8 km/s for 17 seconds, but then must rest for 114 seconds.\nPrancer can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.\nDonner can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.\nDasher can fly 11 km/s for 12 seconds, but then must rest for 125 seconds.\nComet can fly 21 km/s for 6 seconds, but then must rest for 121 seconds.\nBlitzen can fly 18 km/s for 3 seconds, but then must rest for 50 seconds.\nVixen can fly 20 km/s for 4 seconds, but then must rest for 75 seconds.\nDancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.'));
}

function part2(totalTime, input) {
    const lineRegex = /([^ ]+) can fly (\d+) km\/s for (\d+) seconds, but then must rest for (\d+) seconds\./;
    let raindeer = {};

    input.split('\n').forEach(line => {
        let match = lineRegex.exec(line);

        if (!match) {
            throw Error('Invalid line format: ' + line);
        }

        let name = match[1];
        let speed = parseInt(match[2]);
        let flyingDuration = parseInt(match[3]);
        let restingDuration = parseInt(match[4]);

        raindeer[name] = {
            speed: speed,
            flyingDuration: flyingDuration,
            restingDuration: restingDuration,
            points: 0,
        };
    });

    for (let i = 1; i <= totalTime; i++) {
        let maxDistance = -1;
        let winners = [];

        for (let name in raindeer) {
            let distance = calculateDistance(i, raindeer[name]);

            if (distance > maxDistance) {
                maxDistance = distance;
                winners = [name];
            } else if (distance === maxDistance) {
                winners.push(name);
            }
        }

        for (let i in winners) {
            raindeer[winners[i]].points++;
        }
    }

    let maxPoints = -1;

    for (let name in raindeer) {
        let points = raindeer[name].points;

        if (points > maxPoints) {
            maxPoints = points;
        }
    }

    return maxPoints;
}

function calculateDistance(time, raindeer) {
    let timePerCycle = raindeer.flyingDuration + raindeer.restingDuration;
    let distance = Math.floor(time / timePerCycle) * (raindeer.speed * raindeer.flyingDuration);

    let remainingTime = Math.min(time % timePerCycle, raindeer.flyingDuration);
    return distance + remainingTime * raindeer.speed;
}

function runPart2() {
    Utils.assertAreEqual(689, part2(1000, 'Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.\nDancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.'));

    // Answer: 1084
    console.log(part2(2503, 'Rudolph can fly 22 km/s for 8 seconds, but then must rest for 165 seconds.\nCupid can fly 8 km/s for 17 seconds, but then must rest for 114 seconds.\nPrancer can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.\nDonner can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.\nDasher can fly 11 km/s for 12 seconds, but then must rest for 125 seconds.\nComet can fly 21 km/s for 6 seconds, but then must rest for 121 seconds.\nBlitzen can fly 18 km/s for 3 seconds, but then must rest for 50 seconds.\nVixen can fly 20 km/s for 4 seconds, but then must rest for 75 seconds.\nDancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.'));
}

runPart1();
runPart2();
