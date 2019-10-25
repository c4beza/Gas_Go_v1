using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gas_Go_v1.Models
{


    public class UserSearchRequestsViewModel
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
            [Display(Name = "")]
            public string RequestKeyword { get; set; }
    }
}