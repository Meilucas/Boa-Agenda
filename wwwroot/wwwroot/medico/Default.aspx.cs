using Classes.Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.medico
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] != null)
            {
                if (Session["tipo"].ToString() != "0")
                    Response.Write("<script>alert('A pagina so pode ser visualizada pelo administrador');window.location.href = '/'</script>");
            }
            else
                Response.Write("<script>alert('A pagina so pode ser visualizada pelo administrador');window.location.href = '/'</script>");


            PreencheGrid("");
        }
        public void PreencheGrid(string szNome)
        {
            Dao db = new Dao();
            DataTable tb = db.ExecuteReader("select * from medico where concat(nome,' ', sobrenome) like '%" + szNome + "%'", CommandType.Text);
            if (tb != null)
            {
                gdvUser.DataSource = tb;
                gdvUser.DataBind();
            }
        }
        protected void gdvUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Response.Redirect("/medico/cadastrar.aspx?id=" + Convert.ToInt32(gdvUser.Rows[e.NewSelectedIndex].Cells[0].Text));
        }

        protected void btnPequisar_Click(object sender, ImageClickEventArgs e)
        {
            PreencheGrid(txtPesquisa.Text);
        }
    }
}