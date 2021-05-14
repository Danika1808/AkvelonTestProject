using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepApi.Data
{
    public interface ITaskRepository
    {
        public Task<EntityState> CreateAsync(TaskEntity taskEntity);
        public Task<IEnumerable<TaskEntity>> Get();
        public IEnumerable<TaskEntity> Search(string projectName);
        public IEnumerable<TaskEntity> GetByField(string field, string value);
        public Task<TaskEntity> DetailsAsync(Guid id);
        public Task<EntityState> Edit(TaskEntity taskEntity);
        public Task<EntityState> DeleteAsync(Guid id);
        public bool TaskEntityExists(Guid id);

    }
}
