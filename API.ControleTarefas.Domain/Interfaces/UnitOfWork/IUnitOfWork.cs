using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Models;
using API.ControleTarefas.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        IUserRepository UserRepository { get; }
        IProjectRepository ProjectRepository { get; }
        ITaskRepository TaskRepository { get; }
        ICollaboratorRepository CollaboratorRepository { get; }
        ITimeTrackerRepository TimeTrackerRepository { get; }
    }
}
