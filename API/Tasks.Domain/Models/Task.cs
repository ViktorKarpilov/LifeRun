using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Domain.Models
{
    class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
