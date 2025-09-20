using GestaoMensalidades.API.DTOs.Auth;

namespace GestaoMensalidades.API.Services;

/// <summary>
/// Interface para o serviço de autenticação
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Autentica um usuário e retorna o token JWT
    /// </summary>
    /// <param name="loginRequest">Dados de login</param>
    /// <returns>Resposta com token e informações do usuário</returns>
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);

    /// <summary>
    /// Registra um novo usuário no sistema
    /// </summary>
    /// <param name="registerRequest">Dados de registro</param>
    /// <returns>Resposta com token e informações do usuário</returns>
    Task<LoginResponseDto?> RegisterAsync(RegisterRequestDto registerRequest);

    /// <summary>
    /// Verifica se um email já está em uso
    /// </summary>
    /// <param name="email">Email a ser verificado</param>
    /// <returns>True se o email já estiver em uso</returns>
    Task<bool> IsEmailInUseAsync(string email);
}
