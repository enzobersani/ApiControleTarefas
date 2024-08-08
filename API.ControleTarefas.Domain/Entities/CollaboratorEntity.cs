using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities.Base;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Entities
{
    public class CollaboratorEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string UserId { get; private set; }

        public static CollaboratorEntity New(InsertCollaboratorCommand request)
        {
            var collaborator = new CollaboratorEntity();
            request.Adapt(collaborator);
            return collaborator;
        }
    }
}
