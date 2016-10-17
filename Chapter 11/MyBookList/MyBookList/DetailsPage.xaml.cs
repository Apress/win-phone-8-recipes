using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyBookList.Resources;
using MyBookList.ViewModels;
using System.Windows.Input;

namespace MyBookList
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private BookViewModel _bookViewModel;
        
        public DetailsPage()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int bookId = -1;
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("bookId", out selectedIndex))
            {
                bookId = int.Parse(selectedIndex);
                _bookViewModel = new BookViewModel(bookId);
            }
            else
            {
                _bookViewModel = new BookViewModel();
            }

            DataContext = _bookViewModel;
            EnableControls();
        }

        private void EnableControls()
        {
            if (ApplicationBar.Buttons.Count > 1)
            {
                ApplicationBarIconButton saveButton = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                ApplicationBarIconButton addNoteButton = (ApplicationBarIconButton)ApplicationBar.Buttons[1];

                if (_bookViewModel.Book.BookId > 0)
                {
                    ApplicationBar.Buttons.Remove(saveButton);
                    addNoteButton.IsEnabled = true;
                }
                else
                {
                    saveButton.IsEnabled = true;
                    addNoteButton.IsEnabled = false;
                }
            }
           

            titleText.IsEnabled = authorText.IsEnabled = (_bookViewModel.Book.BookId <= 0);            
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            UpdateTextBoxBinding();

            ApplicationBarIconButton btn = (ApplicationBarIconButton)sender;

            switch (btn.Text)
            {
                case "add note":
                    _bookViewModel.InitNewNote();
                    popupNote.IsOpen = true;
                    noteText.Focus();
                    break;
                case "save":
                    _bookViewModel.Save();
                    EnableControls();
                    break;
            }            
        }

        private void UpdateTextBoxBinding()
        {
            //force an update of the binding for the textbox that has focus 
            //so the current text is reflected in the viewmodel property
            object focusedElement = FocusManager.GetFocusedElement();
            if (focusedElement != null && focusedElement is TextBox)
            {
                TextBox textBox = (TextBox)focusedElement;
                var binding = textBox.GetBindingExpression(TextBox.TextProperty);
                binding.UpdateSource();
            }

        }
        
        private void addButton_Click_1(object sender, RoutedEventArgs e)
        {
            popupNote.IsOpen = false;
            _bookViewModel.AddNewNote();
            NotesLongListSelector.ItemsSource = _bookViewModel.Notes;

            if (bookInfoPivot.SelectedIndex != 1)
            {
                bookInfoPivot.SelectedIndex = 1;
            }
        }

        private void cancelButton_Click_1(object sender, RoutedEventArgs e)
        {            
            popupNote.IsOpen = false;
            _bookViewModel.NewNote = null;
        }

    }
}