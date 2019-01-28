<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agendar.aspx.cs" Inherits="wwwroot.agenda.Agendar" MasterPageFile="~/Master.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        #rdlistHorarios input:not(:first-child) {
            display: inline-block;
            margin-left: 10px;
        }

        #rdlistHorarios label {
            display: inline-block;
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <fieldset>
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
                    <asp:Button Text="Buscar" runat="server" CssClass="btn btn-outline-success" />
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Horarios</legend>
        <div class="container">
            <div class="form-group">
                <asp:RadioButtonList runat="server" RepeatLayout="Flow" ID="rdlistHorarios" ClientIDMode="Static" RepeatColumns="3">
                    <asp:ListItem Text="text1" />
                    <asp:ListItem Text="text2" />
                </asp:RadioButtonList>
            </div>
        </div>
    </fieldset>
</asp:Content>
