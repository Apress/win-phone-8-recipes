using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using FileManagement.ViewModels;

namespace FileManagement
{
    public partial class FileDetails : PhoneApplicationPage
    {
        FileViewModel viewModel = new FileViewModel();

        public FileDetails()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            string fileName;
            NavigationContext.QueryString.TryGetValue("fn", out fileName);       
            LoadFile(fileName);

            base.OnNavigatedTo(e);
        }

        private async void LoadFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                viewModel.IsNew = false;
                FileNameText.IsReadOnly = true;
                await viewModel.LoadFile(fileName);
            }
            else
            {
                viewModel.IsNew = true;
            }
        }

        private async void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            UpdateTextBoxBinding();

            if (!string.IsNullOrEmpty(viewModel.FileName))
            {
                bool cancelAction = false;
                if (viewModel.IsNew && App.ViewModel.Files.Where(f => f.Name == viewModel.FileName).Count() > 0)
                {
                    MessageBoxResult result = MessageBox.Show("A file already exists with that name. Would you like to overwrite this file?", 
                        "Overwrite Existing File", 
                        MessageBoxButton.OKCancel);
                    cancelAction = (result == MessageBoxResult.Cancel);
                }

                if (!cancelAction)
                {
                    await viewModel.SaveFile();
                    MessageBox.Show("Your text file has been saved!");

                    //return to main screen once file has been saved
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("enter a file name", "file name required", MessageBoxButton.OK);
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
    }
}