using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace YourShoppingAPP.Views
{
    public partial class AddItemPage : ContentPage
    {
        private int scannedItemCount = 0;

        public AddItemPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<CartPage, int>(this, "UpdateCartBadge", (sender, itemCount) =>
            {
                scannedItemCount = itemCount;
                UpdateBadgeCounter();
            });
        }
        public void UpdateBadgeCounter()
        {
            CartBadgeLabel.Text = scannedItemCount > 0 ? scannedItemCount.ToString() : "";
            CartBadgeLabel.IsVisible = scannedItemCount > 0;
        }

        private async void OnAddItemsClicked(object sender, EventArgs e)
        {
            var scanner = new ZXingScannerPage();
            await Navigation.PushAsync(scanner);

            scanner.OnScanResult += (result) =>
            {
                scanner.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Success", "Item scanned successfully!", "OK");
                    scannedItemCount++;
                    UpdateCartBadge();
                    CartPage.AddItem(result.Text);
                });
            };
        }
        private void UpdateCartBadge()
        {
            if (scannedItemCount > 0)
            {
                CartBadgeLabel.Text = scannedItemCount.ToString();
                CartBadgeLabel.IsVisible = true;
            }
            else
            {
                CartBadgeLabel.IsVisible = false;
            }
        }
        private async void OnViewCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage());
        }
    }
}
