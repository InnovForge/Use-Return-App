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

        public static string GetVnpResponseDescription(string code)
        {
            switch (code)
            {
                case "00": return "Giao dịch thanh toán thành công.";
                case "01": return "Giao dịch chưa hoàn tất.";
                case "02": return "Giao dịch bị lỗi.";
                case "04": return "Giao dịch đảo – Khách hàng đã bị trừ tiền tại Ngân hàng nhưng chưa thành công tại VNPAY.";
                case "05": return "VNPAY đang xử lý giao dịch này (hoàn tiền).";
                case "06": return "VNPAY đã gửi yêu cầu hoàn tiền sang Ngân hàng.";
                case "07": return "Giao dịch bị nghi ngờ gian lận.";
                case "09": return "Giao dịch hoàn trả bị từ chối.";
                default: return "Không rõ trạng thái giao dịch.";
            }
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
                TransactionId = vnpay.GetResponseData("vnp_TransactionNo"),
                OrderId = vnpay.GetResponseData("vnp_TxnRef"),
                PaymentMethod = queryString["vnp_BankCode"],
                PaymentId = vnpay.GetResponseData("vnp_TransactionNo"),
                VnPayResponseCode = vnpay.GetResponseData("vnp_ResponseCode"),
                Token = vnp_SecureHash,
                VnpayTransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus"),
                responseDescription = GetVnpResponseDescription(vnpay.GetResponseData("vnp_ResponseCode"))
        };

            response.Success = isValidSignature &&
                               vnpay.GetResponseData("vnp_ResponseCode") == "00" &&
                               vnpay.GetResponseData("vnp_TransactionStatus") == "00";

            

            return response;
        }
    }

}