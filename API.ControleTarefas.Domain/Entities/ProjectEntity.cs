using API.ControleTarefas.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
