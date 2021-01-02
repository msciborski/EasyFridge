using Abot2.Crawler;
using Abot2.Poco;
using EasyFridge.RecipeExtractor.Core;
using EasyFridge.RecipeExtractor.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFridge.RecipeExtractor.Abot.Impl
{
    public abstract class AbotRecipeExtractor : IRecipeExtractor
    {
        private readonly PoliteWebCrawler _politeWebCrawler;
        private readonly IHtmlRecipeParser _htmlRecipeParser;
        protected ILogger<AbotRecipeExtractor> Logger;

        protected AbotRecipeExtractor(PoliteWebCrawler politeWebCrawler, IHtmlRecipeParser htmlRecipeParser, ILogger<AbotRecipeExtractor> logger = null)
        {
            _politeWebCrawler = politeWebCrawler;
            _htmlRecipeParser = htmlRecipeParser;
            Logger = logger;
            _politeWebCrawler.PageCrawlCompleted += OnPageCrawlCompleted;
        }

        protected abstract bool IsRecipePage(CrawledPage crawledPage);
        public abstract void OnRecipeExtracted(Recipe recipe);

        public async Task ExtractRecipesAsync(Uri recipeWebsiteUri, CancellationToken cancellationToken = default)
        {
            var crawlResult = await _politeWebCrawler.CrawlAsync(recipeWebsiteUri);

            //TODO: Error handling
            //TODO: Logging
        }


        protected void OnPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            if (!IsRecipePage(e.CrawledPage)) return;

            var recipe = _htmlRecipeParser.Parse(e.CrawledPage.Content.Text);
            OnRecipeExtracted(recipe);
        }
    }
}
