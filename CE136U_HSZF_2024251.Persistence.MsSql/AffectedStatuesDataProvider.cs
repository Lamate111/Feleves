using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Persistence.MsSql
{
    public interface IAffectedStatuesDataProvider
    {
        void Create(AffectedStatues entity);
        void Delete(int id);
        AffectedStatues Read(int id);
        void Update(AffectedStatues entity);

    }

    public class AffectedStatuesDataProvider : IAffectedStatuesDataProvider
    {
        public AffectedStatuesDataProvider (TheWitchAppDataBaseContext theWitchAppDataBaseContext)
        {
            _context = theWitchAppDataBaseContext;
            
        }
        readonly TheWitchAppDataBaseContext _context;

        public void Create(AffectedStatues entity)
        {
            _context.AffectedStatues.Add(entity);
            _context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            var ToBeDeleted = _context.AffectedStatues.Find(id);
            _context.AffectedStatues.Remove(ToBeDeleted);
            _context.SaveChanges();

        }

        public AffectedStatues Read(int id)
        {
            return _context.AffectedStatues.Find(id);
        }

        public void Update(AffectedStatues entity)
        {
            _context.AffectedStatues.Update(entity);
            _context.SaveChanges();
        }
    }
}
