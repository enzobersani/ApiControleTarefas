using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.ControleTarefas.Domain.Commands
{
    public class InsertTaskCommand : IRequest<BaseResponseModel>
    {
        /// <summary>
        /// Nome da tarefa. Tamano máximo de 250 caracteres.
        /// </summary>
        /// 
        [Required]
        public string? Name { get; set;}
        /// <summary>
        /// Descrição da tarefa.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Id do projeto. Guid Id
        /// </summary>
        /// 
        [Required]
        public string? ProjectId { get; set; }
        /// <summary>
        /// Id do colaborado. Guid Id
        /// </summary>
        public string? CollaboratorId { get; set; }
    }
}
