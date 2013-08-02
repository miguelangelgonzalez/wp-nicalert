using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Dns
    {
        [DataMember(Name = "host", EmitDefaultValue = true)]
        public string Host { get; set; }
        [DataMember(Name = "ip", EmitDefaultValue = true)]
        public string Ip { get; set; }
        [DataMember(Name = "owner", EmitDefaultValue = true)]
        public string Owner { get; set; }
        [DataMember(Name = "operator", EmitDefaultValue = true)]
        public string Operator { get; set; }
        [DataMember(Name = "handle", EmitDefaultValue = true)]
        public string Handle { get; set; }
    }
}