using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    public static class TestUtils
    {
        public static World CreateWorld(params string[] lines)
        {
            var state = CreateExpectedWorldState(lines);

            var result = new World();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[j, i] is LiveCell)
                    {
                        result.AddLiveCell(new Position(j, i));
                    }
                }
            }

            return result;
        }

        public static Cell[,] CreateExpectedWorldState(params string[] lines)
        {
            var xSize = lines[0].Length;
            var ySize = lines.Length;

            var result = new Cell[ySize, xSize];

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    result[j, i] = lines[j][i] == 'X'
                        ? (Cell)new LiveCell()
                        : new DeadCell();
                }
            }

            return result;
        }

        public static void AssertWorldStatesAreEqual(Cell[,] expected, Cell[,] actual)
        {
            Assert.AreEqual(expected.Rank, actual.Rank);
            Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));

            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}
