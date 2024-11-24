using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface ICharacterDataProvider
    {
        void Create(Character entity);
        void Delete(int id);
        Character Read(int id);
        void Update(Character entity);


    }
    public class CharacterDataProvider :ICharacterDataProvider
    {
        private readonly TheWitchAppDataBaseContext _context;

        public CharacterDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        public void Create(Character entity)
        {
            _context.Characters.Add(entity);
            _context.SaveChanges();
           
            
        }

        public void Delete(int id)
        {
            var ToBeDeleted = _context.Characters.Find(id);
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
            
        }

        public Character Read(int id)
        {
            return _context.Characters.Find(id);
        }

        public void Update(Character entity)
        {
            _context.Characters.Update(entity);
            _context.SaveChanges();
        }
    }
}
