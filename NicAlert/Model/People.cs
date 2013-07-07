using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class People
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "handle")]
        public string Handle { get; set; }
    }
}