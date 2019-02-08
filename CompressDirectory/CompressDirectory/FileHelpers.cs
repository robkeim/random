using System;
using System.IO;
using System.Linq;

namespace CompressDirectory
{
    public static class FileHelpers
    {
        public static void SplitFile(string fileToSplit, string outputDir, int maxFileSizeInMB)
        {
            if (!File.Exists(fileToSplit))
            {
                throw new ArgumentException("File to split needs to exist", nameof(fileToSplit));
            }

            if (!Directory.Exists(outputDir))
            {
                throw new ArgumentException("Output directory needs to exist", nameof(outputDir));
            }

            if (maxFileSizeInMB < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxFileSizeInMB));
            }

            var maxSizeInBytes = maxFileSizeInMB * 1024L * 1024L;
            var length = new FileInfo(fileToSplit).Length;

            if (length < maxSizeInBytes)
            {
                File.Move(fileToSplit, outputDir + Path.DirectorySeparatorChar + "compressed-0");
            }
            else
            {
                // Buffer size of 4k based on .NET's choice to use 4k for FileStream:
                // https://referencesource.microsoft.com/#mscorlib/system/io/filestream.cs,396
                const int BUFFER_SIZE = 4 * 1024;
                var buffer = new byte[BUFFER_SIZE];

                using (var fileStream = File.OpenRead(fileToSplit))
                {
                    var index = 0;

                    while (fileStream.Position < fileStream.Length)
                    {
                        using (var outputStream = File.Create(outputDir + Path.DirectorySeparatorChar + $"compressed-{index}"))
                        {
                            int bytesRead;
                            var remaining = maxSizeInBytes;
                            while (remaining > 0 && (bytesRead = fileStream.Read(buffer, 0, Math.Min((int)Math.Min(remaining, int.MaxValue), BUFFER_SIZE))) > 0)
                            {
                                outputStream.Write(buffer, 0, bytesRead);
                                remaining -= bytesRead;
                            }
                        }

                        index++;
                    }
                }
            }
        }

        public static void JoinFile(string inputDir, string outputFileName)
        {
            if (!Directory.Exists(inputDir))
            {
                throw new ArgumentException("Input directory must exist", nameof(inputDir));
            }

            if (File.Exists(outputFileName))
            {
                throw new ArgumentException("Output file must not exist", nameof(outputFileName));
            }

            // Sort the files since lexographical order is not correct
            var files = Directory.GetFiles(inputDir, "compressed-*")
                .Select(f => new Tuple<string, string>(f, new FileInfo(f).Name))
                .Select(f => new Tuple<string, int>(f.Item1, int.Parse(f.Item2.Split("-".ToCharArray())[1])))
                .OrderBy(t => t.Item2)
                .Select(t => t.Item1)
                .ToArray();

            using (var outputStream = File.Create(outputFileName))
            {
                foreach (var file in files)
                {
                    using (var fileStream = File.OpenRead(file))
                    {
                        fileStream.CopyTo(outputStream);
                    }
                }
            }
        }
    }
}
