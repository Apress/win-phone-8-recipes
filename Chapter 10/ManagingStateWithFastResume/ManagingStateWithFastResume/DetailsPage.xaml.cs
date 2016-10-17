using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using ManagingStateWithFastResume.ViewModels;

namespace ManagingStateWithFastResume
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private ItemViewModel itemViewModel = null;
        private bool pageReconstructed = false;

        public DetailsPage()
        {
            InitializeComponent();
            pageReconstructed = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //If the page constuctor has been called, check State dictionary for
            //the page's ViewModel. If one exists, restore the page state. 
            //If not, then this is a new instance.
            if (pageReconstructed)
            {
                if (itemViewModel == null)
                {
                    if (State.Count > 0)
                    {
                        itemViewModel = (ItemViewModel)State["ViewModel"];
                    }
                    else
                    {
                        string selectedIndex = "";
                        if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                        {
                            int index = int.Parse(selectedIndex);
                            itemViewModel = App.ViewModel.Items[index];
                        }
                    }
                }
                DataContext = itemViewModel;
            }

            pageReconstructed = false;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //If this is a back navigation, just skip over it since the page will be discarded anyway
            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                //Save the ItemViewModel in the page's State dictionary.
                State["ViewModel"] = itemViewModel;
            }
        }

    }
}