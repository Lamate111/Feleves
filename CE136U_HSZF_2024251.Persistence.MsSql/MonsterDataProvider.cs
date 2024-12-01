using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface IMonsterDataProvider
    {
        void Create(Monster entity);
        void Delete(int id);
        Monster Read(int id);
        void Update(Monster entity);


    }
    public class MonsterDataProvider : IMonsterDataProvider
    {
        public MonsterDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        TheWitchAppDataBaseContext _context {  get; set; }


        public void Create(Monster entity)
        {
            _context.Monster.Add(entity);
            _context.SaveChanges();
           
        }

        public void Delete(int id)
        {
            var ToBeKilled = _context.Monster.Find(id);
            _context.Monster.Remove(ToBeKilled);
            _context.SaveChanges();
            
        }

        public Monster Read(int id)
        {
            return _context.Monster.Find(id);
        }

        public void Update(Monster entity)
        {
            _context.Monster.Update(entity);
            _context.SaveChanges();
        }
    }
}
