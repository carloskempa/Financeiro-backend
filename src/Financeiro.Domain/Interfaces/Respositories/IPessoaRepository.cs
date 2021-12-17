﻿using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
        Task<Pessoa> ObterPorNome(string Nome);
        void Cadastrar(Pessoa entity);
        void Atualizar(Pessoa entity);
        void Deletar(Pessoa entity);

        Task<PessoaCentroCusto> ObterPorIds(Guid pessoaId, Guid centroCustoId);
        void Cadastrar(PessoaCentroCusto entity);
        void Atualizar(PessoaCentroCusto entity);
        void Deletar(PessoaCentroCusto entity);
    }
}