using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Use_Return_App
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static List<Card> LoadCards(int page)
        {
            int pageSize = 10;
            var allData = GenerateFakeData();
            return allData.Skip(page * pageSize).Take(pageSize).ToList();
        }
    
        public static List<Card> GenerateFakeData()
        {
            var data = new List<Card>();
            for (int i = 1; i <= 100; i++)
            {
                data.Add(new Card
                {
                    Title = $"Card {i}",
                    Description = $"Description {i}",
                    ImageUrl = $"https://placehold.co/600x400/png?text={i}",
                    LinkUrl = "#"
                });
            }
            return data;
        }
    }

    public class Card
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
    }
}
