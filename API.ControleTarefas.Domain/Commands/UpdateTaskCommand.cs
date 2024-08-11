using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Commands
{
    public class UpdateTaskCommand : IRequest<UpdateTaskResponseModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? CollaboratorId { get; set; }
    }
}
