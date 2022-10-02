using System.ComponentModel.DataAnnotations;

namespace AlertManagmentAPI.Models
{
    public class ThresholdRequest
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        public long MaxCalls { get; set; }

        [Range(1, 100)]
        public int NotificationLevel { get; set; }
        
        [Required]
        public long UserIdId { get; set; }
    }

    public class ThresholdUpdateRequest : ThresholdRequest
    {
        public bool IsAlertSent { get; set; }
    }
}
