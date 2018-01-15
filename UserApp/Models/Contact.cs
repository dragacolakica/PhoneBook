using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Models;

namespace UserApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public virtual IEnumerable<Phone> Phone { get; set; }
        public virtual IEnumerable<Email> Email { get; set; }
        public virtual IEnumerable<ContactTag> ContactTag { get; set; }
    }
}
