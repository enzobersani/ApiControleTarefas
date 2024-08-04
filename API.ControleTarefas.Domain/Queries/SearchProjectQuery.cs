using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Queries
{
    public class SearchProjectQuery : IRequest<SearchProjectResponseModel>
    {
        public string Name { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
        //public string? Expand { get; set; }
    }
}
