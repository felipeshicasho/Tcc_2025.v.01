using GestaoMensalidades.API.DTOs.Customer;

namespace GestaoMensalidades.API.Services;

/// <summary>
/// Interface para o serviço de clientes
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Busca todos os clientes de um proprietário de negócio
    /// </summary>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Lista de clientes</returns>
    Task<IEnumerable<CustomerDto>> GetCustomersAsync(Guid businessOwnerId);

    /// <summary>
    /// Busca um cliente por ID
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Cliente encontrado ou null</returns>
    Task<CustomerDto?> GetCustomerByIdAsync(Guid id, Guid businessOwnerId);

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    /// <param name="createDto">Dados do cliente a ser criado</param>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Cliente criado</returns>
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto, Guid businessOwnerId);

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="updateDto">Dados atualizados do cliente</param>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>Cliente atualizado ou null se não encontrado</returns>
    Task<CustomerDto?> UpdateCustomerAsync(Guid id, UpdateCustomerDto updateDto, Guid businessOwnerId);

    /// <summary>
    /// Remove um cliente
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <returns>True se removido com sucesso</returns>
    Task<bool> DeleteCustomerAsync(Guid id, Guid businessOwnerId);

    /// <summary>
    /// Verifica se um documento já está em uso por outro cliente
    /// </summary>
    /// <param name="document">Documento a ser verificado</param>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <param name="excludeCustomerId">ID do cliente a ser excluído da verificação (para updates)</param>
    /// <returns>True se o documento já estiver em uso</returns>
    Task<bool> IsDocumentInUseAsync(string document, Guid businessOwnerId, Guid? excludeCustomerId = null);

    /// <summary>
    /// Verifica se um email já está em uso por outro cliente
    /// </summary>
    /// <param name="email">Email a ser verificado</param>
    /// <param name="businessOwnerId">ID do proprietário do negócio</param>
    /// <param name="excludeCustomerId">ID do cliente a ser excluído da verificação (para updates)</param>
    /// <returns>True se o email já estiver em uso</returns>
    Task<bool> IsEmailInUseAsync(string email, Guid businessOwnerId, Guid? excludeCustomerId = null);
}
