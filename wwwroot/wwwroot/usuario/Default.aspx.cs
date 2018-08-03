using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.usuario
{
    public partial class Default : System.Web.UI.Page
    {
        string szConnection = "Server=127.0.0.1;Database=boa_agenda;Uid=root;Pwd=root;";
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
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);
            MySqlDataAdapter ad;

            cmd.CommandText = "select * from usuarios where concat(nome,' ', sobrenome) like '%" + szNome + "%'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                DataTable tb = new DataTable();
                con.Open();
                cmd.ExecuteScalar();
                ad = new MySqlDataAdapter(cmd);
                ad.Fill(tb);
                gdvUser.DataSource = tb;
                gdvUser.DataBind();

            }
            catch (Exception)
            {

            }

        }



        protected void gdvUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Response.Redirect("/usuario/cadastrar.aspx?id=" + Convert.ToInt32(gdvUser.Rows[e.NewSelectedIndex].Cells[0].Text));
        }

        protected void btnPequisar_Click(object sender, ImageClickEventArgs e)
        {
            PreencheGrid(txtPesquisa.Text);
        }
    }
}