using Abot2.Crawler;
using Abot2.Poco;
using EasyFridge.RecipeExtractor.Abot;
using EasyFridge.RecipeExtractor.Abot.Impl.CrawlDecisionMakers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyFridge.Crawler
{
    class CrawlResult
    {
        public string Url { get; set; }
        public bool IsRecipePage { get; set; }
        public bool HasContentDownloaded { get; set; }
        public bool IsFailed { get; set; }
    }

    class Program
    {
        static List<CrawlResult> _recipeLinks = new List<CrawlResult>();
        static async Task Main(string[] args)
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 100_000,
                HttpRequestTimeoutInSeconds = 60,
            };
            var crawler = new PoliteWebCrawler(config, new KwestiaSmakuCrawlDecisionMaker(), null, null, null, null, null, null, null);
            crawler.PageCrawlCompleted += PageCrawlCompleted;
            var crawlResult = await crawler.CrawlAsync(new Uri("https://www.kwestiasmaku.com/"));

            await using (var file = File.OpenWrite("result.txt"))
            {
                var jsonResult = JsonConvert.SerializeObject(_recipeLinks);
                await file.WriteAsync(Encoding.UTF8.GetBytes(jsonResult));
            }
        }

        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            if (e.CrawledPage.HttpRequestException != null)
            {
                _recipeLinks.Add(new CrawlResult
                {
                    IsFailed = true,
                    IsRecipePage = e.CrawledPage.IsKwestiaSmakuRecipePage(),
                    HasContentDownloaded = e.CrawledPage.Content != null,
                    Url = e.CrawledPage.Uri.ToString(),
                });
            }
            else
            {
                _recipeLinks.Add(new CrawlResult
                {
                    HasContentDownloaded = e.CrawledPage.Content != null,
                    IsFailed = false,
                    IsRecipePage = e.CrawledPage.IsKwestiaSmakuRecipePage(),
                    Url = e.CrawledPage.Uri.ToString(),
                });
            }

            //if (e.CrawledPage.HttpRequestException != null)
            //{
            //    Console.WriteLine(e.CrawledPage.Uri.ToString());
            //    Console.WriteLine(e.CrawledPage.HttpRequestException.Message);
            //}
            //else
            //{
            //    var httpStatus = e.CrawledPage.HttpResponseMessage.StatusCode;
            //    var rawPageText = e.CrawledPage.Content.Text;
            //    Console.WriteLine($"Http status: {httpStatus}");
            //    Console.WriteLine(e.CrawledPage.Uri.ToString());
            //    _links.Add(e.CrawledPage.Uri.AbsoluteUri);
            //}
        }
    }
}
