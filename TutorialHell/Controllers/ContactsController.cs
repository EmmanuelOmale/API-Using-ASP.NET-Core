using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using MyWebApi.Services;
using TutorialHell.Data;
using TutorialHell.Models;

namespace TutorialHell.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IUploadServices _uploadServices;

        public ContactsController(IContactRepository contactRepository, IMapper mapper, IUploadServices uploadServices)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _uploadServices = uploadServices;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactRepository.GetContactsAsync();
            if (contacts != null)
            {
                return Ok(contacts);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var getContact = await _contactRepository.GetContactAsync(id.ToString());
            return Ok(getContact);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> ImageUpload([FromForm] FileUpload fileUpload)
        {
            var upload = await _uploadServices.UploadFileAsync(fileUpload.file);
            if(upload != null)
            {
                return Ok($"PublicId {upload["publicId"]} Url: {upload["Url"]}");
            }
            return BadRequest("Upload Fail");

        }

        [HttpPost]

        public async Task<IActionResult> AddContact(AddContactDto addContactRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = _mapper.Map<Contact>(addContactRequest);
            var addResult = await _contactRepository.AddContactAsync(contact);
            if (addResult != null)
            {
                return Ok(addResult);
            }
            return BadRequest(addResult);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact(string Id, [FromBody] UpdateContactDto updateContact)
        {
            var contact = await _contactRepository.GetContactAsync(Id);
            if (contact == null)
            { 
                return NotFound(new
                {
                    Messsage = "Contact not found"
                });
            };
            _mapper.Map(updateContact,contact);           
            var addResult = await _contactRepository.UpdateContactAsync(contact);
            
            return Ok(addResult);

        }        

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var deleteContact = await _contactRepository.DeleteContactAsync(id.ToString());
            if (deleteContact < 0)
            {
                return NotFound(new
                {
                    Message = $"The contact with the id: {id} not found",
                    Status = StatusCode(406)
                });
            }
            return NoContent();
        }

    }



}
