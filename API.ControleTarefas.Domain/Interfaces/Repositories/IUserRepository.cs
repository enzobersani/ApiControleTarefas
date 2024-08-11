using API.ControleTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task<UserEntity> GetByUserName(string userName);
        Task<UserEntity> GetById(Guid id);
    }
}
