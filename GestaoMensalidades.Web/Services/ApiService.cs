using System.Text;
using System.Text.Json;
using GestaoMensalidades.Web.Models;

namespace GestaoMensalidades.Web.Services;

/// <summary>
/// Serviço para comunicação com a API
/// </summary>
public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiService> _logger;

    public ApiService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        
        // Configura a URL base da API
        var apiUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
        _httpClient.BaseAddress = new Uri(apiUrl);
    }

    /// <summary>
    /// Define o token JWT para autenticação
    /// </summary>
    public void SetAuthToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    /// <summary>
    /// Remove o token de autenticação
    /// </summary>
    public void ClearAuthToken()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    /// <summary>
    /// Realiza login na API
    /// </summary>
    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginModel loginModel)
    {
        try
        {
            var json = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/auth/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<LoginResponse>
                {
                    IsSuccess = true,
                    Data = loginResponse,
                    Message = "Login realizado com sucesso"
                };
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<LoginResponse>
                {
                    IsSuccess = false,
                    Message = errorResponse?.Error ?? "Erro no login"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar login");
            return new ApiResponse<LoginResponse>
            {
                IsSuccess = false,
                Message = "Erro interno do servidor"
            };
        }
    }

    /// <summary>
    /// Busca todos os clientes
    /// </summary>
    public async Task<ApiResponse<List<CustomerModel>>> GetCustomersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/customers");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var customers = JsonSerializer.Deserialize<List<CustomerModel>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<List<CustomerModel>>
                {
                    IsSuccess = true,
                    Data = customers ?? new List<CustomerModel>(),
                    Message = "Clientes carregados com sucesso"
                };
            }
            else
            {
                return new ApiResponse<List<CustomerModel>>
                {
                    IsSuccess = false,
                    Message = "Erro ao carregar clientes"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar clientes");
            return new ApiResponse<List<CustomerModel>>
            {
                IsSuccess = false,
                Message = "Erro interno do servidor"
            };
        }
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    public async Task<ApiResponse<CustomerModel>> CreateCustomerAsync(CustomerModel customer)
    {
        try
        {
            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/customers", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var createdCustomer = JsonSerializer.Deserialize<CustomerModel>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<CustomerModel>
                {
                    IsSuccess = true,
                    Data = createdCustomer,
                    Message = "Cliente criado com sucesso"
                };
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<CustomerModel>
                {
                    IsSuccess = false,
                    Message = errorResponse?.Error ?? "Erro ao criar cliente"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar cliente");
            return new ApiResponse<CustomerModel>
            {
                IsSuccess = false,
                Message = "Erro interno do servidor"
            };
        }
    }

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    public async Task<ApiResponse<CustomerModel>> UpdateCustomerAsync(CustomerModel customer)
    {
        try
        {
            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/customers/{customer.Id}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var updatedCustomer = JsonSerializer.Deserialize<CustomerModel>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<CustomerModel>
                {
                    IsSuccess = true,
                    Data = updatedCustomer,
                    Message = "Cliente atualizado com sucesso"
                };
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new ApiResponse<CustomerModel>
                {
                    IsSuccess = false,
                    Message = errorResponse?.Error ?? "Erro ao atualizar cliente"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar cliente");
            return new ApiResponse<CustomerModel>
            {
                IsSuccess = false,
                Message = "Erro interno do servidor"
            };
        }
    }

    /// <summary>
    /// Remove um cliente
    /// </summary>
    public async Task<ApiResponse<bool>> DeleteCustomerAsync(Guid customerId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/customers/{customerId}");

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Cliente removido com sucesso"
                };
            }
            else
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Erro ao remover cliente"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover cliente");
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                Message = "Erro interno do servidor"
            };
        }
    }
}

/// <summary>
/// Classe para resposta da API
/// </summary>
public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Classe para resposta de login
/// </summary>
public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserInfo User { get; set; } = new();
}

/// <summary>
/// Classe para informações do usuário
/// </summary>
public class UserInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

/// <summary>
/// Classe para resposta de erro
/// </summary>
public class ErrorResponse
{
    public string Error { get; set; } = string.Empty;
}

