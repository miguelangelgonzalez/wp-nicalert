using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public class Entity
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "address")]
        public string Address { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "province", EmitDefaultValue = true)]
        public string Province { get; set; }
        [DataMember(Name = "zip_code")]
        public string ZipCode { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "activity")]
        public string Activity { get; set; }
        [DataMember(Name = "handle")]
        public string Handle { get; set; }
        [DataMember(Name = "cuit")]
        public string Cuit { get; set; }
        [DataMember(Name = "dni")]
        public string Dni { get; set; }
    }
}