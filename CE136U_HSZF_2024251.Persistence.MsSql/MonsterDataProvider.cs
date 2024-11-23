using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class MonsterDataProvider : DataProvider<Monsters>
    {
        public MonsterDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        TheWitchAppDataBaseContext _context {  get; set; }


        public void Create(Monsters entity)
        {
            _context.Monsters.Add(entity);
            _context.SaveChanges();
           
        }

        public void Delete(int id)
        {
            var ToBeKilled = _context.Monsters.Find(id);
            _context.Remove(ToBeKilled);
            _context.SaveChanges();
            
        }

        public Monsters Read(int id)
        {
            return _context.Monsters.Find(id);
        }

        public void Update(Monsters entity)
        {
            _context.Monsters.Update(entity);
            _context.SaveChanges();
        }
    }
}
