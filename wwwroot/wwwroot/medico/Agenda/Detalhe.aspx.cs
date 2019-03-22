using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.medico.Agenda
{
    public partial class Detalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /* if (!IsPostBack)
            {
                pnlBody.Visible = false;
                if (Session["id"] != null)
                {
                    if (Session["tipo"].ToString() == "2" || Session["tipo"].ToString() == "0")
                    {
                        pnlBody.Visible = true;
                    }
                    else
                        Response.Write("<script>alert('Desculpe mas essa pagina é somente para Medicos');window.location.href = '/'</script>");
                }
                else
                    Response.Write("<script>alert('Efetue login como medico para liberar a pagina');</script>");
            }*/
        }
    }
}