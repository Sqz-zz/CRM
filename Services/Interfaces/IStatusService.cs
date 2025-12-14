using CRM.DTOs.Statuses;

namespace CRM.Services.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusDto>> GetAllAsync();
    }
}