using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WepApi.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EntityState> CreateAsync(TaskEntity taskEntity)
        {
            var result = _context.Tasks.Add(taskEntity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<EntityState> DeleteAsync(Guid id)
        {
            var taskEntity = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            var result = _context.Tasks.Remove(taskEntity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<TaskEntity> DetailsAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<EntityState> Edit(TaskEntity taskEntity)
        {
            var result = _context.Update(taskEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<TaskEntity>> Get()
        {
            return await _context.Tasks.ToListAsync();
        }

        public IEnumerable<TaskEntity> GetByField(string field, string value)
        {
            var param = Expression.Parameter(typeof(TaskEntity));

            var condition = Expression.Lambda<Func<TaskEntity, bool>>
                (Expression.Equal(
                    Expression.Property(param, field),
                    Expression.Constant(value, typeof(string))),
                param)
                .Compile();

            return _context.Tasks.Where(condition);
        }

        public IEnumerable<TaskEntity> Search(string projectName)
        {
            return _context.Tasks.Where(x => x.Project.Name.Equals(projectName));
        }

        public bool TaskEntityExists(Guid id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
