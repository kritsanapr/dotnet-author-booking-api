using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace rest_api_template.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public Genre? Genre { get; set; }
        public string? Publisher { get; set; }
        public string? ISBN { get; set; }
        public double? Rating { get; set; }
        public DateTime ReleaseData { get; set; }


        // Specific relation One-to-many relation with author
        public Guid? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}