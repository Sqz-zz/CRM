using CRM.Domain.Entities;
using CRM.DTOs.Notes;
using CRM.Infrastructure.Persistence;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _http;

        public NoteService(ApplicationDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public async Task<List<NoteDto>> GetByClientAsync(int clientId)
        {
            return await _context.Notes
                .Where(n => n.ClientId == clientId)
                .Include(n => n.Author)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Text = n.Text,
                    Author = n.Author.Username,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<NoteDto> CreateAsync(int clientId, CreateNoteDto dto)
        {
            var userId = int.Parse(
                _http.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var note = new Note
            {
                ClientId = clientId,
                AuthorId = userId,
                Text = dto.Text
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            // Загружаем автора, чтобы вернуть корректный DTO
            await _context.Entry(note)
                .Reference(n => n.Author)
                .LoadAsync();

            return new NoteDto
            {
                Id = note.Id,
                Text = note.Text,
                Author = note.Author.Username,
                CreatedAt = note.CreatedAt
            };
        }

        public async Task DeleteAsync(int clientId, int noteId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == noteId && n.ClientId == clientId);

            if (note == null)
                throw new Exception("Note not found");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}