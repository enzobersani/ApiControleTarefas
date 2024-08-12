using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.ControleTarefas.Domain.Commands
{
    public class DeleteProjectCommand : IRequest<DeleteProjectResponseModel>
    {
        /// <summary>
        /// Id do projeto. Guid Id
        /// </summary>
        /// 
        [Required]
        public Guid Id { get; set; }

        public DeleteProjectCommand(Guid id)
        {
            Id = id;
        }
    }
}
