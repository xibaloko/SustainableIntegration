namespace IntegrationLayer
{
    public class Client
    {
        public static ClientProperties Properties { get; set; }
        public static void Init(ClientProperties properties)
        {
            Properties = properties;
        }
    }

    public class ClientProperties
    {
        public string ApiKey { get; set; }
    }
}
