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
    public class UpdateTaskCommand : IRequest<UpdateTaskResponseModel>
    {

        /// <summary>
        /// Id da tarefa. Guid Id
        /// </summary>
        /// 
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da tarefa. Tamano máximo de 250 caracteres.
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Descrição da tarefa.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Id do projeto. Guid Id
        /// </summary>
        /// 
        [Required]
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// Id do colaborado. Guid Id
        /// </summary>
        public Guid? CollaboratorId { get; set; }
    }
}
