using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class RegModelo : System.Web.UI.Page
    {
        private string HEADER_ERRO = "Cadastro de Modelo";
        private string BACK_URL = "RegModelo.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Marca> lista = Marca.Recuperar("1=1");
                cbxMarca.Items.Clear();
                if (lista.Count != 0)
                {
                    foreach (Marca auxMarca in lista)
                    {
                        cbxMarca.Items.Add(new ListItem($"ID: {auxMarca.IdMarca} - {auxMarca.Descricao}", auxMarca.IdMarca.ToString()));
                    }
                }
                else
                {
                    cbxMarca.Items.Add(new ListItem("Nenhuma marca cadastrada.", "-1"));
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Modelo novoModelo = new Modelo(Convert.ToInt32(cbxMarca.SelectedValue), Convert.ToInt32(txtIdModelo.Text), txtDescricao.Text);

            retorno = novoModelo.Inserir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Cadastro de modelo concluido!");

            LimparCampos();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Modelo AlterarModelo = new Modelo(Convert.ToInt32(cbxMarca.SelectedValue), Convert.ToInt32(txtIdModelo.Text), txtDescricao.Text);

            retorno = AlterarModelo.Alterar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Alteração de modelo concluida!");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Modelo auxModelo = new Modelo{
                IdModelo = Convert.ToInt32(txtIdModelo.Text)
            };

            retorno = auxModelo.Excluir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Modelo excluido!");
            LimparCampos();
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Modelo auxModelo = new Modelo
            {
                IdModelo = Convert.ToInt32(txtIdModelo.Text)
            };

            retorno = auxModelo.Recuperar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            txtIdModelo.Text = auxModelo.IdModelo.ToString();
            txtDescricao.Text = auxModelo.Descricao;
            cbxMarca.SelectedValue = auxModelo.AMarca.IdMarca.ToString();
        }

        private void LimparCampos()
        {
            txtIdModelo.Text = "";
            txtDescricao.Text = "";
        }

        private string ValidarId()
        {
            return txtIdModelo.Text.Trim().Length == 0 ? "Id não pode ser vazio.\r\n" : "";
        }

        private string Validar()
        {
            string retorno = "";

            if (txtIdModelo.Text.Trim().Length == 0)
                retorno += "Id do modelo não pode ser nulo.\r\n";

            if (txtDescricao.Text.Trim().Length == 0)
                retorno += "Descrição do modelo não pode ser nula.\r\n";

            if (Convert.ToInt32(cbxMarca.SelectedValue) == -1)
                retorno += "Nenhuma marca cadastrada, por favor cadastre uma marca primeiro.\r\n";


            return retorno;
        }
    }
}