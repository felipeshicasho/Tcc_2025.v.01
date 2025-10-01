using Microsoft.AspNetCore.Mvc;
using GestaoMensalidades.API.DTOs.Auth;
using GestaoMensalidades.API.Services;

namespace GestaoMensalidades.API.Controllers;

/// <summary>
/// Controller responsável pela autenticação de usuários
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Realiza login de um usuário
    /// </summary>
    /// <param name="loginRequest">Dados de login</param>
    /// <returns>Token JWT e informações do usuário</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginRequest);

            if (result == null)
            {
                return Unauthorized(new { error = "Email ou senha inválidos" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }

    /// <summary>
    /// Registra um novo usuário no sistema
    /// </summary>
    /// <param name="registerRequest">Dados de registro</param>
    /// <returns>Token JWT e informações do usuário</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(LoginResponseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifica se o email já está em uso
            if (await _authService.IsEmailInUseAsync(registerRequest.Email))
            {
                return Conflict(new { error = "Email já está em uso" });
            }

            var result = await _authService.RegisterAsync(registerRequest);

            if (result == null)
            {
                return ErrorResponse("Erro ao criar usuário", 500);
            }

            return CreatedAtAction(nameof(Login), result);
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
    /// <returns>True se disponível, false se já em uso</returns>
    [HttpGet("check-email/{email}")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CheckEmailAvailability(string email)
    {
        try
        {
            var isInUse = await _authService.IsEmailInUseAsync(email);
            return Ok(new { available = !isInUse });
        }
        catch (Exception ex)
        {
            return ErrorResponse($"Erro interno do servidor: {ex.Message}", 500);
        }
    }
}

