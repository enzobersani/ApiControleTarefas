using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Commands
{
    public class InsertTimeTrackersCommand : IRequest<BaseResponseModel>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZoneId { get; set; }
        public Guid TaskId { get; set; }
        public Guid CollaboratorId { get; set; }

    }
}
