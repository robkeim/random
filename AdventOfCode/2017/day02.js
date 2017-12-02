/*
As you walk through the door, a glowing humanoid shape yells in your direction. "You there! Your state appears to be idle. Come help us repair the corruption in this spreadsheet - if we take another millisecond, we'll have to display an hourglass cursor!"

The spreadsheet consists of rows of apparently-random numbers. To make sure the recovery process is on the right track, they need you to calculate the spreadsheet's checksum. For each row, determine the difference between the largest value and the smallest value; the checksum is the sum of all of these differences.

For example, given the following spreadsheet:
    5 1 9 5
    7 5 3
    2 4 6 8

- The first row's largest and smallest values are 9 and 1, and their difference is 8.
- The second row's largest and smallest values are 7 and 3, and their difference is 4.
- The third row's difference is 6.
- In this example, the spreadsheet's checksum would be 8 + 4 + 6 = 18.

What is the checksum for the spreadsheet in your puzzle input?
 */

const Utils = require('./utils.js');

function part1(input) {
    return input.split('\n').map(line => {
        let values = line.split(' ').map(value => parseInt(value));
        let max = values.reduce((a, b) => Math.max(a, b));
        let min = values.reduce((a, b) => Math.min(a, b));
        return max - min;
    }).reduce((a, b) => a + b, 0);
}

function runPart1() {
    Utils.assertAreEqual(18, part1('5 1 9 5\n7 5 3\n2 4 6 8'));

    // Answer: 41887
    console.log(part1('116 1259 1045 679 1334 157 277 1217 218 641 1089 136 247 1195 239 834\n269 1751 732 3016 260 6440 5773 4677 306 230 6928 7182 231 2942 2738 3617\n644 128 89 361 530 97 35 604 535 297 599 121 567 106 114 480\n105 408 120 363 430 102 137 283 123 258 19 101 181 477 463 279\n873 116 840 105 285 238 540 22 117 125 699 953 920 106 113 259\n3695 161 186 2188 3611 2802 157 2154 3394 145 2725 1327 3741 2493 3607 4041\n140 1401 110 119 112 1586 125 937 1469 1015 879 1798 122 1151 100 926\n2401 191 219 607 267 2362 932 2283 889 2567 2171 2409 1078 2247 2441 245\n928 1142 957 1155 922 1039 452 285 467 305 506 221 281 59 667 232\n3882 1698 170 5796 2557 173 1228 4630 174 3508 5629 4395 180 5100 2814 2247\n396 311 223 227 340 313 355 469 229 162 107 76 363 132 453 161\n627 1331 1143 1572 966 388 198 2068 201 239 176 1805 1506 1890 1980 1887\n3390 5336 1730 4072 5342 216 3823 85 5408 5774 247 5308 232 256 5214 787\n176 1694 1787 1586 3798 4243 157 4224 3603 2121 3733 851 2493 4136 148 153\n2432 4030 3397 4032 3952 2727 157 3284 3450 3229 4169 3471 4255 155 127 186\n919 615 335 816 138 97 881 790 855 89 451 789 423 108 95 116'));
}

runPart1();
