using Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilverNetLoginAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetByIdAsync(id);

            if(response == null) 
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        [Route("AddGroup")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] List<DtoBookRequest> requests)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new List<int>();
            foreach(var request in requests)
            {
                response.Add(await _service.CreateAsync(request));
            }
            
            return Ok();
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] DtoBookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.CreateAsync(request);

            return Created($"/{response}", new
            {
                Id = response
            });
        }

        

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, DtoBookRequest dtoBook)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.UpdateAsync(id,dtoBook);
            
            return Accepted(new
            {
                Id = response
            });
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Accepted(new
            {
                Id = id
            });
        }
    }
}
