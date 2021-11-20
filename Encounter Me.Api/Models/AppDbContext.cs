using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter_Me.Api.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
      
    public DbSet<TrailContainer> Trails { get; set; }
    public DbSet<UserData> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<UserData>().HasData(new UserData
            {
                Id = Guid.NewGuid(),
                UserName = "obuolys",
                FirstName = "Dominykas",
                LastName = "Zagreckas",
                Email = "dz@mail.lt",
                Level = 1,
                UserPhotoUrl = "https://i.pinimg.com/originals/83/6d/69/836d69f49e80af2825c7db264be44af0.jpg"

            }) ;






            modelBuilder.Entity<TrailContainer>().HasData(new TrailContainer
            {
                Id = 1,
                Lat = 54.7214,
                Lng = 25.2555,
                Length = 2.4,
                Diff = 2,
                GeoJsonData = "sample-data/test.geojson",
                trailType = TrailType.Simple



            });
            modelBuilder.Entity<TrailContainer>().HasData(new TrailContainer
            {
                Id = 2,
                Lat = 54.6850023616530,
                Lng = 25.240305662155,
                Length = 3,
                Diff = 3,
                GeoJsonData = "sample-data/test1.geojson",
                trailType = TrailType.Nature


            });

            modelBuilder.Entity<TrailContainer>().HasData(new TrailContainer
            {
                Id = 3,
                Lat = 54.6868133,
                Lng = 25.2905595,
                Length = 1.4,
                Diff = 1,
                GeoJsonData = "sample-data/test2.geojson",
                trailType = TrailType.Historic



            });

        }










    }
    
}
