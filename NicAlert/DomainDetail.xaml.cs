using System.Windows.Documents;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NicAlert.Model;

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

            var domainInfo = PhoneApplicationService.Current.State["DomainInfo"] as DomainInfo;

            if(domainInfo == null) return;

            //entidad registrante
            txtEntity.Blocks.Clear();
            txtEntity.Blocks.Add(GetParagraph("Entidad Registrante: ", domainInfo.Contacts.Registrant.Name));
            txtEntity.Blocks.Add(GetParagraph("País: ", domainInfo.Contacts.Registrant.Country));
            txtEntity.Blocks.Add(GetParagraph("Actividad: ", domainInfo.Contacts.Registrant.Occupation));
            txtEntity.Blocks.Add(GetParagraph("Domicilio: ", domainInfo.Contacts.Registrant.Address));
            txtEntity.Blocks.Add(GetParagraph("Ciudad: ", domainInfo.Contacts.Registrant.City));
            txtEntity.Blocks.Add(GetParagraph("Provincia: ", domainInfo.Contacts.Registrant.Province));
            txtEntity.Blocks.Add(GetParagraph("Código Postal: ", domainInfo.Contacts.Registrant.ZipCode));
            txtEntity.Blocks.Add(GetParagraph("Teléfono: ", domainInfo.Contacts.Registrant.Phone));
            txtEntity.Blocks.Add(GetParagraph("Fax: ", domainInfo.Contacts.Registrant.Fax));

            //Persona Responble
            txtResponsable.Blocks.Clear();
            txtResponsable.Blocks.Add(GetParagraph("Persona Responsable: ", domainInfo.Contacts.Responsible.Name));
            txtResponsable.Blocks.Add(GetParagraph("Domicilio: ", domainInfo.Contacts.Responsible.Address));
            txtResponsable.Blocks.Add(GetParagraph("Ciudad: ", domainInfo.Contacts.Responsible.City));
            txtResponsable.Blocks.Add(GetParagraph("Código Postal: ", domainInfo.Contacts.Responsible.ZipCode));
            txtResponsable.Blocks.Add(GetParagraph("Provincia: ", domainInfo.Contacts.Responsible.Province));
            txtResponsable.Blocks.Add(GetParagraph("País: ", domainInfo.Contacts.Responsible.Country));
            txtResponsable.Blocks.Add(GetParagraph("Teléfono: ", domainInfo.Contacts.Responsible.Phone));
            txtResponsable.Blocks.Add(GetParagraph("Fax: ", domainInfo.Contacts.Responsible.Fax));
            txtResponsable.Blocks.Add(GetParagraph("Horario de contacto: ", domainInfo.Contacts.Responsible.WorkHours));

            //Entidad administradora
            txtEntityAdmin.Blocks.Clear();
            txtEntityAdmin.Blocks.Add(GetParagraph("Entidad Administradora: ", domainInfo.Contacts.Administrative.Name));
            txtEntityAdmin.Blocks.Add(GetParagraph("Domicilio: ", domainInfo.Contacts.Administrative.Name));
            txtEntityAdmin.Blocks.Add(GetParagraph("Ciudad: ", domainInfo.Contacts.Administrative.City));
            txtEntityAdmin.Blocks.Add(GetParagraph("Código Postal: ", domainInfo.Contacts.Administrative.ZipCode));
            txtEntityAdmin.Blocks.Add(GetParagraph("Provincia: ", domainInfo.Contacts.Administrative.Province));
            txtEntityAdmin.Blocks.Add(GetParagraph("País: ", domainInfo.Contacts.Administrative.Country));
            txtEntityAdmin.Blocks.Add(GetParagraph("Teléfono: ", domainInfo.Contacts.Administrative.Phone));
            txtEntityAdmin.Blocks.Add(GetParagraph("Fax: ", domainInfo.Contacts.Administrative.Fax));
            txtEntityAdmin.Blocks.Add(GetParagraph("Actividad: ", domainInfo.Contacts.Administrative.Activity));

            //Contacto Técnico
            txtContact.Blocks.Clear();
            txtContact.Blocks.Add(GetParagraph("Contacto Técnico: ", domainInfo.Contacts.Technical.Name));
            txtContact.Blocks.Add(GetParagraph("Domicilio: ", domainInfo.Contacts.Technical.Address));
            txtContact.Blocks.Add(GetParagraph("Ciudad: ", domainInfo.Contacts.Technical.City));
            txtContact.Blocks.Add(GetParagraph("Código Postal: ", domainInfo.Contacts.Technical.ZipCode));
            txtContact.Blocks.Add(GetParagraph("Provincia: ", domainInfo.Contacts.Technical.Province));
            txtContact.Blocks.Add(GetParagraph("País: ", domainInfo.Contacts.Technical.Country));
            txtContact.Blocks.Add(GetParagraph("Teléfono: ", domainInfo.Contacts.Technical.Phone));
            txtContact.Blocks.Add(GetParagraph("Fax: ", domainInfo.Contacts.Technical.Fax));
            txtContact.Blocks.Add(GetParagraph("Horario de contacto: ", domainInfo.Contacts.Technical.WorkHours));

            //Servidores DNS
            txtDns.Blocks.Clear();
            txtDns.Blocks.Add(GetItalicParagraph("DNS Primario: ", "Nombre: " + domainInfo.DnsServers.Primary.Host , "Dirección IP: " + domainInfo.DnsServers.Primary.Ip));
            txtDns.Blocks.Add(GetItalicParagraph("DNS Secundario: ", "Nombre: " + domainInfo.DnsServers.Secondary.Host , "Dirección IP: " + domainInfo.DnsServers.Secondary.Ip));
            txtDns.Blocks.Add(GetItalicParagraph("Servidor alternativo:  ", "Nombre: " + domainInfo.DnsServers.Alternate1.Host , "Dirección IP: " + domainInfo.DnsServers.Alternate1.Ip));
            txtDns.Blocks.Add(GetItalicParagraph("Servidor alternativo:  ", "Nombre: " + domainInfo.DnsServers.Alternate2.Host , "Dirección IP: " + domainInfo.DnsServers.Alternate2.Ip));
            txtDns.Blocks.Add(GetItalicParagraph("Servidor alternativo:  ", "Nombre: " + domainInfo.DnsServers.Alternate3.Host , "Dirección IP: " + domainInfo.DnsServers.Alternate3.Ip));

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