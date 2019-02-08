namespace CompressDirectory
{
    public interface ICompressor
    {
        string Compress(string dirToCompress, string compressedDir);

        void Decompress(string compressedFile, string extractedDir);

        // TODO rkeim: have Compress class do validation then check (dirs exist, etc) then call ZipCompression which contains logic
        // to compress that is specific to zip
    }
}
