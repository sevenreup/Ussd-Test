namespace TestUssd.Core
{
    public class UssdSessionData
    {
        public string CurrentRoute { get; set; }
        public string NextRoute { get; set; }
        public string Input { get; set; }
        public bool IsGetting { get; set; } = true;
    }
}
