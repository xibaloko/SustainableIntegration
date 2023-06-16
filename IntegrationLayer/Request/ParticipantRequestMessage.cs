using Newtonsoft.Json;
using System;

namespace IntegrationLayer.Request
{
    public class ParticipantRequestMessage
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("birth")]
        public DateTime Birth { get; set; }
    }
}
