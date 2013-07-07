using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Transaction
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "domain")]
        public string domain { get; set; }
        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "notes")]
        public string Notes { get; set; }
    }
}