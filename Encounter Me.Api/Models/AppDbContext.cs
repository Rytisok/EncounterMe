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
      
    public DbSet<UserData> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<UserData>().HasData(new UserData
            {
                Id = 1,
                UserName = "obuolys",
                FirstName = "Dominykas",
                LastName = "Zagreckas",
                Email = "dz@mail.lt",
                Level = 1,
                UserPhotoUrl = "https://i.pinimg.com/originals/83/6d/69/836d69f49e80af2825c7db264be44af0.jpg"

            }) ;

        }










    }
    
}
