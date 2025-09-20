using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.API.Models;

/// <summary>
/// Representa um cliente do negócio (aluno, membro da academia, etc.)
/// </summary>
public class Customer
{
    /// <summary>
    /// Identificador único do cliente
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome completo do cliente
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do cliente
    /// </summary>
    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Telefone do cliente
    /// </summary>
    [MaxLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// Documento de identificação (CPF, CNPJ, etc.)
    /// </summary>
    [MaxLength(20)]
    public string? Document { get; set; }

    /// <summary>
    /// Endereço completo do cliente
    /// </summary>
    [MaxLength(500)]
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
    [MaxLength(1000)]
    public string? Notes { get; set; }

    /// <summary>
    /// ID do proprietário do negócio que gerencia este cliente
    /// </summary>
    public Guid BusinessOwnerId { get; set; }

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data da última atualização do registro
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navegação para o proprietário do negócio
    /// </summary>
    public virtual User BusinessOwner { get; set; } = null!;

    /// <summary>
    /// Navegação para as assinaturas do cliente
    /// </summary>
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
