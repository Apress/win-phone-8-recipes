using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;

namespace AppointmentsAndContacts.Views
{
    public partial class AppointmentsView : PhoneApplicationPage
    {


        public AppointmentsView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AppointmentsLoaded(object sender, RoutedEventArgs e)
        {

            GetNextSevenDays();

        }

        private ObservableCollection<Appointment> appointments
            = new ObservableCollection<Appointment>();

        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return appointments;
            }
            set
            {
                appointments = value;
            }
        }

        private void AddAppointmentClick(object sender, EventArgs e)
        {
            SaveAppointmentTask newAppointment = new SaveAppointmentTask();
            newAppointment.StartTime = startDatePicker.Value;
            newAppointment.EndTime = endDatePicker.Value;
            newAppointment.Location = locationTextBox.Text;
            newAppointment.Subject = "The CODEZ";
            newAppointment.Show();


        }

        private void SearchAppointmentsClick(object sender, EventArgs e)
        {
            GetAppointmentsByDateRange(
                startDatePicker.Value ?? DateTime.Today,
                endDatePicker.Value ?? DateTime.Today.AddDays(7));

        }

        private void GetAppointmentsByDateRange(DateTime startOfRange, DateTime endOfRange)
        {
            Appointments apps = new Appointments();

            apps.SearchCompleted += AppointmentsSearchCompleted;

            apps.SearchAsync(startOfRange,
                endOfRange,
                null);
        }

        void AppointmentsSearchCompleted(object sender, AppointmentsSearchEventArgs e)
        {
            Appointments.Clear();
            foreach (Appointment appointment in e.Results)
            {
                Appointments.Add(appointment);

            }
        }


        private void GetNextSevenDays()
        {
            GetAppointmentsByDateRange(DateTime.Today, DateTime.Today.AddDays(7));

        }

    }
}