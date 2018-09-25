using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string HEADER_ERRO = "Erro no cadastro de usuário";
        private string BACK_URL = "NovoUsuario.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
            {
                Utils.Erro(Response,HEADER_ERRO,retorno,BACK_URL);
                return;
            }

            RegistrarUsuario();
        }

        private void RegistrarUsuario()
        {
            string retorno = "";
            string cmdInsert = $"INSERT INTO usuario(login, senha) VALUES('{txtLogin.Text.Trim()}','{txtSenha.Text.Trim()}')";

            retorno = ConexaoBD.GetInstance().ExecutarSql(cmdInsert);

            if (retorno.Length == 0)
            {
                Utils.Alert(Response,"Usuário registrado com sucesso!");
                Response.Redirect("Login.aspx");
            }
            else
            {
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);
            }
        }

        private string Validar()
        {
            string retorno = "";

            retorno += Utils.ValidarTextBox(txtLogin, "Login");
            retorno += Utils.ValidarTextBox(txtSenha, "Senha");

            return retorno;
        }
    }
}