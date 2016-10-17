using System.Collections.ObjectModel;
using System.ComponentModel;
using InspireMe.DataProvider;
using InspireMe.DataProvider.Model;

namespace InspireMe.ViewModel
{
    public class QuoteViewModel : INotifyPropertyChanged
    {
        QuoteDataProvider dataProvider = new QuoteDataProvider();

        public QuoteViewModel()
        {
            this.QuoteList = new ObservableCollection<QuoteItem>(dataProvider.QuoteList);
        }

        /// <summary>
        /// Returns a random quote from the list of available quotes.
        /// </summary>
        /// <returns></returns>
        public string GetRandomQuote()
        {
            return dataProvider.GetRandomQuote();
        }
        

        public ObservableCollection<QuoteItem> QuoteList
        {
            get;
            set;
        }

        #region Notify Property Changed Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
