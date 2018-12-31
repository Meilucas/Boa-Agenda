using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes.Code;
namespace wwwroot.medico
{
    public partial class cadastrar : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] != null)
            {
                if (Session["tipo"].ToString() == "0")
                {
                    pnlEspecialidade.Visible = true;
                }
            }
            if (!IsPostBack)
            {
                // Verificar se a pagina é de edição ou cadastro 
                // verifica se é edição do administrador
                if (Request.QueryString["id"] != null)
                {
                    try
                    {
                        if (Session["tipo"] != null)
                        {
                            if (Session["tipo"].ToString() != "0")
                                Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");
                            Session["IdEdicao"] = Convert.ToInt32(Request.QueryString["id"]);
                            PreencheCampos((int)Session["IdEdicao"]);
                        }
                        else
                            Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");


                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('id invalido');</script>");
                        return;
                    }
                }
                else if (Session["id"] != null)  //verifica se o usuario esta logado
                {

                    if (Session["tipo"].ToString() == "2") // verifica se ele é um medico se não for ele redireciona para home
                    {
                        Session["IdEdicao"] = Convert.ToInt32(Session["id"]);
                        PreencheCampos((int)Session["IdEdicao"]);
                    }
                    else if (Session["tipo"].ToString() == "0") // se for administrador ele não faz nada
                        return;
                    else
                        Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");

                }
            }
        }
        void CarregaEspecialidades(int id = 0)
        {

            string command = "SELECT ep.id_especialidade, ep.especialidade  FROM medicoespecialidade mep inner join especialidade ep on mep.especialidade_id = ep.id_especialidade where medico_id =" + id;
            var dt = new DataTable();
            Dao dao = new Dao();

            dt = dao.ExecuteReader(command, CommandType.Text);

            lstLista.DataSource = dt;
            lstLista.DataValueField = "id_especialidade";
            lstLista.DataTextField = "especialidade";
            lstLista.DataBind();

            //vai pegar as especialidades ja
            string id_especialidade = "0,";
            foreach (DataRow item in dt.Rows)
                id_especialidade += item[0] + ",";
            id_especialidade = id_especialidade.Remove(id_especialidade.LastIndexOf(','));

            command = "SELECT ep.id_especialidade, ep.especialidade FROM especialidade ep inner join medico md on ep.documento = md.documento  where id_especialidade not in(" + id_especialidade + ")";

            dt = dao.ExecuteReader(command, CommandType.Text);
            lstEspecialidade.DataSource = dt;
            lstEspecialidade.DataValueField = "id_especialidade";
            lstEspecialidade.DataTextField = "especialidade";
            lstEspecialidade.DataBind();

        }
        private bool ValidaDados()
        {
            if (txtCPF.Text.Length < 13)
            {
                Response.Write("<script>alert(' CPF invalido');</script>");
                return false;
            }
            else if (txtRG.Text.Length < 12)
            {
                Response.Write("<script>alert(' RG invalido');</script>");
                return false;
            }
            else if (txtCep.Text.Length < 10)
            {
                Response.Write("<script>alert(' CEP invalido');</script>");
                return false;
            }
            else if (lstLista.Items.Count == 0)
            {
                Response.Write("<script>alert('Escolha pelo menos 1 especialidade ');</script>");
                return false;
            }
            return true;
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidaDados())
            {
                return;
            }
            else if (!Page.IsValid)
            {
                Response.Write("<script>alert('preencha todos os campos corretamente');</script>");
                return;
            }
            else if ((txtSenha2.Text != txtSenha.Text))
            {
                Response.Write("<script>alert('as senhas não conferem');</script>");
                return;
            }
            else if (txtSenha2.Text == "" || txtSenha.Text == "")
            {
                Response.Write("<script>alert('a senha não podem ser nulas');</script>");
                return;
            }

            if (Session["IdEdicao"] == null)
            {
                SalvaMedico();
            }
            else
                Editar((int)Session["IdEdicao"]);
        }
        private void Editar(int id)
        {
            Dao dao = new Dao();
            dao.AddParameter("_id", id);
            dao.AddParameter("_nome", txtNome.Text);
            dao.AddParameter("_sobrenome", txtSobreNome.Text);
            dao.AddParameter("_cep", txtCep.Text);
            dao.AddParameter("_telefone", txtTelefone.Text);
            dao.AddParameter("_celular", txtCel.Text);
            dao.AddParameter("_numero", txtnumero.Text);
            dao.AddParameter("_email", txtEmail.Text);
            dao.AddParameter("_login", txtLogin.Text);
            dao.AddParameter("_senha", txtSenha.Text);
            dao.AddParameter("_endereco", txtEndereco.Text);
            dao.AddParameter("_cpf", txtCPF.Text);
            dao.AddParameter("_rg", txtRG.Text);
            var retorno = dao.ExecuteCommand("pr_up_medico", CommandType.StoredProcedure);
            bool insert = false;
            try
            {
                insert = retorno.ToString().Contains("sucesso");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");

                return;
            }

            string szEsp = Salva_especialidade(id);

            if (insert)
            {
                Session.Remove("IdEdicao");
                Response.Write("<script>alert(' dados inserido com sucesso " + szEsp + "');window.location.href = '/'</script>");
            }
            else
                Response.Write("<script>alert('" + insert + "');</script>");
        }
        private void SalvaMedico()
        {
            Dao dao = new Dao();


            dao.AddParameter("_nome", txtNome.Text);
            dao.AddParameter("_sobrenome", txtSobreNome.Text);
            dao.AddParameter("_cep", txtCep.Text);
            dao.AddParameter("_telefone", txtTelefone.Text);
            dao.AddParameter("_celular", txtCel.Text);
            dao.AddParameter("_numero", txtnumero.Text);
            dao.AddParameter("_email", txtEmail.Text);
            dao.AddParameter("_login", txtLogin.Text);
            dao.AddParameter("_senha", txtSenha.Text);
            dao.AddParameter("_endereco", txtEndereco.Text);
            dao.AddParameter("_cpf", txtCPF.Text);
            dao.AddParameter("_rg", txtRG.Text);
            if (ddlDocumento.SelectedValue == "CRM")
                dao.AddParameter("_documento", "CRM");
            else
                dao.AddParameter("_documento", "CRO");

            try
            {

                string msg = dao.ExecuteCommand("pr_in_medico", CommandType.StoredProcedure).ToString();
                if (msg.Contains("sucesso"))
                {
                    Response.Write("<script>alert('" + msg + "');window.location.href = '/'</script>");
                }
                else
                    Response.Write("<script>alert('" + msg + "');</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }
        public void PreencheCampos(int id)
        {
            Dao dao = new Dao();
            var rd = dao.ExecuteReader("select * from medico where id_medico = " + id, CommandType.Text);

            try
            {

                txtNome.Text = rd.Rows[0]["nome"].ToString();
                txtSobreNome.Text = rd.Rows[0]["sobrenome"].ToString();
                txtCep.Text = rd.Rows[0]["cep"].ToString();
                txtTelefone.Text = rd.Rows[0]["telefone"].ToString();
                txtCel.Text = rd.Rows[0]["celular"].ToString();
                txtnumero.Text = rd.Rows[0]["numero"].ToString();
                txtEmail.Text = rd.Rows[0]["email"].ToString();
                txtLogin.Text = rd.Rows[0]["login"].ToString();
                txtSenha.TextMode = TextBoxMode.SingleLine;
                txtSenha.Text = rd.Rows[0]["senha"].ToString();
                txtSenha2.TextMode = TextBoxMode.SingleLine;
                txtSenha2.Text = rd.Rows[0]["senha"].ToString();
                txtEndereco.Text = rd.Rows[0]["endereco"].ToString();
                txtCPF.Text = rd.Rows[0]["cpf"].ToString();
                txtRG.Text = rd.Rows[0]["rg"].ToString();
                ddlDocumento.SelectedValue = rd.Rows[0]["documento"].ToString();
                CarregaEspecialidades(id);

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }

        public string Salva_especialidade(int id)
        {
            string msg = "";
            // primeiro deleta as ja existentes         
            Dao dao = new Dao();

            try
            {
                dao.ExecuteCommand("delete from medicoespecialidade where medico_id = " + id, CommandType.Text);
            }
            catch (Exception ex)
            {
                msg = "\nerro ao inserir as expecialidades erro: " + ex.Message;
            }

            try
            {
                string commandos = "";
                foreach (ListItem item in lstLista.Items)
                {
                    commandos += "insert into medicoespecialidade(" + id + "," + item.Value + ");";
                }
                dao.ExecuteCommand(commandos, CommandType.Text);
            }
            catch (Exception ex)
            {
                msg = "erro ao inserir as expecialidades erro: " + ex.Message;
            }

            return msg;
        }
        protected void ddlDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string szTipo = "";
            if (ddlDocumento.SelectedValue == "CRM")
                szTipo = "CRM";
            else
                szTipo = "CRO";

            Dao dao = new Dao();

            try
            {
                DataTable dt = dao.ExecuteReader("SELECT * FROM especialidade where documento = '" + szTipo + "';", CommandType.Text);
                lstEspecialidade.DataSource = dt;
                lstEspecialidade.DataValueField = "id_especialidade";
                lstEspecialidade.DataTextField = "especialidade";
                lstEspecialidade.DataBind();
                pnlEspecialidade.Visible = true;
                lstEspecialidade.Dispose();
                lstLista.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}