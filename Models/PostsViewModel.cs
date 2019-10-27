using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gas_Go_v1.Models
{
    public class PostsViewModel
    {
        [Required(ErrorMessage = "Content can not be empty!")]
        public String Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public String UserID { get; set; }
        public int ThreadID { get; set; }
    }
}