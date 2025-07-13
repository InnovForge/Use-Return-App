using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Use_Return_App.Models
{
    public class Rental
    {
        [Key]
        public string RentalID { get; set; }

        [Required]
        public string ItemID { get; set; }

        [Required]
        public string BorrowerID { get; set; }

        public DateTime RentDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; }

        [Required]
        public string Status { get; set; }

        public double DepositAmount { get; set; }

        public string DepositStatus { get; set; }

        public string Notes { get; set; }

        [ForeignKey("ItemID")]
        public virtual Item Item { get; set; }

        [ForeignKey("BorrowerID")]
        public virtual User Borrower { get; set; }
    }
}