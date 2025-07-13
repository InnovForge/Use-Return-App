using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Use_Return_App.Models
{
    public class Item
    {
        [Key]
        public string ItemID { get; set; }

        [Required]
        public string OwnerID { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public double DepositAmount { get; set; }

        public string CategoryID { get; set; }

        public bool IsAvailable { get; set; } = true;

        [ForeignKey("OwnerID")]
        public virtual User Owner { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}