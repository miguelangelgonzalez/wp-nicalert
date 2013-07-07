using System;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using NicAlert.Resources;
using NicAlert.Support;

namespace NicAlert
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
                          {
                              txtDomainType.IsEnabled = false;
                              ServiceDomain serviceDomain = new ServiceDomain();
                              serviceDomain.StatusCompleted += (o, eventArgs) =>
                                                                   {
                                                                       txtDomainType.IsEnabled = true;

                                                                       txtDomainType.ItemsSource = App.DomainTypes;
                                                                   };
                              serviceDomain.GetDomainTypes();
                          };
        }

        public bool IsBusy
        {
            set
            {
                progressBar.IsIndeterminate = value;
                txtDomainName.IsEnabled = !value;
                txtDomainType.IsEnabled = !value;
                btnSearch.IsEnabled = !value;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            lblAlert.Text = string.Empty;

            if (string.IsNullOrEmpty(txtDomainName.Text))
            { 
                lblAlert.Text = AppResources.Message_text_must_be_greater_than_one;
            }
            else if (txtDomainName.Text.Length > 19)
            {
                lblAlert.Text = AppResources.Message_text_must_be_less_than_19_chars;
            }
            else
            {
                IsBusy = true;
                var serviceDomain = new ServiceDomain();
                serviceDomain.StatusCompleted += StatusCompleted;
                var domain = string.Concat(txtDomainName.Text, txtDomainType.SelectedItem);
                serviceDomain.Search(domain);
            }
            lblAlert.UpdateLayout();
        }

        private void StatusCompleted(object sender, StatusEventArgs eventArgs)
        {
            lblAlert.Text = string.Empty;
            IsBusy = false;

            if (eventArgs.Status == HttpStatusCode.OK)
            {
                NavigationService.Navigate(new Uri("/DomainDetail.xaml", UriKind.Relative));
            }
            else if (eventArgs.Status == HttpStatusCode.NotFound)
            {
                lblAlert.Text = AppResources.Message_domain_available;
                lblAlert.UpdateLayout();
            }
            else
            {
                lblAlert.Text = "The service is unvailable, please try later.";
                lblAlert.UpdateLayout();
            }
        }
    }
}