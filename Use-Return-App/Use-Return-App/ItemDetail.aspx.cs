using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Helpers;

namespace Use_Return_App
{
    public partial class Item : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAddToCart_onClick(object sender, EventArgs e)
        {
            var cart = CookieCartHelper.LoadCartFromCookie(Request);
            var random = new Random();
            var newItem = new CartItem
            {
                ProductId = "P00_" + random.Next(1000, 9999),
                ProductName = "Sản phẩm A",
                Quantity = 1,
                Price = 100000
            };

            var existing = cart.FirstOrDefault(x => x.ProductId == newItem.ProductId);
            if (existing != null)
            {
                existing.Quantity += newItem.Quantity;
            }
            else
            {
                cart.Add(newItem);
            }

            CookieCartHelper.SaveCartToCookie(cart, Response);
        }

        [WebMethod]
        public static int AddToCart(string productId)
        {
            productId = new Random().Next(1000, 9999).ToString();
            var context = HttpContext.Current;

            var cart = CookieCartHelper.LoadCartFromCookie(context.Request);
         
            var existing = cart.FirstOrDefault(x => x.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = "Tên sản phẩm demo",
                    Quantity = 1,
                    Price = 100000
                });
            }

            CookieCartHelper.SaveCartToCookie(cart, context.Response);

            return cart.Count;
        }

    }
}