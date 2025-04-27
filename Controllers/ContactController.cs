using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;
using ContactApp.Interfaces;

namespace ContactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController: ControllerBase
    {
        private readonly IContactService _service;
        private readonly IFileService _fileService;

        public ContactController(IContactService service, IFileService fileService)
        {
            _service = service;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _service.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null) 
                return NotFound();

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contact contac)
        {
            var created = await _service.CreateAsync(contac);

            if (created == null) return NotFound();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost("upload/{id}")]
        public async Task<IActionResult> UploadPhoto(IFormFile file, int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null) return NotFound("Contact not found");

            var fileUrl = await _fileService.UploadFileAsync(file);

            contact.PhotoUrl = fileUrl;

            await _service.UpdateAsync(id, contact);

            return Ok(new {Url = fileUrl});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Contact dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}