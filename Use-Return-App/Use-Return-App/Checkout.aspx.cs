using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Payments.VNPay;
using Use_Return_App.Payments.VNPay.Models;

namespace Use_Return_App
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSoNgayThue.Attributes["type"] = "number";
                txtSoNgayThue.Attributes["min"] = "1"; 
                string id = Request.QueryString["itid"];
             
                if (Guid.TryParse(id, out Guid itemId))
                {
                    var row = GetChiTietDoDung(itemId);
                    lblTieuDe.Text = row["TieuDe"].ToString();
                    lblGiaMoiNgay.Text = Convert.ToDecimal(row["GiaMoiNgay"]).ToString("N0", new System.Globalization.CultureInfo("vi-VN") { NumberFormat = { NumberGroupSeparator = "." } }) + " đ";
                    lblTienCoc.Text = Convert.ToDecimal(row["TienCoc"]).ToString("N0", new System.Globalization.CultureInfo("vi-VN") { NumberFormat = { NumberGroupSeparator = "." } }) + " đ";
                    img.ImageUrl = row["DuongDanAnh"].ToString();


                    var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = "vnpay", Name = "Ví VNPay", ImageUrl = "/Assets/IconVNPAY.png" },
            new PaymentMethod { Id = "cash", Name = "Thanh toán tiền mặt", ImageUrl = "/Assets/iconCash.png" },

        };

                    rptPaymentMethods.DataSource = paymentMethods;
                    rptPaymentMethods.DataBind();
                }
                else
                {
                    Response.Redirect("~/404.aspx");
                }

            }
        }
        public static DataRow GetChiTietDoDung(Guid maDoDung)
        {
            string sql = @"
        SELECT 
            dd.TieuDe, 
            dd.MoTa, 
            dd.GiaMoiNgay, 
            dd.TienCoc,
            dd.SoLuong,
            dd.TinhTrang,
            dd.NgayTao,
            dm.TenDanhMuc,
            nd.HoTen AS TenNguoiSoHuu,
            ha.DuongDanAnh -- ảnh đại diện
        FROM DoDung dd
        JOIN DanhMuc dm ON dd.MaDanhMuc = dm.MaDanhMuc
        JOIN NguoiDung nd ON dd.MaNguoiSoHuu = nd.MaNguoiDung
        LEFT JOIN HinhAnhDoDung ha ON dd.MaDoDung = ha.MaDoDung AND ha.ThuTuHienThi = 0
        WHERE dd.MaDoDung = @id";

            var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@id", maDoDung));
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string selectedPaymentId = Request.Form["paymentMethod"];

            int soNgayThue = int.Parse(txtSoNgayThue.Text);
            string id = Request.QueryString["itid"];
            string sql = "SELECT GiaMoiNgay, TienCoc FROM DoDung WHERE MaDoDung = @id";
            var parameters = new[] { new SqlParameter("@id", id) };

            DataTable table = SqlHelper.ExecuteDataTable(sql, parameters);
            if (table.Rows.Count == 0)
            {
      
                Response.Write("Không tìm thấy đồ dùng");
                return;
            }

            decimal giaMoiNgay = Convert.ToDecimal(table.Rows[0]["GiaMoiNgay"]);
            decimal tienCoc = Convert.ToDecimal(table.Rows[0]["TienCoc"]);
            decimal tongTien = soNgayThue * (giaMoiNgay + tienCoc);
            if (!string.IsNullOrEmpty(selectedPaymentId))
            {
                if(selectedPaymentId == "vnpay")
                {
                    OrderInfo order = new OrderInfo
                    {
                        OrderId = DateTime.Now.Ticks,
                        Amount = (long)tongTien,
                        Status = "0",
                        CreatedDate = DateTime.Now
                    };


                    string bankCode = null; 
                    string locale = "vn";

                    string paymentUrl = VNPay.BuildPaymentUrl(order, bankCode, locale);
                    Response.Redirect(paymentUrl);
                }
            }
            else
            {
               
           
            }
        }
    }
    public class PaymentMethod
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }

}