using Abot2.Core;
using Abot2.Poco;

namespace EasyFridge.RecipeExtractor.Abot.Impl.CrawlDecisionMakers
{
    public class KwestiaSmakuCrawlDecisionMaker : CrawlDecisionMaker
    {
        public override CrawlDecision ShouldCrawlPageLinks(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            var shouldCrawlPageLinks = base.ShouldCrawlPageLinks(crawledPage, crawlContext);
            if (!shouldCrawlPageLinks.Allow) return shouldCrawlPageLinks;

            if (crawledPage.IsKwestiaSmakuRecipePage())
            {
                return new CrawlDecision
                {
                    Allow = false
                };
            }

            return new CrawlDecision
            {
                Allow = true,
            };

        }

        public override CrawlDecision ShouldCrawlPage(PageToCrawl pageToCrawl, CrawlContext crawlContext)
        {
            return base.ShouldCrawlPage(pageToCrawl, crawlContext);
        }

    }
}
