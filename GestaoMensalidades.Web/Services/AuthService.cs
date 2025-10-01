using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GestaoMensalidades.Web.Services;

/// <summary>
/// Serviço de autenticação para Blazor
/// </summary>
public class AuthService
{
    private readonly ApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ApiService apiService, IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    /// <summary>
    /// Realiza login do usuário
    /// </summary>
    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        try
        {
            var response = await _apiService.LoginAsync(loginModel);

            if (response.IsSuccess && response.Data != null)
            {
                // Define o token no ApiService
                _apiService.SetAuthToken(response.Data.Token);

                // Cria as claims do usuário
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, response.Data.User.Id.ToString()),
                    new(ClaimTypes.Name, response.Data.User.Name),
                    new(ClaimTypes.Email, response.Data.User.Email),
                    new(ClaimTypes.Role, response.Data.User.Role),
                    new("Token", response.Data.Token),
                    new("ExpiresAt", response.Data.ExpiresAt.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Faz login no sistema de autenticação do ASP.NET Core
                await _httpContextAccessor.HttpContext!.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal);

                _logger.LogInformation("Usuário {Email} fez login com sucesso", loginModel.Email);
                return true;
            }

            _logger.LogWarning("Falha no login para o usuário {Email}", loginModel.Email);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar login para o usuário {Email}", loginModel.Email);
            return false;
        }
    }

    /// <summary>
    /// Realiza logout do usuário
    /// </summary>
    public async Task LogoutAsync()
    {
        try
        {
            // Remove o token do ApiService
            _apiService.ClearAuthToken();

            // Faz logout do sistema de autenticação
            await _httpContextAccessor.HttpContext!.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            _logger.LogInformation("Usuário fez logout");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar logout");
        }
    }

    /// <summary>
    /// Verifica se o usuário está autenticado
    /// </summary>
    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    /// <summary>
    /// Obtém o ID do usuário autenticado
    /// </summary>
    public Guid? GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
    }

    /// <summary>
    /// Obtém o nome do usuário autenticado
    /// </summary>
    public string? GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
    }

    /// <summary>
    /// Obtém o email do usuário autenticado
    /// </summary>
    public string? GetCurrentUserEmail()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

    /// <summary>
    /// Obtém o papel do usuário autenticado
    /// </summary>
    public string? GetCurrentUserRole()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
    }

    /// <summary>
    /// Obtém o token JWT do usuário autenticado
    /// </summary>
    public string? GetCurrentUserToken()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("Token")?.Value;
    }

    /// <summary>
    /// Verifica se o token está expirado
    /// </summary>
    public bool IsTokenExpired()
    {
        var expiresAtClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("ExpiresAt");
        if (expiresAtClaim == null) return true;

        if (DateTime.TryParse(expiresAtClaim.Value, out var expiresAt))
        {
            return DateTime.UtcNow >= expiresAt;
        }

        return true;
    }
}

