using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview4
{
    public static class Array
    {
        // Return a list of the missing elements in an array given that the initial array meets the following constraints:
        // - It contains the elements 1-n in any order (where n is the size of the array)
        // - Each value 1-n appears only once in the array
        // - Zeros may be placed at any position to remove specific numbers
        public static List<int> FindMissingElements(int[] arr)
        {
            List<int> results = new List<int>();

            Sort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    results.Add(i + 1);
                }
            }

            return results;
        }

        // Sort an array in place given that the array contains only the elements 1-n where n is the size of the array
        // and that the values are not repeated.  The array may contain zeros to indicate missing elements
        private static void Sort(int[] arr)
        {
            int curIndex = 0;

            while (curIndex < arr.Length)
            {
                if (arr[curIndex] < 0 || arr[curIndex] > arr.Length)
                {
                }
                else if (arr[curIndex] == 0 || arr[curIndex] == curIndex + 1)
                {
                    curIndex++;
                }
                else
                {
                    int tmp = arr[curIndex];
                    arr[curIndex] = arr[tmp - 1];
                    arr[tmp - 1] = tmp;
                }
            }
        }
    }
}
