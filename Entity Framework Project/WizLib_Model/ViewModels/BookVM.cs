using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.ViewModels
{
    // ViewModels for Book's Class
    public class BookVM
    {
        public Book Book { get; set; }
        // Selecting a List(collection of IEnumerable)
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}