using Classes.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.agenda
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlBody.Visible = false;
                if (Session["id"] != null)
                {
                    if (Session["tipo"].ToString() == "1" || Session["tipo"].ToString() == "0")
                    {
                        pnlBody.Visible = true;
                    }
                    else
                        Response.Write("<script>alert('Desculpe mas essa pagina é somente para usuarios');window.location.href = '/'</script>");
                }
                else
                    Response.Write("<script>alert('Efetue login como medico para liberar a pagina');</script>");
            }
        }

        protected void btnPequisar_Click(object sender, ImageClickEventArgs e)
        {
            Dao db = new Dao();

            db.AddParameter("ativa", rbAtivas.Checked);
            db.AddParameter("usuario", Session["id"].ToString());
            DataTable tb = db.ExecuteReader("select * from usuarios u inner join agenda a on a.id_consulta = u.id_usuario where a.dia = '"+ DateTime.Parse(txtData.Text).ToString("yyyy-MM-dd") + "' and a.usuario = @usuario", CommandType.Text);
            lvAgenda.DataSource = tb.Rows;
            lvAgenda.DataBind();
        }

        protected void lvAgenda_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRow item = (DataRow)e.Item.DataItem;

            Literal txtNome = (Literal)e.Item.FindControl("txtNome");
            Literal txtData = (Literal)e.Item.FindControl("txtData");
            Literal txtEmail = (Literal)e.Item.FindControl("txtEmail");
            Literal txtTelefone = (Literal)e.Item.FindControl("txtTelefone");
            Literal txtCelular = (Literal)e.Item.FindControl("txtCelular");
            HyperLink hlkDetail = (HyperLink)e.Item.FindControl("hlkDetail");
         
            txtNome.Text = item["nome"].ToString()+ " "+ item["sobrenome"].ToString();
            txtData.Text = item["dia"].ToString();
            txtEmail.Text = item["email"].ToString();
            txtTelefone.Text = item["telefone"].ToString();
            txtCelular.Text = item["celular"].ToString();
            hlkDetail.NavigateUrl = "/agenda/detalhe.aspx?id=" + item["id_consulta"].ToString(); ;
          

        }
    }
}