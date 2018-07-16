<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastrar.aspx.cs" Inherits="wwwroot.usuario.cadastrar" MasterPageFile="~/Master.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .form-control {
            width: 100% !important
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <script src="/scripts/jquery.mask.min.js"></script>
    <fieldset>
        <legend>Informações pessoais</legend>
        <div class="form-group ">
            <div class="form-inline ">
                <label class="col-sm-1" for="txtNome">Nome</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNome" />
                </div>
                <label for="txtSobreNome" class="col-2">Sobrenome</label>
                <div class=" col-md-5">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtSobreNome" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="form-inline">
                <label class="col-sm-1" for="txtCPF">CPF</label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCPF" />
                </div>
                <label class="col-sm-1" for="txtRG">RG</label>
                <div class="col-md-5">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRG" />
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Endereco</legend>
        <div class="form-group">
            <div class="form-inline">
                <label class="col-sm-1" for="txtCep">Cep</label>
                <div class="col-md-3 ">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCep" />
                </div>
                <label class="col-sm-1" for="txtEndereco">Endereço</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEndereco" />
                </div>
                <label class="col-sm-1" for="txtnumero">Nº</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnumero" />
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Contato</legend>
        <div class="form-group">
            <div class="form-inline">
                <label class="col-sm-1" for="txtTelefone">Telefone</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefone" />
                </div>
                <label for="txtCel" class="col-sm-1">Celular</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCel" />
                </div>
                <label class="col-sm-1" for="txtEmail">Email</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Acesso</legend>
        <div class="form-group">
            <div class="form-inline">
                <label for="txtLogin">Login</label>
                <div>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtLogin" />
                </div>
                <div>Senha<asp:TextBox runat="server" CssClass="form-control" ID="txtSenha" /></div>
                <div>Repita a senha<asp:TextBox runat="server" CssClass="form-control" ID="txtSenha2" /></div>
            </div>
        </div>
    </fieldset>


</asp:Content>
