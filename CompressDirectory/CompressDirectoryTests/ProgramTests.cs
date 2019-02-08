using System;
using System.IO;
using CompressDirectory;
using NUnit.Framework;

namespace CompressDirectoryTests
{
    public class ProgramTests : BaseTests
    {
        [Test]
        public void Main_WhenNumArgumentsLessThanTwo_ThrowsArgumentException()
        {
            var args = new[] { "arg1" };

            // Act
            void action() => CompressDirectory.Program.Main(args);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Main_WhenNumArgumentsMoreThanThree_ThrowsArgumentException()
        {
            var args = new[] { "arg1", "arg2", "arg3", "arg4" };

            // Act
            void action() => CompressDirectory.Program.Main(args);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Main_WhenThirdArgumentNotInteger_ThrowsArgumentException()
        {
            var args = new[] { "arg1", "arg2", "arg3" };

            // Act
            void action() => CompressDirectory.Program.Main(args);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Main_WhenThirdArgumentIsLessThanOner_ThrowsArgumentException()
        {
            var args = new[] { "arg1", "arg2", "0" };

            // Act
            void action() => CompressDirectory.Program.Main(args);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void Main_FullEndToEndTest_CompressesAndDecompresses()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(compressedDir);
            _tmpDirs.Add(compressedDir);

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);

            File.Copy("./RandomFile", dirToCompress + Path.DirectorySeparatorChar + "RandomFile");

            // Act
            Program.Main(new[] { dirToCompress, compressedDir, "3" });
            Program.Main(new[] { compressedDir, extractedDir });

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }
    }
}
