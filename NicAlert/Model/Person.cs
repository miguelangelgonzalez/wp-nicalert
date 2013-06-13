using System.Runtime.Serialization;

namespace NicAlert.Model
{
    [DataContract]
    public abstract class Person
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
        [DataMember(Name = "phone")]
        public string Phone { get; set; }
        [DataMember(Name = "fax")]
        public string Fax { get; set; }        
    }
}