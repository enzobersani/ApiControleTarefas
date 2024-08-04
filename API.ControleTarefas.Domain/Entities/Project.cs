using API.ControleTarefas.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Entities
{
    public class Project : EntityBase
    {
        public string Name { get; private set; }

        public Project() {}

        public Project(string name)
        {
            Name = name;
        }
    }
}
