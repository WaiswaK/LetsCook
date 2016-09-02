using Xamarin.Forms;

namespace LetsCook.Views
{
    public partial class RecipePage : ContentPage
    {
        public RecipePage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = (await ((App)Application.Current).GetRecipeViewModelAsync()).Recipe;
        }
    }
}
