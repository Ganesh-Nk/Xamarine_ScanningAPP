using Xamarin.Forms;
using YourShoppingAPP.Views;

namespace YourShoppingAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
