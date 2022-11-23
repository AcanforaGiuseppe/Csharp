using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(15)] // We're specifying there the Max Length of the field
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }

        // 1 to 1 Relation - Expliciting the field where to take the External Key(ForeignKey)
        [ForeignKey("BookDetail")]
        public int? BookDetail_Id { get; set; }
        public BookDetail BookDetail { get; set; }
        // ||
        //public BookDetail BookDetail { get; set; }

        // 1 to N relation
        [ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }
        public Publisher Publisher { get; set; }

        // N to N relation 
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}