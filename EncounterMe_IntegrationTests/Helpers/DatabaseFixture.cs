using Encounter_Me.Api.Models;
using EncounterMe_IntegrationTests.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace EncounterMe_IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {

        private static readonly object _lock = new object();
        private static bool _databaseInitialized;
        public DbConnection Connection { get; }
        public AppDbContext DbContext { get; set; }

        public DatabaseFixture()
        {
            Connection = new SqlConnection($"Server=(localdb)\\mssqllocaldb;Database=Encounter_Me_Tests;Trusted_Connection=True;MultipleActiveResultSets=true");

            Seed();

            Connection.Open();
        }

        public AppDbContext CreateContext()
        {
            var testingDb = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(Connection).Options);
            return testingDb;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var db = CreateContext())
                    {
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();

                        TestDbDataManager.InitializeDbForTests(db);
                        DbContext = db;
                    }
                    _databaseInitialized = true;
                }
                else
                {
                    using (var db = CreateContext())
                    {
                        TestDbDataManager.ReinitializeDbForTests(db);
                        DbContext = db;
                    }
                }
            }
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
