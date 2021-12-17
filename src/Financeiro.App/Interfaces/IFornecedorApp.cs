﻿using Canducci.Pagination;
using Financeiro.App.Dtos;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IFornecedorApp
    {
        Task<PaginatedRest<Fornecedor>> Listar(Paginacao paginacao);
        Task<RetornoPadrao<FornecedorDto>> ObterPorId(Guid id);
        Task<RetornoPadrao<FornecedorDto>> Cadastrar(FornecedorDto fornecedor);
        Task<RetornoPadrao<FornecedorDto>> Atualizar(FornecedorDto fornecedor);
    }
}
