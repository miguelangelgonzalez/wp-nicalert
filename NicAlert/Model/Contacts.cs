using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Contacts
    {
        [DataMember(Name = "registrant")]
        public Registrant Registrant { get; set; }
        [DataMember(Name = "responsible")]
        public Responsible Responsible { get; set; }
        [DataMember(Name = "administrative")]
        public Administrative Administrative { get; set; }
        [DataMember(Name = "technical")]
        public Technical Technical { get; set; }
    }
}