using CRM.DTOs.Statuses;
using CRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CRM.Services.Interfaces;
using CRM.DTOs.Auth;

public class StatusService : IStatusService
{
    private readonly ApplicationDbContext _context;

    public StatusService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StatusDto>> GetAllAsync()
    {
        return await _context.Statuses
            .OrderBy(s => s.OrderIndex)
            .Select(s => new StatusDto
            {
                Id = s.Id,
                Title = s.Title
            })
            .ToListAsync();
    }
}