using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface IAffectedStatuesService
    {
       
            void Create(AffectedStatues entity);
            void Delete(int id);
            AffectedStatues Read(int id);
            void Update(AffectedStatues entity);
            public IEnumerable<AffectedStatues> GetAffectedStat();

    }
    public class AffectedStatuesService : IAffectedStatuesService
    {
        private readonly IAffectedStatuesDataProvider _provider;

        public AffectedStatuesService(IAffectedStatuesDataProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public void Create(AffectedStatues entity)
        {
            _provider.Create(entity);
        }

        public void Delete(int id)
        {
            _provider.Delete(id);
        }

        public IEnumerable<AffectedStatues> GetAffectedStat()
        {
            return _provider.GetAffStat();
        }

        public AffectedStatues Read(int id)
        {
            return _provider.Read(id);
        }

        public void Update(AffectedStatues entity)
        {
            _provider.Update(entity);
        }
    }

}
