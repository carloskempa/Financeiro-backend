using AutoMapper;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.CentroCusto;
using Financeiro.App.Dtos.Pessoa;
using Financeiro.Domain.Entidades;

namespace Financeiro.App.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<ContaFinanceira, ContaFinanceiraDto>();
            CreateMap<CentroCusto, CentroCustoDto>();
            CreateMap<Fornecedor, FornecedorDto>();
            CreateMap<Pessoa, PessoaCadastroDto>();
            CreateMap<Pessoa, PessoaDto>();
            CreateMap<PessoaCentroCusto, PessoaCentroCustoDto>();
            CreateMap<Movimento, MovimentoDto>();
        }
    }
}
