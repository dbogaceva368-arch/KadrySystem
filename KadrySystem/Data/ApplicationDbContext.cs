using KadrySystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KadrySystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Сотрудник> Сотрудники { get; set; }
        public DbSet<Должность> Должности { get; set; }
        public DbSet<Подразделение> Подразделения { get; set; }
        public DbSet<Приказ> Приказы { get; set; }
        public DbSet<ШтатноеРасписание> ШтатноеРасписание { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Сотрудник>()
                .HasOne(s => s.Должность)
                .WithMany()
                .HasForeignKey(s => s.Код_должности)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Сотрудник>()
                .HasOne(s => s.Подразделение)
                .WithMany()
                .HasForeignKey(s => s.Код_подразделения)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Приказ>()
                .HasOne(p => p.Сотрудник)
                .WithMany()
                .HasForeignKey(p => p.Код_сотрудника)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ШтатноеРасписание>()
                .HasOne(s => s.Должность)
                .WithMany()
                .HasForeignKey(s => s.Код_должности)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
