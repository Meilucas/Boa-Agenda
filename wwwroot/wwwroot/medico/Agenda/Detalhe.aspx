<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalhe.aspx.cs" Inherits="wwwroot.medico.Agenda.Detalhe" MasterPageFile="~/Master.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <asp:panel runat="server" id="pnlBody">
        <asp:HiddenField runat="server" ID="hdnID"></asp:HiddenField>
        <div class="row">
            <div class="col-lg-12">
             
                    <fieldset class="form-inline">
                             <div class="col-md-12">
                    <label class="col-md-1">Paciente</label>
                    <div class="col-md-4"> <asp:Literal runat="server" ID="txtNome"></asp:Literal></div>
                    <div class="col-md-1">Data</div>
                    <div class="col-md-4"><asp:Literal runat="server" ID="txtData"></asp:Literal></div> </div>
                </fieldset>
                  
              
                <fieldset class="form-inline">
                    <legend>Contato</legend>
                    <label class="col-md-1">Telefone</label>
                    <div class="col-md-3"><asp:Literal runat="server" ID="txtCelular" ></asp:Literal></div>
                    <label class="col-md-1">Email</label>
                    <div class="col-md-3"><asp:Literal runat="server" ID="txtEmail" ></asp:Literal></div>
                </fieldset>
                <fieldset class="form-inline">
                    <label class="col-md-2">Especialidade</label>
                    <div class="col-md-3"><asp:Literal runat="server" ID="txtEspecialidade"></asp:Literal></div>
                </fieldset>
            </div>
        </div>
    </asp:panel>
</asp:Content>
