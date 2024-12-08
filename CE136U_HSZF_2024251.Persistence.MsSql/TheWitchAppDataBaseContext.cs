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
        public DbSet<AffectedStatues> AffectedStatues { get; set; }

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
                .WithOne(k => k.Hero)
                .HasForeignKey<Attributes>(a => a.HeroID_attr)
                .OnDelete(DeleteBehavior.NoAction);
                

            // Configure Hero and Resource relationship
            modelBuilder.Entity<Hero>()
                .HasOne(h => h.Resources)
                .WithOne(k => k.Hero)
                .HasForeignKey<Resource>(r => r.HeroID_res)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Tasks and AffectedStatues relationship
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.AffectedStatus)
                .WithOne(k => k.Task)
                .HasForeignKey<AffectedStatues>(a => a.TaskID_affct)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Tasks and RequiredResources relationship
            modelBuilder.Entity<Tasks>()
                           .HasOne(t => t.RequiredResources)
                           .WithOne(r => r.RequiredByTask)
                           .HasForeignKey<Resource>(r => r.RequiredId_res)
                           .OnDelete(DeleteBehavior.NoAction);

            // One-to-One: Tasks -> Resource (Reward)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Reward)
                .WithOne(r => r.RewardForTask)
                .HasForeignKey<Resource>(r => r.RewardForTaskId_res)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Monster and Resource (Loot) relationship
            modelBuilder.Entity<Monster>()
                .HasOne(m => m.Loot)
                .WithOne(r => r.Monster)
                .HasForeignKey<Monster>(m => m.MonsterId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }


    }
}
