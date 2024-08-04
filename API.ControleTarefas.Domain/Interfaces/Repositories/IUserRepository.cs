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
        System.Threading.Tasks.Task AddAsync(User user);
        Task<User> GetByUserName(string userName);
    }
}
