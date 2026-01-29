using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DBFixture
    {
        public ShopContext Context { get; private set; }

        public DBFixture()
        {
            // Set up the test databasection and initialize the context
            var options = new DbContextOptionsBuilder<ShopContext>()

                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ShopTests;Trusted_Connection=True;")
                .Options;
            Context = new ShopContext(options);
            Context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
