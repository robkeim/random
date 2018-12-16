using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day16
    {
        public static int Part1(string input)
        {
            var lines = input.Split("\n".ToCharArray());
            var registersRegex = new Regex(@"\[([0-9 ,]+)\]");
            var result = 0;

            var i = 0;

            while (i < lines.Length)
            {
                var before = registersRegex.Match(lines[i++])
                    .Groups[1]
                    .ToString()
                    .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var operation = lines[i++]
                    .Split(" ".ToCharArray())
                    .Select(int.Parse)
                    .ToArray();
                
                var after = registersRegex.Match(lines[i++])
                    .Groups[1]
                    .ToString()
                    .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                i++; // Skip empty line

                var numMatches = 0;

                var operationId = 0;

                while (numMatches < 3 && operationId < 16)
                {
                    var operationResult = ApplyOperation(before, operation, operationId);

                    if (after.SequenceEqual(operationResult))
                    {
                        numMatches++;
                    }

                    operationId++;
                }

                if (numMatches >= 3)
                {
                    result++;
                }
            }

            return result;
        }

        private static int[] ApplyOperation(int[] inputRegisters, int[] operation, int operationId)
        {
            var a = operation[1];
            var b = operation[2];
            var c = operation[3];

            var result = (int[])inputRegisters.Clone();

            switch (operationId)
            {
                case 0:
                    // addr
                    result[c] = result[a] + result[b];
                    break;
                case 1:
                    // addi
                    result[c] = result[a] + b;
                    break;
                case 2:
                    // mulr
                    result[c] = result[a] * result[b];
                    break;
                case 3:
                    // muli
                    result[c] = result[a] * b;
                    break;
                case 4:
                    // banr
                    result[c] = result[a] & result[b];
                    break;
                case 5:
                    // bani
                    result[c] = result[a] & b;
                    break;
                case 6:
                    // borr
                    result[c] = result[a] | result[b];
                    break;
                case 7:
                    // bori
                    result[c] = result[a] | b;
                    break;
                case 8:
                    // setr
                    result[c] = result[a];
                    break;
                case 9:
                    // seti
                    result[c] = a;
                    break;
                case 10:
                    // gtir
                    result[c] = a > result[b] ? 1 : 0;
                    break;
                case 11:
                    // gtri
                    result[c] = result[a] > b ? 1 : 0;
                    break;
                case 12:
                    // gtrr
                    result[c] = result[a] > result[b] ? 1 : 0;
                    break;
                case 13:
                    // eqir
                    result[c] = a == result[b] ? 1 : 0;
                    break;
                case 14:
                    // eqri
                    result[c] = result[a] == b ? 1 : 0;
                    break;
                case 15:
                    // eqrr
                    result[c] = result[a] == result[b] ? 1 : 0;
                    break;
                default:
                    throw new Exception("Unrecognized operation");
            }

            return result;
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
