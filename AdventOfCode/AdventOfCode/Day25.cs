using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/25
    //
    // --- Day 25: Clock Signal ---

    // You open the door and find yourself on the roof.The city sprawls away from you for miles and miles.

    // There's not much time now - it's already Christmas, but you're nowhere near the North Pole, much too far to deliver these stars to the sleigh in time.

    // However, maybe the huge antenna up here can offer a solution. After all, the sleigh doesn't need the stars, exactly; it needs the timing data they provide, and you happen to have a massive signal generator right here.

    // You connect the stars you have to your prototype computer, connect that to the antenna, and begin the transmission.

    // Nothing happens.

    // You call the service number printed on the side of the antenna and quickly explain the situation. "I'm not sure what kind of equipment you have connected over there," he says, "but you need a clock signal." You try to explain that this is a signal for a clock.

    // "No, no, a clock signal - timing information so the antenna computer knows how to read the data you're sending it. An endless, alternating pattern of 0, 1, 0, 1, 0, 1, 0, 1, 0, 1...." He trails off.

    // You ask if the antenna can handle a clock signal at the frequency you would need to use for the data from the stars. "There's no way it can! The only antenna we've installed capable of that is on top of a top-secret Easter Bunny installation, and you're definitely not-" You hang up the phone.

    // You've extracted the antenna's clock signal generation assembunny code (your puzzle input); it looks mostly compatible with code you worked on just recently.

    // This antenna code, being a signal generator, uses one extra instruction:
    // - out x transmits x (either an integer or the value of a register) as the next value for the clock signal.
    //
    // The code takes a value (via register a) that describes the signal to generate, but you're not sure how it's used.You'll have to find the input to produce the right signal through experimentation.

    // What is the lowest positive integer that can be used to initialize register a and cause the code to output a clock signal of 0, 1, 0, 1... repeating forever?
    // cpy a d\ncpy 7 c\ncpy 365 b\ninc d\ndec b\njnz b -2\ndec c\njnz c -5\ncpy d a\njnz 0 0\ncpy a b\ncpy 0 a\ncpy 2 c\njnz b 2\njnz 1 6\ndec b\ndec c\njnz c -4\ninc a\njnz 1 -7\ncpy 2 b\njnz c 2\njnz 1 4\ndec b\ndec c\njnz 1 -4\njnz 0 0\nout b\njnz a -19\njnz 1 -21
    // Answer: 175
    //
    // --- Part Two ---
    //
    // The antenna is ready.Now, all you need is the fifty stars required to generate the signal for the sleigh, but you don't have enough.
    //
    // You look toward the sky in desperation...suddenly noticing that a lone star has been installed at the top of the antenna! Only 49 more to go.
    public static class Day25
    {
        public static int GetSignalGeneratorSeed(string input)
        {
            var instructions = input
                .Split("\n".ToCharArray())
                .Select(l => new Instruction(l))
                .ToArray();

            int seed = 174;
            
            while (true)
            {
                seed++;
                var result = GenerateSignalValues(instructions, seed).GetEnumerator();

                var isFound = true;
                
                for (int i = 0; i < 30; i++)
                {
                    result.MoveNext();
                    var current = result.Current;
                    var isEven = i % 2 == 0;

                    if ((isEven && current != 0) || (!isEven && current != 1))
                    {
                        isFound = false;
                        break;
                    }
                }

                if (isFound)
                {
                    return seed;
                }
            }
        }

        private static IEnumerable<int> GenerateSignalValues(Instruction[] instructions, int seed)
        {
            var registers = new Dictionary<string, int>
            {
                ["a"] = seed,
                ["b"] = 0,
                ["c"] = 0,
                ["d"] = 0
            };

            var curInstructionIndex = 0;

            while (curInstructionIndex < instructions.Length)
            {
                var curInstruction = instructions[curInstructionIndex];

                switch (curInstruction.Type)
                {
                    case Instruction.InstructionType.Increment:
                        curInstructionIndex++;
                        registers[curInstruction.Arg1Register]++;
                        break;
                    case Instruction.InstructionType.Decrement:
                        curInstructionIndex++;
                        registers[curInstruction.Arg1Register]--;
                        break;
                    case Instruction.InstructionType.Out:
                        curInstructionIndex++;
                        yield return curInstruction.Arg1Value ?? registers[curInstruction.Arg1Register];
                        break;
                    case Instruction.InstructionType.Copy:
                        curInstructionIndex++;
                        if (curInstruction.Arg1Value.HasValue)
                        {
                            registers[curInstruction.Arg2Register] = curInstruction.Arg1Value.Value;
                        }
                        else
                        {
                            registers[curInstruction.Arg2Register] = registers[curInstruction.Arg1Register];
                        }
                        break;
                    case Instruction.InstructionType.Jump:
                        var val = curInstruction.Arg1Value ?? registers[curInstruction.Arg1Register];

                        if (val == 0)
                        {
                            curInstructionIndex++;
                        }
                        else
                        {
                            curInstructionIndex += curInstruction.Arg2Value ?? registers[curInstruction.Arg2Register];
                        }
                        break;
                    default:
                        throw new Exception("Unreachable code");
                }
            }
            
            throw new Exception("Unreachable code");
        }

        private class Instruction
        {
            public InstructionType Type { get; private set; }
            public int? Arg1Value { get; }
            public string Arg1Register { get; }
            public int? Arg2Value { get; }
            public string Arg2Register { get; }

            public Instruction(string line)
            {
                var match = _oneArgumentRegex.Match(line);

                if (match.Success)
                {
                    switch (match.Groups[1].ToString())
                    {
                        case "inc":
                            Type = InstructionType.Increment;
                            break;
                        case "dec":
                            Type = InstructionType.Decrement;
                            break;
                        case "out":
                            Type = InstructionType.Out;
                            break;
                        default:
                            throw new Exception("Unreachable code");
                    }

                    var arg = match.Groups[2].ToString();
                    int val;

                    if (int.TryParse(arg, out val))
                    {
                        Arg1Value = val;
                    }
                    else
                    {
                        Arg1Register = arg;
                    }

                    return;
                }

                match = _twoArgumentRegex.Match(line);

                if (match.Success)
                {
                    switch (match.Groups[1].ToString())
                    {
                        case "cpy":
                            Type = InstructionType.Copy;
                            break;
                        case "jnz":
                            Type = InstructionType.Jump;
                            break;
                        default:
                            throw new Exception("Unreachable code");
                    }

                    var arg = match.Groups[2].ToString();
                    int val;

                    if (int.TryParse(arg, out val))
                    {
                        Arg1Value = val;
                    }
                    else
                    {
                        Arg1Register = arg;
                    }

                    arg = match.Groups[3].ToString();

                    if (int.TryParse(arg, out val))
                    {
                        Arg2Value = val;
                    }
                    else
                    {
                        Arg2Register = arg;
                    }

                    return;
                }

                throw new InvalidOperationException($"Cannot parse instruction: {line}");
            }
            
            public enum InstructionType
            {
                // One argument instructions
                Increment,
                Decrement,
                Out,

                // Two argument instructions
                Copy,
                Jump
            }

            private static readonly Regex _oneArgumentRegex = new Regex(@"(inc|dec|out) ([a-z])", RegexOptions.Compiled);
            private static readonly Regex _twoArgumentRegex = new Regex(@"(cpy|jnz) ([a-z]|-?\d+) ([a-z]|-?\d+)", RegexOptions.Compiled);
        }
    }
}
