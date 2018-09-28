using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguradora.Models.Tests
{
    [TestClass()]
    public class ClienteTests
    {
        private Cliente cliente;
        private Cliente cliente2;

        private readonly int idCliente = 999;
        private readonly int idCliente2 = 1000;

        [TestInitialize()]
        public void StartUp()
        {
            cliente = new Cliente
            {
                IdPessoa = idCliente,
                Nome = "Nome 1",
                Bairro = "Bairro 1",
                Cep = "Cep 1",
                Endereco = "Endereço 1",
                Fone = "Telefone 1",
                Cpf = "cpf 1",
                Rg = "rg 1",
                Sexo = "M",
                DataNascimento = DateTime.Now.AddYears(-25)
            };

            cliente2 = new Cliente
            {
                IdPessoa = idCliente2,
                Nome = "Nome 2",
                Bairro = "Bairro 2",
                Cep = "Cep 2",
                Endereco = "Endereço 2",
                Fone = "Telefone 2",
                Cpf = "cpf 2",
                Rg = "rg 2",
                Sexo = "F",
                DataNascimento = DateTime.Now.AddYears(-20)
            };

            cliente2.Excluir();
            cliente2.Inserir();

            cliente.Excluir();
        }

        [TestCleanup()]
        public void CleanUp()
        {
            cliente.Excluir();
            cliente2.Excluir();
        }

        [TestMethod()]
        public void InserirTest_OK()
        {
            Assert.AreEqual(cliente.Inserir(), "");
        }

        [TestMethod()]
        public void InserirTest_RecuperarDados()
        {
            cliente.Inserir();

            Cliente tmpCliente = new Cliente(idCliente);

            Assert.AreEqual(cliente.IdPessoa, tmpCliente.IdPessoa);
            Assert.AreEqual(cliente.Nome, tmpCliente.Nome);
            Assert.AreEqual(cliente.Bairro, tmpCliente.Bairro);
            Assert.AreEqual(cliente.Cep, tmpCliente.Cep);
            Assert.AreEqual(cliente.Endereco, tmpCliente.Endereco);
            Assert.AreEqual(cliente.Fone, tmpCliente.Fone);
            Assert.AreEqual(cliente.Cpf, tmpCliente.Cpf);
            Assert.AreEqual(cliente.Rg, tmpCliente.Rg);
            Assert.AreEqual(cliente.Sexo, tmpCliente.Sexo);
            Assert.AreEqual(cliente.DataNascimento.Date, tmpCliente.DataNascimento.Date);
        }

        [TestMethod()]
        public void InserirTest_IdInvalido()
        {
            cliente.IdPessoa = -1;
            Assert.AreEqual(cliente.Inserir(), "id de cliente inválido.\r\n");
        }

        [TestMethod()]
        public void InserirTest_IdExistente()
        {
            cliente.IdPessoa = idCliente2;
            Assert.AreEqual(cliente.Inserir(), "Id de cliente já utilizada\r\n");
        }

        [TestMethod()]
        public void InserirTest_CpfVazio()
        {
            cliente.Cpf = "";
            Assert.AreEqual(cliente.Inserir(), "Cpf não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_CpfGrande()
        {
            cliente.Cpf = "123456789012";
            Assert.AreEqual(cliente.Inserir(), "Cpf maior que o limite de 11 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_RgVazio()
        {
            cliente.Rg = "";
            Assert.AreEqual(cliente.Inserir(), "Rg não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_RgGrande()
        {
            cliente.Rg = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(cliente.Inserir(), "Rg maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_SexoVazio()
        {
            cliente.Sexo = "";
            Assert.AreEqual(cliente.Inserir(), "Sexo não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_SexoGrande()
        {
            cliente.Sexo = "Fe";
            Assert.AreEqual(cliente.Inserir(), "Sexo maior que o limite de 1 carácter.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_OK()
        {
            cliente.Inserir();
            cliente.Nome = "Nome alterado";
            cliente.Bairro = "Bairro alterado";
            cliente.Cep = "Cep alterado";
            cliente.Endereco = "Endereço alterado";
            cliente.Fone = "Telefone alterado";

            cliente.Cpf = "cpf 3";
            cliente.Rg = "rg alterado";
            cliente.Sexo = "F";

            Assert.AreEqual(cliente.Alterar(), "");
        }

        [TestMethod()]
        public void AlterarTest_RecuperarDados()
        {
            cliente.Inserir();
            cliente.Nome = "Nome alterado";
            cliente.Bairro = "Bairro alterado";
            cliente.Cep = "Cep alterado";
            cliente.Endereco = "Endereço alterado";
            cliente.Fone = "Telefone alterado";

            cliente.Cpf = "cpf 3";
            cliente.Rg = "rg alterado";
            cliente.Sexo = "M";
            cliente.Alterar();

            Cliente tmpCliente = new Cliente(idCliente);

            Assert.AreEqual(cliente.IdPessoa, tmpCliente.IdPessoa);
            Assert.AreEqual(cliente.Nome, tmpCliente.Nome);
            Assert.AreEqual(cliente.Bairro, tmpCliente.Bairro);
            Assert.AreEqual(cliente.Cep, tmpCliente.Cep);
            Assert.AreEqual(cliente.Endereco, tmpCliente.Endereco);
            Assert.AreEqual(cliente.Fone, tmpCliente.Fone);
            Assert.AreEqual(cliente.Cpf, tmpCliente.Cpf);
            Assert.AreEqual(cliente.Rg, tmpCliente.Rg);
            Assert.AreEqual(cliente.Sexo, tmpCliente.Sexo);
            Assert.AreEqual(cliente.DataNascimento.Date, tmpCliente.DataNascimento.Date);
        }

        [TestMethod()]
        public void AlterarTest_IdInexistente()
        {
            cliente.IdPessoa = 1111;
            cliente.Nome = "Nome alterado";
            cliente.Bairro = "Bairro alterado";
            cliente.Cep = "Cep alterado";
            cliente.Endereco = "Endereço alterado";
            cliente.Fone = "Telefone alterado";

            cliente.Cpf = "cpf 3";
            cliente.Rg = "rg alterado";
            cliente.Sexo = "M";

            Assert.AreEqual(cliente.Alterar(), "Cliente com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_IdInvalido()
        {
            cliente.Inserir();
            cliente.IdPessoa = -1;
            Assert.AreEqual(cliente.Alterar(), "id de cliente inválido.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_CpfVazio()
        {
            cliente.Inserir();
            cliente.Cpf = "";
            Assert.AreEqual(cliente.Alterar(), "Cpf não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_CpfGrande()
        {
            cliente.Inserir();
            cliente.Cpf = "123456789012";
            Assert.AreEqual(cliente.Alterar(), "Cpf maior que o limite de 11 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_RgVazio()
        {
            cliente.Inserir();
            cliente.Rg = "";
            Assert.AreEqual(cliente.Alterar(), "Rg não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_RgGrande()
        {
            cliente.Inserir();
            cliente.Rg = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(cliente.Alterar(), "Rg maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_SexoVazio()
        {
            cliente.Inserir();
            cliente.Sexo = "";
            Assert.AreEqual(cliente.Alterar(), "Sexo não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_SexoGrande()
        {
            cliente.Inserir();
            cliente.Sexo = "Fe";
            Assert.AreEqual(cliente.Alterar(), "Sexo maior que o limite de 1 carácter.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_OK()
        {
            cliente.Inserir();
            Assert.AreEqual("", cliente.Excluir());

            Assert.AreEqual(cliente.Recuperar(), "Cliente com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInvalido()
        {
            cliente.Inserir();
            cliente.IdPessoa = -1;
            Assert.AreEqual("id de cliente inválido.\r\n", cliente.Excluir());
        }

        [TestMethod()]
        public void ExcluirTest_IdInexistente()
        {
            cliente.IdPessoa = 1111;
            Assert.AreEqual("Cliente com este ID não existe.\r\n", cliente.Excluir());
        }

        [TestMethod()]
        public void RecuperarTest_OK()
        {
            cliente.Inserir();

            Cliente tmpCliente = new Cliente(idCliente);

            Assert.AreEqual(cliente.IdPessoa, tmpCliente.IdPessoa);
            Assert.AreEqual(cliente.Nome, tmpCliente.Nome);
            Assert.AreEqual(cliente.Bairro, tmpCliente.Bairro);
            Assert.AreEqual(cliente.Cep, tmpCliente.Cep);
            Assert.AreEqual(cliente.Endereco, tmpCliente.Endereco);
            Assert.AreEqual(cliente.Fone, tmpCliente.Fone);
            Assert.AreEqual(cliente.Cpf, tmpCliente.Cpf);
            Assert.AreEqual(cliente.Rg, tmpCliente.Rg);
            Assert.AreEqual(cliente.Sexo, tmpCliente.Sexo);
            Assert.AreEqual(cliente.DataNascimento.Date, tmpCliente.DataNascimento.Date);
        }

        [TestMethod()]
        public void RecuperarTest_IdInvalido()
        {
            cliente.Inserir();
            cliente.IdPessoa = -1;
            Assert.AreEqual("id de cliente inválido.\r\n", cliente.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_IdInexistente()
        {
            cliente.IdPessoa = 1111;
            Assert.AreEqual("Cliente com este ID não existe.\r\n", cliente.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_Todos()
        {
            cliente.Inserir();

            Cliente[] list = new Cliente[] { cliente, cliente2 };

            List<Cliente> clientes = Cliente.Recuperar("true");

            for (int i = 0; i < clientes.Count; i++)
            {
                Assert.AreEqual(clientes[i].IdPessoa, list[i].IdPessoa);
                Assert.AreEqual(clientes[i].Nome, list[i].Nome);
                Assert.AreEqual(clientes[i].Bairro, list[i].Bairro);
                Assert.AreEqual(clientes[i].Cep, list[i].Cep);
                Assert.AreEqual(clientes[i].Endereco, list[i].Endereco);
                Assert.AreEqual(clientes[i].Fone, list[i].Fone);
                Assert.AreEqual(clientes[i].Cpf, list[i].Cpf);
                Assert.AreEqual(clientes[i].Rg, list[i].Rg);
                Assert.AreEqual(clientes[i].Sexo, list[i].Sexo);
                Assert.AreEqual(clientes[i].DataNascimento.Date, list[i].DataNascimento.Date);
            }
        }

        [TestMethod()]
        public void RecuperarTest_Vazio()
        {
            cliente.Inserir();

            Cliente[] list = new Cliente[] { cliente, cliente2 };

            List<Cliente> clientes = Cliente.Recuperar("false");

            Assert.AreEqual(clientes.Count, 0);
        }
    }
}