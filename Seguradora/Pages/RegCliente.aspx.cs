using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class RegCliente : System.Web.UI.Page
    {

        private string HEADER_ERRO = "Cadastro de Clientes";
        private string BACK_URL = "RegCliente.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rbtM.Checked = true;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Cliente novoCliente = new Cliente(
                Convert.ToInt32(txtIdCliente.Text),
                txtNome.Text,
                txtTelefone.Text,
                txtEndereco.Text,
                txtBairro.Text,
                txtCep.Text,
                txtCpf.Text,
                txtRg.Text,
                rbtM.Checked?"M":"F",
                Convert.ToDateTime(txtDataNascimento.Text)
                );

            retorno = novoCliente.Inserir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Cadastro de cliente concluido!");

            LimparCampos();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Cliente alterarCliente = new Cliente(
                Convert.ToInt32(txtIdCliente.Text),
                txtNome.Text,
                txtTelefone.Text,
                txtEndereco.Text,
                txtBairro.Text,
                txtCep.Text,
                txtCpf.Text,
                txtRg.Text,
                rbtM.Checked ? "M" : "F",
                Convert.ToDateTime(txtDataNascimento.Text)
                );

            retorno = alterarCliente.Alterar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Alteração de cliente concluida!");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Cliente auxCliente = new Cliente {
                IdPessoa = Convert.ToInt32(txtIdCliente.Text)
            };

            retorno = auxCliente.Excluir();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Utils.Alert(Response, "Cliente excluido!");

            LimparCampos();
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string retorno = ValidarId();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            Cliente auxCliente = new Cliente
            {
                IdPessoa = Convert.ToInt32(txtIdCliente.Text)
            };

            retorno = auxCliente.Recuperar();

            if (retorno.Length != 0)
                Utils.Erro(Response, HEADER_ERRO, retorno, BACK_URL);

            txtIdCliente.Text = auxCliente.IdPessoa.ToString();
            txtNome.Text = auxCliente.Nome;
            txtTelefone.Text = auxCliente.Fone;
            txtEndereco.Text = auxCliente.Endereco;
            txtBairro.Text = auxCliente.Bairro;
            txtCep.Text = auxCliente.Cep;
            txtCpf.Text = auxCliente.Cpf;
            txtRg.Text = auxCliente.Rg;
            txtDataNascimento.Text = auxCliente.DataNascimento.ToString("yyyy-MM-dd");
            rbtM.Checked = auxCliente.Sexo=="M";
            rbtF.Checked = auxCliente.Sexo == "F";
        }

        private void LimparCampos()
        {
            txtIdCliente.Text = "";
            txtNome.Text = "";
            txtTelefone.Text = "";
            txtEndereco.Text = "";
            txtBairro.Text = "";
            txtCep.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtDataNascimento.Text = "";
            rbtM.Checked = true;
            rbtF.Checked = false;
        }

        private string ValidarId()
        {
            return txtIdCliente.Text.Trim().Length == 0 ? "Id não pode ser vazio.\r\n" : "";
        }

        private string Validar()
        {
            string retorno = "";

            if (txtIdCliente.Text.Trim().Length == 0)
                retorno += "Id do cliente não pode ser vazio.\r\n";

            if (txtNome.Text.Trim().Length == 0)
                retorno += "Nome não pode ser vazio.\r\n";

            if (txtTelefone.Text.Trim().Length == 0)
                retorno += "Telefone não pode ser vazio.\r\n";

            if (txtEndereco.Text.Trim().Length == 0)
                retorno += "Endereço não pode ser vazio.\r\n";

            if (txtBairro.Text.Trim().Length == 0)
                retorno += "Bairro não pode ser vazio.\r\n";

            if (txtCep.Text.Trim().Length == 0)
                retorno += "Cep não pode ser vazio.\r\n";

            if (txtCpf.Text.Trim().Length == 0)
                retorno += "Cpf não pode ser vazio.\r\n";

            if (txtRg.Text.Trim().Length == 0)
                retorno += "Rg não pode ser vazio.\r\n";

            if (txtDataNascimento.Text.Trim().Length == 0)
                retorno += "Data de nascimento não pode ser vazio.\r\n";

            return retorno;
        }
    }
}