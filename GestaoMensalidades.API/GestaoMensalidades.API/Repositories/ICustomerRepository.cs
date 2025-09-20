using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Repositories;

/// <summary>
/// Interface específica para repositório de clientes
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Busca clientes por proprietário do negócio
    /// </summary>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Lista de clientes do proprietário</returns>
    Task<IEnumerable<Customer>> GetByBusinessOwnerAsync(Guid businessOwnerId);

    /// <summary>
    /// Busca clientes ativos por proprietário do negócio
    /// </summary>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Lista de clientes ativos do proprietário</returns>
    Task<IEnumerable<Customer>> GetActiveByBusinessOwnerAsync(Guid businessOwnerId);

    /// <summary>
    /// Busca cliente por documento
    /// </summary>
    /// <param name="document">Documento do cliente</param>
    /// <returns>Cliente encontrado ou null</returns>
    Task<Customer?> GetByDocumentAsync(string document);

    /// <summary>
    /// Busca cliente por email
    /// </summary>
    /// <param name="email">Email do cliente</param>
    /// <returns>Cliente encontrado ou null</returns>
    Task<Customer?> GetByEmailAsync(string email);

    /// <summary>
    /// Busca clientes com suas assinaturas
    /// </summary>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Lista de clientes com assinaturas</returns>
    Task<IEnumerable<Customer>> GetWithSubscriptionsAsync(Guid businessOwnerId);
}
