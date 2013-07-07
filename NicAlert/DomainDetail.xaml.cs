using System.Windows.Documents;
using Microsoft.Phone.Controls;
using NicAlert.Resources;

namespace NicAlert
{
    public partial class DomainDetail : PhoneApplicationPage
    {
        public DomainDetail()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var domainInfo = App.DomainInfo;

            if(domainInfo == null) return;

            //entidad registrante
            txtEntity.Blocks.Clear();
            txtEntity.Blocks.Add(GetParagraph(AppResources.Registering_Entity + ": ", domainInfo.Contacts.Registrant.Name));
            txtEntity.Blocks.Add(GetParagraph(AppResources.Country, domainInfo.Contacts.Registrant.Country));
            txtEntity.Blocks.Add(GetParagraph(AppResources.Activity, domainInfo.Contacts.Registrant.Occupation));
            txtEntity.Blocks.Add(GetParagraph(AppResources.Address, domainInfo.Contacts.Registrant.Address));
            txtEntity.Blocks.Add(GetParagraph(AppResources.City, domainInfo.Contacts.Registrant.City));
            txtEntity.Blocks.Add(GetParagraph(AppResources.State, domainInfo.Contacts.Registrant.Province));
            txtEntity.Blocks.Add(GetParagraph(AppResources.Zip_Code, domainInfo.Contacts.Registrant.ZipCode));
            txtEntity.Blocks.Add(GetParagraph(AppResources.Phone, domainInfo.Contacts.Registrant.Phone));
            txtEntity.Blocks.Add(GetParagraph(AppResources.Fax, domainInfo.Contacts.Registrant.Fax));

            //Persona Responble
            txtResponsable.Blocks.Clear();
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Person_in_charge + ": ", domainInfo.Contacts.Responsible.Name));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Address, domainInfo.Contacts.Responsible.Address));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.City, domainInfo.Contacts.Responsible.City));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Zip_Code, domainInfo.Contacts.Responsible.ZipCode));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.State, domainInfo.Contacts.Responsible.Province));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Country, domainInfo.Contacts.Responsible.Country));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Phone, domainInfo.Contacts.Responsible.Phone));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Fax, domainInfo.Contacts.Responsible.Fax));
            txtResponsable.Blocks.Add(GetParagraph(AppResources.Contact_Hours, domainInfo.Contacts.Responsible.WorkHours));

            //Entidad administradora
            txtEntityAdmin.Blocks.Clear();
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Managing_Entity + ": ", domainInfo.Contacts.Administrative.Name));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Address, domainInfo.Contacts.Administrative.Name));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.City, domainInfo.Contacts.Administrative.City));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Zip_Code, domainInfo.Contacts.Administrative.ZipCode));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.State, domainInfo.Contacts.Administrative.Province));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Country, domainInfo.Contacts.Administrative.Country));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Phone, domainInfo.Contacts.Administrative.Phone));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Fax, domainInfo.Contacts.Administrative.Fax));
            txtEntityAdmin.Blocks.Add(GetParagraph(AppResources.Activity, domainInfo.Contacts.Administrative.Activity));

            //Contacto Técnico
            txtContact.Blocks.Clear();
            txtContact.Blocks.Add(GetParagraph(AppResources.Technical_Contact + ": ", domainInfo.Contacts.Technical.Name));
            txtContact.Blocks.Add(GetParagraph(AppResources.Address, domainInfo.Contacts.Technical.Address));
            txtContact.Blocks.Add(GetParagraph(AppResources.City, domainInfo.Contacts.Technical.City));
            txtContact.Blocks.Add(GetParagraph(AppResources.Zip_Code, domainInfo.Contacts.Technical.ZipCode));
            txtContact.Blocks.Add(GetParagraph(AppResources.State, domainInfo.Contacts.Technical.Province));
            txtContact.Blocks.Add(GetParagraph(AppResources.Country, domainInfo.Contacts.Technical.Country));
            txtContact.Blocks.Add(GetParagraph(AppResources.Phone, domainInfo.Contacts.Technical.Phone));
            txtContact.Blocks.Add(GetParagraph(AppResources.Fax, domainInfo.Contacts.Technical.Fax));
            txtContact.Blocks.Add(GetParagraph(AppResources.Contact_Hours, domainInfo.Contacts.Technical.WorkHours));

            //Servidores DNS
            txtDns.Blocks.Clear();
            txtDns.Blocks.Add(GetItalicParagraph(AppResources.Primary_dns, AppResources.Name + ": " + domainInfo.DnsServers.Primary.Host, AppResources.IP_Address + domainInfo.DnsServers.Primary.Ip));
            txtDns.Blocks.Add(GetItalicParagraph(AppResources.Secundary_dns, AppResources.Name + ": " + domainInfo.DnsServers.Secondary.Host, AppResources.IP_Address + domainInfo.DnsServers.Secondary.Ip));
            txtDns.Blocks.Add(GetItalicParagraph(AppResources.Alternative_dns, AppResources.Name + ": " + domainInfo.DnsServers.Alternate1.Host, AppResources.IP_Address + domainInfo.DnsServers.Alternate1.Ip));
            txtDns.Blocks.Add(GetItalicParagraph(AppResources.Alternative_dns, AppResources.Name + ": " + domainInfo.DnsServers.Alternate2.Host, AppResources.IP_Address + domainInfo.DnsServers.Alternate2.Ip));
            txtDns.Blocks.Add(GetItalicParagraph(AppResources.Alternative_dns, AppResources.Name + ": " + domainInfo.DnsServers.Alternate3.Host, AppResources.IP_Address + domainInfo.DnsServers.Alternate3.Ip));

        }
        
        private Paragraph GetParagraph(string leftText, string rightText)
        {
            var paragraph = new Paragraph();
            var bold = new Bold();
            bold.Inlines.Add(new Run() { Text = leftText});
            paragraph.Inlines.Add(bold);

            if (!string.IsNullOrEmpty(rightText))
            {
                paragraph.Inlines.Add(new Run() { Text = rightText });    
            }
            paragraph.Inlines.Add(new LineBreak());
            return paragraph;
        }

        private Paragraph GetItalicParagraph(string leftText, string rightText, string rightText2)
        {
            var paragraph = new Paragraph();
            var bold = new Bold();
            var italic = new Italic();

            bold.Inlines.Add(new Run() { Text = leftText });
            paragraph.Inlines.Add(bold);
            paragraph.Inlines.Add(new LineBreak());
            
            italic.Inlines.Add(new Run() { Text = rightText });
            italic.Inlines.Add(new LineBreak());
            italic.Inlines.Add(new Run() { Text = rightText2 });
            paragraph.Inlines.Add(italic);
            paragraph.Inlines.Add(new LineBreak());

            return paragraph;
        }
    }
}