using Abot2.Poco;

namespace EasyFridge.RecipeExtractor.Abot
{
    public static class CrawledPageExtensions
    {
        public static bool IsKwestiaSmakuRecipePage(this CrawledPage crawledPage)
        {
            //TODO: Get rid of magic strings
            return crawledPage.Uri.ToString().Contains("przepis/") || crawledPage.Uri.ToString().EndsWith("przepis.html");
        }
    }
}
