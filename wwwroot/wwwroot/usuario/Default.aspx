<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wwwroot.usuario.Default" MasterPageFile="~/Master.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <div>
        <asp:TextBox runat="server" ID="txtPesquisa" />
        <asp:ImageButton ImageUrl="imageurl" AlternateText="pesquisar" runat="server" ID="btnEnviar" />
    </div>

    <asp:GridView runat="server" AutoGenerateColumns="false" ID="gdvUser">
    </asp:GridView>
</asp:Content>
