using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities.Base;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public User() { }

        public static User New(RegisterUserCommand request, string hashPassword) 
        {
            var users = new User();
            request.Adapt(users);
            users.Password = hashPassword;

            return users;
        }
    }
}
