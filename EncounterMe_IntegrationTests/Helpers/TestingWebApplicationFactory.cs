using Encounter_Me.Api.Models;
using EncounterMe_IntegrationTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EncounterMe_IntegrationTests
{
    public class TestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                services.Remove(descriptor);


                services.AddDbContext<AppDbContext>(options =>
                                  options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=Encounter_Me_Tests;Trusted_Connection=True;MultipleActiveResultSets=true"));

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();

                    try 
                    { 
                        db.Database.EnsureCreated();
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }   
                }
            });

        }
    }
}
