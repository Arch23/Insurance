using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private string HEADER_ERRO = "Erro no login";
        private string BACK_URL = "Login.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null)
            {
                HttpCookie cookie = Request.Cookies["login"];

                txtLogin.Text = cookie["user"];
                ckbLembrar.Checked = Convert.ToBoolean(cookie["lembrar"]);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
            {
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);
            }
            else
            {
                Logon();
            }

        }

        private void Logon()
        {
            string cmdRecuperar = $"SELECT * FROM usuario WHERE login='{txtLogin.Text}'";
            string retorno;

            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
            {
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);
                return;
            }

            if (tabela.Rows.Count == 0)
            {
                Utils.Erro(Response, HEADER_ERRO, "Nenhum usuário com este login encontrado\r\n", BACK_URL);
            }
            else
            {
                if (tabela.Rows[0]["senha"].ToString() == (txtSenha.Text.Trim()))
                {
                    Session["autenticado"] = true;

                    HttpCookie cookie = new HttpCookie("login");

                    cookie.Values.Add("user", txtLogin.Text);
                    cookie.Values.Add("lembrar", ckbLembrar.Checked.ToString());

                    if (ckbLembrar.Checked)
                    {
                        cookie.Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1); //apagar o cookie se existir
                    }

                    Response.Cookies.Add(cookie);

                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Utils.Alert(Response, "Senha incorreta");
                }
            }
        }

        private string Validar()
        {
            string retorno = "";

            retorno += Utils.ValidarTextBox(txtLogin, "Login");
            retorno += Utils.ValidarTextBox(txtSenha, "Senha");

            return retorno;
        }

        protected void btnNovoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("NovoUsuario.aspx");
        }
    }
}