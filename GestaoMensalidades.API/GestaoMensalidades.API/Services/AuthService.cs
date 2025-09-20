using Microsoft.EntityFrameworkCore;
using GestaoMensalidades.API.Data;
using GestaoMensalidades.API.DTOs.Auth;
using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Services;

/// <summary>
/// Serviço de autenticação responsável por login, registro e validação de usuários
/// </summary>
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(ApplicationDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Autentica um usuário e retorna o token JWT
    /// </summary>
    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        // Busca o usuário pelo email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.IsActive);

        if (user == null)
            return null;

        // Verifica a senha
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            return null;

        // Atualiza o último login
        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Gera o token JWT
        var token = _jwtService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60), // Mesmo valor do appsettings
            User = new UserInfoDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            }
        };
    }

    /// <summary>
    /// Registra um novo usuário no sistema
    /// </summary>
    public async Task<LoginResponseDto?> RegisterAsync(RegisterRequestDto registerRequest)
    {
        // Verifica se o email já está em uso
        if (await IsEmailInUseAsync(registerRequest.Email))
            return null;

        // Cria o novo usuário
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
            Role = registerRequest.Role,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Salva no banco de dados
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Gera o token JWT
        var token = _jwtService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            User = new UserInfoDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            }
        };
    }

    /// <summary>
    /// Verifica se um email já está em uso
    /// </summary>
    public async Task<bool> IsEmailInUseAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}
