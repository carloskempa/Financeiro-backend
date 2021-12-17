using Financeiro.App.Dtos;
using Financeiro.Domain.DataTransferObjects.Filtro;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IMovimentoApp
    {
        public Task<TabelaMovimentacao> ObterMovimentacoes(MovimentoFilter filter, Paginacao paginacao);
        public Task<RetornoPadrao<MovimentoDto>> Cadastrar(MovimentoDto movimentoDto);
    }
}
