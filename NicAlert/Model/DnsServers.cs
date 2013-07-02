using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class DnsServers
    {
        [DataMember(Name = "primary")]
        public Dns Primary { get; set; }
        [DataMember(Name = "secondary")]
        public Dns Secondary { get; set; }
        [DataMember(Name = "alternate1")]
        public Dns Alternate1 { get; set; }
        [DataMember(Name = "alternate2")]
        public Dns Alternate2 { get; set; }
        [DataMember(Name = "alternate3")]
        public Dns Alternate3 { get; set; }
    }
}