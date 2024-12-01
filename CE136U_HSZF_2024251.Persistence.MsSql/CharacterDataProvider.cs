using CE136U_HSZF_2024251.Model;
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


    }
    public class CharacterDataProvider :IHeroesDataProvider
    {
        private readonly TheWitchAppDataBaseContext _context;

        public CharacterDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        public void Create(Hero entity)
        {
            _context.Heroes.Add(entity);
            _context.SaveChanges();
           
            
        }

        public void Delete(int id)
        {
            var ToBeDeleted = _context.Heroes.Find(id);
            _context.Heroes.Remove(ToBeDeleted);
            _context.SaveChanges();
            
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
