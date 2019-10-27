using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gas_Go_v1.Models
{
    public class ThreadsViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required (ErrorMessage  = "Subject can not be empty!")]
        public string Subject { get; set; }
    }
}