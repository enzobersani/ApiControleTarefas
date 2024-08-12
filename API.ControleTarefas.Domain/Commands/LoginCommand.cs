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
    public class LoginCommand : IRequest<TokenResponseModel>
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        /// 
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// 
        [Required]
        public string Password { get; set; }
    }
}
