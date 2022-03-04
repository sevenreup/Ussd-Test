namespace TestUssd.Core
{
    public class UssdRequest
    {
        public string Msisdn { get; set; }
        public string SessionId { get; set; }
        public int SessionType { get; set; }
        public string Message { get; set; }

        public UssdRequestType RequestType
        {
            get
            {
                return SessionType switch
                {
                    1 => UssdRequestType.Init,
                    2 => UssdRequestType.Response,
                    _ => UssdRequestType.End
                };
            }
        }

        public string NextRouteKey => $"{Msisdn}_NextRoute";
    }

    public enum UssdRequestType
    {
        Init = 1,
        Response = 2,
        End = 3
    }
}
