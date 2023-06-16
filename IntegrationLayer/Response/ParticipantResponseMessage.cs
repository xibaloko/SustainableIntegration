using Newtonsoft.Json;
using System;

namespace IntegrationLayer.Response
{
    public class ParticipantResponseMessage
    {
        [JsonProperty("id_participant")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("birth")]
        public DateTime Birth { get; set; }
    }
}
