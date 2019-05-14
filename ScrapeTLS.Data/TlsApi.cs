using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ScrapeTLS.Data
{
    public class TlsApi
    {

        public static List<News> ScrapeTLS(string query)
        {
            var html = GetTlsHtml(query);
            return GetQuery(html);
        }
        public static List<News> ScrapeTLS()
        {
            var html = GetTlsHtml();
            return GetItems(html);
        }

        private static string GetTlsHtml(string query)
        {
            var handler = new HttpClientHandler();
            string betterQuery = Uri.EscapeUriString(query);
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("user-agent", "I'm goin' nuts");
                var url = $"https://www.thelakewoodscoop.com/news/?s={betterQuery}";
                var html = client.GetStringAsync(url).Result;
                return html;
            }
        }

        private static string GetTlsHtml()
        {
            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("user-agent", "oh seriously");
                var url = "https://www.thelakewoodscoop.com/";
                var html = client.GetStringAsync(url).Result;
                return html;
            }
        }

        private static List<News> GetItems(string html)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);
            var itemDivs = document.QuerySelectorAll(".post");
            List<News> items = new List<News>();
            foreach (var div in itemDivs)
            {
                News item = new News();
                var href = div.QuerySelectorAll("a").First();
                item.Title = href.TextContent.Trim();
                item.Url = href.Attributes["href"].Value;

                var image = div.QuerySelector("img.aligncenter");
                if(image != null)
                {
                    item.ImageUrl = image.Attributes["src"].Value;
                }
                else
                {
                    image = div.QuerySelector("img.alignleft");
                    item.ImageUrl = image.Attributes["src"].Value;
                }
               

                var date = div.QuerySelector("div.postmetadata-top");
                item.Date = date.TextContent.Trim();

                items.Add(item);
            }

            return items;
        }

        private static List<News> GetQuery(string html)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);
            var itemDivs = document.QuerySelectorAll(".post");
            List<News> items = new List<News>();
            foreach (var div in itemDivs)
            {
                News item = new News();
                var href = div.QuerySelectorAll("a").First();
                item.Title = href.TextContent.Trim();
                item.Url = href.Attributes["href"].Value;

                var image = div.QuerySelector("img.aligncenter");
                if (image != null)
                {
                    item.ImageUrl = image.Attributes["src"].Value;
                }
                else
                {
                    image = div.QuerySelector("img.alignleft");
                    item.ImageUrl = image.Attributes["src"].Value;
                }


                var date = div.QuerySelector("div.postmetadata-top");
                item.Date = date.TextContent.Trim();

                items.Add(item);
            }

            return items;
        }

    }
}
