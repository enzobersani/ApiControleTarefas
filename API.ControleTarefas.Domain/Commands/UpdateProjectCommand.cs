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
    public class UpdateProjectCommand : IRequest<UpdateProjectResponseModel>
    {
        /// <summary>
        /// Id do projeto. Guid Id
        /// </summary>
        /// 
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do projeto. Tamanho máximo de 250 caracteres
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }
    }
}
