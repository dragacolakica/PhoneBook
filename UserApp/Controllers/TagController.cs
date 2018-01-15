using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApp.Data;
using UserApp.Models;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly HRContext db;

        public TagController(HRContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<Tag>();
            list = db.Tag.ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Tag tag)
        {
            if (!string.IsNullOrEmpty(tag.Value)){
                var checkValue = tag.Value.ToLower();
                if (!db.Tag.Any(x => x.Value.ToLower() == tag.Value))
                {
                    db.Tag.Add(tag);
                    db.SaveChanges();
                }
            }
            return Ok();
        }
        [HttpGet("contactId/{contactId}")]
        public IActionResult Get(int contactId)
        {
            var contact = db.Contact.Where(x => x.Id == contactId).Include(x => x.ContactTag).ThenInclude(x => x.Tag).SingleOrDefault();
            var tags = contact.ContactTag.Select(x=> new Tag { Id = x.TagId, Value = x.Tag.Value}).ToList();
            return Ok(tags);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contactTags = db.ContactTag.Where(x => x.TagId == id).ToList();
            db.ContactTag.RemoveRange(contactTags);
            var tagToDelete = db.Tag.Where(x => x.Id == id).SingleOrDefault();
            db.Tag.Remove(tagToDelete);
            db.SaveChanges();
            return Ok();
        }
    }
}
