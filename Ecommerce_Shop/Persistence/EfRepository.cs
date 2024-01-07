using Domain.Abstractrions;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class EfRepository : IRepository
    {
        private readonly ApplicationDbContext _context;
        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public List<Student> FindAll()
        {
            return _context.Students.ToList();
        }
        public async Task<Student> FindById(Guid Id)
        {
            return await _context.Students.FindAsync(Id);
        }
        public async  Task<List<Student>> FindAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
        }
        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
       

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }
    }
}
