using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Buissines.Contracts;
using TaskManager.Data.Models;

namespace TaskManager.Presentation.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        public ProjectController(IProjectService service)
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
        [Route("GetNameList")]
        public async Task<IActionResult> GetNameList()
        {
            return Ok(await _service.GetNameList());
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Project entity)
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
        public async Task<IActionResult> Delete([FromBody] Project entity)
        {
            return Ok(await _service.Add(entity));
        }
    }
}