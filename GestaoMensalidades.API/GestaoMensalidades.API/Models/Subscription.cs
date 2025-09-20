using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.API.Models;

/// <summary>
/// Representa uma assinatura/mensalidade de um cliente
/// </summary>
public class Subscription
{
    /// <summary>
    /// Identificador único da assinatura
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome da assinatura (ex: "Plano Mensal", "Aulas de Piano")
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada da assinatura
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Valor da assinatura
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal Price { get; set; }

    /// <summary>
    /// Ciclo de cobrança (Mensal, Trimestral, Semestral, Anual)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string BillingCycle { get; set; } = "Mensal";

    /// <summary>
    /// Status da assinatura (Ativa, Suspensa, Cancelada, Expirada)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Ativa";

    /// <summary>
    /// Data de início da assinatura
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Data de fim da assinatura (opcional para assinaturas contínuas)
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Data do próximo vencimento
    /// </summary>
    public DateTime NextDueDate { get; set; }

    /// <summary>
    /// ID do cliente proprietário da assinatura
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data da última atualização do registro
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navegação para o cliente
    /// </summary>
    public virtual Customer Customer { get; set; } = null!;

    /// <summary>
    /// Navegação para os pagamentos da assinatura
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
