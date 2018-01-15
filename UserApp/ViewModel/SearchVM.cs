using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Models;

namespace UserApp.ViewModel
{
    public class SearchVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}
