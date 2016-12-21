using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/21
    //
    // --- Day 21: Scrambled Letters and Hash ---
    //
    // The computer system you're breaking into uses a weird scrambling function to store its passwords. It shouldn't be much trouble to create your own scrambled password
    // so you can add it to the system; you just have to implement the scrambler.
    //
    // The scrambling function is a series of operations(the exact list is provided in your puzzle input). Starting with the password to be scrambled, apply each operation
    // in succession to the string. The individual operations behave as follows:
    // - swap position X with position Y means that the letters at indexes X and Y(counting from 0) should be swapped.
    // - swap letter X with letter Y means that the letters X and Y should be swapped(regardless of where they appear in the string).
    // - rotate left/right X steps means that the whole string should be rotated; for example, one right rotation would turn abcd into dabc.
    // - rotate based on position of letter X means that the whole string should be rotated to the right based on the index of letter X (counting from 0) as determined before
    //   this instruction does any rotations. Once the index is determined, rotate the string to the right one time, plus a number of times equal to that index, plus one
    //   additional time if the index was at least 4.
    // - reverse positions X through Y means that the span of letters at indexes X through Y (including the letters at X and Y) should be reversed in order.
    // - move position X to position Y means that the letter which is at index X should be removed from the string, then inserted such that it ends up at index Y.
    //
    // For example, suppose you start with abcde and perform the following operations:
    // - swap position 4 with position 0 swaps the first and last letters, producing the input for the next step, ebcda.
    // - swap letter d with letter b swaps the positions of d and b: edcba.
    // - reverse positions 0 through 4 causes the entire string to be reversed, producing abcde.
    // - rotate left 1 step shifts all letters left one position, causing the first letter to wrap to the end of the string: bcdea.
    // - move position 1 to position 4 removes the letter at position 1 (c), then inserts it at position 4 (the end of the string): bdeac.
    // - move position 3 to position 0 removes the letter at position 3 (a), then inserts it at position 0 (the front of the string): abdec.
    // - rotate based on position of letter b finds the index of letter b (1), then rotates the string right once plus a number of times equal to that index (2): ecabd.
    // - rotate based on position of letter d finds the index of letter d (4), then rotates the string right once, plus a number of times equal to that index, plus an
    //   additional time because the index was at least 4, for a total of 6 right rotations: decab.
    //
    // After these steps, the resulting scrambled password is decab.
    //
    // Now, you just need to generate a new scrambled password and you can access the system.Given the list of scrambling operations in your puzzle input, what is the result
    // of scrambling abcdefgh?
    // rotate based on position of letter d\nmove position 1 to position 6\nswap position 3 with position 6\nrotate based on position of letter c\nswap position 0 with position 1\nrotate right 5 steps\nrotate left 3 steps\nrotate based on position of letter b\nswap position 0 with position 2\nrotate based on position of letter g\nrotate left 0 steps\nreverse positions 0 through 3\nrotate based on position of letter a\nrotate based on position of letter h\nrotate based on position of letter a\nrotate based on position of letter g\nrotate left 5 steps\nmove position 3 to position 7\nrotate right 5 steps\nrotate based on position of letter f\nrotate right 7 steps\nrotate based on position of letter a\nrotate right 6 steps\nrotate based on position of letter a\nswap letter c with letter f\nreverse positions 2 through 6\nrotate left 1 step\nreverse positions 3 through 5\nrotate based on position of letter f\nswap position 6 with position 5\nswap letter h with letter e\nmove position 1 to position 3\nswap letter c with letter h\nreverse positions 4 through 7\nswap letter f with letter h\nrotate based on position of letter f\nrotate based on position of letter g\nreverse positions 3 through 4\nrotate left 7 steps\nswap letter h with letter a\nrotate based on position of letter e\nrotate based on position of letter f\nrotate based on position of letter g\nmove position 5 to position 0\nrotate based on position of letter c\nreverse positions 3 through 6\nrotate right 4 steps\nmove position 1 to position 2\nreverse positions 3 through 6\nswap letter g with letter a\nrotate based on position of letter d\nrotate based on position of letter a\nswap position 0 with position 7\nrotate left 7 steps\nrotate right 2 steps\nrotate right 6 steps\nrotate based on position of letter b\nrotate right 2 steps\nswap position 7 with position 4\nrotate left 4 steps\nrotate left 3 steps\nswap position 2 with position 7\nmove position 5 to position 4\nrotate right 3 steps\nrotate based on position of letter g\nmove position 1 to position 2\nswap position 7 with position 0\nmove position 4 to position 6\nmove position 3 to position 0\nrotate based on position of letter f\nswap letter g with letter d\nswap position 1 with position 5\nreverse positions 0 through 2\nswap position 7 with position 3\nrotate based on position of letter g\nswap letter c with letter a\nrotate based on position of letter g\nreverse positions 3 through 5\nmove position 6 to position 3\nswap letter b with letter e\nreverse positions 5 through 6\nmove position 6 to position 7\nswap letter a with letter e\nswap position 6 with position 2\nmove position 4 to position 5\nrotate left 5 steps\nswap letter a with letter d\nswap letter e with letter g\nswap position 3 with position 7\nreverse positions 0 through 5\nswap position 5 with position 7\nswap position 1 with position 7\nswap position 1 with position 7\nrotate right 7 steps\nswap letter f with letter a\nreverse positions 0 through 7\nrotate based on position of letter d\nreverse positions 2 through 4\nswap position 7 with position 1\nswap letter a with letter h
    // ghfacdbe
    public static class Day21
    {
        public static string ScrambledLettersHash(string input, string instructions)
        {
            var swapPositionRegex = new Regex(@"swap position (\d) with position (\d)", RegexOptions.Compiled);
            var swapLetterRegex = new Regex(@"swap letter ([a-z]) with letter ([a-z])", RegexOptions.Compiled);
            var rotateRegex = new Regex(@"rotate (left|right) (\d) steps?", RegexOptions.Compiled);
            var rotateLetterPositionRegex = new Regex(@"rotate based on position of letter ([a-z])", RegexOptions.Compiled);
            var reversePositionsRegex = new Regex(@"reverse positions (\d) through (\d)", RegexOptions.Compiled);
            var movePositionRegex = new Regex(@"move position (\d) to position (\d)", RegexOptions.Compiled);

            var result = input;

            foreach (var instruction in instructions.Split("\n".ToCharArray()))
            {
                var swapPositionsMatch = swapPositionRegex.Match(instruction);
                var swapLetterMatch = swapLetterRegex.Match(instruction);
                var rotateMatch = rotateRegex.Match(instruction);
                var rotateLetterPositionMatch = rotateLetterPositionRegex.Match(instruction);
                var reversePositionsMatch = reversePositionsRegex.Match(instruction);
                var movePositionMatch = movePositionRegex.Match(instruction);

                if (swapPositionsMatch.Success)
                {
                    var first = int.Parse(swapPositionsMatch.Groups[1].ToString());
                    var second = int.Parse(swapPositionsMatch.Groups[2].ToString());

                    result = result
                        .ReplaceAt(first, result[second])
                        .ReplaceAt(second, result[first]);
                }
                else if (swapLetterMatch.Success)
                {
                    var first = result.IndexOf(swapLetterMatch.Groups[1].ToString());
                    var second = result.IndexOf(swapLetterMatch.Groups[2].ToString());

                    result = result
                        .ReplaceAt(first, result[second])
                        .ReplaceAt(second, result[first]);
                }
                else if (rotateMatch.Success)
                {
                    var direction = rotateMatch.Groups[1].ToString();
                    var distance = int.Parse(rotateMatch.Groups[2].ToString());

                    if (direction == "left")
                    {
                        result = $"{result.Substring(distance)}{result.Substring(0, distance)}";
                    }
                    else
                    {
                        var length = result.Length;
                        result = $"{result.Substring(length - distance)}{result.Substring(0, length - distance)}";
                    }
                }
                else if (rotateLetterPositionMatch.Success)
                {
                    var length = result.Length;

                    var distance = result.IndexOf(rotateLetterPositionMatch.Groups[1].ToString());
                    distance = distance >= 4 ? distance + 1 : distance;
                    distance = (distance + 1) % length;
                    
                    result = $"{result.Substring(length - distance)}{result.Substring(0, length - distance)}";
                }
                else if (reversePositionsMatch.Success)
                {
                    var start = int.Parse(reversePositionsMatch.Groups[1].ToString());
                    var end = int.Parse(reversePositionsMatch.Groups[2].ToString());

                    var original = result.Substring(start, end - start + 1);
                    var reversed = string.Join(string.Empty, original.Reverse());

                    result = result.Replace(original, reversed);
                }
                else if (movePositionMatch.Success)
                {
                    var start = int.Parse(movePositionMatch.Groups[1].ToString());
                    var destination = int.Parse(movePositionMatch.Groups[2].ToString());
                    var value = result[start].ToString();

                    result = result.Remove(start, 1).Insert(destination, value);
                }
                else
                {
                    throw new InvalidOperationException($"Unable to parse instruction: {instruction}");
                }
            }
            
            return result;
        }

        private static string ReplaceAt(this string input, int index, char replacement)
        {
            return input.Remove(index, 1).Insert(index, replacement.ToString());
        }
    }
}
