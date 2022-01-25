<%@ Page Title="Relatório Movimentações Mês a Mês" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioMovimentacaoMesMes.aspx.cs" Inherits="Financeiro.Relatorios.WebApp.RelatorioMovimentacaoMesMes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="updPnl_geral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>Relatório de Movimentação Mês a Mês</h2>
            <hr />

            <div class="linha-col-4">

                <div>
                    <label>Tipo</label>
                    <asp:DropDownList ID="ddl_idTipo" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="(AMBOS)" Value="T"></asp:ListItem>
                        <asp:ListItem Text="RECEITA" Value="R"></asp:ListItem>
                        <asp:ListItem Text="DESPESA" Value="D" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div>
                    <label>Pessoa</label>
                    <asp:DropDownList ID="ddl_idPessoa" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div>
                    <label>Conta ou Cartão</label>
                    <asp:DropDownList ID="ddl_idConta" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div>
                    <label>Centro Custo</label>
                    <asp:DropDownList ID="ddl_idCentroCusto" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>

            </div>

            <div class="linha-col-4">

                <div>
                    <label>Data Inicial (Vencimento)</label>
                    <asp:TextBox ID="txt_dataInicial" runat="server" onkeypress="return MascaraData(this,event);" MaxLength="10"></asp:TextBox>
                    <asp:CalendarExtender ID="calex_txt_dataInicial" runat="server" TargetControlID="txt_dataInicial" Format="dd/MM/yyyy" CssClass="cal_Theme1"></asp:CalendarExtender>
                </div>

                <div>
                    <label>Data Final (Vencimento)</label>
                    <asp:TextBox ID="txt_dataFinal" runat="server" onkeypress="return MascaraData(this,event);" MaxLength="10"></asp:TextBox>
                    <asp:CalendarExtender ID="calex_txt_dataFinal" runat="server" TargetControlID="txt_dataFinal" Format="dd/MM/yyyy" CssClass="cal_Theme1"></asp:CalendarExtender>
                </div>

                <div>
                    <label>Orçamento</label>
                    <asp:CheckBox ID="chkBox_orcado" runat="server" Checked="true" />
                </div>

                <div class="btn-acoes">
                    <asp:LinkButton ID="linkBtn_gerar" runat="server" Text="Gerar Relatório" CssClass="btn sucesso"></asp:LinkButton>
                </div>

            </div>
            <asp:Panel ID="pnl_listagem" runat="server" CssClass="listagem" Width="1100px">
                <asp:Literal ID="lit_mensagem" runat="server"></asp:Literal>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1500px"></rsweb:ReportViewer>
                <div class="clear"></div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <style>
       
    </style>


</asp:Content>
