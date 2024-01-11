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
    public class EfRepository1<TEntity, Tkey> : IRepository1<TEntity, Tkey> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        public EfRepository1(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<TEntity> FindAll()
        {
            //var result =  _context.Set<TEntity>().ToList();
            //return result;
            return _context.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> FindAllAsync()
        {
            var result = _context.Set<TEntity>().AsQueryable();
            return result;
        }

        public async Task<TEntity> FindById(Tkey id)
        {
            var result = await _context.Set<TEntity>().FindAsync(id);
            return result;
        }
        public void Add(TEntity Object)
        {
            _context.Set<TEntity>().Add(Object);
        }
        public void Update(TEntity Object)
        {
            _context.Set<TEntity>().Update(Object);
        }
        

        public void Delete(TEntity Object)
        {
            _context.Set<TEntity>().Remove(Object);
        }
        
    }
}
