using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Dns
    {
        [DataMember(Name = "host", EmitDefaultValue = true)]
        public string Host { get; set; }
        [DataMember(Name = "ip", EmitDefaultValue = true)]
        public object Ip { get; set; }
    }
}