using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CompressDirectory
{
    public static class Decompress
    {
        public static void Execute(string inputDir, string outputDir)
        {
            BuildZip(inputDir);

            using (var zipFile = new FileStream(inputDir + Path.DirectorySeparatorChar + Constants.ZIP_NAME, FileMode.Open))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Read))
            {
                var indexEntity = archive.GetEntry(Constants.ZIP_INDEX_NAME);
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

                        var dir = outputDir;
                        var stoppingPoint = id == Constants.EMPTY_DIRECTORY
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

                        if (id != Constants.EMPTY_DIRECTORY)
                        {
                            var entity = archive.GetEntry(id);
                            var outputFile = outputDir + relativePath;

                            entity.ExtractToFile(outputFile);
                        }
                    }
                }
            }

            File.Delete(inputDir + Path.DirectorySeparatorChar + Constants.ZIP_NAME);
        }

        private static void BuildZip(string inputDir)
        {
            // Sort the files since lexographical order is not correct
            var files = Directory.GetFiles(inputDir, "compressed-*")
                .Select(f => new Tuple<string, int>(f, int.Parse(f.Split("-".ToCharArray())[1])))
                .OrderBy(t => t.Item2)
                .Select(t => t.Item1)
                .ToArray();

            using (var outputStream = File.Create(inputDir + Path.DirectorySeparatorChar + Constants.ZIP_NAME))
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
