using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Use_Return_App.Helpers
{
    public class CookieCartHelper
    {
        private const string CookieKey = "CartCookie";

        public static void SaveCartToCookie(List<CartItem> cart, HttpResponse response)
        {
            var json = JsonConvert.SerializeObject(cart);
            var encoded = HttpUtility.UrlEncode(json);

            var cookie = new HttpCookie("CartCookie", encoded);
            cookie.Expires = DateTime.Now.AddDays(7);
            response.Cookies.Set(cookie);
        }

        public static List<CartItem> LoadCartFromCookie(HttpRequest request)
        {
            var cookie = request.Cookies[CookieKey];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
      
                var decoded = HttpUtility.UrlDecode(cookie.Value);
                return JsonConvert.DeserializeObject<List<CartItem>>(decoded);
            }
            return new List<CartItem>();
        }

        public static void ClearCart(HttpResponse response)
        {
            var cookie = new HttpCookie(CookieKey);
            cookie.Expires = DateTime.Now.AddDays(-1); // xóa
            response.Cookies.Set(cookie);
        }
    }
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}