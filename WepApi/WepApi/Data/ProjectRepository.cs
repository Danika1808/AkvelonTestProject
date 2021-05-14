using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WepApi.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<EntityState> CreateAsync(Project Project)
        {
            var result = _context.Projects.Add(Project).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<EntityState> DeleteAsync(Guid id)
        {
            var Project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            var result = _context.Projects.Remove(Project).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Project> DetailsAsync(Guid id)
        {
            return await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<EntityState> EditAsync(Project Project)
        {
            var result = _context.Update(Project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<Project>> Get()
        {
            var result = await _context.Projects.Include(x => x.Tasks).ToListAsync();
            return result;
        }

        public IEnumerable<Project> GetByField(string field, string value)
        {
            var param = Expression.Parameter(typeof(Project));

            var condition = Expression.Lambda<Func<Project, bool>>
                (Expression.Equal(
                    Expression.Property(param, field), 
                    Expression.Constant(value, typeof(string))), 
                param)
                .Compile();

            return _context.Projects.Where(condition);
        }

        public bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public IEnumerable<Project> OrderbyField()
        {
            return _context.Projects.OrderBy(x => x.CompletionDate);
        }
    }
}
