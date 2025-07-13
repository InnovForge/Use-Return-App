using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Use_Return_App.Models
{
    public class Wallet
    {
        [Key, ForeignKey("User")]
        public string UserID { get; set; }

        public double Balance { get; set; } = 0;

        public virtual User User { get; set; }
    }
}