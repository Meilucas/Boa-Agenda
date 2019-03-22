<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agendar.aspx.cs" Inherits="wwwroot.agenda.Agendar" MasterPageFile="~/Master.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        #rdlistHorarios input:not(:first-child) {
            display: inline-block;
            margin-left: 10px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <fieldset id="pnlPage" runat="server" >
        <legend>Agendar Atendimento</legend>
        <div class="container">

            <div class="form-group ">
                <div class="form-inline">

                    <label class="col-md-2">Tipo de Consulta</label>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="ddlTipoDocumento" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                            <asp:ListItem Text="Medico" Value="CRM" />
                            <asp:ListItem Text="Dentista" Value="CRO" />
                        </asp:DropDownList>
                    </div>
                    <label class=" col-md-2">Especialidade</label>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlEspecialidades" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="form-inline">
                    <asp:Button Text="Buscar" runat="server" CssClass="btn btn-outline-success" ID="btnBuscarMedico" OnClick="btnBuscarMedico_Click" />
                </div>
            </div>

        </div>
    </fieldset>

    <asp:PlaceHolder runat="server" ID="pnlMedico" Visible="false">
        <fieldset>
            <asp:HiddenField runat="server" ID="hdnIdMedico" Value="0" />
            <legend>Medicos</legend>
            <div>
                <table class="table">
                    <tr>
                        <th>Medico</th>
                        <th>Endereço</th>
                        <th>#</th>
                    </tr>
                    <tr>
                        <asp:ListView runat="server" ID="lvMedicos" OnItemDataBound="lvMedicos_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <span>
                                        <asp:Literal Text="text" runat="server" ID="txtMedico" /></span> </td>
                                <td>
                                    <span>
                                        <asp:Literal Text="text" runat="server" ID="txtEndereco" /></span> </td>
                                <td>
                                    <asp:Button Text="Escolher" runat="server" ID="btnEscolherMedico" OnCommand="btnEscolherMedico_Command" CommandName="escolher" CssClass="btn btn-outline-primary" /></td>
                            </ItemTemplate>
                        </asp:ListView>
                    </tr>
                </table>

            </div>
        </fieldset>


        <asp:PlaceHolder runat="server" ID="pnlData" Visible="false">
            <fieldset>
                <legend>Data</legend>
                <div class="form-inline">
                    <asp:TextBox runat="server" ID="txtData" TextMode="Date" CssClass="form-control" Width="50%" />
                    <asp:Button Text="Buscar Horarios" runat="server" ID="btnBuscarHorarios" CssClass="btn btn-success" OnClick="btnBuscarHorarios_Click" />
                </div>
            </fieldset>


            <asp:PlaceHolder runat="server" ID="pnlHorarios" Visible="false">
                <fieldset style="width: 50%">
                    <legend>Horarios</legend>
                    <div class="container">
                        <div class="form-group">
                            <asp:RadioButtonList runat="server" RepeatLayout="Table" ID="rdlistHorarios" ClientIDMode="Static" RepeatColumns="3" CssClass="table">
                                <asp:ListItem Text="text1" />
                                <asp:ListItem Text="text2" />
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </fieldset>
                <asp:Button Text="Agendar" runat="server" ID="btnAgendar" CssClass="btn btn-outline-success" OnClick="btnAgendar_Click" />
            </asp:PlaceHolder>
        </asp:PlaceHolder>
    </asp:PlaceHolder>
</asp:Content>
