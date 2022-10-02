using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class HostActivity
{
    public long HostActivityId { get; set; }

    [Range(1,12)]
    public long Month { get; set; }

    public long Year { get; set; }

    public long CallsMade { get; set; }

    public long ThresholdId { get; set; }

    public Threshold Threshold { get; set; }

    public long UserId { get; set; }

}