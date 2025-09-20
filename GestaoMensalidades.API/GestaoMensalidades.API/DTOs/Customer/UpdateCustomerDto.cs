using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.API.DTOs.Customer;

/// <summary>
/// DTO para atualização de um cliente existente
/// </summary>
public class UpdateCustomerDto
{
    /// <summary>
    /// Nome do cliente
    /// </summary>
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres")]
    [MaxLength(255, ErrorMessage = "Nome não pode exceder 255 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do cliente
    /// </summary>
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [MaxLength(255, ErrorMessage = "Email não pode exceder 255 caracteres")]
    public string? Email { get; set; }

    /// <summary>
    /// Telefone do cliente
    /// </summary>
    [MaxLength(20, ErrorMessage = "Telefone não pode exceder 20 caracteres")]
    public string? Phone { get; set; }

    /// <summary>
    /// Documento do cliente (CPF, CNPJ, etc.)
    /// </summary>
    [MaxLength(20, ErrorMessage = "Documento não pode exceder 20 caracteres")]
    public string? Document { get; set; }

    /// <summary>
    /// Endereço do cliente
    /// </summary>
    [MaxLength(500, ErrorMessage = "Endereço não pode exceder 500 caracteres")]
    public string? Address { get; set; }

    /// <summary>
    /// Data de nascimento do cliente
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Indica se o cliente está ativo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Observações sobre o cliente
    /// </summary>
    [MaxLength(1000, ErrorMessage = "Observações não podem exceder 1000 caracteres")]
    public string? Notes { get; set; }
}
