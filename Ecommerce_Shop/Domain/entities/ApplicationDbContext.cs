using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.entities
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Major> Majors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

    }
}
