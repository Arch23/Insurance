using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class RegApolice : System.Web.UI.Page
    {
        private string HEADER_ERRO = "Cadastro de Apólices";
        private string BACK_URL = "RegApolice.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<Cliente> listaClientes = Cliente.Recuperar("1=1");
                List<Veiculo> listaVeiculos = Veiculo.Recuperar("1=1");

                if (listaClientes == null || listaVeiculos == null)
                {
                    Utils.Erro(Response, HEADER_ERRO, "Erro para recuperar lista de clientes ou veículos", BACK_URL);
                }
                else
                {
                    cbxCliente.Items.Clear();
                    if (listaClientes.Count != 0)
                    {
                        foreach(Cliente auxCliente in listaClientes)
                        {
                            cbxCliente.Items.Add(new ListItem($"ID: {auxCliente.IdPessoa} - {auxCliente.Nome}",auxCliente.IdPessoa.ToString()));
                        }
                    }
                    else
                    {
                        cbxCliente.Items.Add(new ListItem("Nenhum cliente cadastrado.","-1"));
                    }

                    cbxVeiculo.Items.Clear();
                    if(listaVeiculos.Count != 0)
                    {
                        foreach (Veiculo auxVeiculo in listaVeiculos)
                        {
                            cbxVeiculo.Items.Add(new ListItem($"ID: {auxVeiculo.IdVeiculo} - {auxVeiculo.Placa}",auxVeiculo.IdVeiculo.ToString()));
                        }
                    }
                    else
                    {
                        cbxVeiculo.Items.Add(new ListItem("Nenhum veículo cadastrado.", "-1"));
                    }
                }
            }
        }

        private void LimparCampos()
        {
            txtIdApolice.Text = "";
            txtNumeroApolice.Text = "";
            txtValor.Text = "";
            txtFranquia.Text = "";
            txtDataInicio.Text = "";
            txtDataFim.Text = "";
            txtDataDeContrato.Text = "";
        }

        private string ValidarId()
        {
            return txtIdApolice.Text.Trim().Length == 0 ? "Id não pode ser vazio.\r\n" : "";
        }

        private string Validar()
        {
            string retorno = "";

            if (txtIdApolice.Text.Trim().Length == 0)
                retorno += "Id da apólice não pode ser vazio.\r\n";

            if (txtNumeroApolice.Text.Trim().Length == 0)
                retorno += "Número da apólice não pode ser vazio.\r\n";

            if (txtDataInicio.Text.Trim().Length == 0)
                retorno += "Data de inicio não pode ser vazio.\r\n";

            if (txtDataFim.Text.Trim().Length == 0)
                retorno += "Data de fim não pode ser vazio.\r\n";

            if (txtDataDeContrato.Text.Trim().Length == 0)
                retorno += "Data do contrato não pode ser vazio.\r\n";

            if (txtValor.Text.Trim().Length == 0)
                retorno += "Valor não pode ser vazio.\r\n";

            if (txtFranquia.Text.Trim().Length == 0)
                retorno += "Franquia não pode ser vazia.\r\n";

            if (cbxCliente.SelectedValue == "-1")
                retorno += "Nenhum cliente cadastrado, por favor cadastre um cliente primeiro.\r\n";

            if (cbxVeiculo.SelectedValue == "-1")
                retorno += "Nenhum veículo cadastrado, por favor cadastre um veículo primeiro.\r\n";

            if (!ValidarDecimal(txtValor.Text.Trim()))
                retorno += "Valor deve ser um valor decimal.\r\n";

            if (!ValidarDecimal(txtFranquia.Text.Trim()))
                retorno += "Franquia deve ser um valor decimal.\r\n";

            return retorno;
        }

        private bool ValidarDecimal(string texto)
        {
            Regex regex = new Regex("^\\d+([,\\.]\\d{1,2})?$");
            return regex.IsMatch(texto);
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Apolice novaApolice = new Apolice(
                Convert.ToInt32(txtIdApolice.Text),
                txtNumeroApolice.Text,
                Convert.ToDouble(txtValor.Text, CultureInfo.InvariantCulture),
                Convert.ToDouble(txtFranquia.Text, CultureInfo.InvariantCulture),
                Convert.ToDateTime(txtDataInicio.Text),
                Convert.ToDateTime(txtDataFim.Text),
                Convert.ToDateTime(txtDataDeContrato.Text),
                Convert.ToInt32(cbxCliente.SelectedValue),
                Convert.ToInt32(cbxVeiculo.SelectedValue)
                );

            retorno = novaApolice.Inserir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Cadastro de Apólice concluido!");

            LimparCampos();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Apolice alterarApolice = new Apolice(
                Convert.ToInt32(txtIdApolice.Text),
                txtNumeroApolice.Text,
                Convert.ToDouble(txtValor.Text, CultureInfo.InvariantCulture),
                Convert.ToDouble(txtFranquia.Text, CultureInfo.InvariantCulture),
                Convert.ToDateTime(txtDataInicio.Text),
                Convert.ToDateTime(txtDataFim.Text),
                Convert.ToDateTime(txtDataDeContrato.Text),
                Convert.ToInt32(cbxCliente.SelectedValue),
                Convert.ToInt32(cbxVeiculo.SelectedValue)
                );

            retorno = alterarApolice.Alterar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Alteração de Apólice concluida!");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Apolice auxApolice = new Apolice
            {
                IdApolice = Convert.ToInt32(txtIdApolice.Text)
            };

            retorno = auxApolice.Excluir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Apólice excluida!");

            LimparCampos();
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Apolice auxApolice = new Apolice
            {
                IdApolice = Convert.ToInt32(txtIdApolice.Text)
            };

            retorno = auxApolice.Recuperar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);
            
            txtIdApolice.Text = auxApolice.IdApolice.ToString();
            txtNumeroApolice.Text = auxApolice.NumeroApolice;
            txtValor.Text = auxApolice.Valor.ToString(CultureInfo.InvariantCulture);
            txtFranquia.Text = auxApolice.Franquia.ToString(CultureInfo.InvariantCulture);
            txtDataInicio.Text = auxApolice.DataInicio.ToString("yyyy-MM-dd");
            txtDataFim.Text = auxApolice.DataFim.ToString("yyyy-MM-dd");
            txtDataDeContrato.Text = auxApolice.DataContrato.ToString("yyyy-MM-dd");

            cbxCliente.SelectedValue = auxApolice.ACliente.IdPessoa.ToString();
            cbxVeiculo.SelectedValue = auxApolice.AVeiculo.IdVeiculo.ToString();
        }
    }
}