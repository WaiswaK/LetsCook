using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LetsCook.Model;

namespace LetsCook.ViewModels
{
    public class RecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public List<RecipeGroup> RecipeGroups { get; set; }

        public async Task LoadRecipesAsync()
        {
            // Read RecipeData.json from this PCL's DataModel folder
            var name = typeof(RecipeViewModel).AssemblyQualifiedName.Split(',')[1].Trim();
            var assembly = Assembly.Load(new AssemblyName(name));
            var stream = assembly.GetManifestResourceStream(name + ".Repository.RecipeData.json");

            // Parse the JSON and generate a collection of RecipeGroup objects
            using (var reader = new StreamReader(stream))
            {
                string json = await reader.ReadToEndAsync();
                var obj = new { Groups = new List<RecipeGroup>() };
                var groups = JsonConvert.DeserializeAnonymousType(json, obj);
                RecipeGroups = groups.Groups;
            }
        }
    }
}
