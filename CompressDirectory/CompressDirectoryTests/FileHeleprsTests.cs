using System;
using System.IO;
using System.Linq;
using CompressDirectory;
using NUnit.Framework;

namespace CompressDirectoryTests
{
    public class FileHelpersTests : BaseTests
    {
        [Test]
        public void SplitFile_WhenFileSizeInLessThanChunkSize_ReturnsOneFile()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);

            // Act
            FileHelpers.SplitFile("./RandomFile", tmpDir, 5);

            // Assert
            Assert.AreEqual(1, Directory.EnumerateFiles(tmpDir).Count());
        }

        [Test]
        public void SplitFile_WhenFileSizeInMoreThanChunkSize_ReturnsMultipleFiles()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);

            // Act
            FileHelpers.SplitFile("./RandomFile", tmpDir, 3);

            // Assert
            Assert.AreEqual(2, Directory.EnumerateFiles(tmpDir).Count());
        }

        [Test]
        public void SplitFile_WhenFileSizeInMoreThanChunkSize_ReturnsFilesWithMaxChunkSizeExceptLast()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);
            var chunkSize = 2;

            // Act
            FileHelpers.SplitFile("./RandomFile", tmpDir, chunkSize);

            // Assert
            Assert.AreEqual(3, Directory.EnumerateFiles(tmpDir).Count());
            Assert.AreEqual(2, Directory.EnumerateFiles(tmpDir).Count(f => new FileInfo(f).Length == chunkSize * 1024L * 1024L));
        }

        [Test]
        public void SplitFile_WhenFileToSplitDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);

            // Act
            void action() => FileHelpers.SplitFile($"./{Guid.NewGuid()}", tmpDir, 3);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void SplitFile_WhenOutputDirectoryDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();

            // Act
            void action() => FileHelpers.SplitFile("./RandomFile", tmpDir, 3);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void SplitFile_WhenChunkSizeIsLessThanOne_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);

            // Act
            void action() => FileHelpers.SplitFile("./RandomFile", tmpDir, 0);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Test]
        public void JoinFile_WhenInputDirectoryDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            var tmpFile = Path.GetTempPath() + Guid.NewGuid();

            // Act
            void action() => FileHelpers.JoinFile(tmpDir, tmpFile);

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void JoinFile_WhenOutputFileExists_ThrowsArgumentException()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);
            var tmpFile = Path.GetTempFileName();

            // Act
            void action() => FileHelpers.JoinFile(tmpDir, tmpFile);

            try
            {
                // Assert
                Assert.Throws<ArgumentException>(action);
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        [Test]
        public void JoinFile_WhenCalledWithResultsOfSplitFile_ReturnsOriginalFile()
        {
            // Arrange
            var tmpDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpDir);
            _tmpDirs.Add(tmpDir);
            FileHelpers.SplitFile("./RandomFile", tmpDir, 5);
            var outputFile = Path.GetTempPath() + Guid.NewGuid();

            try
            {
                // Act
                FileHelpers.JoinFile(tmpDir, outputFile);

                // Assert
                Assert.AreEqual(new FileInfo("./RandomFile").Length, new FileInfo(outputFile).Length);
                AssertHelpers.FilesAreEqual("./RandomFile", outputFile);
            }
            finally
            {
                File.Delete(outputFile);
            }
        }
    }
}