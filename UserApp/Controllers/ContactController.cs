using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApp.Models;
using UserApp.Data;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly HRContext db;

        public ContactController(HRContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = new List<Contact>();
            list = db.Contact.ToList();
            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = db.Contact.Where(x => x.Id == id).SingleOrDefault();
            var phones = db.Phone.Where(x => x.ContactId == id).ToList();
            foreach (var phone in phones)
            {
                phone.Contact = null;
            }
            var emails = db.Email.Where(x => x.ContactId == id).ToList();
            foreach (var email in emails)
            {
                email.Contact = null;
            }
            contact.Phone = phones;
            contact.Email = emails;
            var tags = db.ContactTag.Where(x => x.ContactId == id).ToList();
            foreach (var tag in tags)
            {
                tag.Contact = null;
            }
            return Ok(contact);
        }
        [HttpPost]
        public IActionResult Post([FromBody]Contact contact)
        {
            if (contact.Id == 0)
            {
                db.Contact.Add(contact);
                db.Email.AddRange(contact.Email);
                db.Phone.AddRange(contact.Phone);
            }
            else
            {
                var exContact = db.Contact.Where(x => x.Id == contact.Id).SingleOrDefault();
                var existEmails = db.Email.Where(x => x.ContactId == contact.Id).ToList();
                var existPhones = db.Phone.Where(x => x.ContactId == contact.Id).ToList();
                exContact.FirstName = contact.FirstName;
                exContact.LastName = contact.LastName;
                exContact.City = contact.City;
                exContact.Street = contact.Street;
                var newEmails = contact.Email.Where(x => x.Id == 0).ToList();
                var newPhones = contact.Phone.Where(x => x.Id == 0).ToList();
                db.Email.AddRange(newEmails);
                db.Phone.AddRange(newPhones);
                foreach (var exPhone in existPhones)
                {
                    if (!contact.Phone.Any(x => x.Id == exPhone.Id))
                    {
                        db.Phone.Remove(exPhone);
                    }
                }
                foreach (var exEmail in existEmails)
                {
                    if (!contact.Email.Any(x => x.Id == exEmail.Id))
                    {
                        db.Email.Remove(exEmail);
                    }
                }
            }
            var contacTags = contact.ContactTag;
            foreach (var cTag in contacTags)
            {
                if (!db.ContactTag.Any(x => x.ContactId == contact.Id && x.TagId == cTag.TagId))
                {
                    db.ContactTag.Add(cTag);
                    db.SaveChanges();
                }
            }
            db.SaveChanges();
            return Ok(contact.Id);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var contact = db.Contact.Where(x => x.Id == id).Include(x => x.Email).Include(x => x.Phone).SingleOrDefault();
            db.Contact.Remove(contact);
            db.SaveChanges();
            return Ok();
        }
    }
}
