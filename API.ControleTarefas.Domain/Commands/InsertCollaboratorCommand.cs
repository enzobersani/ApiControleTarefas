using API.ControleTarefas.Domain.Models.Response;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Commands
{
    public class InsertCollaboratorCommand : IRequest<BaseResponseModel>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
