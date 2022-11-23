using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    [Table("tb_Genre")]
    public class Genre
    {
        public int GenreId { get; set; }
        // We're specifying the Column Name, given a field name (GenreName) that isn't with a name that we want for the column
        [Column("Name")]
        public string GenreName { get; set; }
        //public int DisplayOrder { get; set; }
    }
}