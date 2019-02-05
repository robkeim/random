namespace CompressDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputDir = @"c:\users\robke\desktop\input";
            var compressedDir = @"c:\users\robke\desktop\output";

            Compress.Execute(inputDir, compressedDir);
        }
    }
}
