using CE136U_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class TheWitchAppDataBaseContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Monster> Monster { get; set; }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        public TheWitchAppDataBaseContext()
        {
           Database.EnsureDeleted();
           Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Witcher_team_db;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Hero and Attributes relationship
            modelBuilder.Entity<Hero>()
                .HasOne(h => h.Attributes)
                .WithOne(a => a.Hero)
                .HasForeignKey<Attributes>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Hero and Resource relationship
            modelBuilder.Entity<Hero>()
                .HasOne(h => h.Resources)
                .WithOne(r => r.Hero)
                .HasForeignKey<Resource>(r => r.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Tasks and AffectedStatues relationship
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.AffectedStatus)
                .WithOne(a => a.Task)
                .HasForeignKey<AffectedStatues>(a => a.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Tasks and RequiredResources relationship
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.RequiredResources)
                .WithOne(r => r.RequiredByTask)
                .HasForeignKey<Tasks>(t => t.RequiredResourcesId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Tasks and Reward relationship
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Reward)
                .WithOne(r => r.RewardForTask)
                .HasForeignKey<Tasks>(t => t.RewardId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Monster and Resource (Loot) relationship
            modelBuilder.Entity<Monster>()
                .HasOne(m => m.Loot)
                .WithOne(r => r.Monster)
                .HasForeignKey<Monster>(m => m.MonsterId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }


    }
}
