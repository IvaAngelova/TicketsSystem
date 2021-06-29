using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using TicketSystem.Models;

namespace TicketSystem.Data
{
    public class TicketSystemContext : IdentityDbContext
    {
        public TicketSystemContext(DbContextOptions<TicketSystemContext> options)
        : base(options)
        {

        }
              

        public DbSet<Department> Department { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<TicketCategory> TicketCategory { get; set; }

        public DbSet<TicketPriority> TicketPriority { get; set; }

        public DbSet<TicketStatus> TicketStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(d => d.ModifDate17118162)
                       .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasMany(t => t.CreatorTickets)
                      .WithOne(t => t.CreatorTicket)
                      .HasForeignKey(t => t.CreatorID)
                      .IsRequired(true)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasMany(t => t.АcceptedАТickets)
                      .WithOne(t => t.AcceptedATicket)
                      .HasForeignKey(t => t.AcceptedATicketID)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.ModifDate17118162)
                       .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(t => t.ModifDate17118162)
                       .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<TicketCategory>(entity =>
            {
                entity.Property(tc => tc.ModifDate17118162)
                       .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<TicketPriority>(entity =>
            {
                entity.Property(tp => tp.ModifDate17118162)
                       .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<TicketStatus>(entity =>
            {
                entity.Property(ts => ts.ModifDate17118162)
                       .HasDefaultValueSql("getdate()");
            });

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var EditedEntities = ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                if (E.Properties.Any(m=>m.Metadata.Name.Equals(nameof(BaseEntity.ModifDate17118162))))
                {
                    E.Property(nameof(BaseEntity.ModifDate17118162)).CurrentValue = DateTime.Now;
                }
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
