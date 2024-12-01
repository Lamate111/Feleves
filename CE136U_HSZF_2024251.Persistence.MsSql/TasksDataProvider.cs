using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface ITasksProvider
    {
        void Create (Tasks task);
        void Delete (int id);
        Tasks Read (int id);
        void Update (Tasks entity);

    }
    public class TasksDataProvider : ITasksProvider
    {
        public TasksDataProvider(TheWitchAppDataBaseContext context)
        {
            _context = context;
        }

        readonly TheWitchAppDataBaseContext _context;


        public void Create(Tasks task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var ToBeKilled = _context.Tasks.Find(id);
            _context.Tasks.Remove(ToBeKilled);
            _context.SaveChanges();
           
        }


        public void Update(Tasks entity)
        {
            _context.Tasks.Update(entity);
            _context.SaveChanges();
        }

        public Tasks Read(int id)
        {
            return _context.Tasks.Find(id);
        }
    }
}
