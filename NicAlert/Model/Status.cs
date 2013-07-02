using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Status
    {
        [DataMember(Name = "available")]
        public bool Available { get; set; }
        [DataMember(Name = "delegated")]
        public bool Delegated { get; set; }
        [DataMember(Name = "expiring")]
        public bool Expiring { get; set; }
        [DataMember(Name = "phasing_out")]
        public bool PhasingOut { get; set; }
        [DataMember(Name = "pending")]
        public bool Pending { get; set; }
        [DataMember(Name = "registered")]
        public bool Registered { get; set; }
    }
}