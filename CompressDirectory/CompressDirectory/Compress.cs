using System;
using System.IO;
using System.IO.Compression;

namespace CompressDirectory
{
    public static class Compress
    {
        public static void Execute(string inputDir, string compressedDir, int maxFileSizeInMB)
        {
            using (var zipFile = new FileStream(compressedDir + Path.DirectorySeparatorChar + Constants.ZIP_NAME, FileMode.Create))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
            {
                var indexEntry = archive.CreateEntry(Constants.ZIP_INDEX_NAME);
                var indexWriter = new StreamWriter(indexEntry.Open());

                var files = Directory.GetFiles(inputDir, "*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    var id = Guid.NewGuid().ToString();
                    var relativeDir = file.Substring(inputDir.Length);
                    indexWriter.WriteLine($"{id} {relativeDir}");
                    archive.CreateEntryFromFile(file, id);
                }

                indexWriter.Close();
            }

            SplitFile(compressedDir, maxFileSizeInMB);
        }

        private static void SplitFile(string compressedDir, int maxFileSizeInMB)
        {
            var maxSizeInBytes = maxFileSizeInMB * 1024L * 1024L;
            var compressedFile = compressedDir + Path.DirectorySeparatorChar + Constants.ZIP_NAME;
            var length = new FileInfo(compressedFile).Length;

            if (length < maxSizeInBytes)
            {
                File.Move(compressedFile, compressedDir + Path.DirectorySeparatorChar + "compressed-0");
            }
            else
            {
                // Buffer size of 4k based on .NET's choice to use 4k for FileStream:
                // https://referencesource.microsoft.com/#mscorlib/system/io/filestream.cs,396
                const int BUFFER_SIZE = 4 * 1024;
                var buffer = new byte[BUFFER_SIZE];

                using (var fileToSplit = File.OpenRead(compressedFile))
                {
                    var index = 0;

                    while (fileToSplit.Position < fileToSplit.Length)
                    {
                        using (var outputStream = File.Create(compressedDir + Path.DirectorySeparatorChar + $"compressed-{index}"))
                        {
                            int bytesRead;
                            var remaining = maxSizeInBytes;
                            while (remaining > 0 && (bytesRead = fileToSplit.Read(buffer, 0, Math.Min((int)Math.Min(remaining, int.MaxValue), BUFFER_SIZE))) > 0)
                            {
                                outputStream.Write(buffer, 0, bytesRead);
                                remaining -= bytesRead;
                            }
                        }

                        index++;
                    }
                }
            }

            File.Delete(compressedFile);
        }
    }
}
