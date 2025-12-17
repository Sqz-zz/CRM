using CRM.DTOs.Clients;
using CRM.DTOs.Common;

namespace CRM.Services.Interfaces
{
    public interface IClientService
    {
        Task<PagedResultDto<ClientListDto>> GetPagedAsync(PaginationQuery query);
        Task<ClientDetailsDto> GetByIdAsync(int id);

        Task<ClientDetailsDto> CreateAsync(CreateClientDto dto);
        Task<ClientDetailsDto> UpdateAsync(int id, UpdateClientDto dto);

        Task DeleteAsync(int id);
    }
}