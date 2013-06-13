using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class DomainInfo
    {
        [DataMember(Name = "status")]
        public Status Status { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        [DataMember(Name = "created_on")]
        public string CreatedOn { get; set; }
        [DataMember(Name = "expires_on")]
        public string ExpiresOn { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "contacts")]
        public Contacts Contacts { get; set; }
        [DataMember(Name = "dns_servers")]
        public DnsServers DnsServers { get; set; }
    }
}