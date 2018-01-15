using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApp.Models
{
    public class ContactTag
    {
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int TagId { get; set; }
        public Tag  Tag { get; set; }
    }
}
