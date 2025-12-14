using CRM.DTOs.Clients;
using CRM.DTOs.Common;
using CRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/clients")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>Список клиентов с пагинацией</summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<ClientListDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query)
            => Ok(await _clientService.GetPagedAsync(query));

        /// <summary>Клиент по Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ClientDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _clientService.GetByIdAsync(id));

        /// <summary>Создать клиента (Admin)</summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(typeof(ClientDetailsDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            var client = await _clientService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        /// <summary>Обновить клиента (Admin)</summary>
        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(typeof(ClientDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, UpdateClientDto dto)
            => Ok(await _clientService.UpdateAsync(id, dto));

        /// <summary>Удалить клиента (Admin)</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _clientService.DeleteAsync(id);
            return NoContent();
        }
    }
}