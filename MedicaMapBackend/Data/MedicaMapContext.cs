using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Data
{
    public class MedicaMapContext : DbContext
    {
        public MedicaMapContext(DbContextOptions<MedicaMapContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Municipality>().HasOne(m => m.State).WithMany(s => s.Municipalities).HasForeignKey(m => m.StateId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
