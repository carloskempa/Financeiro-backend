using Financeiro.Relatorios.WebApp.Dtos;
using Financeiro.Relatorios.WebApp.Services;
using Financeiro.Relatorios.WebApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Financeiro.Relatorios.WebApp
{
    public partial class RelatorioMovimentacaoMesMes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarDrops();

            this.txt_dataInicial.Text = DateTime.Parse("01/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString()).ToShortDateString();
            this.txt_dataFinal.Text = DateTime.Parse("31/12/" + DateTime.Now.Year.ToString()).ToShortDateString();
        }


        private void CarregarDrops()
        {
            var _util = new Util();

            var uuu = ObterPessoas().Result;
            _util.CarregaDropDown<PessoaDto>(ddl_idPessoa, ObterPessoas().Result, "nome", "Id", true);
        }


        private async Task<IEnumerable<PessoaDto>> ObterPessoas()
        {
            return await new PessoaService().ObterTodos();
        }


        private void GerarRPT()
        {
                
        }
    }
}