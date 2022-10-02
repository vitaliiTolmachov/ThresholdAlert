namespace Messages
{
    public class AlertMessage
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserName { get; set; }
        public string RequestedHost { get; set; }
        public int Percentage { get; set; }
        public long CallLimit { get; set; }
    }
}