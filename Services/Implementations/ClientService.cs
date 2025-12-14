using CRM.Domain.Entities;
using CRM.DTOs.Clients;
using CRM.DTOs.Common;
using CRM.DTOs.Notes;
using CRM.Infrastructure.Persistence;
using CRM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<ClientListDto>> GetPagedAsync(PaginationQuery query)
        {
            var baseQuery = _context.Clients
                .Include(c => c.Status)
                .AsNoTracking();

            var total = await baseQuery.CountAsync();

            var items = await baseQuery
                .OrderBy(c => c.Id)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(c => new ClientListDto
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName,
                    Email = c.Email,
                    Status = c.Status.Title
                })
                .ToListAsync();

            return new PagedResultDto<ClientListDto>
            {
                Items = items,
                TotalCount = total,
            };
        }

        public async Task<ClientDetailsDto> GetByIdAsync(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Status)
                .Include(c => c.Notes)
                    .ThenInclude(n => n.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                throw new Exception("Client not found");

            return new ClientDetailsDto
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                MiddleName = client.MiddleName,
                Email = client.Email,
                Phone = client.Phone,
                CompanyName = client.CompanyName,
                Status = client.Status.Title,
                Notes = client.Notes.Select(n => new NoteDto
                {
                    Id = n.Id,
                    Text = n.Text,
                    CreatedAt = n.CreatedAt,
                    Author = n.Author.Username
                }).ToList()
            };
        }

        public async Task<ClientDetailsDto> CreateAsync(CreateClientDto dto)
        {
            var client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Email = dto.Email,
                Phone = dto.Phone,
                CompanyName = dto.CompanyName,
                StatusId = dto.StatusId
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(client.Id);
        }

        public async Task<ClientDetailsDto> UpdateAsync(int id, UpdateClientDto dto)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                throw new Exception("Client not found");

            client.FirstName = dto.FirstName;
            client.LastName = dto.LastName;
            client.MiddleName = dto.MiddleName;
            client.Phone = dto.Phone;
            client.CompanyName = dto.CompanyName;
            client.StatusId = dto.StatusId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(client.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                throw new Exception("Client not found");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}