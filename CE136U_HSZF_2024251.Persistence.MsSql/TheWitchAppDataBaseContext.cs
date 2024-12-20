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
            modelBuilder.Entity<Tasks>()
                .HasOne(h => h.Required_resources)
                .WithOne(e => e.RequiredResourcesTask)
                .HasForeignKey<Resource>("RequiredResourcesId");

            modelBuilder.Entity<Tasks>()
                .HasOne(h => h.Reward)
                .WithOne(e => e.RewardTask)
                .HasForeignKey<Resource>("RewardID");

            modelBuilder.Entity<Hero>()
                .HasOne(e => e.Attributes)
                .WithOne(e => e.Hero)
                .HasForeignKey<Attributes>("HeroID");

            modelBuilder.Entity<Hero>()
                .HasOne(e => e.Resources)
                .WithOne(a => a.Hero)
                .HasForeignKey<Resource>("HeroID_res");

            modelBuilder.Entity<Monster>()
                .HasOne(e => e.Loot)
                .WithOne(a => a.Monster)
                .HasForeignKey<Resource>("LootID");

            base.OnModelCreating(modelBuilder);
        }
    }
}
