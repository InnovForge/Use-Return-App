using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Payments.VNPay;
using Use_Return_App.Payments.VNPay.Models;

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
               
                  //  Response.Write("Loại thanh toán không hợp lệ.");
                    break;
            }
        }
        void ProcessVnPayCallback()
        {
            var result = VNPay.ProcessReturn(Request.QueryString);
            containerTb.Visible = true;

            displayTxnRef.InnerText =  result.TransactionId;
            displayOrderId.InnerText = result.OrderId;
            displayAmount.InnerText = Convert.ToDecimal(result.Amount).ToString("N0", new System.Globalization.CultureInfo("vi-VN") { NumberFormat = { NumberGroupSeparator = "." } }) + " đ";
            displayBankCode.InnerText =  result.PaymentMethod;
            if (result.Success)
            {
                //Thanh toan thanh cong
                iconSuccess.Visible = true;
                iconFail.Visible = false;
                lbMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#198754");
           
                lbMessage.Text = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ.";

                string maThanhToan = result.OrderId.ToString();
                string maGiaoDich = result.TransactionId;
                string tenNganHang = result.PaymentMethod;

                // Cập nhật thanh toán thành công
                SqlHelper.ExecuteNonQuery(@"
            UPDATE ThanhToan
            SET TrangThai = 'Success',
                MaGiaoDichNgoai = @maGiaoDich,
                TenNganHang = @tenNganHang,
                ThoiGianThanhToan = GETDATE()
            WHERE MaThanhToan = @maThanhToan
        ",
                new SqlParameter("@maThanhToan", maThanhToan),
                new SqlParameter("@maGiaoDich", maGiaoDich),
                new SqlParameter("@tenNganHang", tenNganHang));

                var dt = SqlHelper.ExecuteDataTable("SELECT MaPhieuThue FROM ThanhToan WHERE MaThanhToan = @maThanhToan",
                    new SqlParameter("@maThanhToan", maThanhToan));
                // log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
            }
            else
            {
                iconSuccess.Visible = false;
                iconFail.Visible = true;
                errMsg.Visible = true;
                string responseCodeDescription = PaymentResponse.VnPayResponseCodeDescription(result.VnPayResponseCode);
                string statusDescription = PaymentResponse.TransactionStatusDescription(result.VnpayTransactionStatus);
                errMsg.InnerText = responseCodeDescription;
               // Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
               lbMessage.Text = "Giao dịch không thành công";
                lbMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dc3545");

                //  log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
            }

        }

    }
}