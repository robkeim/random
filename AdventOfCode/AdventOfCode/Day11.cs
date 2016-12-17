using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/11
    //
    // --- Day 11: Radioisotope Thermoelectric Generators ---
    //
    // You come upon a column of four floors that have been entirely sealed off from the rest of the building except for a small dedicated lobby.There are some
    // radiation warnings and a big sign which reads "Radioisotope Testing Facility".
    //
    // According to the project status board, this facility is currently being used to experiment with Radioisotope Thermoelectric Generators (RTGs, or simply
    // "generators") that are designed to be paired with specially-constructed microchips.Basically, an RTG is a highly radioactive rock that generates electricity
    // through heat.
    //
    // The experimental RTGs have poor radiation containment, so they're dangerously radioactive. The chips are prototypes and don't have normal radiation shielding,
    // but they do have the ability to generate an elecromagnetic radiation shield when powered.Unfortunately, they can only be powered by their corresponding RTG.
    // An RTG powering a microchip is still dangerous to other microchips.
    //
    // In other words, if a chip is ever left in the same area as another RTG, and it's not connected to its own RTG, the chip will be fried. Therefore, it is assumed
    // that you will follow procedure and keep chips connected to their corresponding RTG when they're in the same room, and away from other RTGs otherwise.
    //
    // These microchips sound very interesting and useful to your current activities, and you'd like to try to retrieve them. The fourth floor of the facility has an
    // assembling machine which can make a self-contained, shielded computer for you to take with you - that is, if you can bring it all of the RTGs and microchips.
    //
    // Within the radiation-shielded part of the facility (in which it's safe to have these pre-assembly RTGs), there is an elevator that can move between the four
    // floors. Its capacity rating means it can carry at most yourself and two RTGs or microchips in any combination. (They're rigged to some heavy diagnostic
    // equipment - the assembling machine will detach it for you.) As a security measure, the elevator will only function if it contains at least one RTG or microchip.
    // The elevator always stops on each floor to recharge, and this takes long enough that the items within it and the items on that floor can irradiate each other.
    // (You can prevent this if a Microchip and its Generator end up on the same floor in this way, as they can be connected while the elevator is recharging.)
    //
    // You make some notes of the locations of each component of interest(your puzzle input). Before you don a hazmat suit and start moving things around, you'd like
    // to have an idea of what you need to do.
    //
    // When you enter the containment area, you and the elevator will start on the first floor.
    //
    // For example, suppose the isolated area has the following arrangement:
    // The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
    // The second floor contains a hydrogen generator.
    // The third floor contains a lithium generator.
    // The fourth floor contains nothing relevant.
    // As a diagram (F# for a Floor number, E for Elevator, H for Hydrogen, L for Lithium, M for Microchip, and G for Generator), the initial state looks like this:
    // F4  .  .  .  .  .  
    // F3  .  .  .  LG .
    // F2  .  HG .  .  .
    // F1  E  .  HM .  LM
    //
    // Then, to get everything up to the assembling machine on the fourth floor, the following steps could be taken:
    // Bring the Hydrogen-compatible Microchip to the second floor, which is safe because it can get power from the Hydrogen Generator:
    // F4  .  .  .  .  .
    // F3  .  .  .  LG .
    // F2  E  HG HM .  .  
    // F1  .  .  .  .  LM
    //
    // Bring both Hydrogen-related items to the third floor, which is safe because the Hydrogen-compatible microchip is getting power from its generator:
    // F4 .  .  .  .  .
    // F3 E  HG HM LG .
    // F2 .  .  .  .  .
    // F1 .  .  .  .  LM
    //
    // Leave the Hydrogen Generator on floor three, but bring the Hydrogen-compatible Microchip back down with you so you can still use the elevator:
    // F4 .  .  .  .  .
    // F3 .  HG .  LG .
    // F2 E  .  HM .  .
    // F1 .  .  .  .  LM
    //
    // At the first floor, grab the Lithium-compatible Microchip, which is safe because Microchips don't affect each other:
    // F4 .  .  .  .  .
    // F3 .  HG .  LG .
    // F2 .  .  .  .  .
    // F1 E  .  HM .  LM
    //
    // Bring both Microchips up one floor, where there is nothing to fry them:
    // F4 .  .  .  .  .
    // F3 .  HG .  LG .
    // F2 E  .  HM .  LM
    // F1 .  .  .  .  .  
    //
    // Bring both Microchips up again to floor three, where they can be temporarily connected to their corresponding generators while the elevator recharges,
    // preventing either of them from being fried:
    // F4.  .  .  .  .
    // F3 E  HG HM LG LM
    // F2.  .  .  .  .
    // F1.  .  .  .  .
    //
    // Bring both Microchips to the fourth floor:
    // F4 E  .  HM.LM
    // F3 .  HG.LG.
    // F2.  .  .  .  .
    // F1.  .  .  .  .
    //
    // Leave the Lithium-compatible microchip on the fourth floor, but bring the Hydrogen-compatible one so you can still use the elevator; this is safe because
    // although the Lithium Generator is on the destination floor, you can connect Hydrogen-compatible microchip to the Hydrogen Generator there:
    // F4 .  .  .  .  LM
    // F3 E  HG HM LG .  
    // F2 .  .  .  .  .
    // F1 .  .  .  .  .
    //
    // Bring both Generators up to the fourth floor, which is safe because you can connect the Lithium-compatible Microchip to the Lithium Generator upon arrival:
    // F4 E  HG .  LG LM
    // F3 .  .  HM .  .
    // F2 .  .  .  .  .
    // F1 .  .  .  .  .
    //
    // Bring the Lithium Microchip with you to the third floor so you can use the elevator:
    // F4 .  HG .  LG .
    // F3 E  .  HM .  LM
    // F2 .  .  .  .  .  
    // F1 .  .  .  .  .
    //
    // Bring both Microchips to the fourth floor:
    // F4 E  HG HM LG LM
    // F3 .  .  .  .  .
    // F2 .  .  .  .  .
    // F1 .  .  .  .  .
    //
    // In this arrangement, it takes 11 steps to collect all of the objects at the fourth floor for assembly. (Each elevator stop counts as one step, even if nothing
    // is added to or removed from it.)
    //
    // In your situation, what is the minimum number of steps required to bring all of the objects to the fourth floor?
    // The first floor contains a promethium generator and a promethium-compatible microchip.\nThe second floor contains a cobalt generator, a curium generator, a ruthenium generator, and a plutonium generator.\nThe third floor contains a cobalt-compatible microchip, a curium-compatible microchip, a ruthenium-compatible microchip, and a plutonium-compatible microchip.\nThe fourth floor contains nothing relevant.
    // Answer: 33
    //
    // --- Part Two ---
    //
    // You step into the cleanroom separating the lobby from the isolated area and put on the hazmat suit.
    //
    // Upon entering the isolated containment area, however, you notice some extra parts on the first floor that weren't listed on the record outside:
    // - An elerium generator.
    // - An elerium-compatible microchip.
    // - A dilithium generator.
    // - A dilithium-compatible microchip.
    //
    // These work just like the other generators and microchips.You'll have to get them up to assembly as well.
    //
    // What is the minimum number of steps required to bring all of the objects, including these four new ones, to the fourth floor?
    // Answer: 57
    public static class Day11
    {
        public static int NumSteps()
        {
            var elementsToProcess = new Queue<State>();
            var visited = new HashSet<string>();

            // Sample input
            //elementsToProcess.Enqueue(State.MakeState(0, 1, Pair.MakePair(2, 1), Pair.MakePair(3, 1)));

            // Part I
            //AddQueueItem(elementsToProcess, visited, State.MakeState(0, 1,
            //    Pair.MakePair(1, 1),
            //    Pair.MakePair(2, 3),
            //    Pair.MakePair(2, 3),
            //    Pair.MakePair(2, 3),
            //    Pair.MakePair(2, 3)));

            // Part II
            AddQueueItem(elementsToProcess, visited, State.MakeState(0, 1,
                Pair.MakePair(1, 1),
                Pair.MakePair(1, 1),
                Pair.MakePair(1, 1),
                Pair.MakePair(2, 3),
                Pair.MakePair(2, 3),
                Pair.MakePair(2, 3),
                Pair.MakePair(2, 3)));

            while (elementsToProcess.Count != 0)
            {
                var curElem = elementsToProcess.Dequeue();
                
                var elevatorFloor = curElem.ElevatorFloor;
                var numSteps = curElem.NumSteps;
                var pairs = curElem.Pairs;
                
                if (curElem.IsFinalState())
                {
                    return numSteps;
                }
                
                var elevatorDirections = new List<int>();

                if (elevatorFloor > 1)
                {
                    elevatorDirections.Add(-1);
                }

                if (elevatorFloor < 4)
                {
                    elevatorDirections.Add(1);
                }

                foreach (var elevatorDirection in elevatorDirections)
                {
                    foreach (var pair in pairs)
                    {
                        if (pair.Generator == elevatorFloor)
                        {
                            AddQueueItem(elementsToProcess, visited, State.MakeState(
                                numSteps + 1,
                                elevatorFloor + elevatorDirection,
                                Replace(pairs, pair, Pair.MoveGenerator(pair, elevatorDirection))));
                        }

                        if (pair.Microchip == elevatorFloor)
                        {
                            AddQueueItem(elementsToProcess, visited, State.MakeState(
                                numSteps + 1,
                                elevatorFloor + elevatorDirection,
                                Replace(pairs, pair, Pair.MoveMicrochip(pair, elevatorDirection))));
                        }
                    }

                    for (int i = 0; i < pairs.Length; i++)
                    {
                        if (pairs[i].Generator == elevatorFloor)
                        {
                            if (pairs[i].Microchip == elevatorFloor)
                            {
                                AddQueueItem(elementsToProcess, visited, State.MakeState(
                                numSteps + 1,
                                elevatorFloor + elevatorDirection,
                                Replace(pairs, pairs[i], Pair.MoveBoth(pairs[i], elevatorDirection))));
                            }

                            for (int j = i + 1; j < pairs.Length; j++)
                            {
                                if (pairs[j].Generator == elevatorFloor)
                                {
                                    var replacement = Replace(pairs, pairs[i], Pair.MoveGenerator(pairs[i], elevatorDirection));
                                    replacement = Replace(replacement, pairs[j], Pair.MoveGenerator(pairs[j], elevatorDirection));

                                    AddQueueItem(elementsToProcess, visited, State.MakeState(
                                        numSteps + 1,
                                        elevatorFloor + elevatorDirection,
                                        replacement));
                                }

                                if (pairs[j].Microchip == elevatorFloor)
                                {
                                    var replacement = Replace(pairs, pairs[i], Pair.MoveGenerator(pairs[i], elevatorDirection));
                                    replacement = Replace(replacement, pairs[j], Pair.MoveMicrochip(pairs[j], elevatorDirection));

                                    AddQueueItem(elementsToProcess, visited, State.MakeState(
                                        numSteps + 1,
                                        elevatorFloor + elevatorDirection,
                                        replacement));
                                }
                            }
                        }

                        if (pairs[i].Microchip == elevatorFloor)
                        {
                            for (int j = i + 1; j < pairs.Length; j++)
                            {
                                if (pairs[j].Generator == elevatorFloor)
                                {
                                    var replacement = Replace(pairs, pairs[i], Pair.MoveMicrochip(pairs[i], elevatorDirection));
                                    replacement = Replace(replacement, pairs[j], Pair.MoveGenerator(pairs[j], elevatorDirection));

                                    AddQueueItem(elementsToProcess, visited, State.MakeState(
                                        numSteps + 1,
                                        elevatorFloor + elevatorDirection,
                                        replacement));
                                }

                                if (pairs[j].Microchip == elevatorFloor)
                                {
                                    var replacement = Replace(pairs, pairs[i], Pair.MoveMicrochip(pairs[i], elevatorDirection));
                                    replacement = Replace(replacement, pairs[j], Pair.MoveMicrochip(pairs[j], elevatorDirection));

                                    AddQueueItem(elementsToProcess, visited, State.MakeState(
                                        numSteps + 1,
                                        elevatorFloor + elevatorDirection,
                                        replacement));
                                }
                            }
                        }
                    }
                }
            }

            return -1;
        }

        private static void AddQueueItem(Queue<State> queue, HashSet<string> visited, State state)
        {
            if (!visited.Add(state.GetHashValue()) || !state.IsValidState())
            {
                return;
            }

            queue.Enqueue(state);
        }

        private static IEnumerable<T> Replace<T>(IEnumerable<T> input, T itemToReplace, T replacement)
        {
            return input.Except(MakeArray(itemToReplace)).Concat(MakeArray(replacement));
        }

        private static T[] MakeArray<T>(T elem)
        {
            return new[] { elem };
        }

        private class State
        {
            public int NumSteps { get; }
            public int ElevatorFloor { get; }
            public Pair[] Pairs { get; }

            public State(int numSteps, int elevatorFloor, params Pair[] pairs)
            {
                NumSteps = numSteps;
                ElevatorFloor = elevatorFloor;
                Pairs = pairs;
            }
            
            public static State MakeState(int numSteps, int elevatorFloor, params Pair[] pairs)
            {
                return new State(numSteps, elevatorFloor, pairs);
            }

            public static State MakeState(int numSteps, int elevatorFloor, IEnumerable<Pair> pairs)
            {
                return new State(numSteps, elevatorFloor, pairs.ToArray());
            }

            public bool IsFinalState()
            {
                return Pairs.All(p => p.Generator == 4 && p.Microchip == 4);
            }

            public bool IsValidState()
            {
                return Pairs.All(p => p.Generator == p.Microchip || Pairs.All(p2 => p2.Generator != p.Microchip));
            }

            public override bool Equals(object obj)
            {
                if (!(obj is State))
                {
                    return false;
                }

                var state = (State)obj;

                return ElevatorFloor == state.ElevatorFloor
                       && Pairs.Length == state.Pairs.Length
                       && Pairs.All(p => state.Pairs.Contains(p));
            }

            public string GetHashValue()
            {
                var sortedPairs = string.Join("_", Pairs.Select(p => p.ToString()).OrderBy(p => p));

                return $"{ElevatorFloor}-{sortedPairs}";
            }

            public override string ToString()
            {
                var result = new StringBuilder();

                for (int i = 4; i >= 1; i--)
                {
                    var elevatorString = ElevatorFloor == i ? "E " : string.Empty;
                    var line = $"F{i} {elevatorString}";

                    for (int j = 0; j < Pairs.Length; j++)
                    {
                        if (Pairs[j].Generator == i)
                        {
                            line += $"G{j} ";
                        }

                        if (Pairs[j].Microchip == i)
                        {
                            line += $"M{j} ";
                        }
                    }

                    result.AppendLine(line);
                }

                return result.ToString();
            }
        }

        private class Pair
        {
            private int InnerState { get; set; }

            public int Generator => InnerState >> shiftAmount;
            public int Microchip => InnerState & 0x7;

            private const int shiftAmount = 3;

            public static Pair MakePair(int generator, int microchip)
            {
                return new Pair
                {
                    InnerState = generator << shiftAmount | microchip
                };
            }

            public static Pair MoveGenerator(Pair pair, int direction)
            {
                direction = direction > 0
                    ? direction << shiftAmount
                    : ((-1 * direction) << shiftAmount) * -1;

                return new Pair
                {
                    InnerState = pair.InnerState + direction
                };
            }

            public static Pair MoveMicrochip(Pair pair, int direction)
            {
                return new Pair
                {
                    InnerState = pair.InnerState + direction
                };
            }

            public static Pair MoveBoth(Pair pair, int direction)
            {
                var genDirection = direction > 0
                    ? direction << shiftAmount
                    : ((-1 * direction) << shiftAmount) * -1;

                return new Pair
                {
                    InnerState = pair.InnerState + genDirection + direction
                };
            }

            public override bool Equals(object obj)
            {
                return InnerState == (obj as Pair)?.InnerState;
            }

            public override string ToString()
            {
                return $"G{Generator}M{Microchip}";
            }
        }
    }
}
