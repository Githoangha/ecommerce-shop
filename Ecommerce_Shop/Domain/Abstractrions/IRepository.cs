using Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractrions
{
    public interface IRepository
    {
        Task<List<Student>> FindAllAsync();
        List<Student> FindAll();
        void Add(Student student);
        void Update(Student student);
        Task<Student> FindById(Guid Id);
    }
}
