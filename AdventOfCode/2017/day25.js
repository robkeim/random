const Utils = require('./utils.js');

function part1(numSteps) {
    let values = {};
    let state = 'A';
    let index = 0;

    for (let i = 0; i < numSteps; i++) {
        switch (state) {
            case 'A':
                if (!values[index]) {
                    values[index] = 1;
                    index++;
                    state = 'B';
                } else {
                    values[index] = 0;
                    index--;
                    state = 'B';
                }
                break;
            case 'B':
                if (!values[index]) {
                    values[index] = 1;
                    index--;
                    state = 'C';
                } else {
                    values[index] = 0;
                    index++;
                    state = 'E';
                }
                break;
            case 'C':
                if (!values[index]) {
                    values[index] = 1;
                    index++;
                    state = 'E';
                } else {
                    values[index] = 0;
                    index--;
                    state = 'D';
                }
                break;
            case 'D':
                if (!values[index]) {
                    values[index] = 1;
                    index--;
                    state = 'A';
                } else {
                    values[index] = 1;
                    index--;
                    state = 'A';
                }
                break;
            case 'E':
                if (!values[index]) {
                    values[index] = 0;
                    index++;
                    state = 'A';
                } else {
                    values[index] = 0;
                    index++;
                    state = 'F';
                }
                break;
            case 'F':
                if (!values[index]) {
                    values[index] = 1;
                    index++;
                    state = 'E';
                } else {
                    values[index] = 1;
                    index++;
                    state = 'A';
                }
                break;
            default:
                throw Error('Invalid state: ' + state);
        }
    }

    let totalOnes = 0;

    for (let value in values) {
        if (values[value] === 1) {
            totalOnes++;
        }
    }

    return totalOnes;
}

function runPart1() {
    // Answer: 3554
    console.log(part1(12683008));
}

runPart1();
