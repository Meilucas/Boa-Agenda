<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wwwroot.usuario.Default" MasterPageFile="~/Master.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <div class="form-inline">
        <asp:TextBox runat="server" ID="txtPesquisa" CssClass="form-control" />
        <asp:ImageButton ImageUrl="~/img/icons/lupa.png" AlternateText="pesquisar" runat="server" ID="btnPequisar" OnClick="btnPequisar_Click" CssClass="icon" />
    </div>

    <div class="table-responsive">
        <asp:GridView runat="server" ID="gdvUser" AutoGenerateColumns="False" CssClass=" table table-hover" OnSelectedIndexChanging="gdvUser_SelectedIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Id" DataField="id_usuario" />
                <asp:BoundField HeaderText="Nome" DataField="nome" />
                <asp:BoundField HeaderText="Sobrenome" DataField="sobrenome" />
                <asp:BoundField HeaderText="Email" DataField="email" />
                <asp:BoundField HeaderText="Cpf" DataField="cpf" />
                <asp:BoundField HeaderText="RG" DataField="rg" />              
                <asp:CommandField  HeaderText="#" ShowSelectButton="true"  />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
