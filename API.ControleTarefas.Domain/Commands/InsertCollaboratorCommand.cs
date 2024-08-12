using API.ControleTarefas.Domain.Models.Response;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Commands
{
    public class InsertCollaboratorCommand : IRequest<BaseResponseModel>
    {
        /// <summary>
        /// Nome do colaborador. Tamanho máximo de 250 carateres.
        /// </summary>
        /// 
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Código do usuário vinculado. Guid Id
        /// </summary>
        /// 
        [Required]
        public Guid UserId { get; set; }
    }
}
