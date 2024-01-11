using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractrions;
using Domain.entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Students
{
    public interface IStudentService
    {
        List<StudentViewModel> GetStudents();
        Task<StudentData> GetStudentsAsync(Page model);
        Task<StudentViewModel> GetStudentsByIdAsync(Guid id);
        Task AddStudent(CreateStudentRequest request);
        Task UpdateStudent(UpdateStudentRequest request);
        Task DeleteStudent(Guid Id);
        
    }
    #region bỏ
    public class StudentService //: IStudentService
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
            }).ToListAsync();//IO bound

            return result;
        }

        public List<StudentViewModel> GetStudents()
        {
            var students = _context.Students;
            var result = students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            }).ToList();//CPU bound

            return result;
        }

        public async Task AddStudent(CreateStudentRequest request)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Age = request.Age
            };
            _context.Add(student);
            await _context.SaveChangesAsync();
        }

        public Task UpdateStudent(UpdateStudentRequest request)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    public class StudentService1:IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository1<Student,Guid> _studentRepository;
        private readonly IRepository1<Major, Guid> _majorRepository;
        private readonly IUnitOfWork _UnitOfWork;
        public StudentService1(IRepository1<Student, Guid> studentRepository,IRepository1<Major,Guid>majorRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _UnitOfWork = unitOfWork;
            _majorRepository = majorRepository;
        }

        public async Task AddStudent(CreateStudentRequest request)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Age = request.Age
            };
            _studentRepository.Add(student);
            await _UnitOfWork.SaveChangeAsync();
        }

        public async Task<StudentViewModel> GetStudentsByIdAsync(Guid id)
        {
            var student = await _studentRepository.FindById(id);
            if (student == null)
            {
                throw new Exception("student not found");
            }
            return new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
            };
        }

        public List<StudentViewModel> GetStudents()
        {
            var students = _studentRepository.FindAll();
            var result = students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            }).ToList();//CPU bound

            return result;
        }

        public async Task<StudentData> GetStudentsAsync(Page model)
        {
            var data = new StudentData();
            var students = _studentRepository.FindAllAsync();
            data.TotalStudent = students.Count();
            students = students.OrderBy(s => s.Name).Skip(model.SkipNumber).Take(model.PageSize);
            var result = await students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age
            }).ToListAsync();
            data.Students = result;
            return data;
        }

        public async Task UpdateStudent(UpdateStudentRequest request)
        {
            //var student1 = (await _Repository.FindAllAsync()).FirstOrDefault(s => s.Id == request.Id);
            var student = await _studentRepository.FindById(request.Id);
            if (student == null)
            {
                throw new Exception("student not found");
            }

            student.Name = request.Name;
            student.Age = request.Age;
            _studentRepository.Update(student);
            await _UnitOfWork.SaveChangeAsync();
        }

        public async Task DeleteStudent(Guid Id)
        {
            var student = await _studentRepository.FindById(Id);
            if (student == null)
            {
                throw new Exception("student not found");
            }
            _studentRepository.Delete(student);
            await _UnitOfWork.SaveChangeAsync();
        }
    }
}
