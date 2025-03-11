using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YourShoppingAPP.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        private static CartViewModel _instance;
        public static CartViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CartViewModel();
                }
                return _instance;
            }
        }
        public ObservableCollection<string> CartItems { get; set; } = new ObservableCollection<string>();

        public CartViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
