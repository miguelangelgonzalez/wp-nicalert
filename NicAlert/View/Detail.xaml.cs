using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Phone.Controls;
using NicAlert.Resources;
using NicAlert.Support;
using NicAlert.ViewModel;

namespace NicAlert.View
{
    public partial class Detail : PhoneApplicationPage
    {
        public Detail()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("tp"))
            {
                var type = (TypeSearching) Enum.Parse(typeof(TypeSearching), NavigationContext.QueryString["tp"], false);

                var serviceDomain = new ServiceDomain();
                txtTitleInfo.Text = serviceDomain.GetTypesSearching().First(x => x.Value == type).Text;
                txtDetail.Blocks.Clear();

                switch (type)
                {
                    case TypeSearching.Entity:
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Name + ": ", App.Entity.Name));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("DNI: ", App.Entity.Dni));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("CUIT: ", App.Entity.Cuit));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Address + ": ", App.Entity.Address));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.City + ": ", App.Entity.City));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Country + ": ", App.Entity.Country));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Activity + ": ", App.Entity.Activity));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("Handle: ", App.Entity.Handle));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Type + ": ", App.Entity.Type));
                        break;
                    case TypeSearching.People:
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Name + ": ", App.People.Name));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("Handle: ", App.People.Handle));
                        break;
                    case TypeSearching.DnsServer:
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("Host: ", App.Dns.Host));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("IP: ", App.Dns.Ip));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Operator +": ", App.Dns.Operator));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Owner + ": ", App.Dns.Owner));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("Handle: ", App.Dns.Handle));
                        break;
                    case TypeSearching.TransactionByDomain:
                        foreach (var trans in App.Transactions)
                        {
                            txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Domain + ": ", trans.domain));
                            txtDetail.Blocks.Add(DomainDetail.GetParagraph("ID: ", trans.Id));
                            txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Status + ": ", trans.Status));
                            txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Description + ": ", trans.Description));
                            txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.CreatedAt + ": ", trans.CreatedAt));
                            txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Notes + ": ", trans.Notes));

                            var br = new Paragraph();
                            br.Inlines.Add(new LineBreak());
                            txtDetail.Blocks.Add(br);
                        }
                        break;
                    case TypeSearching.TransactionById:
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Domain + ": ", App.PendingTransaction.domain));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph("ID: ", App.PendingTransaction.Id));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Description + ": ", App.PendingTransaction.Description));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Status + ": ", App.PendingTransaction.Status));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.CreatedAt + ": ", App.PendingTransaction.CreatedAt));
                        txtDetail.Blocks.Add(DomainDetail.GetParagraph(AppResources.Notes + ": ", App.PendingTransaction.Notes));
                        break;
                    default:
                        MessageBox.Show(NicAlert.Resources.AppResources.Message_info_not_available, string.Empty, MessageBoxButton.OK);
                        break;
                }
            }
            else
            {
                MessageBox.Show(NicAlert.Resources.AppResources.Message_info_not_available, string.Empty, MessageBoxButton.OK);
            }
        }
    }
}