using LetsCook.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LetsCook
{
    public class App : Application
    {
        private RecipeViewModel _rvm;
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new Views.MainPage());
        }

        public async Task<RecipeViewModel> GetRecipeViewModelAsync()
        {
            if (_rvm == null)
            {
                _rvm = new RecipeViewModel();
                await _rvm.LoadRecipesAsync();
            }

            return _rvm;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
