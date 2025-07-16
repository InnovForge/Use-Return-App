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
                if (Session["userId"] == null)
                {
                    
                    Response.Redirect("~/DANGNHAP.aspx");
                }
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
            new PaymentMethod { Id = "vnpay", Name = "Ví VNPay", ImageUrl = "/Assets/IconVNPay.png" },
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
            string id = Request.QueryString["itid"];

            int soNgayThue = int.Parse(txtSoNgayThue.Text);
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

        
            Guid maPhieuThue = Guid.NewGuid();
            SqlHelper.ExecuteNonQuery(@"
        INSERT INTO PhieuThue (MaPhieuThue, MaDoDung, MaNguoiThue, NgayBatDau, NgayKetThuc, TongTien, TienCoc, ThoiGianHetHan)
        VALUES (@maPhieuThue, @maDoDung, @maNguoiThue, @batDau, @ketThuc, @tongTien, @tienCoc, @hetHan)
    ", new SqlParameter("@maPhieuThue", maPhieuThue),
               new SqlParameter("@maDoDung", id),
               new SqlParameter("@maNguoiThue", Session["userId"]),
               new SqlParameter("@batDau", DateTime.Today),
               new SqlParameter("@ketThuc", DateTime.Today.AddDays(soNgayThue - 1)),
               new SqlParameter("@tongTien", tongTien),
               new SqlParameter("@tienCoc", tienCoc),
               new SqlParameter("@hetHan", DateTime.Now.AddMinutes(15))
            );

            string maThanhToan = DateTime.Now.Ticks.ToString();

            SqlHelper.ExecuteNonQuery(@"
        INSERT INTO ThanhToan (MaThanhToan, MaPhieuThue, SoTien, PhuongThuc)
        VALUES (@maThanhToan, @maPhieuThue, @soTien, @phuongThuc)
    ", new SqlParameter("@maThanhToan", maThanhToan),
               new SqlParameter("@maPhieuThue", maPhieuThue),
               new SqlParameter("@soTien", tongTien),
               new SqlParameter("@phuongThuc", "E-Wallet")
            );

          
            if (selectedPaymentId == "vnpay")
            {
                OrderInfo order = new OrderInfo
                {
                    OrderId = long.Parse(maThanhToan),
                    Amount = (long)tongTien,
                    Status = "0",
                    CreatedDate = DateTime.Now
                };


                string bankCode = null;
                string locale = "vn";

                string paymentUrl = VNPay.BuildPaymentUrl(order, bankCode, locale);
                Response.Redirect(paymentUrl);
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