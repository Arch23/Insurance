using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class RegVeiculo : System.Web.UI.Page
    {
        private string HEADER_ERRO = "Cadastro de Veículo";
        private string BACK_URL = "RegVeiculo.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Modelo> lista = Modelo.Recuperar("1=1");
                cbxModelo.Items.Clear();
                if (lista.Count != 0)
                {
                    foreach (Modelo auxModelo in lista)
                    {
                        cbxModelo.Items.Add(new ListItem($"ID: {auxModelo.IdModelo} - {auxModelo.Descricao}", auxModelo.IdModelo.ToString()));
                    }
                }
                else
                {
                    cbxModelo.Items.Add(new ListItem("Nenhum modelo cadastrado.", "-1"));
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Veiculo novoVeiculo = new Veiculo(Convert.ToInt32(cbxModelo.SelectedValue),
                Convert.ToInt32(txtIdVeiculo.Text),
                txtPlaca.Text,
                Convert.ToInt32(txtAno.Text));

            retorno = novoVeiculo.Inserir();

            if(retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Cadastro de veículo concluido!");

            LimparCampos();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Veiculo alterarVeiculo = new Veiculo(Convert.ToInt32(cbxModelo.SelectedValue),
                Convert.ToInt32(txtIdVeiculo.Text),
                txtPlaca.Text,
                Convert.ToInt32(txtAno.Text));

            retorno = alterarVeiculo.Alterar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Alteração de veículo concluida!");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Veiculo auxVeiculo = new Veiculo
            {
                IdVeiculo = Convert.ToInt32(txtIdVeiculo.Text)
            };

            retorno = auxVeiculo.Excluir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);
            
            Utils.Alert(Response, "Veículo Excluido!");

            LimparCampos();
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Veiculo auxVeiculo = new Veiculo
            {
                IdVeiculo = Convert.ToInt32(txtIdVeiculo.Text)
            };

            retorno = auxVeiculo.Recuperar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            txtIdVeiculo.Text = auxVeiculo.IdVeiculo.ToString();
            txtPlaca.Text = auxVeiculo.Placa;
            txtAno.Text = auxVeiculo.Ano.ToString();
            cbxModelo.SelectedValue = auxVeiculo.AModelo.IdModelo.ToString();
        }

        private void LimparCampos()
        {
            txtIdVeiculo.Text = "";
            txtPlaca.Text = "";
            txtAno.Text = "";
        }

        private string ValidarId()
        {
            return txtIdVeiculo.Text.Trim().Length == 0 ? "Id não pode ser vazio.\r\n" : "";
        }

        private string Validar()
        {
            string retorno = "";

            if (txtIdVeiculo.Text.Trim().Length == 0)
                retorno += "Id do veículo não pode ser nulo.\r\n";

            if (txtPlaca.Text.Trim().Length == 0)
                retorno += "Placa não pode ser nula.\r\n";

            if (txtAno.Text.Trim().Length == 0)
                retorno += "Ano não pode ser nulo.\r\n";

            if (Convert.ToInt32(cbxModelo.SelectedValue) == -1)
                retorno += "Nenhum modelo cadastrado, por favor cadastre um modelo primeiro.\r\n";

            return retorno;
        }
    }
}