<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalhe.aspx.cs" Inherits="wwwroot.agenda.Detalhe" MasterPageFile="~/Master.Master" %>


<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Panel runat="server" ID="pnlBody">
        <asp:HiddenField runat="server" ID="hdnID"></asp:HiddenField>

        <fieldset class=" mt-3 mb-3">
            <legend>Paciente</legend>
            <div class="row">
                <label class="col-md-2">Paciente</label>
                <div class="col-md-3">
                    <asp:Literal runat="server" ID="txtNome"></asp:Literal>
                </div>
                <div class="col-md-1">Data</div>
                <div class="col-md-4">
                    <asp:Literal runat="server" ID="txtData"></asp:Literal>
                </div>
            </div>
        </fieldset>


        <fieldset class=" mt-3 mb-3">
            <legend>Contato</legend>
            <div class="row">
                <label class="col-md-2">Telefone</label>
                <div class="col-md-3">
                    <asp:Literal runat="server" ID="txtCelular"></asp:Literal>
                </div>
                <label class="col-md-1">Email</label>
                <div class="col-md-3">
                    <asp:Literal runat="server" ID="txtEmail"></asp:Literal>
                </div>
            </div>
        </fieldset>
        <fieldset class=" mt-3 mb-3">
            <legend>Procedimento</legend>
            <div class="row">
                <label class="col-md-2">Especialidade:</label>
                <div class="col-md-3">
                    <asp:Literal runat="server" ID="txtEspecialidade"></asp:Literal>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Dados Adicionais</legend>
            <div class="row">
                <label class="col-md-1">Cancelar</label>
                <div class="col-md-4">
                    <asp:RadioButton Text="Sim" runat="server" GroupName="status" ID="rbCancelarSim" />
                    <asp:RadioButton Text="Não" runat="server" GroupName="status" ID="rbCancelarNao"/>
                </div>
            </div>
            <div class="row">
                <label class="col-md-12">Obs</label>
                <div class="col-md-6 " style="height: 200px">
                    <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control h-100" ID="txtObs" />
                </div>
            </div>
        </fieldset>
        <div class="mb-4 mt-4 col-md-6 ">
            <a class="btn btn-success float-left" href="javascript:history.back()">Voltar</a>
            <asp:Button CssClass="btn btn-danger float-right" Text="Salvar" runat="server" ID="btnSalvar"  OnClick="btnSalvar_Click"/>
        </div>
    </asp:Panel>
</asp:Content>

