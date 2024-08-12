using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities.Base;
using Mapster;

namespace API.ControleTarefas.Domain.Entities
{
    public class CollaboratorEntity : BaseEntity
    {
        public string Name { get; private set; }
        public Guid UserId { get; private set; }

        public static CollaboratorEntity New(InsertCollaboratorCommand request)
        {
            var collaborator = new CollaboratorEntity();
            request.Adapt(collaborator);
            return collaborator;
        }
    }
}
