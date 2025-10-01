using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.Web.Models;

/// <summary>
/// Modelo para cliente
/// </summary>
public class CustomerModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres")]
    [MaxLength(255, ErrorMessage = "Nome não pode exceder 255 caracteres")]
    public string Name { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [MaxLength(255, ErrorMessage = "Email não pode exceder 255 caracteres")]
    public string? Email { get; set; }

    [MaxLength(20, ErrorMessage = "Telefone não pode exceder 20 caracteres")]
    public string? Phone { get; set; }

    [MaxLength(20, ErrorMessage = "Documento não pode exceder 20 caracteres")]
    public string? Document { get; set; }

    [MaxLength(500, ErrorMessage = "Endereço não pode exceder 500 caracteres")]
    public string? Address { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(1000, ErrorMessage = "Observações não podem exceder 1000 caracteres")]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
