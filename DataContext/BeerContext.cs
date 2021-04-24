using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

using BeerApi.Configuration;
using BeerApi.Models;

namespace BeerApi.DataContext
{
    public interface IBeerContext
    {
        DbSet<Beer> Beers { get; set; }
        DbSet<Business> Businesses { get; set; }
        DbSet<Location> Locations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class BeerContext : DbContext, IBeerContext
    {
        private ConnectionStrings _ConnectionStrings;

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Location> Locations { get; set; }

        public BeerContext(DbContextOptions<BeerContext> options, IOptions<ConnectionStrings> ConnectionStrings) : base(options)
        {
            _ConnectionStrings = ConnectionStrings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_ConnectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessBeer>().HasKey(s => new { s.BeerId, s.BusinessId });

            #region BeerSeed
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = 1, Name = "Jupiler", AlchoholPercentage = 3.3, Brewer = "Piedbœuf" });
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = 2, Name = "Duvel", AlchoholPercentage = 9.5, Brewer = "Duvel Moortgat" });
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = 3, Name = "Omer", AlchoholPercentage = 8, Brewer = "Omer Vander Ghinste" });
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = 4, Name = "La Chouffe", AlchoholPercentage = 8, Brewer = "AChouffe" });
            #endregion

            #region LocationSeed
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 1, City = "Antwerpen", Postcode = 2000 });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 2, City = "Brugge", Postcode = 8000 });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 3, City = "Kortijk", Postcode = 8500 });
            #endregion

            #region BusinessSeed
            modelBuilder.Entity<Business>().HasData(new Business { BusinessId = 1, LocationId = 1, Name = "Barbier", Type = "Cafe"});
            modelBuilder.Entity<Business>().HasData(new Business { BusinessId = 2, LocationId = 2, Name = "'t Burgs Beertje", Type = "Restaurant"});
            modelBuilder.Entity<Business>().HasData(new Business { BusinessId = 3, LocationId = 3, Name = "'t Kanon", Type = "Cafe"});
            #endregion

            #region BusinessBeerSeed
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 1, BusinessId = 1 });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 2, BusinessId = 1 });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 3, BusinessId = 1 });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 4, BusinessId = 1 });

            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 2, BusinessId = 2 });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 3, BusinessId = 2 });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 4, BusinessId = 2 });

            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 3, BusinessId = 3 });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = 4, BusinessId = 3 });
            #endregion
        }
    }
}
