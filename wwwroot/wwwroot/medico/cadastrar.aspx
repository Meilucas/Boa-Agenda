<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastrar.aspx.cs" Inherits="wwwroot.medico.cadastrar" MasterPageFile="~/Master.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .form-control {
            width: 100% !important
        }
    </style>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <fieldset>
        <legend>Informações pessoais</legend>
        <div class="form-group ">
            <div class="form-inline ">
                <label class="col-sm-1" for="txtNome">Nome*</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNome" />
                    <asp:RequiredFieldValidator ErrorMessage="Nome" ControlToValidate="txtNome" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
                </div>
                <label for="txtSobreNome" class="col-2">Sobrenome*</label>
                <div class=" col-md-5">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtSobreNome" />
                    <asp:RequiredFieldValidator ErrorMessage="Sobre Nome" ControlToValidate="txtSobreNome" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="form-inline">
                <label class="col-sm-1" for="txtCPF">CPF*</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control cpf" ID="txtCPF" />
                    <asp:RequiredFieldValidator ErrorMessage="CPF" ControlToValidate="txtCPF" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
                </div>
                <label class="col-sm-1" for="txtRG">RG*</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control rg" ID="txtRG" />
                    <asp:RequiredFieldValidator ErrorMessage="RG" ControlToValidate="txtRG" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
                </div>
                <label class="col-sm-1" for="txtRG">Documento*</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control doc" ID="txtDocumento" placeholder="CRM/CRO" />
                    <asp:RequiredFieldValidator ErrorMessage="CRM/CRO" ControlToValidate="txtDocumento" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
                </div>
            </div>
        </div>
        <div>
            <asp:Panel runat="server" ID="pnlTipoUsuario">               
                <div class="col-6">
                    <asp:DropDownList runat="server" ID="ddlDocumento" CssClass="form-control" OnSelectedIndexChanged="ddlDocumento_SelectedIndexChanged" AutoPostBack="true">
                          <asp:ListItem Text="Selecione o Tipo" Value="" />
                        <asp:ListItem Text="Medico" Value="CRM" />
                        <asp:ListItem Text="Dentista" Value="CRO" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Selecione o tipo" ControlToValidate="ddlDocumento" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation"/>
                </div>
            </asp:Panel>
        </div>
    </fieldset>
    <fieldset>
        <legend>Endereco</legend>
        <div class="form-group">
            <div class="form-inline">
                <label class="col-sm-1" for="txtCep">Cep*</label>
                <div class="col-md-3 ">
                    <asp:TextBox runat="server" CssClass="form-control cep" ID="txtCep" />
                    <asp:RequiredFieldValidator ErrorMessage="CEP" ControlToValidate="txtCep" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
                </div>
                <label class="col-sm-1" for="txtEndereco">Endereço</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEndereco" />
                </div>
                <label class="col-sm-1" for="txtnumero">Nº*</label>
                <div class="col-md-2">
                    <asp:TextBox runat="server" CssClass="form-control numero" ID="txtnumero" />
                    <asp:RequiredFieldValidator ErrorMessage="Numero da casa" ControlToValidate="txtnumero" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" />
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
                    <asp:TextBox runat="server" CssClass="form-control tel" ID="txtTelefone" />
                </div>
                <label for="txtCel" class="col-sm-1">Celular</label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control cel" ID="txtCel" />
                </div>
                <label class="col-sm-1" for="txtEmail">Email*</label>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" TextMode="Email" />
                    <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Email" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" BorderColor="Red" />
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset runat="server" id="pnlEspecialidade" visible="false">
        <legend>Especialidade</legend>
        <asp:ListBox runat="server" ID="lstEspecialidade" SelectionMode="Multiple" Rows="7" CssClass="sortable" ValidateRequestMode="Disabled" DataTextField="especialidade" DataValueField="id_especialidade" AppendDataBoundItems="true" ClientIDMode="Static"></asp:ListBox>
        <a href="#" id="btnAdicionar">Adicionar</a>
        <a href="#" id="btnRemove">Remover</a>        
        <asp:HiddenField  runat="server" ID="hdnEspecialidades" value="" ClientIDMode="Static"/>
        <select Multiple="true" size="7" class="sortable" ID="lstLista" runat="server" ClientIDMode="Static"></select>
    
    </fieldset>
    <fieldset>
        <legend>Acesso</legend>
        <div class="form-group">
            <div class="form-inline">

                <div class="col-md-3">
                    Login*
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtLogin" />
                    <asp:RegularExpressionValidator ValidationExpression="^\d{4,8}$" ErrorMessage="O login deve conter de 4 a caracteres 8 " ControlToValidate="txtLogin" runat="server" ValidationGroup="validation" Display="None" />
                </div>

                <div class="col-md-3">Senha*<asp:TextBox runat="server" CssClass="form-control" ID="txtSenha" TextMode="Password" /></div>
                <asp:RegularExpressionValidator ValidationExpression="^\d{4,8}$" ErrorMessage="senha deve conter de 4 a caracteres 8" ForeColor="Red" Display="None" ControlToValidate="txtSenha" runat="server" ValidationGroup="validation" />
                <asp:RequiredFieldValidator ErrorMessage="Senha" ControlToValidate="txtSenha" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" BorderColor="Red" />
                <asp:CompareValidator ErrorMessage="As senhas não conferem " ControlToValidate="txtSenha" runat="server" ControlToCompare="txtSenha2" ValidationGroup="validation" Display="None" />

                <div class="col-md-3">Repita a senha<asp:TextBox runat="server" CssClass="form-control" ID="txtSenha2" TextMode="Password" /></div>
                <asp:RequiredFieldValidator ErrorMessage="Re-Senha" ControlToValidate="txtSenha2" runat="server" ForeColor="Red" Display="None" ValidationGroup="validation" BorderColor="Red" />

            </div>
        </div>
    </fieldset>
    <asp:ValidationSummary runat="server" ValidationGroup="validation" ForeColor="Red" HeaderText="Campos invalidos" />
    <asp:Button Text="Salvar" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" ValidationGroup="validation" CssClass="btn btn-success" />
    <script src="/scripts/jquery.mask.min.js"></script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="footer">
    <script src="/scripts/jquery-ui.js"></script>
    <script src="/scripts/jquery.selectlistactions.js"></script>
    <script>
        $(function () {            
            $('#ddlDocumento').on('change', function () {
                alert($('#ddlDocumento').find('option [selected="true"] '));

            });
            $("#btnRemove").on("click", function (e) {
                $('select').moveToListAndDelete('#lstLista', '#lstEspecialidade');
                setValueEspec();
                e.preventDefault();
            })
            $("#btnAdicionar").on("click", function (e) {
                $('select').moveToListAndDelete('#lstEspecialidade', '#lstLista');
                setValueEspec();
                e.preventDefault();
            });

            $('.cpf').mask('000.000.000-00');
            $('.rg').mask('00.000.000-00');
            $('.numero').mask('0000');
            $('.tel').mask('(00) 0000-0000');
            $('.cel').mask('(00) 00000-0000');
            $('.cep').mask('0000000-00');
            $('.doc').mask('0000000');
        });
        function setValueEspec() {
            var options =$.makeArray( $('#lstLista').find('option').map(function (index, arr, ext) {
                return $(arr).val()
            }));

            $('#hdnEspecialidades').val(options);
        }
    </script>

</asp:Content>
