using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Services;

/// <summary>
/// Interface para o serviço de geração e validação de tokens JWT
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Gera um token JWT para o usuário
    /// </summary>
    /// <param name="user">Usuário para o qual gerar o token</param>
    /// <returns>Token JWT</returns>
    string GenerateToken(User user);

    /// <summary>
    /// Valida um token JWT
    /// </summary>
    /// <param name="token">Token a ser validado</param>
    /// <returns>True se o token for válido, false caso contrário</returns>
    bool ValidateToken(string token);

    /// <summary>
    /// Extrai o ID do usuário de um token JWT
    /// </summary>
    /// <param name="token">Token JWT</param>
    /// <returns>ID do usuário ou null se inválido</returns>
    Guid? GetUserIdFromToken(string token);
}
