using Classes.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace wwwroot.medico.Agenda
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
               
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
            }
        }

        protected void btnPequisar_Click(object sender, ImageClickEventArgs e)
        {
            Dao db = new Dao();
            db.AddParameter("dia", txtData.Text);
            db.AddParameter("user", Session["id"].ToString());
            db.AddParameter("ativa", rbAtivas.Checked);
            DataTable tb = db.ExecuteReader("select id_consulta, hora, dia, concat(nome, ' ', sobrenome) as nome, email , telefone, celular,ativa ,e.especialidade from agenda a inner join usuarios u inner join especialidade e where a.usuario = u.id_usuario and e.id_especialidade = a.especialidade_id and dia = @dia and atendente = @user and ativa = @ativa   ", CommandType.Text);
            if (tb != null)
            {
                lvAgenda.DataSource = tb.Rows;
                lvAgenda.DataBind();
            }
            else
            {
                lvAgenda.DataSource = null;
                lvAgenda.DataBind();
            }
        }

        protected void lvAgenda_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRow tb = (DataRow)e.Item.DataItem;

            Literal txtNome = (Literal)e.Item.FindControl("txtNome");
            Literal txtData = (Literal)e.Item.FindControl("txtData");
            Literal txtEspecialidade = (Literal)e.Item.FindControl("txtCelular");
            Literal txtEmail = (Literal)e.Item.FindControl("txtEmail");
            Literal txtTelefone = (Literal)e.Item.FindControl("txtTelefone");
            Literal txtCelular = (Literal)e.Item.FindControl("txtCelular");
            HyperLink hlkDetail = (HyperLink)e.Item.FindControl("hlkDetail");

            txtEspecialidade.Text = tb["especialidade"].ToString();

            txtNome.Text = tb["nome"].ToString();
            txtData.Text = tb["dia"].ToString().Substring(0, 8) + " " + tb["hora"].ToString().Substring(0, 5);
            txtEmail.Text = tb["email"].ToString();
            txtTelefone.Text = tb["telefone"].ToString();
            txtCelular.Text = tb["celular"].ToString();
            hlkDetail.NavigateUrl = "/medico/Agenda/Detalhe.aspx?id=" + tb["id_consulta"].ToString();
        }

        protected void btnFinalizar_Command(object sender, CommandEventArgs e)
        {

        }
    }
}