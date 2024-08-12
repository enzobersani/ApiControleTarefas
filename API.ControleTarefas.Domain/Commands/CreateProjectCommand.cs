using API.ControleTarefas.Domain.Models.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.ControleTarefas.Domain.Commands
{
    public class CreateProjectCommand : IRequest<BaseResponseModel>
    {
        /// <summary>
        /// Nome do projeto. Tamanho máximo 250 caracteres
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }
    }
}
