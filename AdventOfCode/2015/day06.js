/*
--- Day 6: Probably a Fire Hazard ---

Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided to deploy one million lights in a 1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa has mailed you instructions on how to display the ideal lighting configuration.

Lights in your grid are numbered from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs. Each coordinate pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to 9 lights in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up your lights by doing the instructions Santa sent you in order.

For example:
- turn on 0,0 through 999,999 would turn on (or leave on) every light.
-  toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning off the ones that were on, and turning on the ones that were off.
- turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.

After following the instructions, how many lights are lit?

--- Part Two ---

You just finish implementing your winning light pattern when you realize you mistranslated Santa's message from Ancient Nordic Elvish.

The light grid you bought actually has individual brightness controls; each light can have a brightness of zero or more. The lights all start at zero.

The phrase turn on actually means that you should increase the brightness of those lights by 1.

The phrase turn off actually means that you should decrease the brightness of those lights by 1, to a minimum of zero.

The phrase toggle actually means that you should increase the brightness of those lights by 2.

What is the total brightness of all lights combined after following Santa's instructions?

For example:
- turn on 0,0 through 0,0 would increase the total brightness by 1.
- toggle 0,0 through 999,999 would increase the total brightness by 2000000.
 */

const Utils = require('./utils.js');

// The runtime for this for the puzzle input is ~10 sec. Using arrays would probably be faster, but I'd need to verify.
function part1(input) {
    let lights = new Set();
    let regex = /(turn on|toggle|turn off) (\d+),(\d+) through (\d+),(\d+)/;

    input.split('\n').filter(line => line).forEach(line => {
        let match = regex.exec(line);

        if (!match) {
            throw Error('Invalid line format');
        }

        let minX = parseInt(match[2]);
        let minY = parseInt(match[3]);
        let maxX = parseInt(match[4]);
        let maxY = parseInt(match[5]);

        for (let i = minX; i <= maxX; i++) {
            for (let j = minY; j <= maxY; j++) {
                const key = i + '_' + j;
                if (match[1] === 'turn on') {
                    lights.add(key);
                } else if (match[1] === 'toggle') {
                    if (lights.has(key)) {
                        lights.delete(key);
                    } else {
                        lights.add(key);
                    }
                } else if (match[1] === 'turn off') {
                    lights.delete(key);
                } else {
                    throw new Error('Unsupported command');
                }
            }
        }
    });

    return lights.size;
}

function runPart1() {
    Utils.assertAreEqual(9, part1('turn on 0,0 through 2,2'));
    Utils.assertAreEqual(5, part1('turn on 0,0 through 2,2\ntoggle 0,0 through 1,1'))

    // Answer: 400410
    console.log(part1('turn off 660,55 through 986,197\nturn off 341,304 through 638,850\nturn off 199,133 through 461,193\ntoggle 322,558 through 977,958\ntoggle 537,781 through 687,941\nturn on 226,196 through 599,390\nturn on 240,129 through 703,297\nturn on 317,329 through 451,798\nturn on 957,736 through 977,890\nturn on 263,530 through 559,664\nturn on 158,270 through 243,802\ntoggle 223,39 through 454,511\ntoggle 544,218 through 979,872\nturn on 313,306 through 363,621\ntoggle 173,401 through 496,407\ntoggle 333,60 through 748,159\nturn off 87,577 through 484,608\nturn on 809,648 through 826,999\ntoggle 352,432 through 628,550\nturn off 197,408 through 579,569\nturn off 1,629 through 802,633\nturn off 61,44 through 567,111\ntoggle 880,25 through 903,973\nturn on 347,123 through 864,746\ntoggle 728,877 through 996,975\nturn on 121,895 through 349,906\nturn on 888,547 through 931,628\ntoggle 398,782 through 834,882\nturn on 966,850 through 989,953\nturn off 891,543 through 914,991\ntoggle 908,77 through 916,117\nturn on 576,900 through 943,934\nturn off 580,170 through 963,206\nturn on 184,638 through 192,944\ntoggle 940,147 through 978,730\nturn off 854,56 through 965,591\ntoggle 717,172 through 947,995\ntoggle 426,987 through 705,998\nturn on 987,157 through 992,278\ntoggle 995,774 through 997,784\nturn off 796,96 through 845,182\nturn off 451,87 through 711,655\nturn off 380,93 through 968,676\nturn on 263,468 through 343,534\nturn on 917,936 through 928,959\ntoggle 478,7 through 573,148\nturn off 428,339 through 603,624\nturn off 400,880 through 914,953\ntoggle 679,428 through 752,779\nturn off 697,981 through 709,986\ntoggle 482,566 through 505,725\nturn off 956,368 through 993,516\ntoggle 735,823 through 783,883\nturn off 48,487 through 892,496\nturn off 116,680 through 564,819\nturn on 633,865 through 729,930\nturn off 314,618 through 571,922\ntoggle 138,166 through 936,266\nturn on 444,732 through 664,960\nturn off 109,337 through 972,497\nturn off 51,432 through 77,996\nturn off 259,297 through 366,744\ntoggle 801,130 through 917,544\ntoggle 767,982 through 847,996\nturn on 216,507 through 863,885\nturn off 61,441 through 465,731\nturn on 849,970 through 944,987\ntoggle 845,76 through 852,951\ntoggle 732,615 through 851,936\ntoggle 251,128 through 454,778\nturn on 324,429 through 352,539\ntoggle 52,450 through 932,863\nturn off 449,379 through 789,490\nturn on 317,319 through 936,449\ntoggle 887,670 through 957,838\ntoggle 671,613 through 856,664\nturn off 186,648 through 985,991\nturn off 471,689 through 731,717\ntoggle 91,331 through 750,758\ntoggle 201,73 through 956,524\ntoggle 82,614 through 520,686\ntoggle 84,287 through 467,734\nturn off 132,367 through 208,838\ntoggle 558,684 through 663,920\nturn on 237,952 through 265,997\nturn on 694,713 through 714,754\nturn on 632,523 through 862,827\nturn on 918,780 through 948,916\nturn on 349,586 through 663,976\ntoggle 231,29 through 257,589\ntoggle 886,428 through 902,993\nturn on 106,353 through 236,374\nturn on 734,577 through 759,684\nturn off 347,843 through 696,912\nturn on 286,699 through 964,883\nturn on 605,875 through 960,987\nturn off 328,286 through 869,461\nturn off 472,569 through 980,848\ntoggle 673,573 through 702,884\nturn off 398,284 through 738,332\nturn on 158,50 through 284,411\nturn off 390,284 through 585,663\nturn on 156,579 through 646,581\nturn on 875,493 through 989,980\ntoggle 486,391 through 924,539\nturn on 236,722 through 272,964\ntoggle 228,282 through 470,581\ntoggle 584,389 through 750,761\nturn off 899,516 through 900,925\nturn on 105,229 through 822,846\nturn off 253,77 through 371,877\nturn on 826,987 through 906,992\nturn off 13,152 through 615,931\nturn on 835,320 through 942,399\nturn on 463,504 through 536,720\ntoggle 746,942 through 786,998\nturn off 867,333 through 965,403\nturn on 591,477 through 743,692\nturn off 403,437 through 508,908\nturn on 26,723 through 368,814\nturn on 409,485 through 799,809\nturn on 115,630 through 704,705\nturn off 228,183 through 317,220\ntoggle 300,649 through 382,842\nturn off 495,365 through 745,562\nturn on 698,346 through 744,873\nturn on 822,932 through 951,934\ntoggle 805,30 through 925,421\ntoggle 441,152 through 653,274\ntoggle 160,81 through 257,587\nturn off 350,781 through 532,917\ntoggle 40,583 through 348,636\nturn on 280,306 through 483,395\ntoggle 392,936 through 880,955\ntoggle 496,591 through 851,934\nturn off 780,887 through 946,994\nturn off 205,735 through 281,863\ntoggle 100,876 through 937,915\nturn on 392,393 through 702,878\nturn on 956,374 through 976,636\ntoggle 478,262 through 894,775\nturn off 279,65 through 451,677\nturn on 397,541 through 809,847\nturn on 444,291 through 451,586\ntoggle 721,408 through 861,598\nturn on 275,365 through 609,382\nturn on 736,24 through 839,72\nturn off 86,492 through 582,712\nturn on 676,676 through 709,703\nturn off 105,710 through 374,817\ntoggle 328,748 through 845,757\ntoggle 335,79 through 394,326\ntoggle 193,157 through 633,885\nturn on 227,48 through 769,743\ntoggle 148,333 through 614,568\ntoggle 22,30 through 436,263\ntoggle 547,447 through 688,969\ntoggle 576,621 through 987,740\nturn on 711,334 through 799,515\nturn on 541,448 through 654,951\ntoggle 792,199 through 798,990\nturn on 89,956 through 609,960\ntoggle 724,433 through 929,630\ntoggle 144,895 through 201,916\ntoggle 226,730 through 632,871\nturn off 760,819 through 828,974\ntoggle 887,180 through 940,310\ntoggle 222,327 through 805,590\nturn off 630,824 through 885,963\nturn on 940,740 through 954,946\nturn on 193,373 through 779,515\ntoggle 304,955 through 469,975\nturn off 405,480 through 546,960\nturn on 662,123 through 690,669\nturn off 615,238 through 750,714\nturn on 423,220 through 930,353\nturn on 329,769 through 358,970\ntoggle 590,151 through 704,722\nturn off 884,539 through 894,671\ntoggle 449,241 through 984,549\ntoggle 449,260 through 496,464\nturn off 306,448 through 602,924\nturn on 286,805 through 555,901\ntoggle 722,177 through 922,298\ntoggle 491,554 through 723,753\nturn on 80,849 through 174,996\nturn off 296,561 through 530,856\ntoggle 653,10 through 972,284\ntoggle 529,236 through 672,614\ntoggle 791,598 through 989,695\nturn on 19,45 through 575,757\ntoggle 111,55 through 880,871\nturn off 197,897 through 943,982\nturn on 912,336 through 977,605\ntoggle 101,221 through 537,450\nturn on 101,104 through 969,447\ntoggle 71,527 through 587,717\ntoggle 336,445 through 593,889\ntoggle 214,179 through 575,699\nturn on 86,313 through 96,674\ntoggle 566,427 through 906,888\nturn off 641,597 through 850,845\nturn on 606,524 through 883,704\nturn on 835,775 through 867,887\ntoggle 547,301 through 897,515\ntoggle 289,930 through 413,979\nturn on 361,122 through 457,226\nturn on 162,187 through 374,746\nturn on 348,461 through 454,675\nturn off 966,532 through 985,537\nturn on 172,354 through 630,606\nturn off 501,880 through 680,993\nturn off 8,70 through 566,592\ntoggle 433,73 through 690,651\ntoggle 840,798 through 902,971\ntoggle 822,204 through 893,760\nturn off 453,496 through 649,795\nturn off 969,549 through 990,942\nturn off 789,28 through 930,267\ntoggle 880,98 through 932,434\ntoggle 568,674 through 669,753\nturn on 686,228 through 903,271\nturn on 263,995 through 478,999\ntoggle 534,675 through 687,955\nturn off 342,434 through 592,986\ntoggle 404,768 through 677,867\ntoggle 126,723 through 978,987\ntoggle 749,675 through 978,959\nturn off 445,330 through 446,885\nturn off 463,205 through 924,815\nturn off 417,430 through 915,472\nturn on 544,990 through 912,999\nturn off 201,255 through 834,789\nturn off 261,142 through 537,862\nturn off 562,934 through 832,984\nturn off 459,978 through 691,980\nturn off 73,911 through 971,972\nturn on 560,448 through 723,810\nturn on 204,630 through 217,854\nturn off 91,259 through 611,607\nturn on 877,32 through 978,815\nturn off 950,438 through 974,746\ntoggle 426,30 through 609,917\ntoggle 696,37 through 859,201\ntoggle 242,417 through 682,572\nturn off 388,401 through 979,528\nturn off 79,345 through 848,685\nturn off 98,91 through 800,434\ntoggle 650,700 through 972,843\nturn off 530,450 through 538,926\nturn on 428,559 through 962,909\nturn on 78,138 through 92,940\ntoggle 194,117 through 867,157\ntoggle 785,355 through 860,617\nturn off 379,441 through 935,708\nturn off 605,133 through 644,911\ntoggle 10,963 through 484,975\nturn off 359,988 through 525,991\nturn off 509,138 through 787,411\ntoggle 556,467 through 562,773\nturn on 119,486 through 246,900\nturn on 445,561 through 794,673\nturn off 598,681 through 978,921\nturn off 974,230 through 995,641\nturn off 760,75 through 800,275\ntoggle 441,215 through 528,680\nturn off 701,636 through 928,877\nturn on 165,753 through 202,780\ntoggle 501,412 through 998,516\ntoggle 161,105 through 657,395\nturn on 113,340 through 472,972\ntoggle 384,994 through 663,999\nturn on 969,994 through 983,997\nturn on 519,600 through 750,615\nturn off 363,899 through 948,935\nturn on 271,845 through 454,882\nturn off 376,528 through 779,640\ntoggle 767,98 through 854,853\ntoggle 107,322 through 378,688\nturn off 235,899 through 818,932\nturn on 445,611 through 532,705\ntoggle 629,387 through 814,577\ntoggle 112,414 through 387,421\ntoggle 319,184 through 382,203\nturn on 627,796 through 973,940\ntoggle 602,45 through 763,151\nturn off 441,375 through 974,545\ntoggle 871,952 through 989,998\nturn on 717,272 through 850,817\ntoggle 475,711 through 921,882\ntoggle 66,191 through 757,481\nturn off 50,197 through 733,656\ntoggle 83,575 through 915,728\nturn on 777,812 through 837,912\nturn on 20,984 through 571,994\nturn off 446,432 through 458,648\nturn on 715,871 through 722,890\ntoggle 424,675 through 740,862\ntoggle 580,592 through 671,900\ntoggle 296,687 through 906,775'));
}

function part2(input) {
    let lights = {};
    let regex = /(turn on|toggle|turn off) (\d+),(\d+) through (\d+),(\d+)/;

    input.split('\n').filter(line => line).forEach(line => {
        let match = regex.exec(line);

        if (!match) {
            throw Error('Invalid line format');
        }

        let minX = parseInt(match[2]);
        let minY = parseInt(match[3]);
        let maxX = parseInt(match[4]);
        let maxY = parseInt(match[5]);

        for (let i = minX; i <= maxX; i++) {
            for (let j = minY; j <= maxY; j++) {
                const key = i + '_' + j;
                if (match[1] === 'turn on') {
                    if (lights[key] === undefined) {
                        lights[key] = 1;
                    } else {
                        lights[key]++;
                    }
                } else if (match[1] === 'toggle') {
                    if (lights[key] === undefined) {
                        lights[key] = 2;
                    } else {
                        lights[key] += 2;
                    }
                } else if (match[1] === 'turn off') {
                    const value = lights[key];
                    if (value !== undefined && value > 0) {
                        lights[key] = value - 1;
                    }
                } else {
                    throw new Error('Unsupported command');
                }
            }
        }
    });

    let totalLight = 0;
    for (var light in lights) {
        totalLight += lights[light];
    }

    return totalLight;
}

function runPart2() {
    Utils.assertAreEqual(9, part2('turn on 0,0 through 2,2'));

    // Answer: 15343601
    console.log(part2('turn off 660,55 through 986,197\nturn off 341,304 through 638,850\nturn off 199,133 through 461,193\ntoggle 322,558 through 977,958\ntoggle 537,781 through 687,941\nturn on 226,196 through 599,390\nturn on 240,129 through 703,297\nturn on 317,329 through 451,798\nturn on 957,736 through 977,890\nturn on 263,530 through 559,664\nturn on 158,270 through 243,802\ntoggle 223,39 through 454,511\ntoggle 544,218 through 979,872\nturn on 313,306 through 363,621\ntoggle 173,401 through 496,407\ntoggle 333,60 through 748,159\nturn off 87,577 through 484,608\nturn on 809,648 through 826,999\ntoggle 352,432 through 628,550\nturn off 197,408 through 579,569\nturn off 1,629 through 802,633\nturn off 61,44 through 567,111\ntoggle 880,25 through 903,973\nturn on 347,123 through 864,746\ntoggle 728,877 through 996,975\nturn on 121,895 through 349,906\nturn on 888,547 through 931,628\ntoggle 398,782 through 834,882\nturn on 966,850 through 989,953\nturn off 891,543 through 914,991\ntoggle 908,77 through 916,117\nturn on 576,900 through 943,934\nturn off 580,170 through 963,206\nturn on 184,638 through 192,944\ntoggle 940,147 through 978,730\nturn off 854,56 through 965,591\ntoggle 717,172 through 947,995\ntoggle 426,987 through 705,998\nturn on 987,157 through 992,278\ntoggle 995,774 through 997,784\nturn off 796,96 through 845,182\nturn off 451,87 through 711,655\nturn off 380,93 through 968,676\nturn on 263,468 through 343,534\nturn on 917,936 through 928,959\ntoggle 478,7 through 573,148\nturn off 428,339 through 603,624\nturn off 400,880 through 914,953\ntoggle 679,428 through 752,779\nturn off 697,981 through 709,986\ntoggle 482,566 through 505,725\nturn off 956,368 through 993,516\ntoggle 735,823 through 783,883\nturn off 48,487 through 892,496\nturn off 116,680 through 564,819\nturn on 633,865 through 729,930\nturn off 314,618 through 571,922\ntoggle 138,166 through 936,266\nturn on 444,732 through 664,960\nturn off 109,337 through 972,497\nturn off 51,432 through 77,996\nturn off 259,297 through 366,744\ntoggle 801,130 through 917,544\ntoggle 767,982 through 847,996\nturn on 216,507 through 863,885\nturn off 61,441 through 465,731\nturn on 849,970 through 944,987\ntoggle 845,76 through 852,951\ntoggle 732,615 through 851,936\ntoggle 251,128 through 454,778\nturn on 324,429 through 352,539\ntoggle 52,450 through 932,863\nturn off 449,379 through 789,490\nturn on 317,319 through 936,449\ntoggle 887,670 through 957,838\ntoggle 671,613 through 856,664\nturn off 186,648 through 985,991\nturn off 471,689 through 731,717\ntoggle 91,331 through 750,758\ntoggle 201,73 through 956,524\ntoggle 82,614 through 520,686\ntoggle 84,287 through 467,734\nturn off 132,367 through 208,838\ntoggle 558,684 through 663,920\nturn on 237,952 through 265,997\nturn on 694,713 through 714,754\nturn on 632,523 through 862,827\nturn on 918,780 through 948,916\nturn on 349,586 through 663,976\ntoggle 231,29 through 257,589\ntoggle 886,428 through 902,993\nturn on 106,353 through 236,374\nturn on 734,577 through 759,684\nturn off 347,843 through 696,912\nturn on 286,699 through 964,883\nturn on 605,875 through 960,987\nturn off 328,286 through 869,461\nturn off 472,569 through 980,848\ntoggle 673,573 through 702,884\nturn off 398,284 through 738,332\nturn on 158,50 through 284,411\nturn off 390,284 through 585,663\nturn on 156,579 through 646,581\nturn on 875,493 through 989,980\ntoggle 486,391 through 924,539\nturn on 236,722 through 272,964\ntoggle 228,282 through 470,581\ntoggle 584,389 through 750,761\nturn off 899,516 through 900,925\nturn on 105,229 through 822,846\nturn off 253,77 through 371,877\nturn on 826,987 through 906,992\nturn off 13,152 through 615,931\nturn on 835,320 through 942,399\nturn on 463,504 through 536,720\ntoggle 746,942 through 786,998\nturn off 867,333 through 965,403\nturn on 591,477 through 743,692\nturn off 403,437 through 508,908\nturn on 26,723 through 368,814\nturn on 409,485 through 799,809\nturn on 115,630 through 704,705\nturn off 228,183 through 317,220\ntoggle 300,649 through 382,842\nturn off 495,365 through 745,562\nturn on 698,346 through 744,873\nturn on 822,932 through 951,934\ntoggle 805,30 through 925,421\ntoggle 441,152 through 653,274\ntoggle 160,81 through 257,587\nturn off 350,781 through 532,917\ntoggle 40,583 through 348,636\nturn on 280,306 through 483,395\ntoggle 392,936 through 880,955\ntoggle 496,591 through 851,934\nturn off 780,887 through 946,994\nturn off 205,735 through 281,863\ntoggle 100,876 through 937,915\nturn on 392,393 through 702,878\nturn on 956,374 through 976,636\ntoggle 478,262 through 894,775\nturn off 279,65 through 451,677\nturn on 397,541 through 809,847\nturn on 444,291 through 451,586\ntoggle 721,408 through 861,598\nturn on 275,365 through 609,382\nturn on 736,24 through 839,72\nturn off 86,492 through 582,712\nturn on 676,676 through 709,703\nturn off 105,710 through 374,817\ntoggle 328,748 through 845,757\ntoggle 335,79 through 394,326\ntoggle 193,157 through 633,885\nturn on 227,48 through 769,743\ntoggle 148,333 through 614,568\ntoggle 22,30 through 436,263\ntoggle 547,447 through 688,969\ntoggle 576,621 through 987,740\nturn on 711,334 through 799,515\nturn on 541,448 through 654,951\ntoggle 792,199 through 798,990\nturn on 89,956 through 609,960\ntoggle 724,433 through 929,630\ntoggle 144,895 through 201,916\ntoggle 226,730 through 632,871\nturn off 760,819 through 828,974\ntoggle 887,180 through 940,310\ntoggle 222,327 through 805,590\nturn off 630,824 through 885,963\nturn on 940,740 through 954,946\nturn on 193,373 through 779,515\ntoggle 304,955 through 469,975\nturn off 405,480 through 546,960\nturn on 662,123 through 690,669\nturn off 615,238 through 750,714\nturn on 423,220 through 930,353\nturn on 329,769 through 358,970\ntoggle 590,151 through 704,722\nturn off 884,539 through 894,671\ntoggle 449,241 through 984,549\ntoggle 449,260 through 496,464\nturn off 306,448 through 602,924\nturn on 286,805 through 555,901\ntoggle 722,177 through 922,298\ntoggle 491,554 through 723,753\nturn on 80,849 through 174,996\nturn off 296,561 through 530,856\ntoggle 653,10 through 972,284\ntoggle 529,236 through 672,614\ntoggle 791,598 through 989,695\nturn on 19,45 through 575,757\ntoggle 111,55 through 880,871\nturn off 197,897 through 943,982\nturn on 912,336 through 977,605\ntoggle 101,221 through 537,450\nturn on 101,104 through 969,447\ntoggle 71,527 through 587,717\ntoggle 336,445 through 593,889\ntoggle 214,179 through 575,699\nturn on 86,313 through 96,674\ntoggle 566,427 through 906,888\nturn off 641,597 through 850,845\nturn on 606,524 through 883,704\nturn on 835,775 through 867,887\ntoggle 547,301 through 897,515\ntoggle 289,930 through 413,979\nturn on 361,122 through 457,226\nturn on 162,187 through 374,746\nturn on 348,461 through 454,675\nturn off 966,532 through 985,537\nturn on 172,354 through 630,606\nturn off 501,880 through 680,993\nturn off 8,70 through 566,592\ntoggle 433,73 through 690,651\ntoggle 840,798 through 902,971\ntoggle 822,204 through 893,760\nturn off 453,496 through 649,795\nturn off 969,549 through 990,942\nturn off 789,28 through 930,267\ntoggle 880,98 through 932,434\ntoggle 568,674 through 669,753\nturn on 686,228 through 903,271\nturn on 263,995 through 478,999\ntoggle 534,675 through 687,955\nturn off 342,434 through 592,986\ntoggle 404,768 through 677,867\ntoggle 126,723 through 978,987\ntoggle 749,675 through 978,959\nturn off 445,330 through 446,885\nturn off 463,205 through 924,815\nturn off 417,430 through 915,472\nturn on 544,990 through 912,999\nturn off 201,255 through 834,789\nturn off 261,142 through 537,862\nturn off 562,934 through 832,984\nturn off 459,978 through 691,980\nturn off 73,911 through 971,972\nturn on 560,448 through 723,810\nturn on 204,630 through 217,854\nturn off 91,259 through 611,607\nturn on 877,32 through 978,815\nturn off 950,438 through 974,746\ntoggle 426,30 through 609,917\ntoggle 696,37 through 859,201\ntoggle 242,417 through 682,572\nturn off 388,401 through 979,528\nturn off 79,345 through 848,685\nturn off 98,91 through 800,434\ntoggle 650,700 through 972,843\nturn off 530,450 through 538,926\nturn on 428,559 through 962,909\nturn on 78,138 through 92,940\ntoggle 194,117 through 867,157\ntoggle 785,355 through 860,617\nturn off 379,441 through 935,708\nturn off 605,133 through 644,911\ntoggle 10,963 through 484,975\nturn off 359,988 through 525,991\nturn off 509,138 through 787,411\ntoggle 556,467 through 562,773\nturn on 119,486 through 246,900\nturn on 445,561 through 794,673\nturn off 598,681 through 978,921\nturn off 974,230 through 995,641\nturn off 760,75 through 800,275\ntoggle 441,215 through 528,680\nturn off 701,636 through 928,877\nturn on 165,753 through 202,780\ntoggle 501,412 through 998,516\ntoggle 161,105 through 657,395\nturn on 113,340 through 472,972\ntoggle 384,994 through 663,999\nturn on 969,994 through 983,997\nturn on 519,600 through 750,615\nturn off 363,899 through 948,935\nturn on 271,845 through 454,882\nturn off 376,528 through 779,640\ntoggle 767,98 through 854,853\ntoggle 107,322 through 378,688\nturn off 235,899 through 818,932\nturn on 445,611 through 532,705\ntoggle 629,387 through 814,577\ntoggle 112,414 through 387,421\ntoggle 319,184 through 382,203\nturn on 627,796 through 973,940\ntoggle 602,45 through 763,151\nturn off 441,375 through 974,545\ntoggle 871,952 through 989,998\nturn on 717,272 through 850,817\ntoggle 475,711 through 921,882\ntoggle 66,191 through 757,481\nturn off 50,197 through 733,656\ntoggle 83,575 through 915,728\nturn on 777,812 through 837,912\nturn on 20,984 through 571,994\nturn off 446,432 through 458,648\nturn on 715,871 through 722,890\ntoggle 424,675 through 740,862\ntoggle 580,592 through 671,900\ntoggle 296,687 through 906,775'));
}

runPart1();
runPart2();
