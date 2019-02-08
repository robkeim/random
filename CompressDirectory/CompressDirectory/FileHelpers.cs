using System;
using System.IO;

namespace CompressDirectory
{
    public static class FileHelpers
    {
        public static void SplitFile(string fileToSplit, string outputDir, int maxFileSizeInMB)
        {
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
    }
}
