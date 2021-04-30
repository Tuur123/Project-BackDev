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
        DbSet<BusinessBeer> BusinessBeers { get; set; }
        DbSet<Location> Locations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class BeerContext : DbContext, IBeerContext
    {
        private ConnectionStrings _ConnectionStrings;

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessBeer> BusinessBeers { get; set; }
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
            modelBuilder.Entity<BusinessBeer>().HasKey(b => new { b.BeerId, b.BusinessId });

            #region BeerSeed
            Guid jup = Guid.NewGuid();
            Guid duv = Guid.NewGuid();
            Guid omer = Guid.NewGuid();
            Guid chou = Guid.NewGuid();

            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = jup, Name = "Jupiler", AlchoholPercentage = 3.3, Brewer = "Piedbœuf" });
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = duv, Name = "Duvel", AlchoholPercentage = 9.5, Brewer = "Duvel Moortgat" });
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = omer, Name = "Omer", AlchoholPercentage = 8, Brewer = "Omer Vander Ghinste" });
            modelBuilder.Entity<Beer>().HasData(new Beer { BeerId = chou, Name = "La Chouffe", AlchoholPercentage = 8, Brewer = "AChouffe" });
            #endregion

            #region LocationSeed
            Guid ant = Guid.NewGuid();
            Guid bru = Guid.NewGuid();
            Guid kor = Guid.NewGuid();

            modelBuilder.Entity<Location>().HasData(new Location { LocationId = ant, City = "Antwerpen", Postcode = 2000 });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = bru, City = "Brugge", Postcode = 8000 });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = kor, City = "Kortijk", Postcode = 8500 });
            #endregion

            #region BusinessSeed
            Guid bar = Guid.NewGuid();
            Guid bur = Guid.NewGuid();
            Guid kan = Guid.NewGuid();

            modelBuilder.Entity<Business>().HasData(new Business { BusinessId = bar, LocationId = ant, Name = "Barbier", Type = "Cafe" });
            modelBuilder.Entity<Business>().HasData(new Business { BusinessId = bur, LocationId = bru, Name = "'t Burgs Beertje", Type = "Restaurant" });
            modelBuilder.Entity<Business>().HasData(new Business { BusinessId = kan, LocationId = kor, Name = "'t Kanon", Type = "Cafe" });
            #endregion

            #region BusinessBeerSeed
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = jup, BusinessId = bar });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = duv, BusinessId = bar });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = omer, BusinessId = bar });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = chou, BusinessId = bar });

            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = duv, BusinessId = bur });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = omer, BusinessId = bur });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = chou, BusinessId = bur });

            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = omer, BusinessId = kan });
            modelBuilder.Entity<BusinessBeer>().HasData(new BusinessBeer { BeerId = chou, BusinessId = kan });
            #endregion
        }
    }
}
