using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Use_Return_App.Payments.VNPay.Models
{
    public class PaymentResponse
    {
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string VnpayTransactionStatus { get; set; }
        public string responseDescription { get;set; }

    }
}