using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot
{
    public partial class Master : System.Web.UI.MasterPage
    {
        string szConnection = "Server=127.0.0.1;Database=boa_agenda;Uid=root;Pwd=root;";
        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaLogin();
        }
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "root" && txtSenha.Text == "root")
            {
                LoginADM();
                return;
            }

            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.CommandText = "select * from usuarios where senha = '" + txtSenha.Text + "' and login = '" + txtLogin.Text + "'";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Session["id"] = rd["id_usuario"].ToString();
                        Session["nome"] = rd["nome"].ToString();
                        Session["tipo"] = 1;
                        Session["email"] = rd["email"].ToString();
                        Response.Redirect("/");
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Login ou Senha não encontrados";
                }

                con.Close();
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro desconhecido " + ex.Message;
            }
        }
        public void LoginADM()
        {
            Session["id"] = 0;
            Session["nome"] = "ADM";
            Session["tipo"] = 0;
            Session["email"] = "nicolas@hotmail.com";
            Response.Redirect("/");
        }
        public void VerificaLogin()
        {
            if (Session["id"] != null)
            {
                lnkLogin.Visible = false;
                btnLogout.Visible = true;
                lblname.Visible = true;
                lblname.InnerText = Session["nome"].ToString();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("/");
        }
        private void LiberaMenuADM() {

        }


    }
}