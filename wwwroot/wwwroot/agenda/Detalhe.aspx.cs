﻿using Classes.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.agenda
{
    public partial class Detalhe : System.Web.UI.Page
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
                        PreencherCampos();
                    }
                    else
                        Response.Write("<script>alert('Desculpe mas essa pagina é somente para Medicos');window.location.href = '/'</script>");
                }
                else
                    Response.Write("<script>alert('Efetue login como medico para liberar a pagina');</script>");
            }
        }

        public void PreencherCampos()
        {
            string id = Request.QueryString["id"].ToString();
            string query = "SELECT a.id_consulta,a.hora,a.dia, concat(u.nome, ' ', u.sobrenome) usuario,u.email ,u.cep,u.celular , concat(m.nome, ' ', m.sobrenome) as medico, " +
                            "e.especialidade ,a.ativa,a.obs FROM agenda a inner join medico m inner join especialidade e inner join usuarios u where m.id_medico = a.atendente and e.id_especialidade = a.especialidade_id and u.id_usuario = a.usuario " +
                            " and a.id_consulta = @id";
            Dao db = new Dao();
            db.AddParameter("id", id);
            db.AddParameter("medico", Session["id"]);
            DataTable td = db.ExecuteReader(query, CommandType.Text);
            if (td != null)
            {
                if (td.Rows.Count > 0)
                {
                    txtNome.Text = td.Rows[0]["usuario"].ToString();
                    hdnID.Value = td.Rows[0]["id_consulta"].ToString();
                    txtData.Text = DateTime.Parse(td.Rows[0]["dia"].ToString()).Add(TimeSpan.Parse(td.Rows[0]["hora"].ToString())).ToString("dd/MM/yyyy HH:mm");
                    txtCelular.Text = td.Rows[0]["celular"].ToString();
                    txtEmail.Text = td.Rows[0]["email"].ToString();
                    txtEspecialidade.Text = td.Rows[0]["especialidade"].ToString();
                    txtObs.Text = td.Rows[0]["obs"].ToString();
                    rbCancelarSim.Checked = Convert.ToBoolean(td.Rows[0]["ativa"]);
                    rbCancelarNao.Checked = !Convert.ToBoolean(td.Rows[0]["ativa"]);
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Dao db = new Dao();
            db.AddParameter("obs", txtObs.Text);
            db.AddParameter("ativa", rbCancelarSim.Checked);
            db.AddParameter("id", Request.QueryString["id"].ToString());
            db.ExecuteCommand("update agenda set obs = @obs, ativa = @ativa where id_consulta = @id", CommandType.Text);
            Response.Write("<script>alert('Dados atualizados com sucesso')</script>");
        }
    }
}