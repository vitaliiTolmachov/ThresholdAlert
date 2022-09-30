using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Threshold
    {
        [Key]
        public long ThresholdId { get; set; }

        [Required]
        public string HostName { get; set; }

        [Required]
        public long MaxCalls { get; set; }

        [Range(1, 100)]
        public int NotificationLevel { get; set; }

        public ICollection<HostActivity> HostActivities { get; set; }
    }

}
