using Microsoft.EntityFrameworkCore;

namespace RVezyTest.DBContext
{
    public class Context : DbContext
    {
        public DbSet<Listings> Listings { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Reviews> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=GZKNote;Database=RVezyTest;Trusted_Connection=True");
        }
    }
}