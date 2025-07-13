using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Use_Return_App.Models
{
    public class Category
    {
        [Key]
        public string CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}