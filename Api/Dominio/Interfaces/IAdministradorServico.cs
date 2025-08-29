using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces;

public interface IAdministradorServico
{
    Task<Administrador?> LoginAsync(LoginDTO loginDTO);
    Task<Administrador> IncluirAsync(Administrador administrador);
    Task<Administrador?> BuscaPorIdAsync(int id);
    Task<List<Administrador>> TodosAsync(int? pagina);
}