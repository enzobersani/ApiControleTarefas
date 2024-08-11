using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Models.Response
{
    public class UpdateProjectResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsInactive { get; set; }
    }
}
