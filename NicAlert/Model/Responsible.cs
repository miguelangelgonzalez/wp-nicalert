using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Responsible: Person
    {
        [DataMember(Name = "work_hours")]
        public string WorkHours { get; set; }
    }
}