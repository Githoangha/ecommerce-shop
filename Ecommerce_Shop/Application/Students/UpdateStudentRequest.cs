using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Students
{
    public class UpdateStudentRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
