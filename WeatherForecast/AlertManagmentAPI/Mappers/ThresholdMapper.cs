using AlertManagmentAPI.Models;
using DataAccess.Entities;

namespace AlertManagmentAPI.Mappers
{
    public static class ThresholdMapper
    {
        public static ThresholdResponse ToResponse(this Threshold entity) =>
            new ThresholdResponse
            {
                UserIdId = entity.UserIdId,
                NotificationLevel = entity.NotificationLevel,
                MaxCalls = entity.MaxCalls,
                HostName = entity.HostName,
                IsAlertSent = entity.IsAlertSent,
                Id = entity.ThresholdId
                
            };
    }
}
