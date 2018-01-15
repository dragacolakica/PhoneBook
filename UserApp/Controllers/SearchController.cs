using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApp.Data;
using UserApp.Models;
using UserApp.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly HRContext db;

        public SearchController(HRContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult searchContacts([FromBody] SearchVM searchParams)
        {
            var List = new List<Contact>();
            var searchedList = db.Contact.ToList();
            var contactIds = new List<ContactTag>();
            var cIDS = new List<int>();
            foreach (var tag in searchParams.Tags)
            {
                var cId = db.ContactTag.Where(x => x.TagId == tag.Id).ToList();
                contactIds.AddRange(cId);
            }
            cIDS = contactIds.Select(x => x.ContactId).Distinct().ToList();

            if (!(searchParams.FirstName.Equals("-")))
            {
                searchParams.FirstName = searchParams.FirstName.ToLower();
                searchedList = searchedList.Where(x => x.FirstName.ToLower().Contains(searchParams.FirstName)).ToList();
            }
            if (!(searchParams.LastName.Equals("-")))
            {
                searchParams.LastName = searchParams.LastName.ToLower();
                searchedList = searchedList.Where(x => x.LastName.ToLower().Contains(searchParams.LastName)).ToList();
            }

            foreach (var id in cIDS)
            {
                var item = searchedList.Where(x => x.Id == id).SingleOrDefault();
                if (item != null)
                {
                    item.ContactTag = null;
                    List.Add(item);
                }
            }
            List.Select(m => m.Id).Distinct();
            return Ok(List);
        }
    }
}
