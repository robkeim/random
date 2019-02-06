using System.IO;

namespace CompressDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputDir = @"c:\users\robke\desktop\input";
            var compressedDir = @"c:\users\robke\desktop\compressed";
            var uncompressedDir = @"c:\users\robke\desktop\uncompressed";
            var maxFileSizeInMB = 3;

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

            Compress.Execute(inputDir, compressedDir, maxFileSizeInMB);
            Decompress.Execute(compressedDir, uncompressedDir);
        }
    }
}
