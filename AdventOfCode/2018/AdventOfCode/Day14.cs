using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day14
    {
        public static string Part1(int recipeNumber)
        {
            var recipes = new List<int>();
            recipes.Add(3);
            recipes.Add(7);

            var elfIndex1 = 0;
            var elfIndex2 = 1;

            while (recipes.Count < recipeNumber + 10)
            {
                var recipesArray = recipes.ToArray();
                var total = recipesArray[elfIndex1] + recipesArray[elfIndex2];

                var newRecipe1 = total < 10 ? total : total / 10;
                var newRecipe2 = total >= 10 ? total % 10 : -1;

                recipes.Add(newRecipe1);

                if (total >= 10)
                {
                    recipes.Add(newRecipe2);
                }

                elfIndex1 = (elfIndex1 + 1 + recipesArray[elfIndex1]) % recipes.Count;
                elfIndex2 = (elfIndex2 + 1 + recipesArray[elfIndex2]) % recipes.Count;
            }
            
            return string.Join("", recipes.Skip(recipeNumber).Take(10));
        }

        // Debug method to print current state of simulation
        private static string PrintState(List<int> recipes, int index1, int index2)
        {
            var arr = recipes.ToArray();
            var result = new StringBuilder();

            for (int i = 0; i < arr.Length; i++)
            {
                if (i == index1)
                {
                    result.Append("(");
                }
                else if (i == index2)
                {
                    result.Append("[");
                }
                else
                {
                    result.Append(" ");
                }

                result.Append(arr[i]);

                if (i == index1)
                {
                    result.Append(")");
                }
                else if (i == index2)
                {
                    result.Append("]");
                }
                else
                {
                    result.Append(" ");
                }
            }

            return result.ToString();
        }

        public static int Part2(int recipeNumber)
        {
            return -1;
        }
    }
}
