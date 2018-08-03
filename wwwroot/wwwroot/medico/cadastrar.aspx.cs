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
    public partial class cadastrar : System.Web.UI.Page
    {
        string szConnection = "Server=127.0.0.1;Database=boa_agenda;Uid=root;Pwd=root;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] != null)
            {
                if (Session["tipo"].ToString() == "0")
                {
                    pnlEspecialidade.Visible = true;
                }
            }
            if (!IsPostBack)
            {
                // Verificar se a pagina é de edição ou cadastro 
                // verifica se é edição do administrador
                if (Request.QueryString["id"] != null)
                {
                    try
                    {
                        if (Session["tipo"] != null)
                        {
                            if (Session["tipo"].ToString() != "0")
                                Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");
                            Session["IdEdicao"] = Convert.ToInt32(Request.QueryString["id"]);
                            PreencheCampos((int)Session["IdEdicao"]);
                        }
                        else
                            Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");


                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('id invalido');</script>");
                        return;
                    }
                }
                else if (Session["id"] != null)  //verifica se o usuario esta logado
                {

                    if (Session["tipo"].ToString() == "2") // verifica se ele é um medico se não for ele redireciona para home
                    {
                        Session["IdEdicao"] = Convert.ToInt32(Session["id"]);
                        PreencheCampos((int)Session["IdEdicao"]);
                    }
                    else if (Session["tipo"].ToString() == "0") // se for administrador ele não faz nada
                        return;
                    else
                        Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");

                }
            }
        }
        void CarregaEspecialidades(int id = 0)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter ad;
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ep.id_especialidade, ep.especialidade  FROM medicoespecialidade mep inner join especialidade ep on mep.especialidade_id = ep.id_especialidade where medico_id =" + id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            lstLista.DataSource = dt;
            lstLista.DataValueField = "id_especialidade";
            lstLista.DataTextField = "especialidade";
            lstLista.DataBind();

            //vai pegar as especialidades ja
            string id_especialidade = "0,";
            foreach (DataRow item in dt.Rows)
                id_especialidade += item[0] + ",";
            id_especialidade = id_especialidade.Remove(id_especialidade.LastIndexOf(','));

            cmd.CommandText = "SELECT ep.id_especialidade, ep.especialidade FROM especialidade ep inner join medico md on ep.documento = md.documento  where id_especialidade not in(" + id_especialidade + ")";
            dt = new DataTable();
            cmd.ExecuteNonQuery();
            ad = new MySqlDataAdapter(cmd);
            ad.Fill(dt);

            lstEspecialidade.DataSource = dt;
            lstEspecialidade.DataValueField = "id_especialidade";
            lstEspecialidade.DataTextField = "especialidade";
            lstEspecialidade.DataBind();

        }
        private bool ValidaDados()
        {
            if (txtCPF.Text.Length < 13)
            {
                Response.Write("<script>alert(' CPF invalido');</script>");
                return false;
            }
            else if (txtRG.Text.Length < 12)
            {
                Response.Write("<script>alert(' RG invalido');</script>");
                return false;
            }
            else if (txtCep.Text.Length < 10)
            {
                Response.Write("<script>alert(' CEP invalido');</script>");
                return false;
            }
            else if (lstLista.Items.Count == 0)
            {
                Response.Write("<script>alert('Escolha pelo menos 1 especialidade ');</script>");
                return false;
            }
            return true;
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidaDados())
            {
                return;
            }
            else if (!Page.IsValid)
            {
                Response.Write("<script>alert('preencha todos os campos corretamente');</script>");
                return;
            }
            else if ((txtSenha2.Text != txtSenha.Text))
            {
                Response.Write("<script>alert('as senhas não conferem');</script>");
                return;
            }
            else if (txtSenha2.Text == "" || txtSenha.Text == "")
            {
                Response.Write("<script>alert('a senha não podem ser nulas');</script>");
                return;
            }

            if (Session["IdEdicao"] == null)
            {
                SalvaMedico();
            }
            else
                Editar((int)Session["IdEdicao"]);
        }
        private void Editar(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pr_up_medico";
            cmd.Parameters.AddWithValue("_id", id);
            cmd.Parameters.AddWithValue("_nome", txtNome.Text);
            cmd.Parameters.AddWithValue("_sobrenome", txtSobreNome.Text);
            cmd.Parameters.AddWithValue("_cep", txtCep.Text);
            cmd.Parameters.AddWithValue("_telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("_celular", txtCel.Text);
            cmd.Parameters.AddWithValue("_numero", txtnumero.Text);
            cmd.Parameters.AddWithValue("_email", txtEmail.Text);
            cmd.Parameters.AddWithValue("_login", txtLogin.Text);
            cmd.Parameters.AddWithValue("_senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("_endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("_cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("_rg", txtRG.Text);
            bool insert = false;
            try
            {
                con.Open();
                 insert = cmd.ExecuteScalar().ToString().Contains("sucesso");
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
                con.Close();
                return;
            }

            string szEsp= Salva_especialidade(id);

            if (insert)
            {
                Session.Remove("IdEdicao");
                Response.Write("<script>alert(' dados inserido com sucesso " +szEsp + "');window.location.href = '/'</script>");
            }
            else
                Response.Write("<script>alert('" + insert + "');</script>");
            con.Close();
        }
        private void SalvaMedico()
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);

            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pr_in_medico";
            cmd.Parameters.AddWithValue("_nome", txtNome.Text);
            cmd.Parameters.AddWithValue("_sobrenome", txtSobreNome.Text);
            cmd.Parameters.AddWithValue("_cep", txtCep.Text);
            cmd.Parameters.AddWithValue("_telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("_celular", txtCel.Text);
            cmd.Parameters.AddWithValue("_numero", txtnumero.Text);
            cmd.Parameters.AddWithValue("_email", txtEmail.Text);
            cmd.Parameters.AddWithValue("_login", txtLogin.Text);
            cmd.Parameters.AddWithValue("_senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("_endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("_cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("_rg", txtRG.Text);
            if (ddlDocumento.SelectedValue == "CRM")
                cmd.Parameters.AddWithValue("_documento", "CRM");
            else
                cmd.Parameters.AddWithValue("_documento", "CRO");

            try
            {
                con.Open();
                string msg = cmd.ExecuteScalar().ToString();
                con.Close();
                if (msg.Contains("sucesso"))
                {
                    Response.Write("<script>alert('" + msg + "');window.location.href = '/'</script>");
                }
                else
                    Response.Write("<script>alert('" + msg + "');</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }
        public void PreencheCampos(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from medico where id_medico = " + id;
            try
            {
                con.Open();
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    txtNome.Text = rd["nome"].ToString();
                    txtSobreNome.Text = rd["sobrenome"].ToString();
                    txtCep.Text = rd["cep"].ToString();
                    txtTelefone.Text = rd["telefone"].ToString();
                    txtCel.Text = rd["celular"].ToString();
                    txtnumero.Text = rd["numero"].ToString();
                    txtEmail.Text = rd["email"].ToString();
                    txtLogin.Text = rd["login"].ToString();
                    txtSenha.TextMode = TextBoxMode.SingleLine;
                    txtSenha.Text = rd["senha"].ToString();
                    txtSenha2.TextMode = TextBoxMode.SingleLine;
                    txtSenha2.Text = rd["senha"].ToString();
                    txtEndereco.Text = rd["endereco"].ToString();
                    txtCPF.Text = rd["cpf"].ToString();
                    txtRG.Text = rd["rg"].ToString();
                    ddlDocumento.SelectedValue = rd["documento"].ToString();
                    CarregaEspecialidades(id);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }

        public string Salva_especialidade(int id)
        {
            string msg = "";
            // primeiro deleta as ja existentes         
            MySqlConnection con = new MySqlConnection(szConnection);
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "delete from medicoespecialidade where medico_id = " + id;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                msg = "\nerro ao inserir as expecialidades erro: " + ex.Message;
            }

            try
            {
                string commandos = "";
                foreach (ListItem item in lstLista.Items)
                {
                    commandos += "insert into medicoespecialidade(" + id + "," + item.Value + ");";
                }
                cmd.CommandText = commandos;
                int ret = cmd.ExecuteNonQuery();
                if (ret == lstLista.Items.Count)
                    msg = "";
                else if (ret == 0)
                    msg = "\nNão foi possivel salvar as especialidades";
                else if (ret < lstLista.Items.Count)
                    msg = "\nErro, foram salvas so algumas especialidade";

                con.Close();


            }
            catch (Exception ex)
            {
                msg = "erro ao inserir as expecialidades erro: " + ex.Message;
            }

            return msg;
        }
        protected void ddlDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string szTipo = "";
            if (ddlDocumento.SelectedValue == "CRM")
                szTipo = "CRM";
            else
                szTipo = "CRO";

            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter ad;
            MySqlConnection con = new MySqlConnection(szConnection);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM especialidade where documento = '" + szTipo + "';";
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            lstEspecialidade.DataSource = dt;
            lstEspecialidade.DataValueField = "id_especialidade";
            lstEspecialidade.DataTextField = "especialidade";
            lstEspecialidade.DataBind();
            pnlEspecialidade.Visible = true;
            lstEspecialidade.Dispose();
            lstLista.Dispose();
        }
    }
}