using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using YourShoppingAPP.Views;

namespace YourShoppingAPP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _customerId;
        private string _password;

        public string CashierIdEntry
        {
            get => _customerId;
            set
            {
                _customerId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CashierIdEntry)));
            }
        }

        public string PasswordEntry
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PasswordEntry)));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(CashierIdEntry) || string.IsNullOrWhiteSpace(PasswordEntry))
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Customer ID and Password are required.", "OK");
                return;
            }

           
            if (CashierIdEntry == "admin" && PasswordEntry == "1234")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid credentials. Try again.", "OK");
            }
        }
    }
}