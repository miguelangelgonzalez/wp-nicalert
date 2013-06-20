using System;
using System.Globalization;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NicAlert.Resources;

namespace NicAlert
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
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
                serviceDomain.SearchCompleted += ServiceDomainOnSearchCompleted;
                var domain = string.Concat(txtDomainName.Text, txtDomainType.SelectedItem);
                serviceDomain.Search(domain);
            }
            lblAlert.UpdateLayout();
        }

        private void ServiceDomainOnSearchCompleted(object sender, ServiceDomain.DomainStatusEventArgs domainStatusEventArgs)
        {
            lblAlert.Text = string.Empty;
            var status = domainStatusEventArgs.Status;
            IsBusy = false;

            if (status == HttpStatusCode.NotFound)
            {
                lblAlert.Text = AppResources.Message_domain_available;
                lblAlert.UpdateLayout();
            }
            if (status == HttpStatusCode.OK)
            {
                PhoneApplicationService.Current.State["DomainInfo"] = domainStatusEventArgs.DomainInfo;
                NavigationService.Navigate(new Uri("/DomainDetail.xaml", UriKind.Relative));
            }
        }
    }
}