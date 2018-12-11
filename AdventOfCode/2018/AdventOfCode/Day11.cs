namespace AdventOfCode
{
    public static class Day11
    {
        public static string Part1(int serialNumber)
        {
            var grid = new int[301, 301];

            for (int x = 1; x <= 300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    grid[x, y] = PowerLevel(x, y, serialNumber);
                }
            }

            var maxPower = int.MinValue;
            var coordinate = string.Empty;

            for (int x = 1; x <= 298; x++)
            {
                for (int y = 1; y <= 298; y++)
                {
                    var power = 0;

                    for (int dx = 0; dx < 3; dx++)
                    {
                        for (int dy = 0; dy < 3; dy++)
                        {
                            power += grid[x + dx, y + dy];
                        }
                    }

                    if (power > maxPower)
                    {
                        maxPower = power;
                        coordinate = $"{x},{y}";
                    }
                }
            }

            return coordinate;
        }

        public static int PowerLevel(int x, int y, int serialNumber)
        {
            var rackId = x + 10;
            var result = rackId * y;
            result += serialNumber;
            result *= rackId;
            result %= 1000;
            result /= 100;
            result -= 5;

            return result;
        }

        public static int Part2(int serialNumber)
        {
            return -1;
        }
    }
}
