<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastrar.aspx.cs" Inherits="wwwroot.usuario.cadastrar" MasterPageFile="~/Master.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <script src="/scripts/jquery.mask.min.js"></script>
    <div>
        <div class="form-group">
            <div class="col-md-5">
                Nome
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNome" />
            </div>
            <div class=" col-md-7">
                Sobrenome
                <asp:TextBox runat="server" CssClass="form-control" ID="txtSobreNome" />
            </div>
            
        </div>
        <div class="form-group">
            <div class="text-body col-12">Endereco</div>
            <div>Cep<asp:TextBox runat="server" CssClass="form-control" ID="txtCep" /></div>
            <div>Endereço<asp:TextBox runat="server" CssClass="form-control" ID="txtEndereco" /></div>
            <div>Nº<asp:TextBox runat="server" CssClass="form-control" ID="txtnumero" /></div>
            <div>Telefone<asp:TextBox runat="server" CssClass="form-control" ID="txtTelefone" /></div>
            <div>Celular<asp:TextBox runat="server" CssClass="form-control" ID="txtCel" /></div>
            <div>Email<asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" /></div>
        </div>
        <div class="form-group">
            <div class="text-body col-12">Acesso</div>
            <div>Login<asp:TextBox runat="server" CssClass="form-control" ID="txtLogin" /></div>
            <div>Senha<asp:TextBox runat="server" CssClass="form-control" ID="txtSenha" /></div>
            <div>Repita a senha<asp:TextBox runat="server" CssClass="form-control" ID="txtSenha2" /></div>
        </div>
    </div>
</asp:Content>
