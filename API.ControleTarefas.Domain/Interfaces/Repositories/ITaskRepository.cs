using API.ControleTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task);
        void Update(TaskEntity task);
        Task<TaskEntity> GetById(Guid id);
        IQueryable<TaskEntity> Query();
    }
}
