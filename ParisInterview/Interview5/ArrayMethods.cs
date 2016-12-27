using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview5
{
    public static class ArrayMethods
    {
        // Given an array of potentially repeated elements A, find the number of occurances of all of the elements
        // in array B (which must be unique)
        public static Dictionary<string, int> FindOccurancesInRepeatedArray(string[] A, string[] B)
        {
            if (A == null || B == null)
            {
                throw new ArgumentNullException("The input arrays cannot be null!");
            }

            Dictionary<string, int> frequencyMap = new Dictionary<string, int>();
            foreach (string str in A)
            {
                if (!frequencyMap.ContainsKey(str))
                {
                    frequencyMap[str] = 0;
                }

                frequencyMap[str]++;
            }

            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (string str in B)
            {
                if (result.ContainsKey(str))
                {
                    throw new ArgumentException("All elements in the find array must be unique");
                }

                if (!frequencyMap.ContainsKey(str))
                {
                    result[str] = 0;
                }
                else
                {
                    result[str] = frequencyMap[str];
                }
            }

            return result;
        }

        // Given two sorted arrays of unsigned integers return a sorted array merging the two arrays
        public static uint[] MergeArrays(uint[] A, uint[] B)
        {
            if (A == null || B == null)
            {
                throw new ArgumentNullException("The input arrays cannot be null!");
            }

            int aIndex = 0;
            int bIndex = 0;

            uint[] result = new uint[A.Length + B.Length];
            int resultIndex = 0;

            while (resultIndex < result.Length)
            {
                if (aIndex == A.Length)
                {
                    result[resultIndex++] = B[bIndex++];
                }
                else if (bIndex == B.Length)
                {
                    result[resultIndex++] = A[aIndex++];
                }
                else
                {
                    if (A[aIndex] < B[bIndex])
                    {
                        result[resultIndex++] = A[aIndex++];
                    }
                    else
                    {
                        result[resultIndex++] = B[bIndex++];
                    }
                }
            }

            return result;
        }
    }
}
