namespace CompressDirectory
{
    public interface ICompressor
    {
        /// <summary>
        /// Given a directory, output a compressed version to the specified file
        /// </summary>
        void Compress(string dirToCompress, string outputFile);

        /// <summary>
        /// Given a file compressed with Compress, extract it to the specified directory
        /// </summary>
        void Decompress(string compressedFile, string extractedDir);
    }
}
