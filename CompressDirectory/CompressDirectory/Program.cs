using System;
using System.IO;

namespace CompressDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO rkeim: remove these hard coded arguments and read them from args
            var inputDir = @"c:\users\robke\desktop\input";
            var compressedDir = @"c:\users\robke\desktop\compressed";
            var uncompressedDir = @"c:\users\robke\desktop\uncompressed";
            var maxFileSizeInMB = 3;

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

            var compressor = new ZipCompressor();

            Console.WriteLine("Compressing...");
            Compress.Execute(compressor, inputDir, compressedDir, maxFileSizeInMB);

            Console.WriteLine("Decompressing...");
            Decompress.Execute(compressor, compressedDir, uncompressedDir);
        }
    }
}
