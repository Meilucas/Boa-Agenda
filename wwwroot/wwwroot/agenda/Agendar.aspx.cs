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
        public DateTime Abertura = DateTime.Parse("08:30");
        /// <summary>
        /// hora que a clinica fecha
        /// </summary>
        public DateTime Fechamento = DateTime.Parse("18:30");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                PreencherEspecialidade();
            }
            
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
            List<DateTime> horarios = VerificarAgendaData(dta,idMedico);

            while (Abertura <= Fechamento)
            {
                if (true)
                {
                    horarios.Add(Abertura);
                    Abertura = Abertura.Add(new TimeSpan(1, 0, 0));
                }
            }

            rdlistHorarios.DataSource = horarios;
            rdlistHorarios.DataTextFormatString = "HH:mm";
            rdlistHorarios.DataBind();
        }
        /// <summary>
        /// Verifica os horarios ocupados
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<DateTime> VerificarAgendaData(DateTime dt, int idMedico)
        {
            Dao db = new Dao();
            List<DateTime> horarios = new List<DateTime>();
            db.AddParameter("data", dt);//.ToString("yyyy-MM-dd"));
            db.AddParameter("medico", idMedico);
            DataTable tb = db.ExecuteReader("select * from agenda where data = @data and atendente = @medico", CommandType.Text);
            if (tb != null)
            {
                foreach (DataRow item in tb.Rows)
                {
                    horarios.Add(Convert.ToDateTime(dt.ToString("yyyy-MM-dd") + " "+ item["hora"]));
                }

            }
            return horarios;
        }
        public void MostrarHorarios(string dta)
        {
            DateTime date;
            int idMedico = Convert.ToInt32(hdnIdMedico.Value);
            if (DateTime.TryParse(dta, out date) && idMedico > 0)
            {
                PreencherHorarios(date, idMedico);
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
                pnlMedico.Visible = true; // mostra os medicos
            }
            else
            {
                hdnIdMedico.Value = "0";
                pnlMedico.Visible = true;
            }
        }
        public void MostrarMensagem(string msg)
        {
            Response.Write("<script>alert('" + msg + "');</script>");
        }
    }
}