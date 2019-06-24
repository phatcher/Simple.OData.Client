using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Simple.OData.Client.Tests
{
    public class Ship : Transport
    {
        public string ShipName { get; set; }

        [NotMapped]
        public int Foo { get; set; }

        [JsonIgnore]
        public int Bar { get; set; }

        [DataMember(Name = "Data")]
        public string DataMapping { get; set; }

        [JsonProperty("Json")]
        public string JsonMapping { get; set; }
    }
}