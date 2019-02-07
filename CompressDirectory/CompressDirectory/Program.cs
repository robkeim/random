using System;
using System.IO;

namespace CompressDirectory
{
    class Program
    {
        // TODO rkeim: handle empty directories being compressed
        static void Main(string[] args)
        {
            var inputDir = @"c:\users\robke\desktop\input";
            var compressedDir = @"c:\users\robke\desktop\compressed";
            var uncompressedDir = @"c:\users\robke\desktop\uncompressed";
            var maxFileSizeInMB = 3000;

            // TODO rkeim: ensure that existing directories/missing directories are handled and remove this chunk of code
            if (Directory.Exists(compressedDir))
            {
                Directory.Delete(compressedDir, true);
            }
            
            if (Directory.Exists(uncompressedDir))
            {
                Directory.Delete(uncompressedDir, true);
            }

            Directory.CreateDirectory(compressedDir);
            Directory.CreateDirectory(uncompressedDir);

            Console.WriteLine("Compressing...");
            Compress.Execute(inputDir, compressedDir, maxFileSizeInMB);

            Console.WriteLine("Decompressing...");
            Decompress.Execute(compressedDir, uncompressedDir);
        }
    }
}
