using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourShoppingAPP.ViewModels;

namespace YourShoppingAPP.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        private void OnLoginClicked(object sender, System.EventArgs e)
        {
            var vm = (LoginViewModel)BindingContext;
            vm.LoginCommand.Execute(null);
        }
    }
}