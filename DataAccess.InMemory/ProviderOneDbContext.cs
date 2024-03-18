using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.InMemory
{
    public class ProviderOneDbContext : DbContext
    {
        public ProviderOneDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Entities.Route> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Route>().HasData(GetInitialRouteData());
        }

        private IEnumerable<Entities.Route> GetInitialRouteData()
        {
            yield return new Entities.Route
            {
                Id = Guid.NewGuid(),
                Origin = "Moscow",
                Destination = "Sochi",
                OriginDateTime = new DateTime(2015, 1, 31, 5, 10, 20, DateTimeKind.Utc),
                DestinationDateTime = new DateTime(2015, 1, 31, 8, 10, 20, DateTimeKind.Utc),
                Price = 1000,
                TimeLimit = new DateTime(2015, 1, 31, 4, 10, 20, DateTimeKind.Utc)
            };

            yield return new Entities.Route
            {
                Id = Guid.NewGuid(),
                Origin = "Moscow",
                Destination = "Kazan",
                OriginDateTime = new DateTime(2015, 3, 31, 5, 10, 20, DateTimeKind.Utc),
                DestinationDateTime = new DateTime(2015, 3, 31, 8, 10, 20, DateTimeKind.Utc),
                Price = 1000,
                TimeLimit = new DateTime(2015, 3, 31, 4, 10, 20, DateTimeKind.Utc)
            };
        }
    }
}
