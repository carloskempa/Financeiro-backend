using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Financeiro.Relatorios.WebApp.Utils
{
    public class Util
    {
        public void CarregaDropDown<T>(DropDownList sender,
                                    IEnumerable<T> list,
                                    string campoTexto,
                                    string campoValorRetorno,
                                    bool isNotObrigatorio,
                                    string p_textoZERO = "Selecione...")  where T : class
        {
            try
            {
                sender.Items.Clear();
                sender.DataSource = list;
                sender.DataTextField = campoTexto;
                sender.DataValueField = campoValorRetorno;
                sender.DataBind();

                if (sender.Items.Count == 0)
                    sender.Items.Insert(0, new ListItem("**** [Atenção: Nenhum Item Cadastrado] ****", "0"));

                else if (sender.Items.Count > 1 || isNotObrigatorio)
                    sender.Items.Insert(0, new ListItem(p_textoZERO, "0"));

            }
            catch (Exception ex)
            {
                throw new Exception("Problema Interno! Não Posso Carregar os Dados Para Campo DropDown: " + sender.ID, ex);
            }
        }
    }
}