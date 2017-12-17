/*
--- Day 15: Science for Hungry People ---
Today, you set out on the task of perfecting your milk-dunking cookie recipe. All you have to do is find the right balance of ingredients.

Your recipe leaves room for exactly 100 teaspoons of ingredients. You make a list of the remaining ingredients you could use to finish the recipe (your puzzle input) and their properties per teaspoon:
- capacity (how well it helps the cookie absorb milk)
- durability (how well it keeps the cookie intact when full of milk)
- flavor (how tasty it makes the cookie)
- texture (how it improves the feel of the cookie)
- calories (how many calories it adds to the cookie)

You can only measure ingredients in whole-teaspoon amounts accurately, and you have to be accurate so you can reproduce your results in the future. The total score of a cookie can be found by adding up each of the properties (negative totals become 0) and then multiplying together everything except calories.

For instance, suppose you have these two ingredients:

Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
Then, choosing to use 44 teaspoons of butterscotch and 56 teaspoons of cinnamon (because the amounts of each ingredient must add up to 100) would result in a cookie with the following properties:
- A capacity of 44*-1 + 56*2 = 68
- A durability of 44*-2 + 56*3 = 80
- A flavor of 44*6 + 56*-2 = 152
- A texture of 44*3 + 56*-1 = 76

Multiplying these together (68 * 80 * 152 * 76, ignoring calories for now) results in a total score of 62842880, which happens to be the best score possible given these ingredients. If any properties had produced a negative total, it would have instead become zero, causing the whole score to multiply to zero.

Given the ingredients in your kitchen and their properties, what is the total score of the highest-scoring cookie you can make?
 */

const Utils = require('./utils.js');

function part1(input) {
    const regex = /[a-zA-Z]+: capacity (-?\d+), durability (-?\d+), flavor (-?\d+), texture (-?\d+), calories -?\d+/;

    let ingredients = input.split('\n').map(line => {
        let match = regex.exec(line);

        if (!match) {
            throw Error('Invalid line format: ' + line);
        }

        return [parseInt(match[1]), parseInt(match[2]), parseInt(match[3]), parseInt(match[4])];
    });

    let possibilities = getPossibilities(ingredients.length, 100, []);

    let bestScore = Number.MIN_SAFE_INTEGER;
    for (let i = 0; i < possibilities.length; i++) {
        let capacity = 0;
        let durability = 0;
        let flavor = 0;
        let texture = 0;

        for (let j = 0; j < possibilities[i].length; j++) {
            capacity += ingredients[j][0] * possibilities[i][j];
            durability += ingredients[j][1] * possibilities[i][j];
            flavor += ingredients[j][2] * possibilities[i][j];
            texture += ingredients[j][3] * possibilities[i][j];
        }

        capacity = Math.max(0, capacity);
        durability = Math.max(0, durability);
        flavor = Math.max(0, flavor);
        texture = Math.max(0, texture);

        let score = capacity * durability * flavor * texture;

        if (score > bestScore) {
            bestScore = score;
        }
    }

    return bestScore;
}

function getPossibilities(ingredients, quantity, soFar) {
    if (ingredients === 1) {
        soFar.push(quantity);
        return [soFar];
    }

    let solutions = [];

    for (let i = 0; i <= quantity; i++) {
        let next = Array.from(soFar);
        next.push(i);
        solutions.push(...getPossibilities(ingredients - 1, quantity - i, next));
    }

    return solutions;
}

function runPart1() {
    Utils.assertAreEqual(62842880, part1('Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8\nCinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3'));

    // Answer: 21367368
    console.log(part1('Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3\nButterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3\nChocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8\nCandy: capacity 0, durability -1, flavor 0, texture 5, calories 8'));
}

runPart1();
