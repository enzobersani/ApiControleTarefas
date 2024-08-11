using API.ControleTarefas.Domain.Entities.Base;

namespace API.ControleTarefas.Domain.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Name { get; private set; }

        public ProjectEntity() {}

        public ProjectEntity(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
