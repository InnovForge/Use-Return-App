using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Helpers;
using Use_Return_App.Helpers.Use_Return_App.Helpers;

namespace Use_Return_App
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCartData();
            }
        }

        private void BindCartData()
        {
            // Lấy dữ liệu từ cookie
            var cartItems = CookieCartHelper.LoadCartFromCookie(Request);

            if (cartItems.Any())
            {
                // Lấy danh sách ID sản phẩm từ giỏ hàng
                var productIds = cartItems.Select(item => item.ProductId).ToList();

                // Lấy thông tin sản phẩm từ database
                var products = GetProductsByIds(productIds);

                if (products.Any())
                {
                    rptCart.DataSource = products;
                    rptCart.DataBind();
                    lblEmptyCart.Visible = false;
                }
                else
                {
                    ShowEmptyCartMessage();
                }
            }
            else
            {
                ShowEmptyCartMessage();
            }
        }

        private List<ProductInfo> GetProductsByIds(List<string> productIds)
        {
            var products = new List<ProductInfo>();
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Lọc ra các GUID hợp lệ
            var validGuids = new List<Guid>();
            foreach (string id in productIds)
            {
                if (Guid.TryParse(id, out Guid guid))
                {
                    validGuids.Add(guid);
                }
            }

            if (validGuids.Count == 0)
                return products;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Tạo câu SQL với tham số @MaDoDung0, @MaDoDung1,...
                var parameters = new List<string>();
                for (int i = 0; i < validGuids.Count; i++)
                {
                    parameters.Add($"@MaDoDung{i}");
                }

                string inClause = string.Join(",", parameters);
                string query = $"SELECT MaDoDung, TieuDe, MoTa, GiaMoiNgay FROM DoDung WHERE MaDoDung IN ({inClause})";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm từng tham số
                for (int i = 0; i < validGuids.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@MaDoDung{i}", validGuids[i]);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new ProductInfo
                        {
                            MaDoDung = reader["MaDoDung"].ToString(),
                            TieuDe = reader["TieuDe"].ToString(),
                            MoTa = reader["MoTa"].ToString(),
                            GiaMoiNgay = Convert.ToDecimal(reader["GiaMoiNgay"])
                        });
                    }
                }
            }

            return products;
        }
        private void ShowEmptyCartMessage()
        {
            rptCart.Visible = false;
            lblEmptyCart.Visible = true;
            lblEmptyCart.Text = "Giỏ hàng của bạn đang trống!";
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string productId = button.CommandArgument;

            var cartItems = CookieCartHelper.LoadCartFromCookie(Request);
            var itemToRemove = cartItems.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                CookieCartHelper.SaveCartToCookie(cartItems, Response);

                // Hiển thị thông báo
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Đã xóa sản phẩm khỏi giỏ hàng');", true);
            }

            BindCartData();
        }
    }

    // Lớp phụ để lưu thông tin sản phẩm
    public class ProductInfo
    {
        public string MaDoDung { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public decimal GiaMoiNgay { get; set; }
    }
}