<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wwwroot.medico.Agenda.Default" MasterPageFile="~/Master.Master" %>


<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:Panel runat="server" ID="pnlBody">
        <div class="form-inline mb-2">
            <asp:TextBox runat="server" ID="txtData" CssClass="form-control" TextMode="Date" />
            <asp:ImageButton ImageUrl="~/img/icons/lupa.png" AlternateText="pesquisar" runat="server" ID="btnPequisar" OnClick="btnPequisar_Click" CssClass="icon" />
        </div>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>Nome</th>
                    <th>Data</th>
                    <th>Email</th>
                    <th>Tel</th>
                    <th>Cel</th>
                    <th>Editar</th>
                </tr>
            </thead>          
            <asp:ListView runat="server" ID="lvAgenda" OnItemDataBound="lvAgenda_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Literal Text="text" runat="server" ID="txtNome" /></td>
                        <td>
                            <asp:Literal Text="text" runat="server" ID="txtData" /></td>
                        <td>
                            <asp:Literal Text="text" runat="server" ID="txtEmail" /></td>
                        <td>
                            <asp:Literal Text="text" runat="server" ID="txtTelefone" /></td>
                        <td>
                            <asp:Literal Text="text" runat="server" ID="txtCelular" /></td>
                        <td>
                            <asp:HyperLink NavigateUrl="navigateurl" runat="server" ID="hlkDetail" Target="_self" Text="Detalhe" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>             
        </table>
    </asp:Panel>
</asp:Content>
