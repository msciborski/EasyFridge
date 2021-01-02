using Abot2.Crawler;
using Abot2.Poco;
using EasyFridge.RecipeExtractor.Core;
using EasyFridge.RecipeExtractor.Core.Models;
using Microsoft.Extensions.Logging;

namespace EasyFridge.RecipeExtractor.Abot.Impl
{
    public class KwestiaSmakuRecipeExtractor : AbotRecipeExtractor
    {
        public KwestiaSmakuRecipeExtractor(
            PoliteWebCrawler politeWebCrawler,
            IHtmlRecipeParser htmlRecipeParser,
            ILogger<AbotRecipeExtractor> logger = null)
                : base(politeWebCrawler, htmlRecipeParser, logger)
        {
        }

        protected override bool IsRecipePage(CrawledPage crawledPage)
        {
            return crawledPage.IsKwestiaSmakuRecipePage();
        }

        public override void OnRecipeExtracted(Recipe recipe)
        {
            //TODO: Here happens adding to db, or something like that. This methods receives recipe
            throw new System.NotImplementedException();
        }
    }
}
