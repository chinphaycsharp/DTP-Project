using System;
using System.ComponentModel.DataAnnotations.Schema;
using EPS.Data.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EPS.Data
{
    public partial class EPSContext : DbContext, IDataProtectionKeyContext
    {
        public EPSContext()
        {
        }

        public EPSContext(DbContextOptions<EPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<IdentityClient> IdentityClients { get; set; }
        public virtual DbSet<IdentityRefreshToken> IdentityRefreshTokens { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePrivilege> RolePrivileges { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UnitAncestor> UnitAncestors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Footer> Footers{ get; set; }
        public virtual DbSet<New> News { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseLazyLoadingProxies().UseMySql(
                    "Server=127.0.0.1; Port=3306; Database=landingpage; Uid=root; Pwd=07081999",
              // "server=127.0.0.1:3306;uid=root;pwd=07081999;database=landingpage",
               options => options.EnableRetryOnFailure(
                   maxRetryCount: 5,
                   maxRetryDelay: System.TimeSpan.FromSeconds(30),
                   errorNumbersToAdd: null)
               );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<UserPrivilege>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PrivilegeId });
            });

            modelBuilder.Entity<RolePrivilege>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PrivilegeId });
            });
        }
    }
}
