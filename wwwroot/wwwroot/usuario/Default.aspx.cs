using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.usuario
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] != null)
            {
                if (Session["tipo"].ToString() != "root")
                    Response.Write("<script>alert('A pagina so pode ser visualizada pelo administrador');window.location.href = '/'</script>");
            }
            else
                Response.Write("<script>alert('A pagina so pode ser visualizada pelo administrador');window.location.href = '/'</script>");
        }
        public void PreencheGrid(string szNome)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection();

        }
    }
}