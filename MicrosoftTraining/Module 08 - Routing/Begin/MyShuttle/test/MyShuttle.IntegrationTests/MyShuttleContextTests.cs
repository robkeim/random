using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MyShuttle.Data;
using System.Data.Entity;

namespace MyShuttle.IntegrationTests
{
    [TestClass]
    public class MyShuttleContextTests
    {
        [TestMethod]
        public void Should_Create_DB()
        {
            // Arrange
            var connectionString = $"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MyShuttle-Test-{Guid.NewGuid().ToString()}";
            var context = new MyShuttleContext(connectionString);

            // Act
            var created = InitializeDatabaseForTests(context);
            var deleted = context.Database.Delete();

            // Assert
            Assert.IsTrue(created);
            Assert.IsTrue(deleted);
        }

        private static bool InitializeDatabaseForTests(DbContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.Delete())
                {
                    return false;
                };
            }
            context.Database.Create();

            return context.Database.Exists();
        }
    }
}
