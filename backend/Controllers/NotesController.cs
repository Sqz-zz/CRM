using CRM.DTOs.Notes;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/clients/{clientId:int}/notes")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>Заметки клиента</summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<NoteDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int clientId)
            => Ok(await _noteService.GetByClientAsync(clientId));

        /// <summary>Добавить заметку (Admin)</summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(typeof(NoteDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(int clientId, CreateNoteDto dto)
            => Ok(await _noteService.CreateAsync(clientId, dto));

        /// <summary>Удалить заметку (Admin)</summary>
        [HttpDelete("{noteId:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int clientId, int noteId)
        {
            await _noteService.DeleteAsync(clientId, noteId);
            return NoContent();
        }
    }
}