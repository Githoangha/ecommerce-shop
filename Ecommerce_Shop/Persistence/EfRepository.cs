using Domain.Abstractrions;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class EfRepository : IRepository
    {
        private readonly ApplicationDbContext _context;
        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
        }
        public List<Student> FindAll()
        {
            return _context.Students.ToList();
        }
    }
}
