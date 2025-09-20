using System.ComponentModel.DataAnnotations;

namespace GestaoMensalidades.API.DTOs.Auth;

/// <summary>
/// DTO para requisição de registro de usuário
/// </summary>
public class RegisterRequestDto
{
    /// <summary>
    /// Nome completo do usuário
    /// </summary>
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres")]
    [MaxLength(255, ErrorMessage = "Nome não pode exceder 255 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do usuário
    /// </summary>
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Senha do usuário
    /// </summary>
    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Confirmação da senha
    /// </summary>
    [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
    [Compare(nameof(Password), ErrorMessage = "Senhas não coincidem")]
    public string ConfirmPassword { get; set; } = string.Empty;

    /// <summary>
    /// Papel do usuário (Admin, BusinessOwner)
    /// </summary>
    [Required(ErrorMessage = "Papel é obrigatório")]
    public string Role { get; set; } = "BusinessOwner";
}
