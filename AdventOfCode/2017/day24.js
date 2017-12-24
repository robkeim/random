/*
--- Day 24: Electromagnetic Moat ---
The CPU itself is a large, black building surrounded by a bottomless pit. Enormous metal tubes extend outward from the side of the building at regular intervals and descend down into the void. There's no way to cross, but you need to get inside.

No way, of course, other than building a bridge out of the magnetic components strewn about nearby.

Each component has two ports, one on each end. The ports come in all different types, and only matching types can be connected. You take an inventory of the components by their port types (your puzzle input). Each port is identified by the number of pins it uses; more pins mean a stronger connection for your bridge. A 3/7 component, for example, has a type-3 port on one side, and a type-7 port on the other.

Your side of the pit is metallic; a perfect surface to connect a magnetic, zero-pin port. Because of this, the first port you use must be of type 0. It doesn't matter what type of port you end with; your goal is just to make the bridge as strong as possible.

The strength of a bridge is the sum of the port types in each component. For example, if your bridge is made of components 0/3, 3/7, and 7/4, your bridge has a strength of 0+3 + 3+7 + 7+4 = 24.

For example, suppose you had the following components:

0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10
With them, you could make the following valid bridges:
- 0/1
- 0/1--10/1
- 0/1--10/1--9/10
- 0/2
- 0/2--2/3
- 0/2--2/3--3/4
- 0/2--2/3--3/5
- 0/2--2/2
- 0/2--2/2--2/3
- 0/2--2/2--2/3--3/4
- 0/2--2/2--2/3--3/5

(Note how, as shown by 10/1, order of ports within a component doesn't matter. However, you may only use each port on a component once.)

Of these bridges, the strongest one is 0/1--10/1--9/10; it has a strength of 0+1 + 1+10 + 10+9 = 31.

What is the strength of the strongest bridge you can make with the components you have available?
 */

const Utils = require('./utils.js');

function part1(input) {
    let regex = /(\d+)\/(\d+)/;

    let components = input.split('\n').map(line =>
    {
       let match = regex.exec(line);

       if (!match) {
           throw Error('Invalid line format: ' + line);
       }

       return [parseInt(match[1]), parseInt(match[2])];
    });

    return buildBridge(components, []);
}

function buildBridge(components, soFar) {
    let maxScore = calcBridgeValue(soFar);
    let lastValue = soFar.length === 0 ? 0 : soFar[soFar.length - 1][1];

    for (let i = 0; i < components.length; i++) {
        let remainingComponents, newChain, newValue;

        if (components[i][0] === lastValue) {
            remainingComponents = components.slice(0);
            remainingComponents.splice(i, 1);
            newChain = soFar.slice(0);
            newChain.push(components[i]);
            newValue = buildBridge(remainingComponents, newChain);

            if (newValue > maxScore) {
                maxScore = newValue;
            }
        }

        if (components[i][1] === lastValue) {
            remainingComponents = components.slice(0);
            remainingComponents.splice(i, 1);
            newChain = soFar.slice(0);
            newChain.push(components[i].reverse());
            newValue = buildBridge(remainingComponents, newChain);

            if (newValue > maxScore) {
                maxScore = newValue;
            }
        }
    }

    return maxScore;
}

function calcBridgeValue(bridge) {
    let value = 0;

    for (let i = 0; i < bridge.length; i++) {
        value += (bridge[i][0] + bridge[i][1]);
    }

    return value;
}

function runPart1() {
    Utils.assertAreEqual(31, part1('0/2\n2/2\n2/3\n3/4\n3/5\n0/1\n10/1\n9/10'));

    // Answer: 1868
    console.log(part1('25/13\n4/43\n42/42\n39/40\n17/18\n30/7\n12/12\n32/28\n9/28\n1/1\n16/7\n47/43\n34/16\n39/36\n6/4\n3/2\n10/49\n46/50\n18/25\n2/23\n3/21\n5/24\n46/26\n50/19\n26/41\n1/50\n47/41\n39/50\n12/14\n11/19\n28/2\n38/47\n5/5\n38/34\n39/39\n17/34\n42/16\n32/23\n13/21\n28/6\n6/20\n1/30\n44/21\n11/28\n14/17\n33/33\n17/43\n31/13\n11/21\n31/39\n0/9\n13/50\n10/14\n16/10\n3/24\n7/0\n50/50'));
}

runPart1();
