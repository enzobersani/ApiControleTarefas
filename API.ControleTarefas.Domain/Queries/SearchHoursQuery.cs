using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Queries
{
    public class SearchHoursQuery : IRequest<SearchHoursResponseModel>
    {
        public Guid CollaboratorId { get; set; }
        public DateTime Date { get; set; }
    }
}
