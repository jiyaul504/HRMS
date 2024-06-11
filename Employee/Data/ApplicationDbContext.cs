using Employee.Entities;
using Employee.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee.Models.OrderStatus> Qry_WIP_Live_OrderStatus { get; set; }
        public virtual DbSet<Employee.Entities.Contact> Contacts { get; set; } = null!;
        public DbSet<Employee.Models.Employee> Employees { get; set; }
        public DbSet<Employee.Models.LeaveRequest> LeaveRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Add this line
            modelBuilder.Entity<Employee.Entities.Contact>(entity =>
            {
                entity.HasKey(e => e.ContactId);
                entity.ToTable("Contact");

                entity.Property(e => e.ContactId)
                    .HasColumnName("ContactID")
                    .HasComment("Primary key for Contact records.");

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(25);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(60);

                entity.Property(e => e.ZipCode).HasMaxLength(15);
            });

        }
    }
}
