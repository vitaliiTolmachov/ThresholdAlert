using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class HostActivity
{
    [Key]
    public long HostActivityId { get; set; }

    [Range(1,12)]
    public long Month { get; set; }

    public long Year { get; set; }

    [Required]
    public string HostName { get; set; }

    [Required]
    public long CallsMade { get; set; }

    public long ThresholdId { get; set; }

    public Threshold Threshold { get; set; }

}