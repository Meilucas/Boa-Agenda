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
            if (Session["tipo"] == null)
            {
                if (Session["tipo"].ToString() == "root")
                {
                    pnlTipoUsuario.Visible = true;
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
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
            if (Session["id"] == null)
                Salvar();
            else
                Editar();
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
                Response.Write("<script>alert('" + msg + "');</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }

        }

        private void Editar()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);

            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pr_up_user";
            cmd.Parameters.AddWithValue("_id", Session["id"].ToString());
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
            cmd.Parameters.AddWithValue("_tipo", );
            try
            {
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                Response.Write("<script>alert('" + msg + "');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }

    }
}