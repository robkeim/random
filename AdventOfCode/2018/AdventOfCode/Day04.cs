using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day04
    {
        public static int Part1(string input)
        {
            var lines = input
                .Split("\n".ToCharArray())
                .OrderBy(l => l);

            var dateRegex = new Regex(@"\[(\d{4}-\d{2}-\d{2} \d{2}:\d{2})\]", RegexOptions.Compiled);
            var guardNumRegex = new Regex(@"#(\d+)", RegexOptions.Compiled);

            var sleepingMinutes = new Dictionary<int, int>();
            var sleepingTime = new Dictionary<int, int[]>();
            var curGuardNum = -1;
            var fallsAsleepAt = DateTime.MinValue;
            var timeAsleep = -1;

            foreach (var line in lines)
            {
                var dateMatch = dateRegex.Match(line);
                var date = DateTime.Parse(dateMatch.Groups[1].ToString());

                var guardNumMatch = guardNumRegex.Match(line);
                
                if (guardNumMatch.Success)
                {
                    if (curGuardNum != -1)
                    {
                        if (sleepingMinutes.ContainsKey(curGuardNum))
                        {
                            sleepingMinutes[curGuardNum] += timeAsleep;
                        }
                        else
                        {
                            sleepingMinutes[curGuardNum] = timeAsleep;
                        }
                    }

                    curGuardNum = int.Parse(guardNumMatch.Groups[1].ToString());
                    timeAsleep = 0;
                }
                else if (line.Contains("falls asleep"))
                {
                    fallsAsleepAt = date;
                }
                else if (line.Contains("wakes up"))
                {
                    timeAsleep += (int)(date - fallsAsleepAt).TotalMinutes;

                    if (!sleepingTime.ContainsKey(curGuardNum))
                    {
                        sleepingTime[curGuardNum] = new int[60];
                    }

                    for (var i = fallsAsleepAt; i < date; i = i.AddMinutes(1))
                    {
                        sleepingTime[curGuardNum][i.Minute]++;
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid line fomrat: {line}");
                }
            }

            if (sleepingMinutes.ContainsKey(curGuardNum))
            {
                sleepingMinutes[curGuardNum] += timeAsleep;
            }
            else
            {
                sleepingMinutes[curGuardNum] = timeAsleep;
            }

            var guard = sleepingMinutes
                .OrderByDescending(kvp => kvp.Value)
                .First();

            var maxMinute = -1;
            var maxValue = -1;

            for (int i = 0; i < 60; i++)
            {
                if (sleepingTime[guard.Key][i] > maxValue)
                {
                    maxValue = sleepingTime[guard.Key][i];
                    maxMinute = i;
                }
            }

            return guard.Key * maxMinute;
        }

        public static int Part2(string input)
        {
            var lines = input
                .Split("\n".ToCharArray())
                .OrderBy(l => l);

            var dateRegex = new Regex(@"\[(\d{4}-\d{2}-\d{2} \d{2}:\d{2})\]", RegexOptions.Compiled);
            var guardNumRegex = new Regex(@"#(\d+)", RegexOptions.Compiled);

            var curGuardNum = -1;
            var fallsAsleepAt = DateTime.MinValue;
            // Key is GuardNum_Minute, Value is NumTimes
            var sleepingSchedule = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var dateMatch = dateRegex.Match(line);
                var date = DateTime.Parse(dateMatch.Groups[1].ToString());

                var guardNumMatch = guardNumRegex.Match(line);

                if (guardNumMatch.Success)
                {
                    curGuardNum = int.Parse(guardNumMatch.Groups[1].ToString());
                }
                else if (line.Contains("falls asleep"))
                {
                    fallsAsleepAt = date;
                }
                else if (line.Contains("wakes up"))
                {
                    for (var i = fallsAsleepAt; i < date; i = i.AddMinutes(1))
                    {
                        var key = $"{curGuardNum}_{i.Minute}";
                        
                        if (!sleepingSchedule.ContainsKey(key))
                        {
                            sleepingSchedule[key] = 0;
                        }

                        sleepingSchedule[key]++;
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid line fomrat: {line}");
                }
            }

            var result = sleepingSchedule
                .OrderByDescending(kvp => kvp.Value)
                .First()
                .Key
                .Split("_".ToCharArray())
                .Select(int.Parse)
                .ToArray();
            
            return result[0] * result[1];
        }
    }
}
