using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Registrant: Person
    {
        [DataMember(Name = "occupation")]
        public string Occupation { get; set; }
    }
}