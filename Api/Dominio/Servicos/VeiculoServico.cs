using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _contexto;
    public VeiculoServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task ApagarAsync(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Veiculo?> BuscaPorIdAsync(int id)
    {
        return await _contexto.Veiculos
            .AsNoTracking()
            .Where(v => v.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task IncluirAsync(Veiculo veiculo)
    {
        await _contexto.Veiculos.AddAsync(veiculo);
        await _contexto.SaveChangesAsync();
    }

    public async Task<List<Veiculo>> TodosAsync(int? pagina = 1, string? nome = null, string? marca = null)
    {
        var query = _contexto.Veiculos
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(nome))
        {
            query = query
                .AsNoTracking()
                .Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
        }

        if (pagina != null)
        {
            int itensPorPagina = 10;

            query = query
                .AsNoTracking()
                .Skip(((int)pagina - 1) * itensPorPagina)
                .Take(itensPorPagina);
        }

        return await query.AsNoTracking().ToListAsync();
    }
}