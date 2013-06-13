using System;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace NicAlert
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lblAlert.Text = string.Empty;

            if (string.IsNullOrEmpty(txtDomainName.Text))
            {
                lblAlert.Text = "The text must be greater than or equal to 1 char";
            }
            else if (txtDomainName.Text.Length > 19)
            {
                lblAlert.Text = "The text must be less than or equal to 19 chars";
            }
            else
            {
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

            if (status == HttpStatusCode.NotFound)
            {
                lblAlert.Text = "The domain is available";
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