using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities.Base;
using Mapster;

namespace API.ControleTarefas.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? CollaboratorId { get; set; }
        public TaskEntity() { }

        public static TaskEntity New(InsertTaskCommand request)
        {
            var tasks = new TaskEntity();
            request.Adapt(tasks);

            return tasks;
        }

        public void Update(UpdateTaskCommand request)
        {
            if(!string.IsNullOrEmpty(request.Name))
                Name = request.Name;

            if(request.Description != null)
                Description = request.Description;

            if(request.ProjectId != null)
                ProjectId = request.ProjectId;

            if(request.CollaboratorId != null)
                CollaboratorId = request.CollaboratorId;
        }
    }
}
