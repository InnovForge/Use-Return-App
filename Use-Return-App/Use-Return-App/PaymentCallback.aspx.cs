using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Payments.VNPay;

namespace Use_Return_App
{
    public partial class PaymentCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack) return;
            string paymentType = Page.RouteData.Values["slug"] as string;


            switch (paymentType)
            {
                case "vnpay":
                 ProcessVnPayCallback();
                    break;
                case "momo":
                  //  ProcessMomoCallback();
                    break;
                default:
               
                    Response.Write("Loại thanh toán không hợp lệ.");
                    break;
            }
        }
        void ProcessVnPayCallback()
        {
            var result = VNPay.ProcessReturn(Request.QueryString);

     
            if (result.Success)
            {
                //Thanh toan thanh cong
                displayMsg.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                // log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
            }
            else
            {
                // Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý. Mã lỗi: " + result.VnpayTransactionStatus;
                //  log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
            }




            ////  log.InfoFormat("Begin VNPAY Return, URL={0}", Request.RawUrl);
            //if (Request.QueryString.Count > 0)
            //{
            //    var vnpayData = Request.QueryString;
            //    VNPayLibrary vnpay = new VNPayLibrary();

            //    foreach (string s in vnpayData)
            //    {
            //        //get all querystring data
            //        if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
            //        {
            //            vnpay.AddResponseData(s, vnpayData[s]);
            //        }
            //    }
            //    //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
            //    //vnp_TransactionNo: Ma GD tai he thong VNPAY
            //    //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
            //    //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

            //    long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            //    long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            //    string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            //    string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            //    String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
            //    String TerminalID = Request.QueryString["vnp_TmnCode"];
            //    long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            //    String bankCode = Request.QueryString["vnp_BankCode"];

            //    bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, VNPay.HashSecret);

            //    if (checkSignature)
            //    {
            //        if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
            //        {
            //            //Thanh toan thanh cong
            //            txtMes.Text = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
            //            // log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
            //        }
            //        else
            //        {
            //            // Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
            //            displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
            //            //  log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
            //        }
            //        displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
            //        displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
            //        displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
            //        displayAmount.InnerText = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
            //        displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
            //    }
            //    else
            //    {
            //        //  log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
            //        displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
            //    }

            //}
        }

    }
}