using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rest_api_template.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // One-to-many relationship with books
        public List<Book>? Books{ get; set; }
    }
}