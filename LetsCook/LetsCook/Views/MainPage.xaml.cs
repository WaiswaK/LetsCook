using LetsCook.Model;
using LetsCook.ViewModels;
using Xamarin.Forms;

namespace LetsCook.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext == null)
                BindingContext = await ((App)Application.Current).GetRecipeViewModelAsync();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            ((RecipeViewModel)BindingContext).Recipe = (Recipe)args.Item;
            await Navigation.PushAsync(new RecipePage());
        }
    }
}
