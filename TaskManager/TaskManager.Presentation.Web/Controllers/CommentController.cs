using System;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Buissines.Contracts;
using TaskManager.Data.Models;
using TaskManager.Buissines.Models;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace TaskManager.Presentation.Web.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ITaskCommentService _service;
        public CommentController(ITaskCommentService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.GetBykey(id));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _service.GetAll());
           
        }
        [HttpGet]
        [Route("file/{commentId}")]
        public async Task<IActionResult> GetFile(Guid commentId)
        {

            Response.Headers.Add("content-disposition", $"attachment;");
            (byte[] content, string type, string fileName) data  = await _service.GetFile(commentId);

            return File(data.content, data.type, data.fileName);
        }
        [HttpGet]
        [Route("GetByTaskId/{taskId}")]
        public async Task<IActionResult> GetByTaskId(Guid taskId)
        {
            return Ok(await _service.GetByTaskId(taskId));
        }
       
      [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskCommentReuestModel entity)
        {
            return Ok(await _service.Update(entity));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _service.Delete(id));
             
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] TaskCommentReuestModel entity)
        {
            return Ok(await _service.Add(entity));
        }

    }
}

