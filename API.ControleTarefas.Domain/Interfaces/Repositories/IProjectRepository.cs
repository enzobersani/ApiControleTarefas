using API.ControleTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace API.ControleTarefas.Domain.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
        Task<List<Project>> GetByName(string name);
        Task<List<Project>> GetAllProjects();
    }
}
