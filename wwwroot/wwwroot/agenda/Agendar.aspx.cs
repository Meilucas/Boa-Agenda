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
    public partial class Agendar : System.Web.UI.Page
    {
        /// <summary>
        /// hora que a clinica abre
        /// </summary>
        public TimeSpan Abertura = TimeSpan.Parse("08:30");
        /// <summary>
        /// hora que a clinica fecha
        /// </summary>
        public TimeSpan Fechamento = TimeSpan.Parse("18:30");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                PreencherEspecialidade();
            }
            MostrarHorarios(txtData.Text);
        }

        public void PreencherEspecialidade(string documento = "CRM")
        {
            Dao db = new Dao();
            DataTable tb = db.ExecuteReader("select * from especialidade where documento = '" + documento + "'", CommandType.Text);
            ddlEspecialidades.DataValueField = "id_especialidade";
            ddlEspecialidades.DataTextField = "especialidade";
            ddlEspecialidades.DataSource = tb;
            ddlEspecialidades.DataBind();
        }

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            PreencherEspecialidade(ddlTipoDocumento.SelectedValue);
        }

        public void PreencherHorarios(DateTime dta, int idMedico)
        {
            List<TimeSpan> horarios = new List<TimeSpan>();

            while (Abertura <= Fechamento)
            {
                horarios.Add(Abertura);
                Abertura = Abertura.Add(new TimeSpan(1, 0, 0));
            }

            rdlistHorarios.DataSource = horarios;
            rdlistHorarios.DataBind();
        }

        public void MostrarHorarios(string dta)
        {
            DateTime date;
            int idMedico = Convert.ToInt32(hdnIdMedico.Value);
            if (DateTime.TryParse(dta, out date) && idMedico > 0)
            {
                PreencherHorarios(date, idMedico);
                
            }
            else
            {
              
            }
        }

        protected void btnEscolherMedico_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "escolher")
            {
                hdnIdMedico.Value = e.CommandArgument.ToString();
                MostrarHorarios(txtData.Text);
            }
        }

        protected void lvMedicos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRow medico = (DataRow)e.Item.DataItem;
            Literal txtMedico = (Literal)e.Item.FindControl("txtMedico");
            Literal txtEndereco = (Literal)e.Item.FindControl("txtEndereco");
            Button btnEscolher = (Button)e.Item.FindControl("btnEscolherMedico");

            txtMedico.Text = string.Concat(medico["nome"], " ", medico["sobrenome"]);
            txtEndereco.Text = string.Concat(medico["endereco"], "-", medico["numero"], ", ", medico["cep"]);
            btnEscolher.CommandArgument = medico["id_medico"].ToString();

        }

        protected void btnBuscarMedico_Click(object sender, EventArgs e)
        {
            Dao db = new Dao();
            db.AddParameter("doc", ddlTipoDocumento.SelectedValue);
            db.AddParameter("esp", ddlEspecialidades.SelectedValue);
            DataTable tb = db.ExecuteReader("select * from medico m inner join medicoespecialidade e where e.medico_id = m.id_medico and m.documento = @doc and e.especialidade_id = @esp", CommandType.Text);
            if (tb.Rows.Count > 0) //Validação caso a especialidade existe mas o medico não
            {
                lvMedicos.DataSource = tb.Rows;
                lvMedicos.DataBind();
            }
            else
            {
                hdnIdMedico.Value = "0";
            }
        }
    }
}