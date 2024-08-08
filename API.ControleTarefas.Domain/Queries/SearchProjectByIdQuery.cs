using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Queries
{
    public class SearchProjectByIdQuery : IRequest<ProjectResultModel>
    {
        public string Id { get; set; }
    }
}
