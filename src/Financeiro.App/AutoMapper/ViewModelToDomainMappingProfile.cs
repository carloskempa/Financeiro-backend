using AutoMapper;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.CentroCusto;
using Financeiro.App.Dtos.Pessoa;
using Financeiro.Domain.Entidades;

namespace Financeiro.App.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioDto, Usuario>();
            CreateMap<ContaFinanceiraDto, ContaFinanceira>();
            CreateMap<CentroCustoDto, CentroCusto>();
            CreateMap<FornecedorDto, Fornecedor>();
            CreateMap<PessoaDto, Pessoa>();
            CreateMap<PessoaCadastroDto, Pessoa>();
            CreateMap<PessoaCentroCustoDto, PessoaCentroCusto>();
            CreateMap<MovimentoDto, Movimento>();
        }
    }
}