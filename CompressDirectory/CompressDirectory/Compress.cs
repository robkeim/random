using System;
using System.IO;
using System.IO.Compression;

namespace CompressDirectory
{
    public static class Compress
    {
        public static void Execute(string inputDir, string compressedDir, int maxFileSizeInMB)
        {
            using (var zipFile = new FileStream(compressedDir + Path.DirectorySeparatorChar + "compressed.zip", FileMode.Create))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
            {
                var indexEntry = archive.CreateEntry("index.txt");
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
            var maxSizeInBytes = maxFileSizeInMB * 1024 * 1024;
            var compressedFile = compressedDir + Path.DirectorySeparatorChar + "compressed.zip";
            var length = new FileInfo(compressedFile).Length;

            if (length < maxSizeInBytes)
            {
                File.Move(compressedFile, compressedDir + Path.DirectorySeparatorChar + "compressed-0");
            }
            else
            {
                const int BUFFER_SIZE = 20 * 1024; // TODO rkeim: justify this
                var buffer = new byte[BUFFER_SIZE];

                using (var fileToSplit = File.OpenRead(compressedFile))
                {
                    int index = 0;

                    while (fileToSplit.Position < fileToSplit.Length)
                    {
                        using (var outputStream = File.Create(compressedDir + Path.DirectorySeparatorChar + $"compressed-{index}"))
                        {
                            int bytesRead;
                            int remaining = maxSizeInBytes;
                            while (remaining > 0 && (bytesRead = fileToSplit.Read(buffer, 0, Math.Min(remaining, BUFFER_SIZE))) > 0)
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
