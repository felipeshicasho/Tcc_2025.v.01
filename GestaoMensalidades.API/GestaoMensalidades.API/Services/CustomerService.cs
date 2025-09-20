using AutoMapper;
using GestaoMensalidades.API.DTOs.Customer;
using GestaoMensalidades.API.Models;
using GestaoMensalidades.API.Repositories;

namespace GestaoMensalidades.API.Services;

/// <summary>
/// Serviço para operações relacionadas a clientes
/// </summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Busca todos os clientes de um proprietário de negócio
    /// </summary>
    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync(Guid businessOwnerId)
    {
        var customers = await _customerRepository.GetActiveByBusinessOwnerAsync(businessOwnerId);
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    /// <summary>
    /// Busca um cliente por ID
    /// </summary>
    public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id, Guid businessOwnerId)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(c => 
            c.Id == id && c.BusinessOwnerId == businessOwnerId);

        return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createDto, Guid businessOwnerId)
    {
        // Verifica se o documento já está em uso
        if (!string.IsNullOrEmpty(createDto.Document) && 
            await IsDocumentInUseAsync(createDto.Document, businessOwnerId))
        {
            throw new InvalidOperationException("Documento já está em uso por outro cliente.");
        }

        // Verifica se o email já está em uso
        if (!string.IsNullOrEmpty(createDto.Email) && 
            await IsEmailInUseAsync(createDto.Email, businessOwnerId))
        {
            throw new InvalidOperationException("Email já está em uso por outro cliente.");
        }

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            Email = createDto.Email,
            Phone = createDto.Phone,
            Document = createDto.Document,
            Address = createDto.Address,
            BirthDate = createDto.BirthDate,
            Notes = createDto.Notes,
            BusinessOwnerId = businessOwnerId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    public async Task<CustomerDto?> UpdateCustomerAsync(Guid id, UpdateCustomerDto updateDto, Guid businessOwnerId)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(c => 
            c.Id == id && c.BusinessOwnerId == businessOwnerId);

        if (customer == null)
            return null;

        // Verifica se o documento já está em uso por outro cliente
        if (!string.IsNullOrEmpty(updateDto.Document) && 
            await IsDocumentInUseAsync(updateDto.Document, businessOwnerId, id))
        {
            throw new InvalidOperationException("Documento já está em uso por outro cliente.");
        }

        // Verifica se o email já está em uso por outro cliente
        if (!string.IsNullOrEmpty(updateDto.Email) && 
            await IsEmailInUseAsync(updateDto.Email, businessOwnerId, id))
        {
            throw new InvalidOperationException("Email já está em uso por outro cliente.");
        }

        // Atualiza os dados do cliente
        customer.Name = updateDto.Name;
        customer.Email = updateDto.Email;
        customer.Phone = updateDto.Phone;
        customer.Document = updateDto.Document;
        customer.Address = updateDto.Address;
        customer.BirthDate = updateDto.BirthDate;
        customer.IsActive = updateDto.IsActive;
        customer.Notes = updateDto.Notes;
        customer.UpdatedAt = DateTime.UtcNow;

        await _customerRepository.UpdateAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    /// <summary>
    /// Remove um cliente
    /// </summary>
    public async Task<bool> DeleteCustomerAsync(Guid id, Guid businessOwnerId)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(c => 
            c.Id == id && c.BusinessOwnerId == businessOwnerId);

        if (customer == null)
            return false;

        await _customerRepository.DeleteAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Verifica se um documento já está em uso por outro cliente
    /// </summary>
    public async Task<bool> IsDocumentInUseAsync(string document, Guid businessOwnerId, Guid? excludeCustomerId = null)
    {
        var query = _customerRepository.FindAsync(c => 
            c.Document == document && c.BusinessOwnerId == businessOwnerId);

        if (excludeCustomerId.HasValue)
        {
            query = _customerRepository.FindAsync(c => 
                c.Document == document && 
                c.BusinessOwnerId == businessOwnerId && 
                c.Id != excludeCustomerId.Value);
        }

        var customers = await query;
        return customers.Any();
    }

    /// <summary>
    /// Verifica se um email já está em uso por outro cliente
    /// </summary>
    public async Task<bool> IsEmailInUseAsync(string email, Guid businessOwnerId, Guid? excludeCustomerId = null)
    {
        var query = _customerRepository.FindAsync(c => 
            c.Email == email && c.BusinessOwnerId == businessOwnerId);

        if (excludeCustomerId.HasValue)
        {
            query = _customerRepository.FindAsync(c => 
                c.Email == email && 
                c.BusinessOwnerId == businessOwnerId && 
                c.Id != excludeCustomerId.Value);
        }

        var customers = await query;
        return customers.Any();
    }
}
