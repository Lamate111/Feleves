using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface ICharacterService
    {
        void Create(Character entity);
        void Delete(int id);
        Character Read(int id);
        void Update(Character entity);


    }
    public class CharacterService : ICharacterService
    {
        readonly CharacterDataProvider provider;

        public CharacterService(CharacterDataProvider provider)
        {
            this.provider = provider;
        }

        public void Create(Character entity)
        {
            provider.Create(entity);
        }

        public void Delete(int id)
        {
            provider?.Delete(id);
        }

        public Character Read(int id)
        {
           return provider.Read(id);
        }

        public void Update(Character entity)
        {
            provider.Update(entity);
        }
    }
}
