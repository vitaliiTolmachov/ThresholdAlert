using System.ComponentModel.DataAnnotations;

namespace AlertManagmentAPI.Models
{
    public class ThresholdResponse
    {
        public long Id { get; set; }

        public string HostName { get; set; }

        public long MaxCalls { get; set; }

        public int NotificationLevel { get; set; }

        public long UserIdId { get; set; }

        public bool IsAlertSent { get; set; }
    }
}
