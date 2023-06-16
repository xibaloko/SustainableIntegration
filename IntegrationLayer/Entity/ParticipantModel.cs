using Newtonsoft.Json;
using System;

namespace IntegrationLayer.Entity
{
    public class ParticipantModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("birth")]
        public DateTime Birth { get; set; }
    }
}
