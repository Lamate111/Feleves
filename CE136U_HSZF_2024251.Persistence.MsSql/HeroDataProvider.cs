﻿using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface IHeroesDataProvider
    {
        void Create(Hero entity);
        void Delete(int id);
        Hero Read(int id);
        void Update(Hero entity);

        IEnumerable<Hero> GetHeroes();

        public bool IsAlreadyIn(string name);

        


    }
    public class HeroDataProvider :IHeroesDataProvider
    {
        private readonly TheWitchAppDataBaseContext _context;

        public HeroDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        public void Create(Hero entity)
        {
            Console.WriteLine($"Creating Hero: {entity.Name}");

            _context.Heroes.Add(entity);

            try
            {
                _context.SaveChanges();
                Console.WriteLine("Hero saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving Hero: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }


        public void Delete(int id)
        {
            var ToBeDeleted = _context.Heroes.Find(id);
            _context.Heroes.Remove(ToBeDeleted);
            _context.SaveChanges();
            
        }

        public IEnumerable<Hero> GetHeroes()
        {
            return  _context.Heroes;
        }

        public bool IsAlreadyIn(string name)
        {
            return _context.Heroes.Any(x => x.Name == name);
        }

        public Hero Read(int id)
        {
            return _context.Heroes.Find(id);
        }

        public void Update(Hero entity)
        {
            _context.Heroes.Update(entity);
            _context.SaveChanges();
        }



    }
}
