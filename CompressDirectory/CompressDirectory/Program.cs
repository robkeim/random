namespace CompressDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputDir = @"c:\users\robke\desktop\input";
            var compressedDir = @"c:\users\robke\desktop\compressed";
            var uncompressedDir = @"c:\users\robke\desktop\uncompressed";

            Compress.Execute(inputDir, compressedDir);
            Decompress.Execute(compressedDir, uncompressedDir);
        }
    }
}
