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
    public class DeleteTaskCommand : IRequest<DeleteTaskResponseModel>
    {
        /// <summary>
        /// Id da tarefa. Guid Id
        /// </summary>
        /// 
        [Required]
        public Guid Id { get; set; }

        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }
    }
}
