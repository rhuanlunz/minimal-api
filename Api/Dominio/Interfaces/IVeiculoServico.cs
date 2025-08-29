
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Interfaces;

public interface IVeiculoServico
{
    Task<List<Veiculo>> TodosAsync(int? pagina = 1, string? nome = null, string? marca = null);
    Task<Veiculo?> BuscaPorIdAsync(int id);
    Task IncluirAsync(Veiculo veiculo);
    Task AtualizarAsync(Veiculo veiculo);
    Task ApagarAsync(Veiculo veiculo);
}