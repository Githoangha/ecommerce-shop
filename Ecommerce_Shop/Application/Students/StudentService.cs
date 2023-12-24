using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Students
{
    public interface IStudentService
    {
        List<StudentViewModel> GetStudents();
        Task<List<StudentViewModel>> GetStudentsAsync();
    }
    public class StudentService:IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<StudentViewModel>> GetStudentsAsync()
        {
            var students = _context.Students;
            var result = await students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            }).ToListAsync();

            return result;
        }

        public  List<StudentViewModel> GetStudents()
        {
            var students = _context.Students;
            var result =  students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            }).ToList();

            return result;
        }
    }
}
