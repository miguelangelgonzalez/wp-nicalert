using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Administrative : Person
    {
        [DataMember(Name = "activity")]
        public string Activity { get; set; }
    }
}