using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WepApi.Data;
using Domain;

namespace WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        private readonly IProjectRepository _projectRepository;
        public TaskController(ITaskRepository repository, IProjectRepository projectRepository)
        {
            _repository = repository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Get all items from Db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<TaskEntity>> Get()
        {
            var result = await _repository.Get();
            return result;
        }

        [HttpGet("GetByField")]
        public ActionResult<IEnumerable<Project>> GetByField([FromQuery] string field, [FromQuery] string value)
        {
            try
            {
                return Ok(_repository.GetByField(field, value));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Search by Name
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet("searchString")]
        public IEnumerable<TaskEntity> Search(string searchString)
        {
            return _repository.Search(searchString);
        }
        /// <summary>
        /// Get information about item by Id
        /// </summary>
        /// <param name="id">item Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var taskEntity = await _repository.DetailsAsync(id.Value);

            if (taskEntity == null)
            {
                return NotFound();
            }

            return taskEntity;
        }
        /// <summary>
        /// Create new item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stateNum"></param>
        /// <param name="priorityNum"></param>
        /// <param name="description"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EntityState>> Create(string name, int stateNum, int priorityNum, string description, Guid projectId)
        {
            var project = await _projectRepository.DetailsAsync(projectId);

            var state = (Project.States)stateNum;
            var taskEntity = new TaskEntity(name, state.ToString(), priorityNum, description, project);
            var result = await _repository.CreateAsync(taskEntity);
            if (result.Equals(EntityState.Added))
                return result;

            return NotFound();
        }
        /// <summary>
        /// Update item by Id
        /// </summary>
        /// <param name="id">item's Id</param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="stateNum"></param>
        /// <param name="priorityNum"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<EntityState>> Edit(Guid? id, string name, string description, int stateNum, int priorityNum)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var taskEntity = await _repository.DetailsAsync(id.Value);

            if (taskEntity == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(name))
                taskEntity.UpdateName(name);

            if (!string.IsNullOrEmpty(description))
            {
                taskEntity.UpdateDescription(description);
            }


            if (stateNum >= 0 && stateNum <= 2)
                taskEntity.UpdateState(stateNum);
            else
                return BadRequest();

            if (priorityNum >= 0 && priorityNum <= 3)
                taskEntity.UpdatePriority(priorityNum);
            else
                return BadRequest();

            var result = await _repository.Edit(taskEntity);

            if (result.Equals(EntityState.Modified))
                return result;

            return NotFound();
        }
        /// <summary>
        /// Delete item by Id   
        /// </summary>
        /// <param name="id">item's id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EntityState>> Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (!_repository.TaskEntityExists(id.Value))
            {
                return NotFound();
            }
            var result = await _repository.DeleteAsync(id.Value);

            if (result.Equals(EntityState.Deleted))
                return result;

            return NotFound();
        }
    }
}
