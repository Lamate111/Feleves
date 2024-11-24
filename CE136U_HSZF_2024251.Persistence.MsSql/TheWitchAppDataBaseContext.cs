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
        public DbSet<Character> Characters { get; set; }
        public DbSet<Abilities> Abilities { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Monsters> Monsters { get; set; }

        public DbSet<Resources> Resources { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        public TheWitchAppDataBaseContext()
        {
           Database.EnsureDeleted();
           Database.EnsureCreated();
        }

        public TheWitchAppDataBaseContext(DbContextOptions<TheWitchAppDataBaseContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Witcher_team_db;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasMany(b => b.abilities)
                .WithOne()
                .HasForeignKey(b => b.char_id);

            modelBuilder.Entity<Character>()
                .HasMany(k => k.attributes)
                .WithOne()
                .HasForeignKey(k => k.char_id);

            modelBuilder.Entity<Character>()
                .HasMany(b => b.resources)
                .WithOne()
                .HasForeignKey(b => b.char_id);

            modelBuilder.Entity<Character>()
                .HasMany(b => b.tasks)
                .WithOne()
                .HasForeignKey(b => b.char_id);

            modelBuilder.Entity<Monsters>()
                .HasMany(b => b.loot)
                .WithOne()
                .HasForeignKey(b => b.monster_id);

            base.OnModelCreating(modelBuilder);
        }


    }
}
