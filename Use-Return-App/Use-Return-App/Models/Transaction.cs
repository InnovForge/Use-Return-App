using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Use_Return_App.Models
{
    public class Transaction
    {
        [Key]
        public string TransactionID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Type { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}