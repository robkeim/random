using System;
using System.IO;
using System.Linq;

namespace CompressDirectory
{
    public static class Decompress
    {
        public static void Execute(ICompressor compressor, string compressedDir, string outputDir)
        {
            if (!Directory.Exists(compressedDir))
            {
                throw new ArgumentException("Compressed directory must exist", nameof(compressedDir));
            }

            if (Directory.Exists(outputDir)
                && (Directory.EnumerateFiles(outputDir).Any() || Directory.EnumerateDirectories(outputDir).Any()))
            {
                throw new ArgumentException("Output directory must exist and be empty", nameof(compressedDir));
            }

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var compressedFileName = compressedDir + Path.DirectorySeparatorChar + Constants.COMPRESSED_FILE_NAME;

            FileHelpers.JoinFile(compressedDir, compressedFileName);
            
            compressor.Decompress(compressedFileName, outputDir);

            File.Delete(compressedFileName);
        }
    }
}
