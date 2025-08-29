using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;
    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Administrador?> BuscaPorIdAsync(int id)
    {
        return await _contexto.Administradores
            .AsNoTracking()
            .Where(v => v.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Administrador> IncluirAsync(Administrador administrador)
    {
        await _contexto.Administradores.AddAsync(administrador);
        await _contexto.SaveChangesAsync();

        return administrador;
    }

    public async Task<Administrador?> LoginAsync(LoginDTO loginDTO)
    {
        return await _contexto.Administradores
            .AsNoTracking()
            .Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Administrador>> TodosAsync(int? pagina)
    {
        var query = _contexto.Administradores
            .AsNoTracking()
            .AsQueryable();

        if (pagina != null)
        {
            int itensPorPagina = 10;

            query = query
                .AsNoTracking()
                .Skip(((int)pagina - 1) * itensPorPagina)
                .Take(itensPorPagina);
        }

        return await query.ToListAsync();
    }
}