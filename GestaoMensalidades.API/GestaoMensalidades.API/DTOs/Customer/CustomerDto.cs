namespace GestaoMensalidades.API.DTOs.Customer;

/// <summary>
/// DTO para representar um cliente
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// ID do cliente
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome do cliente
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do cliente
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Telefone do cliente
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Documento do cliente
    /// </summary>
    public string? Document { get; set; }

    /// <summary>
    /// Endereço do cliente
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Data de nascimento do cliente
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Indica se o cliente está ativo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Observações sobre o cliente
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data da última atualização
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
