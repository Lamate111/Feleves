using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public class TasksDataProvider : DataProvider<Tasks>
    {
        public TasksDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        TheWitchAppDataBaseContext _context {  get; set; }

        

        public void Create(Tasks entity)
        {
            _context.Tasks.Add(entity);
            _context.SaveChanges();
            

        }

        public void Delete(int id)
        {
            var ToBeKilled = _context.Tasks.Find(id);
            _context.Tasks.Remove(ToBeKilled);
            _context.SaveChanges();
           
        }

        public Tasks Read(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void Update(Tasks entity)
        {
            _context.Tasks.Update(entity);
            _context.SaveChanges();
            
        }
    }
}
