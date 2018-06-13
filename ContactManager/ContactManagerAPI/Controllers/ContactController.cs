using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ContactApi.Models;

#region ContactController
namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;
        #endregion

        public ContactController(ContactContext context)
        {
            _context = context;

            if (_context.ContactItems.Count() == 0)
            {
                _context.ContactItems.Add(new ContactItem {IsActive = false});
                _context.SaveChanges();
            }
        }

        #region Region_GetAll
        [HttpGet]
        public List<ContactItem> GetAll()
        {
            return _context.ContactItems.ToList();
        }

        #region Region_GetByID
        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult GetById(long id)
        {
            var item = _context.ContactItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        #endregion
        #endregion

        #region Region_Create
        [HttpPost]
        public IActionResult Create([FromBody] ContactItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            // check for duplicate
            int CountExistingContact = _context.ContactItems.Count(a =>
                                                a.FirstName == item.FirstName &&
                                                a.LastName == item.LastName &&
                                                a.Phone == item.Phone);
            if (CountExistingContact > 0)
                return BadRequest();

            _context.ContactItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetContact", new { id = item.Id }, item);
        }
        #endregion

        #region Region_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ContactItem item)
        {
            if (item == null || item.Id != id || id ==1)
            {
                return BadRequest();
            }

            var Contact = _context.ContactItems.Find(id);
            if (Contact == null)
            {
                return NotFound();
            }

            Contact.IsActive = item.IsActive;
            Contact.FirstName = item.FirstName;
            Contact.LastName = item.LastName;
            Contact.Email = item.Email;
            Contact.Phone = item.Phone;

            _context.ContactItems.Update(Contact);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region Region_Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var Contact = _context.ContactItems.Find(id);
            if (Contact == null || id == 1)
            {
                return NotFound();
            }

            _context.ContactItems.Remove(Contact);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}