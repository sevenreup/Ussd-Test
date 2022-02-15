namespace TestUssd.Core
{
    public class UssdRequest
    {
        public string Msisdn { get; set; }
        public string SessionId { get; set; }
        public string SessionType { get; set; }
        public string Message { get; set; }

        public UssdRequestType RequestType
        {
            get
            {
                return SessionType.ToLower() switch
                {
                    "init" => UssdRequestType.Init,
                    "response" => UssdRequestType.Response,
                    "finish" => UssdRequestType.End,
                    _ => UssdRequestType.Timeout,
                };
            }
        }

        public string NextRouteKey => $"{Msisdn}_NextRoute";
    }

    public enum UssdRequestType
    {
        Init,
        Response,
        Timeout,
        End
    }
}
