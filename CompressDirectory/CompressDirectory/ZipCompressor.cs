using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CompressDirectory
{
    public class ZipCompressor : ICompressor
    {
        private const string EMPTY_DIRECTORY = "EMPTY_DIR";
        private const string ZIP_INDEX_NAME = "index.txt";

        public void Compress(string dirToCompress, string outputFile)
        {
            using (var zipFile = new FileStream(outputFile, FileMode.Create))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
            {
                var indexEntry = archive.CreateEntry(ZIP_INDEX_NAME);
                using (var indexWriter = new StreamWriter(indexEntry.Open()))
                {
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
                        indexWriter.WriteLine($"{EMPTY_DIRECTORY} {relativeDir}");
                    }

                    indexWriter.Close();
                }
            }
        }

        public void Decompress(string compressedFile, string extractedDir)
        {
            using (var zipFile = new FileStream(compressedFile, FileMode.Open))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Read))
            {
                var indexEntity = archive.GetEntry(ZIP_INDEX_NAME);
                using (var reader = new StreamReader(indexEntity.Open()))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var split = line.Split(" ".ToCharArray(), 2);
                        var id = split[0];
                        var relativePath = split[1];

                        var path = relativePath
                            .Split(Path.DirectorySeparatorChar.ToString().ToCharArray())
                            .ToArray();

                        var dir = extractedDir;
                        var stoppingPoint = id == EMPTY_DIRECTORY
                            ? path.Length
                            : path.Length - 1;

                        for (int i = 0; i < stoppingPoint; i++)
                        {
                            dir += Path.DirectorySeparatorChar + path[i];

                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                        }

                        if (id != EMPTY_DIRECTORY)
                        {
                            var entity = archive.GetEntry(id);
                            var outputFile = extractedDir + relativePath;

                            entity.ExtractToFile(outputFile);
                        }
                    }
                }
            }
        }
    }
}
