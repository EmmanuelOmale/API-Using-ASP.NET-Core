using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialHell.Models;

namespace Data.Entities
{
    public class PaginatedContacts
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
