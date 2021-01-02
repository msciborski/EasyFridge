using EasyFridge.RecipeExtractor.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFridge.RecipeExtractor.Core
{
    public interface IRecipeExtractor
    {
        Task ExtractRecipesAsync(Uri recipeWebsiteUri, CancellationToken cancellationToken = default);

        void OnRecipeExtracted(Recipe recipe);
    }
}
