using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual IEnumerable<ContactTag> ContactTag { get; set; }
    }
}
