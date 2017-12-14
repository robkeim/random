/*
--- Day 21: RPG Simulator 20XX ---

Little Henry Case got a new video game for Christmas. It's an RPG, and he's stuck on a boss. He needs to know what equipment to buy at the shop. He hands you the controller.

In this game, the player (you) and the enemy (the boss) take turns attacking. The player always goes first. Each attack reduces the opponent's hit points by at least 1. The first character at or below 0 hit points loses.

Damage dealt by an attacker each turn is equal to the attacker's damage score minus the defender's armor score. An attacker always does at least 1 damage. So, if the attacker has a damage score of 8, and the defender has an armor score of 3, the defender loses 5 hit points. If the defender had an armor score of 300, the defender would still lose 1 hit point.

Your damage score and armor score both start at zero. They can be increased by buying items in exchange for gold. You start with no items and have as much gold as you need. Your total damage or armor is equal to the sum of those stats from all of your items. You have 100 hit points.

Here is what the item shop is selling:

Weapons:    Cost  Damage  Armor
Dagger        8     4       0
Shortsword   10     5       0
Warhammer    25     6       0
Longsword    40     7       0
Greataxe     74     8       0

Armor:      Cost  Damage  Armor
Leather      13     0       1
Chainmail    31     0       2
Splintmail   53     0       3
Bandedmail   75     0       4
Platemail   102     0       5

Rings:      Cost  Damage  Armor
Damage +1    25     1       0
Damage +2    50     2       0
Damage +3   100     3       0
Defense +1   20     0       1
Defense +2   40     0       2
Defense +3   80     0       3
You must buy exactly one weapon; no dual-wielding. Armor is optional, but you can't use more than one. You can buy 0-2 rings (at most one for each hand). You must use any items you buy. The shop only has one of each item, so you can't buy, for example, two rings of Damage +3.

For example, suppose you have 8 hit points, 5 damage, and 5 armor, and that the boss has 12 hit points, 7 damage, and 2 armor:

The player deals 5-2 = 3 damage; the boss goes down to 9 hit points.
The boss deals 7-5 = 2 damage; the player goes down to 6 hit points.
The player deals 5-2 = 3 damage; the boss goes down to 6 hit points.
The boss deals 7-5 = 2 damage; the player goes down to 4 hit points.
The player deals 5-2 = 3 damage; the boss goes down to 3 hit points.
The boss deals 7-5 = 2 damage; the player goes down to 2 hit points.
The player deals 5-2 = 3 damage; the boss goes down to 0 hit points.
In this scenario, the player wins! (Barely.)

You have 100 hit points. The boss's actual stats are in your puzzle input. What is the least amount of gold you can spend and still win the fight?

--- Part Two ---

Turns out the shopkeeper is working with the boss, and can persuade you to buy whatever items he wants. The other rules still apply, and he still only has one of each item.

What is the most amount of gold you can spend and still lose the fight?
 */

const Utils = require('./utils.js');

function part1(bossDamage, bossArmor, bossHitPoints) {
    return calculate(bossDamage, bossArmor, bossHitPoints).minAndWin;
}

function part2(bossDamage, bossArmor, bossHitPoints) {
    return calculate(bossDamage, bossArmor, bossHitPoints).maxAndLose;
}

function calculate(bossDamage, bossArmor, bossHitPoints) {
    let weapons = [
        [8, 4],
        [10, 5],
        [25, 6],
        [40, 7],
        [74, 8],
    ];

    let armor = [
        [0, 0], // Simulate selecting no armor
        [13, 1],
        [31, 2],
        [53, 3],
        [75, 4],
        [102, 5],
    ];

    let rings = [
        [0, 0, 0], // Simulate not selecting a ring
        [25, 1, 0],
        [50, 2, 0],
        [100, 3, 0],
        [20, 0, 1],
        [40, 0, 2],
        [80, 0, 3],
    ];

    let minAndWinGold = Number.MAX_SAFE_INTEGER;
    let maxAndLoseGold = Number.MIN_SAFE_INTEGER;
    for (let w = 0; w < weapons.length; w++) {
        for (let a = 0; a < armor.length; a++) {
            for (let r1 = 0; r1 < rings.length; r1++) {
                for (let r2 = 0; r2 < rings.length; r2++) {
                    if (r1 === r2 && r1 === 0) { // We can't buy two of the same ring
                        continue;
                    }

                    let playerDamage = 0;
                    let playerArmor = 0;
                    let goldSpent = 0;

                    let weapon = weapons[w];
                    goldSpent += weapon[0];
                    playerDamage += weapon[1];

                    let curArmor = armor[a];
                    goldSpent += curArmor[0];
                    playerArmor += curArmor[1];

                    let ring = rings[r1];
                    goldSpent += ring[0];
                    playerDamage += ring[1];
                    playerArmor += ring[2];

                    ring = rings[r2];
                    goldSpent += ring[0];
                    playerDamage += ring[1];
                    playerArmor += ring[2];

                    if (winSimulation(playerDamage, playerArmor, 100,
                            bossDamage, bossArmor, bossHitPoints)) {
                        if (goldSpent < minAndWinGold) {
                            minAndWinGold = goldSpent;
                        }
                    } else {
                        if (goldSpent > maxAndLoseGold) {
                            maxAndLoseGold = goldSpent;
                        }
                    }
                }
            }
        }
    }

    return {
        minAndWin: minAndWinGold,
        maxAndLose: maxAndLoseGold,
    };
}

function winSimulation(playerDamage, playerArmor, playerHitPoints,
                       bossDamage, bossArmor, bossHitPoints) {
    playerDamage = Math.max(1, playerDamage - bossArmor);
    bossDamage = Math.max(1, bossDamage - playerArmor);

    while (true) {
        bossHitPoints -= playerDamage;

        if (bossHitPoints <= 0) {
            return true;
        }

        playerHitPoints -= bossDamage;

        if (playerHitPoints <= 0) {
            return false;
        }
    }
}

function runPart1() {
    Utils.assertAreEqual(true, winSimulation(5, 5, 8, 7, 2, 12));

    // Answer: 78
    console.log(part1(8, 1, 104));
}

function runPart2() {
    // Answer: XXX
    console.log(part2(8, 1, 104));
}

runPart1();
runPart2();
