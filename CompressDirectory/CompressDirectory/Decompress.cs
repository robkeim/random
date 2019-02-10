using System;
using System.IO;
using System.Linq;

namespace CompressDirectory
{
    public static class Decompress
    {
        public static void Execute(ICompressor compressor, string compressedDir, string extracedDir)
        {
            if (!Directory.Exists(compressedDir))
            {
                throw new ArgumentException("Compressed directory must exist", nameof(compressedDir));
            }

            if (Directory.Exists(extracedDir)
                && (Directory.EnumerateFiles(extracedDir).Any() || Directory.EnumerateDirectories(extracedDir).Any()))
            {
                throw new ArgumentException("Extracted directory must not exist or be empty", nameof(compressedDir));
            }

            if (!Directory.Exists(extracedDir))
            {
                Directory.CreateDirectory(extracedDir);
            }

            var compressedFileName = compressedDir + Path.DirectorySeparatorChar + Constants.COMPRESSED_FILE_NAME;

            FileHelpers.JoinFile(compressedDir, compressedFileName);
            
            compressor.Decompress(compressedFileName, extracedDir);

            File.Delete(compressedFileName);
        }
    }
}
