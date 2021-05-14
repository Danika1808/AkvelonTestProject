using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WepApi.Data
{
    public interface IProjectRepository
    {
        public Task<EntityState> CreateAsync(Project taskEntity);
        public Task<IEnumerable<Project>> Get();
        public IEnumerable<Project> GetByField(string field, string value);
        public IEnumerable<Project> OrderbyField();
        public Task<Project> DetailsAsync(Guid id);
        public Task<EntityState> EditAsync(Project taskEntity);
        public Task<EntityState> DeleteAsync(Guid id);
        public bool ProjectExists(Guid id);
    }
}
