using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CompressDirectory
{
    public class ZipCompressor : ICompressor
    {
        public string Compress(string dirToCompress, string compressedDir)
        {
            var outputFile = compressedDir + Path.DirectorySeparatorChar + Constants.ZIP_NAME;

            using (var zipFile = new FileStream(outputFile, FileMode.Create))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
            {
                var indexEntry = archive.CreateEntry(Constants.ZIP_INDEX_NAME);
                var indexWriter = new StreamWriter(indexEntry.Open());

                var files = Directory.GetFiles(dirToCompress, "*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    var id = Guid.NewGuid().ToString();
                    var relativeDir = file.Substring(dirToCompress.Length);
                    indexWriter.WriteLine($"{id} {relativeDir}");
                    archive.CreateEntryFromFile(file, id);
                }

                var emptyDirectories = Directory.GetDirectories(dirToCompress, "*", SearchOption.AllDirectories)
                    .Where(d => !files.Any(f => f.StartsWith(d)))
                    .ToArray();

                foreach (var dir in emptyDirectories)
                {
                    var relativeDir = dir.Substring(dirToCompress.Length);
                    indexWriter.WriteLine($"{Constants.EMPTY_DIRECTORY} {relativeDir}");
                }

                indexWriter.Close();

                return outputFile;
            }
        }

        public void Decompress(string compressedFile, string extractedDir)
        {
        }
    }
}
