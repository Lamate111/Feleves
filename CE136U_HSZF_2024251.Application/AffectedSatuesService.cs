using CE136U_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface IAffectedSatuesService
    {
       
            void Create(AffectedStatues entity);
            void Delete(int id);
            AffectedStatues Read(int id);
            void Update(AffectedStatues entity);

    }
    public class AffectedSatuesService : IAffectedSatuesService
    {
        readonly IAffectedSatuesService provider;

        public AffectedSatuesService(IAffectedSatuesService provider)
        {
            this.provider = provider;
        }

        public void Create(AffectedStatues entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
           provider.Delete(id);
        }

        public AffectedStatues Read(int id)
        {
            return provider.Read(id);
        }

        public void Update(AffectedStatues entity)
        {
            provider.Update(entity);
        }
    }
}
