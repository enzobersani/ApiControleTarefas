using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Commands
{
    public class InsertTimeTrackersCommand : IRequest<BaseResponseModel>
    {
        /// <summary>
        /// Data inicial.
        /// </summary>
        /// 
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Data Final
        /// </summary>
        /// 
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Time Zone Local. Tamanho máximo de 250 caracteres.
        /// </summary>
        /// 
        [Required]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Id da Tarefa. Guid ID
        /// </summary>
        /// 
        [Required]
        public Guid TaskId { get; set; }

        /// <summary>
        /// Id do colaborador. GuidId
        /// </summary>
        /// 
        [Required]
        public Guid CollaboratorId { get; set; }

    }
}
