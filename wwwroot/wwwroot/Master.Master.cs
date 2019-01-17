using Classes.Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                VerificaLogin();
        }
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "root" && txtSenha.Text == "root")
            {
                LoginADM();
                return;
            }
            else if (ddlTipoLogin.SelectedValue == "1")
                Login_user();
            else if (ddlTipoLogin.SelectedValue == "2")
                Login_Medico();
        }


        private void Login_Medico()
        {
            Dao db = new Dao();
            DataTable tb = db.ExecuteReader("select * from medico where senha = '" + txtSenha.Text + "' and login = '" + txtLogin.Text + "'", CommandType.Text);
            if (tb != null)
            {
                if (tb.Rows.Count > 0)
                {
                    Session["id"] = tb.Rows[0]["id_medico"].ToString();
                    Session["nome"] = tb.Rows[0]["nome"].ToString();
                    Session["tipo"] = 2;
                    Session["email"] = tb.Rows[0]["email"].ToString();
                    SendScript("window.location.href = '" + Request.Url.AbsoluteUri + "'");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Login ou Senha não encontrados";
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro desconhecido";
            }
        }
        private void Login_user()
        {


            Dao db = new Dao();
            DataTable tb = db.ExecuteReader("select * from medico where senha = '" + txtSenha.Text + "' and login = '" + txtLogin.Text + "'", CommandType.Text);
            if (tb != null)
            {
                if (tb.Rows.Count > 0)
                {
                    if (tb.Rows.Count > 0)
                    {
                        Session["id"] = tb.Rows[0]["id_usuario"].ToString();
                        Session["nome"] = tb.Rows[0]["nome"].ToString();
                        Session["tipo"] = 1;
                        Session["email"] = tb.Rows[0]["email"].ToString();
                        SendScript("window.location.href = '" + Request.Url.AbsoluteUri + "'");
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Login ou Senha não encontrados";
                }

            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro desconhecido ";
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
        private void LiberarMenuADM()
        {

        }
        private void LiberarMenuMedico() { }
        private void LiberarMenuFuncionario() { }
        public void SendScript(string script)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", script, true);
        }
        public void MontarMenu()
        {
            if (Session["tipo"] != null)
            {
                switch (Convert.ToInt32(Session["tipo"]))
                {
                    case 0: // se for 0 menu é do admin

                        break;
                    case 1: // se for 1 menu é de usuario comum
                        LiberarMenuFuncionario();
                        break;
                    case 2: // se for 1 menu é de usuario comum
                        LiberarMenuMedico();
                        break;
                    default:
                        break;
                }
            }
        }
    }

}