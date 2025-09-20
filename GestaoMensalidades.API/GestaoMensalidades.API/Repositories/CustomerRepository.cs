using Microsoft.EntityFrameworkCore;
using GestaoMensalidades.API.Data;
using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Repositories;

/// <summary>
/// Implementação específica do repositório de clientes
/// </summary>
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Busca clientes por proprietário do negócio
    /// </summary>
    public async Task<IEnumerable<Customer>> GetByBusinessOwnerAsync(Guid businessOwnerId)
    {
        return await _dbSet
            .Where(c => c.BusinessOwnerId == businessOwnerId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Busca clientes ativos por proprietário do negócio
    /// </summary>
    public async Task<IEnumerable<Customer>> GetActiveByBusinessOwnerAsync(Guid businessOwnerId)
    {
        return await _dbSet
            .Where(c => c.BusinessOwnerId == businessOwnerId && c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Busca cliente por documento
    /// </summary>
    public async Task<Customer?> GetByDocumentAsync(string document)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Document == document);
    }

    /// <summary>
    /// Busca cliente por email
    /// </summary>
    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
    }

    /// <summary>
    /// Busca clientes com suas assinaturas
    /// </summary>
    public async Task<IEnumerable<Customer>> GetWithSubscriptionsAsync(Guid businessOwnerId)
    {
        return await _dbSet
            .Include(c => c.Subscriptions)
            .Where(c => c.BusinessOwnerId == businessOwnerId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}
