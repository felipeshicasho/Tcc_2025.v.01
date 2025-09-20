namespace GestaoMensalidades.API.DTOs.Auth;

/// <summary>
/// DTO para resposta de login
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Token JWT para autenticação
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Data de expiração do token
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Informações do usuário autenticado
    /// </summary>
    public UserInfoDto User { get; set; } = new();
}

/// <summary>
/// DTO com informações básicas do usuário
/// </summary>
public class UserInfoDto
{
    /// <summary>
    /// ID do usuário
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome do usuário
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Papel do usuário no sistema
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
