using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title required")]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Posted { get; set; }
    }
}
