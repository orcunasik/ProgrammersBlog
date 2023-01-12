using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        public string Name { get; set;}
        public string Description { get; set;}
        public bool IsActive { get; set; }
        public string Note { get; set; }
    }
}
