using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    // Models in which we specify the classes with their attributes
    public class Author
    {
        [Key] // Primary Key
        public int Author_Id { get; set; }
        [Required] // Required Field
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        [NotMapped] // Not Mapped Field, we can use it in c# coding, but it's not mapped in the database-side
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}