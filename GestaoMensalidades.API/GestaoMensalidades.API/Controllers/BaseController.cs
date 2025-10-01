using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoMensalidades.API.Controllers;

/// <summary>
/// Controller base com funcionalidades comuns
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Obtém o ID do usuário autenticado a partir do token JWT
    /// </summary>
    /// <returns>ID do usuário ou null se não autenticado</returns>
    protected Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
    }

    /// <summary>
    /// Obtém o papel do usuário autenticado
    /// </summary>
    /// <returns>Papel do usuário ou null se não autenticado</returns>
    protected string? GetCurrentUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value;
    }

    /// <summary>
    /// Verifica se o usuário atual é administrador
    /// </summary>
    /// <returns>True se for administrador</returns>
    protected bool IsAdmin()
    {
        return GetCurrentUserRole() == "Admin";
    }

    /// <summary>
    /// Retorna uma resposta de erro padronizada
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    /// <param name="statusCode">Código de status HTTP</param>
    /// <returns>Resposta de erro</returns>
    protected IActionResult ErrorResponse(string message, int statusCode = 400)
    {
        return StatusCode(statusCode, new { error = message });
    }

    /// <summary>
    /// Retorna uma resposta de sucesso padronizada
    /// </summary>
    /// <param name="data">Dados da resposta</param>
    /// <param name="message">Mensagem de sucesso</param>
    /// <returns>Resposta de sucesso</returns>
    protected IActionResult SuccessResponse(object? data = null, string message = "Operação realizada com sucesso")
    {
        return Ok(new { message, data });
    }
}

