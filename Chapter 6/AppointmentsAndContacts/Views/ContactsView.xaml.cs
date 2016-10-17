using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Phone.PersonalInformation;

namespace AppointmentsAndContacts.Views
{
    public partial class ContactsView : PhoneApplicationPage
    {

        public ContactsView()
        {
            InitializeComponent();
            Loaded += ContactsViewLoaded;
        }

        void ContactsViewLoaded(object sender, RoutedEventArgs e)
        {
            Contacts = new ObservableCollection<Contact>();
            Accounts = new ObservableCollection<Account>();
            DataContext = this;

            GetContacts(string.Empty);
            GetAccounts();

        }

        private void SearchContactsClick(object sender, EventArgs e)
        {
            GetContacts(searchTextBox.Text);


        }

        private void GetAccounts()
        {
            Accounts.Clear();
            Contacts contacts = new Contacts();
            foreach (Account account in contacts.Accounts)
            {
                Accounts.Add(account);
            }

        }

        private void GetContacts(string searchString)
        {
            Contacts searchContacts = new Contacts();

            searchContacts.SearchCompleted += SearchContactsSearchCompleted;
            if (string.IsNullOrEmpty(searchString))
            {
                // FilterKind.None returns all contacts
                searchContacts.SearchAsync(string.Empty, FilterKind.None, "Search Contacts");
            }
            else
            {
                searchContacts.SearchAsync(string.Empty, FilterKind.DisplayName, "Search Contacts");
            }
        }

        void SearchContactsSearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            Contacts.Clear();
            foreach (Contact contact in e.Results)
            {
                Contacts.Add(contact);
            }
        }

        private void NewContactClick(object sender, EventArgs e)
        {
            SaveContactTask saveContact = new SaveContactTask();
            saveContact.FirstName = firstNameTextBox.Text;
            saveContact.LastName = lastNameTextBox.Text;
            saveContact.Completed += SaveContactCompleted;
            saveContact.Show();

        }

        void SaveContactCompleted(object sender, SaveContactResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show("Contact Added");
            }
            else if (e.TaskResult == TaskResult.Cancel)
            {
                MessageBox.Show("Contact not added. You cancelled it.");
            }
        }

        private async void NewCustomContactClick(object sender, EventArgs e)
        {
            ContactStore store = await ContactStore.CreateOrOpenAsync();
            StoredContact contact = new StoredContact(store);
            contact.RemoteId = Guid.NewGuid().ToString();
            contact.FamilyName = "Totzke";
            contact.GivenName = "Dave";

            IDictionary<string, object> props = await contact.GetExtendedPropertiesAsync();
            props.Add("FavoriteColor", "CobaltBlue");

            await contact.SaveAsync();
            GetAccounts();
            GetContacts(string.Empty);

        }

        #region Properties

        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
            }
        }

        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get
            {
                return accounts;
            }
            set
            {
                accounts = value;
            }
        }

        private ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
            }
        }
        #endregion

    }

}