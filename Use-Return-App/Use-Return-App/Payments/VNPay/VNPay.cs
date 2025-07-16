using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using Use_Return_App.Payments.VNPay.Models;

namespace Use_Return_App.Payments.VNPay
{
    public class VNPay
    {
        public static string TmnCode => ConfigurationManager.AppSettings["Vnpay:TmnCode"];
        public static string HashSecret => ConfigurationManager.AppSettings["Vnpay:HashSecret"];
        public static string BaseUrl => ConfigurationManager.AppSettings["Vnpay:BaseUrl"];
        public static string Command => ConfigurationManager.AppSettings["Vnpay:Command"];
        public static string CurrCode => ConfigurationManager.AppSettings["Vnpay:CurrCode"];
        public static string Version => ConfigurationManager.AppSettings["Vnpay:Version"];
        public static string Locale => ConfigurationManager.AppSettings["Vnpay:Locale"];
        public static string ReturnUrl => ConfigurationManager.AppSettings["Vnpay:PaymentBackReturnUrl"];

        public static string BuildPaymentUrl(OrderInfo order, string bankCode = null, string locale = "vn")
        {
            VNPayLibrary vnpay = new VNPayLibrary();

            vnpay.AddRequestData("vnp_Version", VNPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", locale);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang: " + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());

            if (!string.IsNullOrEmpty(bankCode))
            {
                vnpay.AddRequestData("vnp_BankCode", bankCode);
            }

            string paymentUrl = vnpay.CreateRequestUrl(BaseUrl, HashSecret);
            return paymentUrl;
        }

   



        public static PaymentResponse ProcessReturn(NameValueCollection queryString)
        {
           
            VNPayLibrary vnpay = new VNPayLibrary(); 

            foreach (string key in queryString.AllKeys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, queryString[key]);
                }
            }

            string vnp_SecureHash = queryString["vnp_SecureHash"];

            bool isValidSignature = vnpay.ValidateSignature(vnp_SecureHash, VNPay.HashSecret);

            var response = new PaymentResponse
            {
                OrderId = vnpay.GetResponseData("vnp_TxnRef"),
                TransactionId = vnpay.GetResponseData("vnp_TransactionNo"),
                VnPayResponseCode = vnpay.GetResponseData("vnp_ResponseCode"),
                VnpayTransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus"),
                PaymentMethod = queryString["vnp_BankCode"],
                Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100,

                Token = vnp_SecureHash,
  
           
        };

            response.Success = isValidSignature &&
                               vnpay.GetResponseData("vnp_ResponseCode") == "00" &&
                               vnpay.GetResponseData("vnp_TransactionStatus") == "00";

            return response;
        }
    }

}