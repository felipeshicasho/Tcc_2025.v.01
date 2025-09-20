using Microsoft.EntityFrameworkCore;
using GestaoMensalidades.API.Data;
using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Repositories;

/// <summary>
/// Implementação específica do repositório de usuários
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Busca um usuário pelo email
    /// </summary>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Verifica se um email já está em uso
    /// </summary>
    public async Task<bool> IsEmailInUseAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    /// <summary>
    /// Busca usuários ativos
    /// </summary>
    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _dbSet.Where(u => u.IsActive).ToListAsync();
    }

    /// <summary>
    /// Busca usuários por papel
    /// </summary>
    public async Task<IEnumerable<User>> GetByRoleAsync(string role)
    {
        return await _dbSet.Where(u => u.Role == role && u.IsActive).ToListAsync();
    }
}
