using CRM.DTOs.Notes;

namespace CRM.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<NoteDto>> GetByClientAsync(int clientId);

        Task<NoteDto> CreateAsync(int clientId, CreateNoteDto dto);

        Task DeleteAsync(int clientId, int noteId);
    }
}