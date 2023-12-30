using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Post ID")]
        public int PostId { get; set; }

        [Required]
        public string MemberID { get; set; }

        [Required(ErrorMessage = "Comment required")]
        [StringLength(200)]
        public string Content { get; set; }

    }
}
