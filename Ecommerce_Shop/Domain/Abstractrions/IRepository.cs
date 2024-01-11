using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractrions
{
    public interface IRepository
    {
        Task<List<Student>> FindAllAsync();
        Task<Student> FindById(Guid Id);
        List<Student> FindAll();
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
    }

    public interface IRepository1<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> FindAllAsync();
        List<TEntity> FindAll();
        Task<TEntity> FindById(TKey id);
        void Add(TEntity Object);
        void Update(TEntity Object);
        void Delete(TEntity Object);
        
    }

}
