using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Models.Response
{
    public class SearchHoursResponseModel
    {
        public string HoursToday { get; set; }
        public string HoursMonth { get; set; }
    }
}
