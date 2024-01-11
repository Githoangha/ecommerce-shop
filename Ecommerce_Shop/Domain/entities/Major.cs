using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.entities
{
    [Table("Majors")]
    public class Major
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Name { get; set; }

    }
}
