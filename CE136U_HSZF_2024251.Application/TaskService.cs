using CE136U_HSZF_2024251.Model;
using CE136U_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE136U_HSZF_2024251.Application
{
    public interface ITasksService
    {
        void Create(Tasks task);
        void Delete(int id);
        Tasks Read(int id);
        void Update(Tasks entity);

    }
    public class TaskService : ITasksService
    {
        readonly ITasksProvider serviceProvider;

        public TaskService(ITasksProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Create(Tasks task)
        {
            serviceProvider.Create(task);
        }

        public void Delete(int id)
        {
            serviceProvider.Delete(id);
        }

        public Tasks Read(int id)
        {
            return serviceProvider.Read(id);
        }

        public void Update(Tasks entity)
        {
            serviceProvider.Update(entity);
        }
    }
}
