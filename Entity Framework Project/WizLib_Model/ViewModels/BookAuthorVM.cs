﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.ViewModels
{
    public class BookAuthorVM
    {
        public BookAuthor BookAuthor { get; set; }
        public Book Book { get; set; }
        public IEnumerable<BookAuthor> BookAuthorList { get; set; }
        public IEnumerable<SelectListItem> AuthorList { get; set; }
    }
}