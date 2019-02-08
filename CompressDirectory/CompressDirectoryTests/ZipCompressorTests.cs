using CompressDirectory;
using NUnit.Framework;
using System;
using System.IO;

namespace CompressDirectoryTests
{
    public class ZipCompressorTests : BaseTests
    {
        [Test]
        public void Decompress_WithSingleFile_ReturnsOriginalFile()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedFile = Path.GetTempPath() + Guid.NewGuid();

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);
            
            File.Copy("./RandomFile", dirToCompress + Path.DirectorySeparatorChar + "RandomFile");

            // Act
            compressor.Compress(dirToCompress, compressedFile);
            compressor.Decompress(compressedFile, extractedDir);

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }

        [Test]
        public void Decompress_WithSingleFileWithSpaceInName_ReturnsOriginalFile()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedFile = Path.GetTempPath() + Guid.NewGuid();

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);

            File.Copy("./RandomFile", dirToCompress + Path.DirectorySeparatorChar + "Random File");

            // Act
            compressor.Compress(dirToCompress, compressedFile);
            compressor.Decompress(compressedFile, extractedDir);

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }

        [Test]
        public void Decompress_WithSingleFileInDirectory_ReturnsOriginalFile()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedFile = Path.GetTempPath() + Guid.NewGuid();

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);

            var nestedDir = dirToCompress + Path.DirectorySeparatorChar + "NestedDir";
            Directory.CreateDirectory(nestedDir);

            File.Copy("./RandomFile", nestedDir + Path.DirectorySeparatorChar + "RandomFile");

            // Act
            compressor.Compress(dirToCompress, compressedFile);
            compressor.Decompress(compressedFile, extractedDir);

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }

        [Test]
        public void Decompress_WithEmptyDirectory_ReturnsEmptyDirectory()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedFile = Path.GetTempPath() + Guid.NewGuid();

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);

            var nestedDir = dirToCompress + Path.DirectorySeparatorChar + "NestedDir";
            Directory.CreateDirectory(nestedDir);

            // Act
            compressor.Compress(dirToCompress, compressedFile);
            compressor.Decompress(compressedFile, extractedDir);

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }

        [Test]
        public void Decompress_WithEmptyDirectoryWithSpace_ReturnsEmptyDirectory()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedFile = Path.GetTempPath() + Guid.NewGuid();

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);

            var nestedDir = dirToCompress + Path.DirectorySeparatorChar + "Nested dir";
            Directory.CreateDirectory(nestedDir);

            // Act
            compressor.Compress(dirToCompress, compressedFile);
            compressor.Decompress(compressedFile, extractedDir);

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }

        [Test]
        public void Decompress_WithTwoFiles_ReturnsOriginalFiles()
        {
            // Arrange
            var compressor = new ZipCompressor();
            var dirToCompress = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(dirToCompress);
            _tmpDirs.Add(dirToCompress);

            var compressedFile = Path.GetTempPath() + Guid.NewGuid();

            var extractedDir = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(extractedDir);
            _tmpDirs.Add(extractedDir);

            File.Copy("./RandomFile", dirToCompress + Path.DirectorySeparatorChar + "RandomFile");
            File.Copy("./RandomFile", dirToCompress + Path.DirectorySeparatorChar + "RandomFile2");

            // Act
            compressor.Compress(dirToCompress, compressedFile);
            compressor.Decompress(compressedFile, extractedDir);

            // Assert
            AssertHelpers.DirectoriesAreEqual(dirToCompress, extractedDir);
        }
    }
}
