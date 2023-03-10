using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManager.Buissines.Contracts;
using TaskManager.Buissines.Models;
using TaskManager.Data.Models;

namespace TaskManager.Presentation.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
	{
        private readonly IProjectTaskService _service;
        public TaskController(IProjectTaskService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.GetBykey(id));
        }

        [HttpPost]
        [Route("GetTable")]
        public async Task<IActionResult> GetTable([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] ProjectTasksFilterOptions? options = null)
        {
            return Ok(await _service.GetTable(options));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProjectTaskUpdateRequestModel entity)
        {
            return Ok(await _service.Update(entity));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProjectTaskAddRequestModel entity)
        {

            return Ok(await _service.Add(entity));
        }
      
    }
}

