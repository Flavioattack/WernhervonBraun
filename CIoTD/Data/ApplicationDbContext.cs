using Microsoft.EntityFrameworkCore;
using CIoTD.Models;

namespace CIoTD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<CommandDescription> CommandDescriptions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommandDescription>()
                .HasKey(cd => cd.Id);

            modelBuilder.Entity<CommandDescription>()
                .Property(cd => cd.Id)
                .UseIdentityColumn(); // Configura auto-incremento para Id

            modelBuilder.Entity<CommandDescription>()
                .HasOne<Device>()
                .WithMany()
                .HasForeignKey(cd => cd.DeviceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Se o Device for deletado, os CommandDescriptions também serão

            base.OnModelCreating(modelBuilder);
        }
    }
}
