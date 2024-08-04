using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Commands
{
    public class LoginCommand : IRequest<RegisterUserResponseModel>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
