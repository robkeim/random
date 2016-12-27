using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PhoneScreen
{
    public static class FindDuplicateFiles
    {
        // Given a directory, recurse through all of the files in the directory or any
        // subdirectories and print out any duplicate files
        public static void PrintDuplicateFiles(string initialDirectory)
        {
            string[] files = Directory.GetFiles(initialDirectory, "*.*", SearchOption.AllDirectories);

            Dictionary<string, List<string>> equalFiles = new Dictionary<string, List<string>>();

            foreach (var file in files)
            {
                string hash = HashFile(file);

                if (!equalFiles.ContainsKey(hash))
                {
                    equalFiles[hash] = new List<string>();
                }

                equalFiles[hash].Add(file);
            }

            foreach (string key in equalFiles.Keys)
            {
                if (equalFiles[key].Count > 1)
                {
                    Console.WriteLine(key);

                    foreach (string file in equalFiles[key])
                    {
                        Console.WriteLine(string.Format("    {0}", file));
                    }

                    Console.WriteLine();
                }
            }
        }

        // Given a specific file return a hash of it's contents
        private static string HashFile(string fileName)
        {
            using (HashAlgorithm hashAlgorithm = new SHA1Managed())
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream == null)
                    {
                        throw new ArgumentNullException(string.Format("File {0} cannot be found", fileName));
                    }

                    return BitConverter.ToString(hashAlgorithm.ComputeHash(fileStream));
                }
            }
        }
    }
}
