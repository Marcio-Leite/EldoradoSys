using Domain;
using Infra.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Identity.Domain;

namespace BaseRepository.Persistence
{
    public class PersistenceDbContext : DbContext
    {
        public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options)
        {
            
        }

        //public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     //modelBuilder.ApplyConfiguration(new AccountMap());
        //
        //     base.OnModelCreating(modelBuilder);
        // }
    }
}
