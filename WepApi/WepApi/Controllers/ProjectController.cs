using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WepApi.Data;
using Domain;

namespace WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repository;

        public ProjectController(IProjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all items from Db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await _repository.Get();
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
        [HttpGet("OrderbyDate")]
        public ActionResult<IEnumerable<Project>> OrderbyDate()
        {
            return Ok(_repository.OrderbyField());
        }

        /// <summary>
        /// Get information about item by Id
        /// </summary>
        /// <param name="id">item Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var project = await _repository.DetailsAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }
        /// <summary>
        /// Create new item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stateNum"></param>
        /// <param name="priorityNum"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EntityState>> Create(string name, int stateNum, int priorityNum, string startDate)
        {
            if (!DateTime.TryParse(startDate, out DateTime date))
            {
                return BadRequest();
            }
            if (!(stateNum >= 0 && stateNum <= 2))
            {
                return BadRequest();
            }
            if (!(priorityNum >= 0 && priorityNum <= 3))
            {
                return BadRequest();
            }
            var state = (Project.States)stateNum;
            var project = new Project(name, state.ToString(), priorityNum, date);
            var result = await _repository.CreateAsync(project);
            if (result.Equals(EntityState.Added))
                return result;

            return NotFound();
        }
        /// <summary>
        /// Update item by Id
        /// </summary>
        /// <param name="id">item's Id</param>
        /// <param name="name"></param>
        /// <param name="stateNum"></param>
        /// <param name="priorityNum"></param>
        /// <param name="completionDate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<EntityState>> Edit(Guid? id, string name, int stateNum, int priorityNum, string completionDate)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var project = await _repository.DetailsAsync(id.Value);

            if (project == null)
                return NotFound();

            if (!string.IsNullOrEmpty(name))
                project.UpdateName(name);

            if (stateNum >= 0 && stateNum <= 2)
                project.UpdateState(stateNum);
            else
                return BadRequest();

            if (priorityNum >= 0 && priorityNum <= 3)
                project.UpdatePriority(priorityNum);
            else
                return BadRequest();

            if (!string.IsNullOrEmpty(completionDate))
            {
                if (!DateTime.TryParse(completionDate, out DateTime date))
                    return BadRequest();

                project.UpdateCompletionDate(date);
            } 

            var result = await _repository.EditAsync(project);
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
            if (id.HasValue)
            {
                return NotFound();
            }

            if (!_repository.ProjectExists(id.Value))
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
