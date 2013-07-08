using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using NicAlert.Resources;
using NicAlert.Support;
using NicAlert.ViewModel;

namespace NicAlert
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly Dictionary<TypeSearching, Uri> _dictionaryNavigation = new Dictionary<TypeSearching, Uri>
        {
            {TypeSearching.Domain,  new Uri("/View/DomainDetail.xaml", UriKind.Relative)},
            {TypeSearching.TransactionByDomain,  new Uri("/View/TransactionDetail.xaml", UriKind.Relative)},
            {TypeSearching.TransactionById,  new Uri("/View/TransactionDetail.xaml", UriKind.Relative)},
            {TypeSearching.Entity,  new Uri("/View/EntityDetail.xaml", UriKind.Relative)},
            {TypeSearching.People,  new Uri("/View/EntityDetail.xaml", UriKind.Relative)},
            {TypeSearching.DnsServer,  new Uri("/View/EntityDetail.xaml", UriKind.Relative)}
                
        };

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            var serviceDomain = new ServiceDomain();
            lstTypesSearching.ItemsSource = serviceDomain.GetTypesSearching();

            Loaded += (sender, args) =>
            {
                lstTypesDomain.IsEnabled = false;
                              
                serviceDomain.StatusCompleted += (o, eventArgs) =>
                {
                    lstTypesDomain.IsEnabled = true;

                    lstTypesDomain.ItemsSource = App.DomainTypes;
                };
                serviceDomain.GetDomainTypes();
            };
        }

        public bool IsBusy
        {
            set
            {
                progressBar.IsIndeterminate = value;
                txtName.IsEnabled = !value;
                lstTypesDomain.IsEnabled = !value;
                btnSearch.IsEnabled = !value;
            }
        }

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
            
            lblAlert.Text = string.Empty;

            if (string.IsNullOrEmpty(txtName.Text))
            { 
                lblAlert.Text = AppResources.Message_text_must_be_greater_than_one;
            }
            else if (txtName.Text.Length > 19)
            {
                lblAlert.Text = AppResources.Message_text_must_be_less_than_19_chars;
            }
            else
            {
                IsBusy = true;
                var term = string.Concat(txtName.Text, lstTypesDomain.SelectedItem);
                var serviceDomain = new ServiceDomain();
                serviceDomain.StatusCompleted += StatusCompleted;
                
                var typeSearching = lstTypesSearching.SelectedItem as TypeSearchingViewModel;
                if (typeSearching != null) serviceDomain.Search(term, typeSearching.Value);
            }

            lblAlert.UpdateLayout();
        }



        private void StatusCompleted(object sender, StatusEventArgs eventArgs)
        {
            lblAlert.Text = string.Empty;
            IsBusy = false;
            var typeSearching = lstTypesSearching.SelectedItem as TypeSearchingViewModel;

            if (typeSearching != null)
            {
                switch (eventArgs.Status)
                {
                    case HttpStatusCode.OK:
                        NavigationService.Navigate(_dictionaryNavigation[typeSearching.Value]);
                        break;
                    case HttpStatusCode.NotFound:
                        lblAlert.Text = typeSearching.Value == TypeSearching.Domain ? AppResources.Message_domain_available : AppResources.NotFound;
                        break;
                    case HttpStatusCode.NoContent:
                        lblAlert.Text = AppResources.Message_domain_available;
                        break;
                    default:
                        lblAlert.Text = AppResources.Unknow_Error;
                        break;
                }
            }
            else
            {
                lblAlert.Text = AppResources.Unknow_Error;
            }

            lblAlert.UpdateLayout();
        }

        private void LstTypesSearchingSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as TypeSearchingViewModel;

            if (item != null && (item.Value == TypeSearching.Domain || item.Value == TypeSearching.TransactionByDomain))
            {
                lstTypesSearching.IsEnabled = false;
            }
            else
            {
                lstTypesSearching.IsEnabled = true;
            }
        }
    }
}