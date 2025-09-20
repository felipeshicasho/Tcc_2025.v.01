using System.Linq.Expressions;

namespace GestaoMensalidades.API.Repositories;

/// <summary>
/// Interface genérica para repositórios
/// Define operações básicas de CRUD para qualquer entidade
/// </summary>
/// <typeparam name="T">Tipo da entidade</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Busca uma entidade por ID
    /// </summary>
    /// <param name="id">ID da entidade</param>
    /// <returns>Entidade encontrada ou null</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Busca todas as entidades
    /// </summary>
    /// <returns>Lista de entidades</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Busca entidades com filtro
    /// </summary>
    /// <param name="predicate">Expressão de filtro</param>
    /// <returns>Lista de entidades que atendem ao filtro</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Busca a primeira entidade que atende ao filtro
    /// </summary>
    /// <param name="predicate">Expressão de filtro</param>
    /// <returns>Primeira entidade encontrada ou null</returns>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Adiciona uma nova entidade
    /// </summary>
    /// <param name="entity">Entidade a ser adicionada</param>
    /// <returns>Entidade adicionada</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Atualiza uma entidade existente
    /// </summary>
    /// <param name="entity">Entidade a ser atualizada</param>
    /// <returns>Entidade atualizada</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Remove uma entidade
    /// </summary>
    /// <param name="entity">Entidade a ser removida</param>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Remove uma entidade por ID
    /// </summary>
    /// <param name="id">ID da entidade a ser removida</param>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Verifica se existe uma entidade que atende ao filtro
    /// </summary>
    /// <param name="predicate">Expressão de filtro</param>
    /// <returns>True se existe, false caso contrário</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Conta quantas entidades atendem ao filtro
    /// </summary>
    /// <param name="predicate">Expressão de filtro</param>
    /// <returns>Número de entidades</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Salva as alterações no banco de dados
    /// </summary>
    /// <returns>Número de registros afetados</returns>
    Task<int> SaveChangesAsync();
}
