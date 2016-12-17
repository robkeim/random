using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/17
    //
    // --- Day 17: Two Steps Forward ---
    //
    // You're trying to access a secure vault protected by a 4x4 grid of small rooms connected by doors. You start in the top-left room (marked S), and you can access the
    // vault (marked V) once you reach the bottom-right room:
    // #########
    // #S| | | #
    // #-#-#-#-#
    // # | | | #
    // #-#-#-#-#
    // # | | | #
    // #-#-#-#-#
    // # | | |  
    // ####### V
    //
    // Fixed walls are marked with #, and doors are marked with - or |.
    //
    // The doors in your current room are either open or closed(and locked) based on the hexadecimal MD5 hash of a passcode(your puzzle input) followed by a sequence of
    // uppercase characters representing the path you have taken so far(U for up, D for down, L for left, and R for right).
    //
    // Only the first four characters of the hash are used; they represent, respectively, the doors up, down, left, and right from your current position.Any b, c, d, e, or
    // f means that the corresponding door is open; any other character(any number or a) means that the corresponding door is closed and locked.
    //
    // To access the vault, all you need to do is reach the bottom-right room; reaching this room opens the vault and all doors in the maze.
    //
    // For example, suppose the passcode is hijkl.Initially, you have taken no steps, and so your path is empty: you simply find the MD5 hash of hijkl alone.The first four
    // characters of this hash are ced9, which indicate that up is open (c), down is open (e), left is open (d), and right is closed and locked (9). Because you start in the
    // top-left corner, there are no "up" or "left" doors to be open, so your only choice is down.
    //
    // Next, having gone only one step (down, or D), you find the hash of hijklD. This produces f2bc, which indicates that you can go back up, left (but that's a wall), or
    // right. Going right means hashing hijklDR to get 5745 - all doors closed and locked. However, going up instead is worthwhile: even though it returns you to the room
    // you started in, your path would then be DU, opening a different set of doors.
    //
    // After going DU (and then hashing hijklDU to get 528e), only the right door is open; after going DUR, all doors lock. (Fortunately, your actual passcode is not hijkl).
    //
    // Passcodes actually used by Easter Bunny Vault Security do allow access to the vault if you know the right path. For example:
    //
    // If your passcode were ihgpwlah, the shortest path would be DDRRRD.
    // With kglvqrro, the shortest path would be DDUDRLRRUDRD.
    // With ulqzkmiv, the shortest would be DRURDRUDDLLDLUURRDULRLDUUDDDRR.
    // Given your vault's passcode, what is the shortest path (the actual path, not just the length) to reach the vault?
    //
    // udskfozm
    // Answer: DDRLRRUDDR
    public static class Day17
    {
        public static string TwoStepsForward(string input)
        {
            var elementsToProcess = new Queue<State>();

            elementsToProcess.Enqueue(new State(0, 0, string.Empty));

            while (elementsToProcess.Count != 0)
            {
                var curElem = elementsToProcess.Dequeue();
                var xPos = curElem.XPos;
                var yPos = curElem.YPos;
                var moves = curElem.Moves;
                
                if (xPos == 3 && yPos == 3)
                {
                    return moves;
                }

                var hash = CalculateMd5Hash(input, moves);

                // Up
                if (yPos > 0 && DoorIsOpen(hash[0]))
                {
                    elementsToProcess.Enqueue(new State(xPos, yPos - 1, $"{moves}U"));
                }

                // Down
                if (yPos < 3 && DoorIsOpen(hash[1]))
                {
                    elementsToProcess.Enqueue(new State(xPos, yPos + 1, $"{moves}D"));
                }

                // Left
                if (xPos > 0 && DoorIsOpen(hash[2]))
                {
                    elementsToProcess.Enqueue(new State(xPos - 1, yPos, $"{moves}L"));
                }

                // Right
                if (xPos < 3 && DoorIsOpen(hash[3]))
                {
                    elementsToProcess.Enqueue(new State(xPos + 1, yPos, $"{moves}R"));
                }
            }

            return "No valid path exists";
        }

        private static bool DoorIsOpen(char c)
        {
            return c >= 'b' && c <= 'f';
        }

        private static string CalculateMd5Hash(string input, string moves)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes($"{input}{moves}"));

                return string.Concat(hash.Select(h => h.ToString("x2"))).Substring(0, 4);
            }
        }

        private class State
        {
            public int XPos { get; }
            public int YPos { get; }
            public string Moves { get; }

            public State(int xPos, int yPos, string moves)
            {
                XPos = xPos;
                YPos = yPos;
                Moves = moves;
            }
        }
    }
}
