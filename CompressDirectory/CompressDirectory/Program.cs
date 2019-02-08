using System;

namespace CompressDirectory
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                throw new ArgumentException("Invalid number of input arguments");
            }

            var compressor = new ZipCompressor();

            if (args.Length == 3)
            {
                if (!int.TryParse(args[2], out var maxFileSizeInMB) || maxFileSizeInMB < 1)
                {
                    throw new ArgumentException("Invalid value for max file size in MB");
                }

                Compress.Execute(compressor, args[0], args[1], maxFileSizeInMB);
            }
            else
            {
                Decompress.Execute(compressor, args[0], args[1]);
            }
        }
    }
}
