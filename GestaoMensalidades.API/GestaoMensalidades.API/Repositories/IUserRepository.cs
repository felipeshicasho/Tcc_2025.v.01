using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Repositories;

/// <summary>
/// Interface específica para repositório de usuários
/// Define operações específicas para a entidade User
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Busca um usuário pelo email
    /// </summary>
    /// <param name="email">Email do usuário</param>
    /// <returns>Usuário encontrado ou null</returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Verifica se um email já está em uso
    /// </summary>
    /// <param name="email">Email a ser verificado</param>
    /// <returns>True se o email já estiver em uso</returns>
    Task<bool> IsEmailInUseAsync(string email);

    /// <summary>
    /// Busca usuários ativos
    /// </summary>
    /// <returns>Lista de usuários ativos</returns>
    Task<IEnumerable<User>> GetActiveUsersAsync();

    /// <summary>
    /// Busca usuários por papel
    /// </summary>
    /// <param name="role">Papel do usuário</param>
    /// <returns>Lista de usuários com o papel especificado</returns>
    Task<IEnumerable<User>> GetByRoleAsync(string role);
}
