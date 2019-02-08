using System.IO;

namespace CompressDirectory
{
    public static class Compress
    {
        public static void Execute(string dirToCompress, string compressedDir, int maxFileSizeInMB)
        {
            var compressor = new ZipCompressor();
            var compressedFile = compressor.Compress(dirToCompress, compressedDir);

            FileHelpers.SplitFile(compressedFile, compressedDir, maxFileSizeInMB);
            File.Delete(compressedFile);
        }
    }
}
