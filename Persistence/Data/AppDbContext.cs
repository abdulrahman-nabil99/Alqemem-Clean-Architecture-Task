using CleanArchTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public virtual DbSet<Employee> Employees { get; set; } 
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<EmployeesView> EmployeesView { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeesView>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView("vw_Employees");
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);         
                entity.Property(e => e.FullNameAr)
                      .HasComputedColumnSql("[FNameAr] + ' ' + [LNameAr]");
                entity.Property(e => e.FullNameEn)
                      .HasComputedColumnSql("[FNameEn] + ' ' + [LNameEn]");
                entity.HasIndex(e => e.Email)
                      .IsUnique();
                entity.HasIndex(e => e.Mobile)
                      .IsUnique();

            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.HasMany(d=>d.Employees)
                      .WithOne(e=>e.Department)
                      .HasForeignKey(e=>e.DepartmentId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.HasMany(m => m.Employees)
                      .WithOne(e => e.MaritalStatus)
                      .HasForeignKey(e => e.MaritalStatusId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
