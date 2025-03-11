using System;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace YourShoppingAPP.Views
{
    public partial class MainPage : ContentPage
    {
        public static int ScannedItemCount { get; set; } = 0;

        public MainPage()
        {
            InitializeComponent();
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ScannedItemCount = ScannedItemCount;
                    UpdateBadgeCounter();
                });

            }
        }
        private async System.Threading.Tasks.Task AnimateBounce(View view)
        {
            await view.ScaleTo(1.2, 100, Easing.Linear);
            await view.ScaleTo(1.0, 100, Easing.Linear); 
        }
        private async void OnMenuClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Menu", "Cancel", null, "Cart Page", "Logout");

            if (action == "Cart Page")
            {
                await Navigation.PushAsync(new CartPage());
            }
            else if (action == "Logout")
            {
                bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
                if (confirm)
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
            }
        }
        private async void OnBarcodeScannerClicked(object sender, EventArgs e)
        {
            await AnimateBounce((Image)sender); 

            var scanner = new ZXingScannerPage();
            await Navigation.PushAsync(scanner);

            scanner.OnScanResult += (result) =>
            {
                scanner.IsScanning = false;
                scanner.OnScanResult -= null;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Success", "Barcode scanned successfully!", "OK");

                    CartPage.AddItem(result.Text);
                    ScannedItemCount = CartPage.CartItems.Sum(i => i.Quantity);
                    UpdateBadgeCounter();
                    MessagingCenter.Send(this, "UpdateCartBadge", ScannedItemCount);
                });
            };
        }
        private async void OnViewCartClicked(object sender, EventArgs e)
        {
            await AnimateBounce((Image)sender);
            await Navigation.PushAsync(new CartPage());
        }
        public void UpdateBadgeCounter()
        {
            CartBadgeLabel.Text = ScannedItemCount > 0 ? ScannedItemCount.ToString() : "";
            CartBadgeLabel.IsVisible = ScannedItemCount > 0;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            int updatedCount = CartPage.CartItems.Sum(i => i.Quantity);
            ScannedItemCount = updatedCount;
            UpdateBadgeCounter();
        }
    }
}
