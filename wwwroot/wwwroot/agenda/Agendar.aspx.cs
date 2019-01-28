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
        public TimeSpan Abertura { get { return TimeSpan.Parse("08:00"); } }
        /// <summary>
        /// hora que a clinica fecha
        /// </summary>
        public TimeSpan Fechamento { get { return TimeSpan.Parse("18:30"); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        public void PreencherHorarios()
        {
            List<TimeSpan> horarios = new List<TimeSpan>();
            for (TimeSpan i = Abertura; i < 23 / 2; i++)
            {

            }
        }
    }
}