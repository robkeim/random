using System;
using System.IO;
using System.IO.Compression;

namespace CompressDirectory
{
    public static class Compress
    {
        public static void Execute(string inputDir, string compressedDir)
        {
            using (var zipToOpen = new FileStream(compressedDir + @"\compressed.zip", FileMode.Create))
            using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
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
        }
    }
}
