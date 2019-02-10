using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace CompressDirectoryTests
{
    public class BaseTests
    {
        protected List<string> _tmpDirs;

        [SetUp]
        public void Setup()
        {
            _tmpDirs = new List<string>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var tmpDir in _tmpDirs)
            {
                try
                {
                    Directory.Delete(tmpDir, true);
                }
                catch (DirectoryNotFoundException)
                {
                    // Ignore exception since directory doesn't exist
                }
            }
        }
    }
}
