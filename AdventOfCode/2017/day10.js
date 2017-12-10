/*
--- Day 10: Knot Hash ---

You come across some programs that are trying to implement a software emulation of a hash based on knot-tying. The hash these programs are implementing isn't very strong, but you decide to help them anyway. You make a mental note to remind the Elves later not to invent their own cryptographic functions.

This hash function simulates tying a knot in a circle of string with 256 marks on it. Based on the input to be hashed, the function repeatedly selects a span of string, brings the ends together, and gives the span a half-twist to reverse the order of the marks within it. After doing this many times, the order of the marks is used to build the resulting hash.

  4--5   pinch   4  5           4   1
 /    \  5,0,1  / \/ \  twist  / \ / \
3      0  -->  3      0  -->  3   X   0
 \    /         \ /\ /         \ / \ /
  2--1           2  1           2   5
To achieve this, begin with a list of numbers from 0 to 255, a current position which begins at 0 (the first element in the list), a skip size (which starts at 0), and a sequence of lengths (your puzzle input). Then, for each length:

Reverse the order of that length of elements in the list, starting with the element at the current position.
Move the current position forward by that length plus the skip size.
Increase the skip size by one.
The list is circular; if the current position and the length try to reverse elements beyond the end of the list, the operation reverses using as many extra elements as it needs from the front of the list. If the current position moves past the end of the list, it wraps around to the front. Lengths larger than the size of the list are invalid.

Here's an example using a smaller list:

Suppose we instead only had a circular list containing five elements, 0, 1, 2, 3, 4, and were given input lengths of 3, 4, 1, 5.
- The list begins as [0] 1 2 3 4 (where square brackets indicate the current position).
- The first length, 3, selects ([0] 1 2) 3 4 (where parentheses indicate the sublist to be reversed).
- After reversing that section (0 1 2 into 2 1 0), we get ([2] 1 0) 3 4.
- Then, the current position moves forward by the length, 3, plus the skip size, 0: 2 1 0 [3] 4. Finally, the skip size increases to 1.
- The second length, 4, selects a section which wraps: 2 1) 0 ([3] 4.
- The sublist 3 4 2 1 is reversed to form 1 2 4 3: 4 3) 0 ([1] 2.
- The current position moves forward by the length plus the skip size, a total of 5, causing it not to move because it wraps around: 4 3 0 [1] 2. The skip size increases to 2.
- The third length, 1, selects a sublist of a single element, and so reversing it has no effect.
- The current position moves forward by the length (1) plus the skip size (2): 4 [3] 0 1 2. The skip size increases to 3.
- The fourth length, 5, selects every element starting with the second: 4) ([3] 0 1 2. Reversing this sublist (3 0 1 2 4 into 4 2 1 0 3) produces: 3) ([4] 2 1 0.
- Finally, the current position moves forward by 8: 3 4 2 1 [0]. The skip size increases to 4.

In this example, the first two numbers in the list end up being 3 and 4; to check the process, you can multiply them together to produce 12.

However, you should instead use the standard list size of 256 (with values 0 to 255) and the sequence of lengths in your puzzle input. Once this process is complete, what is the result of multiplying the first two numbers in the list?
 */

const Utils = require('./utils.js');

/*function part1(length, input) {
    let lengths = input.split(',').map(v => parseInt(v));

    let list = [];

    for (let i = 0; i < length; i++) {
        list.push(i);
    }

    // Always reset curPos to 0 for each iteration?
    let curPos = 0;
    let skipSize = 0;
    let startIndex = 0;

    for (let i = 0; i < lengths.length; i++) {
        let curLength = lengths[i];
        let splice = list.splice(0, curLength);

        for (let j = 0; j < splice.length; j++) {
            list.splice(0, 0, splice[j]);
        }

        console.log(list.join(' ') + ' -> index: ' + startIndex + ' value: ' + list[startIndex]);

        curPos += curLength + skipSize;
        skipSize++;

        curPos %= length;

        let newList = [];

        for (let j = 0; j < list.length; j++) {
            newList.push(list[(j + curPos) % list.length]);
        }

        list = newList;
        startIndex = (startIndex + list.length - curPos - 1) % list.length;
        curPos = 0;
    }

    return list[startIndex - 1] * list[startIndex];
    //return list[startIndex] * list[(startIndex + 1) % list.length];
}*/

function part1(listLength, input) {
    let lengths = input.split(',').map(v => parseInt(v));

    let list = [];

    for (let i = 0; i < listLength; i++) {
        list.push(i);
    }

    let curPos = 0;
    let skipSize = 0;

    for (let i = 0; i < lengths.length; i++) {
        let curLength = lengths[i];
        list = reverse(list, curPos, curLength);

        curPos += curLength + skipSize;
        skipSize++;

        curPos %= list.length;
    }

    return list[0] * list[1];
}

function reverse(arr, start, length) {
    if (start < 0 || length < 0 || length > arr.length) {
        throw Error('Invalid request for array');
    }

    let reversedArr = [];

    for (let i = 0; i < length; i++) {
        reversedArr.push(arr[(start + i) % arr.length]);
    }

    reversedArr.reverse();

    for (let i = 0; i < length; i++) {
        arr[(start + i) % arr.length] = reversedArr[i];
    }

    return arr;
}

function runPart1() {
    /*console.log([1, 2, 3] + ' = ' + reverse([1, 2, 3], 0, 0));
    console.log([1, 2, 3] + ' = ' + reverse([1, 2, 3], 0, 1));
    console.log([2, 1, 3] + ' = ' + reverse([1, 2, 3], 0, 2));
    console.log([3, 2, 1] + ' = ' + reverse([1, 2, 3], 0, 3));
    console.log([1, 3, 2] + ' = ' + reverse([1, 2, 3], 1, 2));
    console.log([3, 2, 1] + ' = ' + reverse([1, 2, 3], 2, 2));
    console.log([1, 2, 3, 4, 5] + ' = ' + reverse([1, 2, 3, 4, 5], ));*/
    Utils.assertAreEqual(12, part1(5, '3,4,1,5'));

    // Answer: 62238
    console.log(part1(256, '157,222,1,2,177,254,0,228,159,140,249,187,255,51,76,30'));
}

function part2(input) {
}

function runPart2() {
    //Utils.assertAreEqual(6, part2('1212'));

    // Answer: XXX
    console.log(part2(''));
}

runPart1();
runPart2();
