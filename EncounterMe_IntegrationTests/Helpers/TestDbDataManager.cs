using Encounter_Me;
using Encounter_Me.Api.Models;
using Encounter_Me.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EncounterMe_IntegrationTests.Helpers
{
    class TestDbDataManager
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.SaveChanges();
        }

        public static List<UserData>GetSeedingUsers()
        {
            var hashSalt = PasswordManager.EncryptPassword("Testing1");
            
            return new List<UserData>()
            {
                new UserData(Guid.Parse("d68ba044-95a1-442b-8754-e329410c766b"), (Factions)2, "Username", "Fn",  "Ln",  "userName@mail", hashSalt.Hash, hashSalt.Salt),
                new UserData(Guid.NewGuid(), (Factions)1, "Rr", "Fn",  "Ln",  "Rr@mail", hashSalt.Hash, hashSalt.Salt),
                new UserData(Guid.NewGuid(), (Factions)3, "Dm", "Fn",  "Ln",  "Dd@mail", hashSalt.Hash, hashSalt.Salt)
            };
        }

        public static void ReinitializeEmptyDbForTests(AppDbContext db)
        {
            db.Users.RemoveRange(db.Users);
            db.Trails.RemoveRange(db.Trails);
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext db)
        {
            ReinitializeEmptyDbForTests(db);
            InitializeDbForTests(db);
            db.SaveChanges();
        }
    }
}
