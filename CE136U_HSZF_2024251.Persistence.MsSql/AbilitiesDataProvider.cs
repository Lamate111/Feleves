using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class AbilitiesDataProvider : DataProvider<Abilities>
    {
        public AbilitiesDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        TheWitchAppDataBaseContext _context {  get; set; }

        public void Create(Abilities entity)
        {
            _context.Abilities.Add(entity);
            _context.SaveChanges();
            
        }

        public void Delete(int id)
        {
         var ToBeDeleted = _context.Abilities.Find(id);
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();

        }

        public Abilities Read(int id)
        {
            return _context.Abilities.Find(id);
            
        }

        public void Update(Abilities entity)
        {
            _context.Abilities.Update(entity);
            _context.SaveChanges();
        }
    }
}
