using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestaoMensalidades.API.DTOs.Customer;
using GestaoMensalidades.API.Services;

namespace GestaoMensalidades.API.Controllers;

/// <summary>
/// Controller responsável pelas operações CRUD de clientes
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] // Requer autenticação para todas as operações
public class CustomersController : BaseController
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Lista todos os clientes do usuário autenticado
    /// </summary>
    /// <returns>Lista de clientes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetCustomers()
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var customers = await _customerService.GetCustomersAsync(userId.Value);
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Busca um cliente específico por ID
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>Cliente encontrado</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var customer = await _customerService.GetCustomerByIdAsync(id, userId.Value);

            if (customer == null)
            {
                return NotFound(new { error = "Cliente não encontrado" });
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    /// <param name="createDto">Dados do cliente a ser criado</param>
    /// <returns>Cliente criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var customer = await _customerService.CreateCustomerAsync(createDto, userId.Value);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="updateDto">Dados atualizados do cliente</param>
    /// <returns>Cliente atualizado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var customer = await _customerService.UpdateCustomerAsync(id, updateDto, userId.Value);

            if (customer == null)
            {
                return NotFound(new { error = "Cliente não encontrado" });
            }

            return Ok(customer);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Remove um cliente
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>Confirmação da remoção</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var success = await _customerService.DeleteCustomerAsync(id, userId.Value);

            if (!success)
            {
                return NotFound(new { error = "Cliente não encontrado" });
            }

            return Ok(new { message = "Cliente removido com sucesso" });
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Verifica se um documento está disponível para uso
    /// </summary>
    /// <param name="document">Documento a ser verificado</param>
    /// <param name="excludeId">ID do cliente a ser excluído da verificação (para updates)</param>
    /// <returns>True se disponível, false se já em uso</returns>
    [HttpGet("check-document/{document}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> CheckDocumentAvailability(string document, [FromQuery] Guid? excludeId = null)
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var isInUse = await _customerService.IsDocumentInUseAsync(document, userId.Value, excludeId);
            return Ok(new { available = !isInUse });
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Verifica se um email está disponível para uso
    /// </summary>
    /// <param name="email">Email a ser verificado</param>
    /// <param name="excludeId">ID do cliente a ser excluído da verificação (para updates)</param>
    /// <returns>True se disponível, false se já em uso</returns>
    [HttpGet("check-email/{email}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> CheckEmailAvailability(string email, [FromQuery] Guid? excludeId = null)
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(new { error = "Usuário não autenticado" });
            }

            var isInUse = await _customerService.IsEmailInUseAsync(email, userId.Value, excludeId);
            return Ok(new { available = !isInUse });
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }
}

