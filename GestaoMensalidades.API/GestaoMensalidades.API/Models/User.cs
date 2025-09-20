using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.API.Models;

/// <summary>
/// Representa um usuário do sistema (administradores, proprietários de negócios)
/// </summary>
public class User
{
    /// <summary>
    /// Identificador único do usuário
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nome completo do usuário
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do usuário (usado para login)
    /// </summary>
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash da senha (nunca armazenar senha em texto plano)
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Papel do usuário no sistema (Admin, BusinessOwner, etc.)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o usuário está ativo no sistema
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data da última atualização do registro
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Data da última tentativa de login
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Navegação para os clientes gerenciados por este usuário
    /// </summary>
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
