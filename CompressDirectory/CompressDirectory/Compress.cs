using System;
using System.IO;
using System.Linq;

namespace CompressDirectory
{
    public static class Compress
    {
        public static void Execute(ICompressor compressor, string dirToCompress,
            string compressedDir, int maxFileSizeInMB)
        {
            if (!Directory.Exists(dirToCompress))
            {
                throw new ArgumentException("Directory to compress must exist", nameof(dirToCompress));
            }

            if (!Directory.Exists(compressedDir)
                || Directory.EnumerateFiles(compressedDir).Any()
                || Directory.EnumerateDirectories(compressedDir).Any())
            {
                throw new ArgumentException("Compressed directory must exist and be empty", nameof(compressedDir));
            }

            var compressedFile = compressedDir + Path.DirectorySeparatorChar + Constants.COMPRESSED_FILE_NAME;
            
            compressor.Compress(dirToCompress, compressedFile);

            FileHelpers.SplitFile(compressedFile, compressedDir, maxFileSizeInMB);

            File.Delete(compressedFile);
        }
    }
}
