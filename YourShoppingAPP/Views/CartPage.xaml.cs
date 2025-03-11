using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using Xamarin.Forms;

namespace YourShoppingAPP.Views
{
    public partial class CartPage : ContentPage
    {
        public static ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();

        public CartPage()
        {
            InitializeComponent();
            CartListView.ItemsSource = CartItems;
            UpdateTotalItems();
        }

        public static void AddItem(string barcode)
        {
            var existingItem = CartItems.FirstOrDefault(item => item.Barcode == barcode);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem { Barcode = barcode, Quantity = 1 });
            }
        }
        private void OnRemoveItemClicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            var barcode = button.CommandParameter as string;

            var item = CartItems.FirstOrDefault(i => i.Barcode == barcode);
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    CartItems.Remove(item);
                }
            }

            UpdateTotalItems();
            int updatedTotal = CartItems.Sum(i => i.Quantity);
            MessagingCenter.Send(this, "UpdateCartBadge", updatedTotal);
        }
        private void UpdateTotalItems()
        {
            int total = CartItems.Sum(item => item.Quantity);
            TotalItemsLabel.Text = $"Total Items: {total}";
        }
    }

    public class CartItem : INotifyPropertyChanged
    {
        public string Barcode { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
