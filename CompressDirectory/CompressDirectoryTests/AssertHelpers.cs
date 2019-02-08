using NUnit.Framework;
using System.IO;
using System.Linq;

namespace CompressDirectoryTests
{
    public static class AssertHelpers
    {
        public static void DirectoriesAreEqual(string expected, string actual)
        {
            if (!Directory.Exists(expected) || !Directory.Exists(actual))
            {
                Assert.Fail("Missing directory");
            }

            var expectedDirectories = Directory.GetDirectories(expected, "*", SearchOption.AllDirectories)
                .Select(d => d.Substring(expected.Length))
                .ToArray();

            var actualDirectories = Directory.GetDirectories(expected, "*", SearchOption.AllDirectories)
                .Select(d => d.Substring(actual.Length))
                .ToArray();

            Assert.AreEqual(expectedDirectories.Length, actualDirectories.Length);
            
            for (int i = 0; i < expectedDirectories.Length; i++)
            {
                Assert.AreEqual(expectedDirectories[i], actualDirectories[i]);
            }

            var expectedFiles = Directory.GetFiles(expected, "*", SearchOption.AllDirectories);
            var actualFiles = Directory.GetFiles(actual, "*", SearchOption.AllDirectories);

            Assert.AreEqual(expectedFiles.Length, actualFiles.Length);

            for (int i = 0; i < expectedFiles.Length; i++)
            {
                Assert.AreEqual(new FileInfo(expectedFiles[i]).Name, new FileInfo(actualFiles[i]).Name);
                FilesAreEqual(expectedFiles[i], expectedFiles[i]);
            }
        }

        public static void FilesAreEqual(string expected, string actual)
        {
            if (!File.Exists(expected) || !File.Exists(actual))
            {
                Assert.Fail("Missing file");
            }

            if (new FileInfo(expected).Length != new FileInfo(actual).Length)
            {
                Assert.Fail("Files are not equal length");
            }

            if (!File.ReadAllBytes(expected).SequenceEqual(File.ReadAllBytes(actual)))
            {
                Assert.Fail("Files are not equal");
            }
        }
    }
}
