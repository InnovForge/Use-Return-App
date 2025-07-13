using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Use_Return_App.Models
{
    public class User
    {
        [Key]
        public string UserID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        public virtual Wallet Wallet { get; set; }
    }
}