using System;
using System.IO;

namespace CompressDirectoryTests
{
    public class TemporaryFolder : IDisposable
    {
        public string Path { get; }

        public TemporaryFolder()
        {
            Path = System.IO.Path.GetTempPath() + Guid.NewGuid();
        }

        public void Dispose()
        {
            File.Delete(Path);
        }
    }
}
