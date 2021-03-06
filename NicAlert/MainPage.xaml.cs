﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using NicAlert.Resources;
using NicAlert.Support;
using NicAlert.View;
using NicAlert.ViewModel;

namespace NicAlert
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Private Members
        private Popup _popup;
        #endregion

        #region Private Methods
        private void ShowPopup()
        {
            _popup = new Popup {Child = new PopupSplash(), IsOpen = true};
        }
        private void HidePopup()
        {
            if(_popup != null)
            {
                _popup.IsOpen = false;
            }
        }
        private void StartLoadingData()
        {
            ShowPopup();

            var serviceDomain = new ServiceDomain();
            //loading searching type
            lstTypesSearching.ItemsSource = serviceDomain.GetTypesSearching();
            //loading domain types
            serviceDomain.StatusCompleted += (o, eventArgs) =>
            {
                if (eventArgs.Status == HttpStatusCode.NotFound)
                {
                    MessageBox.Show(AppResources.Message_Error_Conection, AppResources.Message_Error_Conection_Caption, MessageBoxButton.OK);
                }
                lstTypesDomain.ItemsSource = App.DomainTypes;
                HidePopup();
            };
            serviceDomain.GetDomainTypes();
        }

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
            lblAlert.Text = string.Empty;
            var typeSearching = lstTypesSearching.SelectedItem as TypeSearchingViewModel;

            if (typeSearching == null)
            {
                lblAlert.Text = AppResources.Unknow_Error;
            }
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                lblAlert.Text = AppResources.Message_text_must_be_greater_than_one;
            }
            else if ((typeSearching.Value == TypeSearching.Domain || typeSearching.Value == TypeSearching.TransactionByDomain) && txtName.Text.Length > 19)
            {
                lblAlert.Text = AppResources.Message_text_must_be_less_than_19_chars;
            }
            else
            {
                ShowPopup();

                var serviceDomain = new ServiceDomain();
                serviceDomain.StatusCompleted += StatusCompleted;

                string term;

                if (typeSearching.Value == TypeSearching.Domain || typeSearching.Value == TypeSearching.TransactionByDomain)
                {
                    term = string.Concat(txtName.Text, lstTypesDomain.SelectedItem);
                }
                else
                {
                    term = txtName.Text;
                }

                serviceDomain.Search(term, typeSearching.Value);
            }

            lblAlert.UpdateLayout();
        }

        private void StatusCompleted(object sender, StatusEventArgs eventArgs)
        {
            lblAlert.Text = string.Empty;
            HidePopup();
            var typeSearching = lstTypesSearching.SelectedItem as TypeSearchingViewModel;

            if (typeSearching != null)
            {
                switch (eventArgs.Status)
                {
                    case HttpStatusCode.OK:
                        NavigationService.Navigate(typeSearching.Value == TypeSearching.Domain
                                                       ? new Uri("/View/DomainDetail.xaml", UriKind.Relative)
                                                       : new Uri("/View/Detail.xaml?tp=" + typeSearching.Value, UriKind.Relative));
                        break;
                    case HttpStatusCode.NotFound:
                        if (typeSearching.Value == TypeSearching.Domain)
                        {
                            lblAlert.Text = AppResources.Message_domain_available;
                        }
                        else
                        {
                            lblAlert.Text = AppResources.NotFound;
                        }

                        break;
                    case HttpStatusCode.NoContent:
                        if (typeSearching.Value == TypeSearching.TransactionByDomain || typeSearching.Value == TypeSearching.TransactionById)
                        {
                            lblAlert.Text = AppResources.MessageNoFoundTransaction;
                        }
                        else
                        {
                            lblAlert.Text = AppResources.Message_info_not_available;
                        }
                        break;
                    case HttpStatusCode.NotAcceptable:
                        lblAlert.Text = AppResources.MessageNotAcceptable;
                        break;
                    case HttpStatusCode.BadRequest:
                        if (typeSearching.Value == TypeSearching.Entity || typeSearching.Value == TypeSearching.People)
                        {
                            var items = eventArgs.Message as IEnumerable;
                            var popup = new Popup
                            {
                                Child = new ListName(items.OfType<string>()),
                                IsOpen = true
                            };
                            popup.Closed += (o, args) =>
                            {
                                var ctrl = ((Popup)o).Child as ListName;
                                if (ctrl != null && !string.IsNullOrEmpty(ctrl.NameSelected))
                                {
                                    ShowPopup();
                                    var serviceDomain = new ServiceDomain();
                                    serviceDomain.StatusCompleted += (sender1, statusEventArgs) =>
                                    {
                                        if (statusEventArgs.Status == HttpStatusCode.OK)
                                        {
                                            NavigationService.Navigate(new Uri("/View/Detail.xaml?tp=" + typeSearching.Value, UriKind.Relative));
                                        }
                                        HidePopup();
                                    };
                                    serviceDomain.Search(ctrl.NameSelected, typeSearching.Value);
                                }
                            };
                        }
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        MessageBox.Show(AppResources.ServiceUnavailable, AppResources.Message_Error_Conection, MessageBoxButton.OK);
                        break;
                    default:
                        MessageBox.Show(AppResources.Unknow_Error, AppResources.Message_Error_Conection, MessageBoxButton.OK);
                        break;
                }
            }
            else
            {
                MessageBox.Show(AppResources.Unknow_Error, AppResources.Message_Error_Conection, MessageBoxButton.OK);
            }

            lblAlert.UpdateLayout();
        }

        private void LstTypesSearchingSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.RemovedItems.Count > 0)
            {
                var item = e.AddedItems[0] as TypeSearchingViewModel;
                lstTypesDomain.IsEnabled = (item != null && (item.Value == TypeSearching.Domain || item.Value == TypeSearching.TransactionByDomain));
            }
        }

        #endregion

        #region Constructors
        public MainPage()
        {
            InitializeComponent();
            
            StartLoadingData();
        }

        #endregion
    }
}