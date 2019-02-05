using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CompressDirectory
{
    public static class Decompress
    {
        public static void Execute(string inputDir, string outputDir)
        {
            using (var zipFile = new FileStream(inputDir + Path.DirectorySeparatorChar + @"compressed.zip", FileMode.Open))
            using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Read))
            {
                var indexEntity = archive.GetEntry("index.txt");
                using (var reader = new StreamReader(indexEntity.Open()))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var split = line.Split(" ".ToCharArray());
                        var id = split[0];
                        var relativePath = split[1];

                        var entity = archive.GetEntry(id);
                        var outputFile = outputDir + relativePath;

                        var path = relativePath
                            .Split(Path.DirectorySeparatorChar.ToString().ToCharArray())
                            .ToArray();

                        var dir = outputDir;

                        for (int i = 0; i < path.Length - 1; i++)
                        {
                            dir += Path.DirectorySeparatorChar + path[i];

                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                        }

                        entity.ExtractToFile(outputFile);
                    }
                }
            }
        }
    }
}
