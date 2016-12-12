using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/10
    //
    // --- Day 10: Balance Bots ---
    //
    // You come upon a factory in which many robots are zooming around handing small microchips to each other.
    //
    // Upon closer examination, you notice that each bot only proceeds when it has two microchips, and once it does, it gives each one to a different bot or puts it
    // in a marked "output" bin. Sometimes, bots take microchips from "input" bins, too.
    //
    // Inspecting one of the microchips, it seems like they each contain a single number; the bots must use some logic to decide what to do with each chip.You access
    // the local control computer and download the bots' instructions (your puzzle input).
    //
    // Some of the instructions specify that a specific-valued microchip should be given to a specific bot; the rest of the instructions indicate what a given bot
    // should do with its lower-value or higher-value chip.
    //
    // For example, consider the following instructions:
    //
    // - value 5 goes to bot 2
    // - bot 2 gives low to bot 1 and high to bot 0
    // - value 3 goes to bot 1
    // - bot 1 gives low to output 1 and high to bot 0
    // - bot 0 gives low to output 2 and high to output 0
    // - value 2 goes to bot 2
    //
    // Initially, bot 1 starts with a value-3 chip, and bot 2 starts with a value-2 chip and a value-5 chip.
    // Because bot 2 has two microchips, it gives its lower one (2) to bot 1 and its higher one(5) to bot 0.
    // Then, bot 1 has two microchips; it puts the value-2 chip in output 1 and gives the value-3 chip to bot 0.
    // Finally, bot 0 has two microchips; it puts the 3 in output 2 and the 5 in output 0.
    // In the end, output bin 0 contains a value-5 microchip, output bin 1 contains a value-2 microchip, and output bin 2 contains a value-3 microchip. In this
    // configuration, bot number 2 is responsible for comparing value-5 microchips with value-2 microchips.
    //
    // Based on your instructions, what is the number of the bot that is responsible for comparing value-61 microchips with value-17 microchips?
    // bot 119 gives low to bot 18 and high to bot 3\nbot 69 gives low to bot 47 and high to bot 172\nbot 51 gives low to output 6 and high to bot 174\nbot 11 gives low to bot 94 and high to bot 165\nbot 42 gives low to bot 0 and high to bot 95\nbot 195 gives low to bot 69 and high to bot 99\nbot 96 gives low to bot 169 and high to bot 85\nbot 24 gives low to bot 119 and high to bot 44\nbot 114 gives low to output 5 and high to bot 43\nbot 13 gives low to bot 203 and high to bot 197\nbot 20 gives low to bot 146 and high to bot 164\nbot 127 gives low to bot 132 and high to bot 11\nbot 50 gives low to bot 80 and high to bot 148\nbot 77 gives low to bot 35 and high to bot 67\nbot 151 gives low to output 16 and high to bot 68\nbot 72 gives low to bot 130 and high to bot 88\nbot 89 gives low to bot 184 and high to bot 170\nbot 158 gives low to bot 178 and high to bot 29\nbot 85 gives low to bot 93 and high to bot 191\nbot 6 gives low to bot 159 and high to bot 7\nbot 156 gives low to bot 25 and high to bot 16\nbot 177 gives low to output 8 and high to bot 157\nbot 26 gives low to bot 131 and high to bot 149\nvalue 61 goes to bot 119\nbot 18 gives low to bot 161 and high to bot 118\nbot 53 gives low to bot 128 and high to bot 74\nbot 110 gives low to bot 126 and high to bot 90\nbot 95 gives low to bot 52 and high to bot 62\nbot 67 gives low to bot 121 and high to bot 156\nbot 138 gives low to bot 151 and high to bot 171\nbot 173 gives low to bot 137 and high to bot 15\nbot 175 gives low to bot 136 and high to bot 117\nbot 28 gives low to bot 120 and high to bot 92\nbot 143 gives low to bot 70 and high to bot 26\nbot 78 gives low to bot 90 and high to bot 63\nbot 113 gives low to bot 73 and high to bot 193\nbot 207 gives low to bot 23 and high to bot 125\nbot 116 gives low to bot 100 and high to bot 158\nbot 172 gives low to bot 10 and high to bot 61\nbot 64 gives low to bot 24 and high to bot 41\nbot 16 gives low to bot 147 and high to bot 106\nvalue 11 goes to bot 2\nbot 203 gives low to bot 125 and high to bot 197\nvalue 5 goes to bot 18\nbot 49 gives low to bot 160 and high to bot 83\nbot 112 gives low to bot 39 and high to bot 32\nbot 129 gives low to bot 186 and high to bot 112\nbot 68 gives low to output 7 and high to bot 87\nbot 201 gives low to bot 56 and high to bot 124\nbot 56 gives low to bot 83 and high to bot 207\nbot 38 gives low to bot 89 and high to bot 136\nbot 194 gives low to bot 40 and high to bot 105\nbot 15 gives low to bot 1 and high to bot 50\nbot 47 gives low to bot 6 and high to bot 10\nbot 63 gives low to bot 205 and high to bot 139\nbot 176 gives low to bot 30 and high to bot 138\nvalue 41 goes to bot 24\nbot 71 gives low to bot 165 and high to bot 185\nbot 84 gives low to bot 209 and high to bot 129\nbot 204 gives low to bot 61 and high to bot 91\nbot 152 gives low to bot 163 and high to bot 166\nbot 139 gives low to bot 45 and high to bot 5\nbot 100 gives low to bot 55 and high to bot 178\nbot 81 gives low to bot 95 and high to bot 188\nbot 163 gives low to output 12 and high to bot 114\nbot 133 gives low to bot 124 and high to bot 13\nbot 206 gives low to bot 37 and high to bot 21\nbot 23 gives low to bot 116 and high to bot 82\nbot 134 gives low to bot 195 and high to bot 202\nbot 73 gives low to bot 54 and high to bot 22\nbot 99 gives low to bot 172 and high to bot 204\nbot 192 gives low to bot 34 and high to bot 115\nbot 8 gives low to bot 171 and high to bot 60\nbot 142 gives low to bot 199 and high to bot 28\nbot 41 gives low to bot 44 and high to bot 140\nbot 0 gives low to bot 71 and high to bot 52\nbot 154 gives low to bot 148 and high to bot 33\nbot 62 gives low to bot 66 and high to bot 65\nbot 160 gives low to bot 175 and high to bot 153\nvalue 73 goes to bot 111\nbot 164 gives low to bot 141 and high to bot 9\nbot 202 gives low to bot 99 and high to bot 19\nbot 12 gives low to bot 46 and high to bot 48\nbot 170 gives low to bot 190 and high to bot 194\nbot 27 gives low to bot 101 and high to bot 200\nbot 148 gives low to bot 127 and high to bot 182\nbot 147 gives low to bot 57 and high to bot 96\nbot 65 gives low to bot 78 and high to bot 46\nbot 125 gives low to bot 82 and high to bot 79\nbot 86 gives low to output 10 and high to output 18\nbot 105 gives low to bot 108 and high to bot 98\nbot 103 gives low to output 15 and high to bot 199\nbot 14 gives low to bot 65 and high to bot 12\nbot 132 gives low to bot 113 and high to bot 94\nvalue 53 goes to bot 102\nbot 136 gives low to bot 170 and high to bot 196\nbot 55 gives low to bot 105 and high to bot 145\nbot 36 gives low to bot 67 and high to bot 126\nbot 92 gives low to bot 177 and high to bot 101\nbot 161 gives low to output 17 and high to bot 208\nbot 32 gives low to bot 180 and high to bot 123\nbot 179 gives low to output 19 and high to bot 86\nbot 90 gives low to bot 76 and high to bot 205\nbot 21 gives low to bot 103 and high to bot 142\nbot 174 gives low to output 11 and high to bot 30\nbot 46 gives low to bot 63 and high to bot 48\nbot 94 gives low to bot 193 and high to bot 77\nbot 70 gives low to bot 21 and high to bot 131\nbot 29 gives low to bot 181 and high to bot 144\nbot 82 gives low to bot 158 and high to bot 135\nbot 33 gives low to bot 182 and high to bot 0\nbot 144 gives low to bot 192 and high to bot 141\nbot 197 gives low to bot 79 and high to bot 20\nvalue 17 goes to bot 2\nbot 137 gives low to bot 41 and high to bot 1\nbot 121 gives low to bot 8 and high to bot 25\nbot 40 gives low to bot 15 and high to bot 108\nbot 83 gives low to bot 153 and high to bot 23\nvalue 2 goes to bot 64\nvalue 47 goes to bot 184\nbot 188 gives low to bot 62 and high to bot 14\nbot 171 gives low to bot 68 and high to bot 59\nbot 39 gives low to bot 143 and high to bot 180\nbot 157 gives low to output 2 and high to bot 179\nbot 126 gives low to bot 156 and high to bot 76\nvalue 19 goes to bot 187\nbot 60 gives low to bot 59 and high to bot 57\nbot 104 gives low to bot 188 and high to bot 4\nbot 74 gives low to bot 183 and high to bot 113\nbot 102 gives low to bot 187 and high to bot 173\nbot 120 gives low to output 9 and high to bot 177\nbot 111 gives low to bot 134 and high to bot 107\nbot 155 gives low to bot 109 and high to bot 34\nbot 19 gives low to bot 204 and high to bot 91\nbot 145 gives low to bot 98 and high to bot 155\nvalue 3 goes to bot 150\nbot 107 gives low to bot 202 and high to bot 19\nvalue 43 goes to bot 195\nbot 97 gives low to bot 129 and high to bot 168\nbot 48 gives low to bot 139 and high to bot 5\nbot 58 gives low to bot 17 and high to bot 27\nbot 149 gives low to bot 75 and high to bot 58\nbot 44 gives low to bot 3 and high to bot 53\nbot 186 gives low to bot 191 and high to bot 39\nbot 2 gives low to bot 111 and high to bot 107\nbot 22 gives low to bot 176 and high to bot 198\nbot 167 gives low to bot 43 and high to bot 206\nvalue 59 goes to bot 6\nbot 135 gives low to bot 29 and high to bot 146\nbot 166 gives low to bot 114 and high to bot 167\nbot 5 gives low to bot 97 and high to bot 168\nbot 205 gives low to bot 31 and high to bot 45\nbot 17 gives low to bot 92 and high to bot 27\nbot 3 gives low to bot 118 and high to bot 128\nbot 209 gives low to bot 85 and high to bot 186\nbot 30 gives low to output 1 and high to bot 151\nvalue 67 goes to bot 69\nbot 193 gives low to bot 22 and high to bot 35\nbot 196 gives low to bot 194 and high to bot 55\nbot 4 gives low to bot 14 and high to bot 12\nbot 98 gives low to bot 154 and high to bot 109\nbot 184 gives low to bot 102 and high to bot 190\nbot 180 gives low to bot 26 and high to bot 123\nbot 200 gives low to bot 179 and high to bot 86\nbot 185 gives low to bot 36 and high to bot 110\nbot 52 gives low to bot 185 and high to bot 66\nbot 123 gives low to bot 149 and high to bot 58\nbot 88 gives low to bot 51 and high to bot 54\nbot 208 gives low to output 4 and high to bot 130\nbot 153 gives low to bot 117 and high to bot 116\nbot 131 gives low to bot 142 and high to bot 75\nbot 54 gives low to bot 174 and high to bot 176\nbot 91 gives low to bot 133 and high to bot 13\nbot 130 gives low to output 0 and high to bot 51\nbot 140 gives low to bot 53 and high to bot 162\nbot 165 gives low to bot 77 and high to bot 36\nbot 61 gives low to bot 201 and high to bot 133\nbot 57 gives low to bot 152 and high to bot 169\nbot 182 gives low to bot 11 and high to bot 71\nbot 37 gives low to output 20 and high to bot 103\nbot 199 gives low to output 13 and high to bot 120\nbot 59 gives low to bot 87 and high to bot 152\nbot 181 gives low to bot 155 and high to bot 192\nbot 178 gives low to bot 145 and high to bot 181\nbot 31 gives low to bot 106 and high to bot 84\nbot 122 gives low to bot 150 and high to bot 160\nbot 25 gives low to bot 60 and high to bot 147\nbot 189 gives low to bot 206 and high to bot 70\nvalue 23 goes to bot 47\nbot 128 gives low to bot 72 and high to bot 183\nbot 34 gives low to bot 42 and high to bot 81\nbot 43 gives low to output 14 and high to bot 37\nbot 66 gives low to bot 110 and high to bot 78\nbot 101 gives low to bot 157 and high to bot 200\nvalue 71 goes to bot 89\nbot 198 gives low to bot 138 and high to bot 8\nbot 106 gives low to bot 96 and high to bot 209\nvalue 31 goes to bot 159\nbot 117 gives low to bot 196 and high to bot 100\nbot 159 gives low to bot 122 and high to bot 49\nbot 168 gives low to bot 112 and high to bot 32\nbot 187 gives low to bot 64 and high to bot 137\nbot 79 gives low to bot 135 and high to bot 20\nbot 35 gives low to bot 198 and high to bot 121\nbot 80 gives low to bot 162 and high to bot 127\nbot 1 gives low to bot 140 and high to bot 80\nbot 45 gives low to bot 84 and high to bot 97\nbot 108 gives low to bot 50 and high to bot 154\nbot 115 gives low to bot 81 and high to bot 104\nbot 118 gives low to bot 208 and high to bot 72\nbot 190 gives low to bot 173 and high to bot 40\nbot 10 gives low to bot 7 and high to bot 201\nbot 169 gives low to bot 166 and high to bot 93\nvalue 29 goes to bot 38\nbot 9 gives low to bot 104 and high to bot 4\nvalue 37 goes to bot 122\nbot 150 gives low to bot 38 and high to bot 175\nbot 75 gives low to bot 28 and high to bot 17\nvalue 13 goes to bot 134\nbot 124 gives low to bot 207 and high to bot 203\nbot 109 gives low to bot 33 and high to bot 42\nvalue 7 goes to bot 161\nbot 93 gives low to bot 167 and high to bot 189\nbot 191 gives low to bot 189 and high to bot 143\nbot 146 gives low to bot 144 and high to bot 164\nbot 141 gives low to bot 115 and high to bot 9\nbot 162 gives low to bot 74 and high to bot 132\nbot 87 gives low to output 3 and high to bot 163\nbot 7 gives low to bot 49 and high to bot 56\nbot 183 gives low to bot 88 and high to bot 73\nbot 76 gives low to bot 16 and high to bot 31
    // Answer: 73
    //
    // --- Part Two ---
    //
    // What do you get if you multiply together the values of one chip in each of outputs 0, 1, and 2?
    // Answer: 3965
    public static class Day10
    {
        private static readonly Dictionary<int, Bot> _bots = new Dictionary<int, Bot>();
        private static readonly Dictionary<int, int> _outputValues = new Dictionary<int, int>();
        private static int _solutionId;
        private static int _lowValue;
        private static int _highValue;
        
        public static int BalanceBots(string input, int lowValue, int highValue)
        {
            _lowValue = lowValue;
            _highValue = highValue;
            
            BalanceBotsSetup(input);
            
            return _solutionId;
        }

        public static int BalanceBots(string input)
        {
            BalanceBotsSetup(input);

            return _outputValues[0] * _outputValues[1] * _outputValues[2];
        }

        private static void BalanceBotsSetup(string input)
        {
            var instructionRegex = new Regex(@"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)", RegexOptions.Compiled);

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var match = instructionRegex.Match(line);

                if (match.Success)
                {
                    var id = int.Parse(match.Groups[1].ToString());
                    var lowTarget = match.Groups[2].ToString() == "bot" ? Target.Bot : Target.Output;
                    var lowTargetValue = int.Parse(match.Groups[3].ToString());
                    var highTarget = match.Groups[4].ToString() == "bot" ? Target.Bot : Target.Output;
                    var highTargetValue = int.Parse(match.Groups[5].ToString());

                    Bot bot;

                    if (!_bots.TryGetValue(id, out bot))
                    {
                        bot = new Bot(id);
                        _bots[id] = bot;
                    }

                    bot.SetInstructions(lowTarget, lowTargetValue, highTarget, highTargetValue);
                }
            }

            var valueRegex = new Regex(@"value (\d+) goes to bot (\d+)", RegexOptions.Compiled);

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var match = valueRegex.Match(line);

                if (match.Success)
                {
                    var chipId = int.Parse(match.Groups[1].ToString());
                    var id = int.Parse(match.Groups[2].ToString());

                    _bots[id].GiveChip(chipId);
                }
            }
        }

        private class Bot
        {
            private readonly int _id;
            private int? _chip;
            private Target _lowTarget;
            private int _lowValue;
            private Target _highTarget;
            private int _highValue;

            public Bot(int id)
            {
                _id = id;
            }

            public void SetInstructions(Target lowTarget, int lowValue, Target highTarget, int highValue)
            {
                _lowTarget = lowTarget;
                _lowValue = lowValue;
                _highTarget = highTarget;
                _highValue = highValue;
            }
            
            public void GiveChip(int chip)
            {
                if (!_chip.HasValue)
                {
                    _chip = chip;
                }
                else
                {
                    var low = Math.Min(_chip.Value, chip);
                    var high = Math.Max(_chip.Value, chip);

                    _chip = null;

                    if (low == Day10._lowValue && high == Day10._highValue)
                    {
                        _solutionId = _id;
                    }

                    if (_lowTarget == Target.Bot)
                    {
                        _bots[_lowValue].GiveChip(low);
                    }
                    else
                    {
                        _outputValues[_lowValue] = low;
                    }

                    if (_highTarget == Target.Bot)
                    {
                        _bots[_highValue].GiveChip(high);
                    }
                    else
                    {
                        _outputValues[_highValue] = high;
                    }
                }
            }

            public override string ToString()
            {
                return $"Id = {_id}, Chip = {_chip}, LowDestination = {_lowValue}, HighDestination = {_highValue}";
            }
        }

        public enum Target
        {
            Bot,
            Output
        }
    }
}
