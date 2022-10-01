﻿using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Threshold
    {
        public long ThresholdId { get; set; }

        public string HostName { get; set; }

        public long MaxCalls { get; set; }

        [Range(1, 100)]
        public int NotificationLevel { get; set; }

        public ICollection<HostActivity> HostActivities { get; set; }
    }

}
