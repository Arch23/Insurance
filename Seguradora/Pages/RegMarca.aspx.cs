using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class RegMarca : System.Web.UI.Page
    {
        private string HEADER_ERRO = "Cadastro de Marca";
        private string BACK_URL = "RegMarca.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Marca novaMarca = new Marca(Convert.ToInt32(txtIdMarca.Text), txtDescricao.Text);

            retorno = novaMarca.Inserir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Cadastro de marca concluido!");
            LimparCampos();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Marca alterarMarca = new Marca(Convert.ToInt32(txtIdMarca.Text), txtDescricao.Text);

            retorno = alterarMarca.Alterar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Alteração de marca concluida!");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();
            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Marca auxMarca = new Marca
            {
                IdMarca = Convert.ToInt32(txtIdMarca.Text)
            };

            retorno = auxMarca.Excluir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);
            
            Utils.Alert(Response, "Marca Excluida!");
            LimparCampos();
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();
            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Marca auxMarca = new Marca
            {
                IdMarca = Convert.ToInt32(txtIdMarca.Text)
            };

            retorno = auxMarca.Recuperar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            txtIdMarca.Text = auxMarca.IdMarca.ToString();
            txtDescricao.Text = auxMarca.Descricao;
        }

        private void LimparCampos()
        {
            txtIdMarca.Text = "";
            txtDescricao.Text = "";
        }

        private string ValidarId()
        {
            return txtIdMarca.Text.Trim().Length==0? "Id não pode ser vazio.\r\n" : "";
        }

        private string Validar()
        {
            string retorno = "";

            if (txtIdMarca.Text.Trim().Length == 0)
                retorno += "Id da marca não pode ser vazio.\r\n";

            if (txtDescricao.Text.Trim().Length == 0)
                retorno += "descrição da marca não pode ser vazio.\r\n";

            return retorno;
        }
    }
}