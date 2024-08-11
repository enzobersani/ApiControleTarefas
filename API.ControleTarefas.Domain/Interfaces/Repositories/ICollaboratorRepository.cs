using API.ControleTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Interfaces.Repositories
{
    public interface ICollaboratorRepository
    {
        Task AddAsync(CollaboratorEntity collaborator);
        Task<CollaboratorEntity> GetById(Guid id);
        Task<List<CollaboratorEntity>> GetByName(string name);
        Task<List<CollaboratorEntity>> GetAllCollaborators();
        IQueryable<CollaboratorEntity> Query();
    }
}
