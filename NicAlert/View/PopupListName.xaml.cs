using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NicAlert.View
{
    public partial class ListName : UserControl
    {
        public string NameSelected { get; protected set; }

        public ListName()
        {
            InitializeComponent();
        }

        public ListName(IEnumerable<string> list):this()
        {
            lstPeopleNames.ItemsSource = list;
        }

        private void PeopleNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                NameSelected = e.AddedItems[0] as string;
                Button_Click(null, null);
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var popup = Parent as Popup;
            if (popup != null) popup.IsOpen = false;
        }
    }
}