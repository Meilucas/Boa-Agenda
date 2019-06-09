using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes.Code;
using MySql.Data.MySqlClient; 

namespace wwwroot.usuario
{
    public partial class cadastrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] != null)
            {
                if (Session["tipo"].ToString() == "0")
                {
                    //   pnlEspecialidade.Visible = true;
                }
            }
            if (!IsPostBack)
            {
                // Verificar se a pagina é de edição ou cadastro 
                // verifica se é edição do administrador
                if (Request.QueryString["id"] != null)  // Verifica se veio um id da url, se vim é porque ele vai editar um cadastro
                {
                    try
                    {
                        if (Session["tipo"] != null) // verifica se esta logado
                        {
                            if (Session["tipo"].ToString() != "0") // verifica se é um admin, so admin pode alterar cadastro de outras pessoas
                                Response.Write("<script>alert('você não tem permição para editar esse cadastro');window.location.href = '/'</script>");
                            Session["IdEdicao"] = Convert.ToInt32(Request.QueryString["id"]);
                            PreencheCampos((int)Session["IdEdicao"]);// pega o id passado na url e preenche o campos em tela
                        }
                        else
                        {
                            Response.Write("<script>alert('Você precisa esta logado realizar essa ação');window.location.href = '/'</script>");
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('id invalido');</script>"); 
                        return;
                    }
                }
                else if (Session["id"] != null)  //verifica se o usuario esta logado
                {
                    if (Session["tipo"].ToString() == "1") //verifica se é um usuario ou 
                    {
                        Session["IdEdicao"] = Convert.ToInt32(Session["id"]);
                        PreencheCampos((int)Session["IdEdicao"]);
                    }
                    else if (Session["tipo"].ToString() == "0") // se for administrador ele não faz nada
                        return;
                    else
                    {
                        Response.Write("<script>alert('Você precisa esta logado realizar essa ação');window.location.href = '/'</script>");
                    }
                }
            }
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
                Salvar();
            }
            else
                Editar((int)Session["IdEdicao"]);
        }

        private void Salvar()
        {
            Dao db = new Dao();
            db.AddParameter("_nome", txtNome.Text);
            db.AddParameter("_sobrenome", txtSobreNome.Text);
            db.AddParameter("_cep", txtCep.Text);
            db.AddParameter("_telefone", txtTelefone.Text);
            db.AddParameter("_celular", txtCel.Text);
            db.AddParameter("_numero", txtnumero.Text);
            db.AddParameter("_email", txtEmail.Text);
            db.AddParameter("_login", txtLogin.Text);
            db.AddParameter("_senha", txtSenha.Text);
            db.AddParameter("_endereco", txtEndereco.Text);
            db.AddParameter("_cpf", txtCPF.Text);
            db.AddParameter("_rg", txtRG.Text);
            object ob = db.ExecuteCommand("pr_in_user", CommandType.StoredProcedure);
            try
            {
                string msg = ob.ToString();
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
        private bool ValidaDados()
        {
            if (!IsCpf(txtCPF.Text))
            {
                Response.Write("<script>alert(' CPF invalido');</script>");
                return false;
            }
            else if (txtRG.Text.Length < 12)
            {
                Response.Write("<script>alert(' RG invalido');</script>");
                return false;
            }
            else if (txtCep.Text.Length < 8 )
            {
                Response.Write("<script>alert(' CEP invalido');</script>");
                return false;
            }
            else if (txtLogin.Text.Length < 4 && txtLogin.Text.Length > 8)
            {
                Response.Write("<script>alert(' O login deve conter de 4 a caracteres 8');</script>");
                return false;            
            }
            else if (txtSenha.Text.Length < 4 && txtSenha.Text.Length > 8)
            {
                Response.Write("<script>alert(' O Senha deve conter de 4 a caracteres 8');</script>");
                return false;
            }
            return true;
        }
        private void Editar(int id)
        {
            Dao db = new Dao();
           
            db.AddParameter("_id", id);
            db.AddParameter("_nome", txtNome.Text);
            db.AddParameter("_sobrenome", txtSobreNome.Text);
            db.AddParameter("_cep", txtCep.Text);
            db.AddParameter("_telefone", txtTelefone.Text);
            db.AddParameter("_celular", txtCel.Text);
            db.AddParameter("_numero", txtnumero.Text);
            db.AddParameter("_email", txtEmail.Text);
            db.AddParameter("_login", txtLogin.Text);
            db.AddParameter("_senha", txtSenha.Text);
            db.AddParameter("_endereco", txtEndereco.Text);
            db.AddParameter("_cpf", txtCPF.Text);
            db.AddParameter("_rg", txtRG.Text);
            object ob = db.ExecuteCommand("pr_up_user", CommandType.StoredProcedure);
            try
            {
                string msg = ob.ToString();
                if (msg.Contains("sucesso"))
                {
                    Session.Remove("IdEdicao");
                    Response.Write("<script>alert('" + msg + "');</script>");
                }
                else
                    Response.Write("<script>alert(\"" + msg + "\");</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(\"erro " + ex.Message + "\");</script>");
            }
        }
        public void PreencheCampos(int id)
        {
            Dao db = new Dao();
            try
            {
                DataTable rd = db.ExecuteReader("select * from usuarios where id_usuario = " + id,CommandType.Text);
               

                if(rd !=null)
                {
                    txtNome.Text = rd.Rows[0]["nome"].ToString();
                    txtSobreNome.Text = rd.Rows[0]["sobrenome"].ToString();
                    txtCep.Text = rd.Rows[0]["cep"].ToString();
                    txtTelefone.Text = rd.Rows[0]["telefone"].ToString();
                    txtCel.Text = rd.Rows[0]["celular"].ToString();
                    txtnumero.Text = rd.Rows[0]["numero"].ToString();
                    txtEmail.Text = rd.Rows[0]["email"].ToString();
                    txtLogin.Text = rd.Rows[0]["login"].ToString();
                    txtSenha.TextMode = TextBoxMode.SingleLine;
                    txtSenha.Text = rd.Rows[0]["senha"].ToString();
                    txtSenha2.TextMode = TextBoxMode.SingleLine;
                    txtSenha2.Text = rd.Rows[0]["senha"].ToString();
                    txtEndereco.Text = rd.Rows[0]["endereco"].ToString();
                    txtCPF.Text = rd.Rows[0]["cpf"].ToString();
                    txtRG.Text = rd.Rows[0]["rg"].ToString();
                }         
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('erro " + ex.Message + "');</script>");
            }
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", ""); // remove pontos e traços
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}