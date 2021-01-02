using AngleSharp.Html.Dom;
using EasyFridge.RecipeExtractor.Core.Models;

namespace EasyFridge.RecipeExtractor.Core
{
    public interface IHtmlRecipeParser
    {
        Recipe Parse(string content);
        Recipe Parse(IHtmlDocument htmlDocument);
    }
}
