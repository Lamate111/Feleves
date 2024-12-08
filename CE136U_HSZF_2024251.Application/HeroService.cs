using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface IHeroService
    {
        void Create(Hero entity);
        void Delete(int id);
        Hero Read(int id);
        void Update(Hero entity);


    }
    public class HeroService : IHeroService
    {
        readonly IHeroesDataProvider provider;

        public HeroService(IHeroesDataProvider provider)
        {
            this.provider = provider;
        }

        public void Create(Hero entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
            provider?.Delete(id);
        }

        public Hero Read(int id)
        {
           return provider.Read(id);
        }

        public void Update(Hero entity)
        {
            provider.Update(entity);
        }
    }
}
