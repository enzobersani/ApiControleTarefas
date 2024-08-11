using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain
{
    public static class QueryExtensions
    {
        public static string SqlBuilder(this List<string> queryParts)
        {
            return string.Join(" ", queryParts);
        }
    }
}
