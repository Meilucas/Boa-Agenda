<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wwwroot.agenda.Default" MasterPageFile="~/Master.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <div>
        <div>Agendamentos</div>
        <div>
            <asp:GridView runat="server">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="id_consulta" />
                    <asp:BoundField HeaderText="ID" DataField="hora" />
                    <asp:BoundField HeaderText="ID" DataField="dia" />
                    <asp:BoundField HeaderText="ID" DataField="usuario" />
                    <asp:BoundField HeaderText="ID" DataField="atendente" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:ListBox runat="server" ID="lstEspecialidade"></asp:ListBox>
    
    <asp:ListBox runat="server" ID="lstSelecionado"></asp:ListBox>
</asp:Content>
