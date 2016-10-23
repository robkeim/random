using System.Text;

namespace GameOfLife
{
    public static class Utils
    {
        public static World CreateWorld(params string[] lines)
        {
            var state = CreateWorldState(lines);

            var result = new World();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    result.AddCell(new Position(j, i), state[i, j]);
                }
            }

            return result;
        }

        public static Cell[,] CreateWorldState(params string[] lines)
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

        public static string GetPrintableState(World world)
        {
            StringBuilder result = new StringBuilder();

            var state = world.GetCurrentState();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    result.Append(state[i, j] is LiveCell ? "X" : ".");
                }

                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
