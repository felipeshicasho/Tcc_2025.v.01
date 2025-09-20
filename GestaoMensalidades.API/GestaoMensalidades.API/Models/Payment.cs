using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.API.Models;

/// <summary>
/// Representa um pagamento de uma assinatura
/// </summary>
public class Payment
{
    /// <summary>
    /// Identificador único do pagamento
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Valor do pagamento
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Método de pagamento (PIX, Cartão de Crédito, Boleto, etc.)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Status do pagamento (Pendente, Pago, Falhou, Cancelado, Reembolsado)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pendente";

    /// <summary>
    /// ID da transação no gateway de pagamento
    /// </summary>
    [MaxLength(255)]
    public string? TransactionId { get; set; }

    /// <summary>
    /// Data do pagamento
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// Data de vencimento do pagamento
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Data de processamento do pagamento
    /// </summary>
    public DateTime? ProcessedAt { get; set; }

    /// <summary>
    /// Observações sobre o pagamento
    /// </summary>
    [MaxLength(500)]
    public string? Notes { get; set; }

    /// <summary>
    /// ID da assinatura relacionada
    /// </summary>
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data da última atualização do registro
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navegação para a assinatura
    /// </summary>
    public virtual Subscription Subscription { get; set; } = null!;
}
