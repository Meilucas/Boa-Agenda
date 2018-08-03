using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace wwwroot.usuario
{
    public partial class cadastrar : System.Web.UI.Page
    {
        string szConnection = "Server=127.0.0.1;Database=boa_agenda;Uid=root;Pwd=root;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] != null)
            {
                if (Session["tipo"].ToString() == "0")
                {
                    //   pnlEspecialidade.Visible = true;
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
                        {
                            Response.Write("<script>alert('Você precisa esta logado realizar essa ação');window.location.href = '/'</script>");
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('id invalido');</script>");
                        return;
                    }
                }
                else if (Session["id"] != null)  //verifica se o usuario esta logado
                {
                    if (Session["tipo"].ToString() == "1") //verifica se é um usuario ou 
                    {
                        Session["IdEdicao"] = Convert.ToInt32(Session["id"]);
                        PreencheCampos((int)Session["IdEdicao"]);
                    }
                    else if (Session["tipo"].ToString() == "0") // se for administrador ele não faz nada
                        return;
                    else
                    {
                        Response.Write("<script>alert('Você precisa esta logado realizar essa ação');window.location.href = '/'</script>");
                    }
                }
            }
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
                Salvar();
            }
            else
                Editar((int)Session["IdEdicao"]);
        }

        private void Salvar()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);

            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pr_in_user";
            cmd.Parameters.AddWithValue("_nome", txtNome.Text);
            cmd.Parameters.AddWithValue("_sobrenome", txtSobreNome.Text);
            cmd.Parameters.AddWithValue("_cep", txtCep.Text);
            cmd.Parameters.AddWithValue("_telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("_celular", txtCel.Text);
            cmd.Parameters.AddWithValue("_numero", txtnumero.Text);
            cmd.Parameters.AddWithValue("_email", txtEmail.Text);
            cmd.Parameters.AddWithValue("_login", txtLogin.Text);
            cmd.Parameters.AddWithValue("_senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("_endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("_cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("_rg", txtRG.Text);
            try
            {
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
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
            return true;
        }
        private void Editar(int id)
        {

            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pr_up_user";
            cmd.Parameters.AddWithValue("_id", id);
            cmd.Parameters.AddWithValue("_nome", txtNome.Text);
            cmd.Parameters.AddWithValue("_sobrenome", txtSobreNome.Text);
            cmd.Parameters.AddWithValue("_cep", txtCep.Text);
            cmd.Parameters.AddWithValue("_telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("_celular", txtCel.Text);
            cmd.Parameters.AddWithValue("_numero", txtnumero.Text);
            cmd.Parameters.AddWithValue("_email", txtEmail.Text);
            cmd.Parameters.AddWithValue("_login", txtLogin.Text);
            cmd.Parameters.AddWithValue("_senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("_endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("_cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("_rg", txtRG.Text);

            try
            {
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg.Contains("sucesso"))
                {
                    Session.Remove("IdEdicao");
                    Response.Write("<script>alert('" + msg + "');</script>");
                }
                else
                    Response.Write("<script>alert(\"" + msg + "\");</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"erro " + ex.Message + "\");</script>");
            }
        }
        public void PreencheCampos(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from usuarios where id_usuario = " + id;
            try
            {
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();


                while (rd.Read())
                {
                    txtNome.Text = rd["nome"].ToString();
                    txtSobreNome.Text = rd["sobrenome"].ToString();
                    txtCep.Text = rd["cep"].ToString();
                    txtTelefone.Text = rd["telefone"].ToString();
                    txtCel.Text = rd["celular"].ToString();
                    txtnumero.Text = rd["numero"].ToString();
                    txtEmail.Text = rd["email"].ToString();
                    txtLogin.Text = rd["login"].ToString();
                    txtSenha.TextMode = TextBoxMode.SingleLine;
                    txtSenha.Text = rd["senha"].ToString();
                    txtSenha2.TextMode = TextBoxMode.SingleLine;
                    txtSenha2.Text = rd["senha"].ToString();
                    txtEndereco.Text = rd["endereco"].ToString();
                    txtCPF.Text = rd["cpf"].ToString();
                    txtRG.Text = rd["rg"].ToString();

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }


    }
}