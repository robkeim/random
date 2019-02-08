using System;
using NUnit.Framework;

namespace CompressDirectoryTests
{
    public class ProgramTests
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
    }
}
