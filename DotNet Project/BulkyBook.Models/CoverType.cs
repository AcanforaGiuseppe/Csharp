using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class CoverType
    {
        // Attribute that stays for PK (Primary Key)
        [Key]
        public int Id { get; set; }

        // Displays a name that is not the one of the variable
        [Display(Name = "Cover Type")]
        [Required]
        // Gives to the field a Max Length of the input
        [MaxLength(50)]
        public string Name { get; set; }
    }
}