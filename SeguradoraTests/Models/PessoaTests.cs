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
    public class PessoaTests
    {
        private Pessoa pessoa;
        private Pessoa pessoa2;

        private readonly int idPessoa = 999;
        private readonly int idPessoa2 = 1000;

        [TestInitialize()]
        public void StartUp()
        {
            pessoa = new Pessoa
            {
                IdPessoa = idPessoa,
                Nome = "Nome 1",
                Bairro = "Bairro 1",
                Cep = "Cep 1",
                Endereco = "Endereço 1",
                Fone = "Telefone 1"
            };

            pessoa2 = new Pessoa
            {
                IdPessoa = idPessoa2,
                Nome = "Nome 2",
                Bairro = "Bairro 2",
                Cep = "Cep 2",
                Endereco = "Endereço 2",
                Fone = "Telefone 2"
            };

            pessoa2.Excluir();
            pessoa2.Inserir();

            pessoa.Excluir();
        }

        [TestCleanup()]
        public void CleanUp()
        {
            pessoa.Excluir();
            pessoa2.Excluir();
        }

        [TestMethod()]
        public void InserirTest_OK()
        {
            Assert.AreEqual(pessoa.Inserir(), "");
        }

        [TestMethod()]
        public void InserirTest_RecuperarDados()
        {
            pessoa.Inserir();

            Pessoa tmpPessoa = new Pessoa(idPessoa);

            Assert.AreEqual(pessoa.IdPessoa, tmpPessoa.IdPessoa);
            Assert.AreEqual(pessoa.Nome, tmpPessoa.Nome);
            Assert.AreEqual(pessoa.Bairro, tmpPessoa.Bairro);
            Assert.AreEqual(pessoa.Cep, tmpPessoa.Cep);
            Assert.AreEqual(pessoa.Endereco, tmpPessoa.Endereco);
            Assert.AreEqual(pessoa.Fone, tmpPessoa.Fone);
        }

        [TestMethod()]
        public void InserirTest_IdInvalido()
        {
            pessoa.IdPessoa = -1;
            Assert.AreEqual(pessoa.Inserir(), "id de pessoa inválido.\r\n");
        }

        [TestMethod()]
        public void InserirTest_IdExistente()
        {
            pessoa.IdPessoa = idPessoa2;
            Assert.AreEqual(pessoa.Inserir(), "Id de pessoa já utilizada\r\n");
        }

        [TestMethod()]
        public void InserirTest_NomeVazio()
        {
            pessoa.Nome = "";
            Assert.AreEqual(pessoa.Inserir(), "Nome não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_NomeGrande()
        {
            pessoa.Nome = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Inserir(), "Nome maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_FoneVazio()
        {
            pessoa.Fone = "";
            Assert.AreEqual(pessoa.Inserir(), "Telefone não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_FoneGrande()
        {
            pessoa.Fone = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Inserir(), "Telefone maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_EnderecoVazio()
        {
            pessoa.Endereco = "";
            Assert.AreEqual(pessoa.Inserir(), "Endereço não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_EnderecoGrande()
        {
            pessoa.Endereco = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Inserir(), "Endereço maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_BairroVazio()
        {
            pessoa.Bairro = "";
            Assert.AreEqual(pessoa.Inserir(), "Bairro não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_BairroGrande()
        {
            pessoa.Bairro = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Inserir(), "Bairro maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_CepVazio()
        {
            pessoa.Cep = "";
            Assert.AreEqual(pessoa.Inserir(), "Cep não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_CepGrande()
        {
            pessoa.Cep = "abcdefghijklmnop";
            Assert.AreEqual(pessoa.Inserir(), "Cep maior que o limite de 15 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_OK()
        {
            pessoa.Inserir();
            pessoa.Nome = "Nome alterado";
            pessoa.Bairro = "Bairro alterado";
            pessoa.Cep = "Cep alterado";
            pessoa.Endereco = "Endereço alterado";
            pessoa.Fone = "Telefone alterado";

            Assert.AreEqual(pessoa.Alterar(), "");
        }

        [TestMethod()]
        public void AlterarTest_RecuperarDados()
        {
            pessoa.Inserir();
            pessoa.Nome = "Nome alterado";
            pessoa.Bairro = "Bairro alterado";
            pessoa.Cep = "Cep alterado";
            pessoa.Endereco = "Endereço alterado";
            pessoa.Fone = "Telefone alterado";
            pessoa.Alterar();

            Pessoa tmpPessoa = new Pessoa(idPessoa);

            Assert.AreEqual(pessoa.IdPessoa, tmpPessoa.IdPessoa);
            Assert.AreEqual(pessoa.Nome, tmpPessoa.Nome);
            Assert.AreEqual(pessoa.Bairro, tmpPessoa.Bairro);
            Assert.AreEqual(pessoa.Cep, tmpPessoa.Cep);
            Assert.AreEqual(pessoa.Endereco, tmpPessoa.Endereco);
            Assert.AreEqual(pessoa.Fone, tmpPessoa.Fone);
        }

        [TestMethod()]
        public void AlterarTest_IdInexistente()
        {
            pessoa.IdPessoa = 1111;
            pessoa.Nome = "Nome alterado";
            pessoa.Bairro = "Bairro alterado";
            pessoa.Cep = "Cep alterado";
            pessoa.Endereco = "Endereço alterado";
            pessoa.Fone = "Telefone alterado";

            Assert.AreEqual(pessoa.Alterar(), "Pessoa com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_IdInvalido()
        {
            pessoa.Inserir();
            pessoa.IdPessoa = -1;
            Assert.AreEqual(pessoa.Alterar(), "id de pessoa inválido.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_NomeVazio()
        {
            pessoa.Inserir();
            pessoa.Nome = "";
            Assert.AreEqual(pessoa.Alterar(), "Nome não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_NomeGrande()
        {
            pessoa.Inserir();
            pessoa.Nome = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Alterar(), "Nome maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_FoneVazio()
        {
            pessoa.Inserir();
            pessoa.Fone = "";
            Assert.AreEqual(pessoa.Alterar(), "Telefone não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_FoneGrande()
        {
            pessoa.Inserir();
            pessoa.Fone = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Alterar(), "Telefone maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_EnderecoVazio()
        {
            pessoa.Inserir();
            pessoa.Endereco = "";
            Assert.AreEqual(pessoa.Alterar(), "Endereço não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_EnderecoGrande()
        {
            pessoa.Inserir();
            pessoa.Endereco = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Alterar(), "Endereço maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_BairroVazio()
        {
            pessoa.Inserir();
            pessoa.Bairro = "";
            Assert.AreEqual(pessoa.Alterar(), "Bairro não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_BairroGrande()
        {
            pessoa.Inserir();
            pessoa.Bairro = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(pessoa.Alterar(), "Bairro maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_CepVazio()
        {
            pessoa.Inserir();
            pessoa.Cep = "";
            Assert.AreEqual(pessoa.Alterar(), "Cep não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_CepGrande()
        {
            pessoa.Inserir();
            pessoa.Cep = "abcdefghijklmnop";
            Assert.AreEqual(pessoa.Alterar(), "Cep maior que o limite de 15 caracteres.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_OK()
        {
            pessoa.Inserir();
            Assert.AreEqual("", pessoa.Excluir());

            Assert.AreEqual(pessoa.Recuperar(), "Pessoa com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInvalido()
        {
            pessoa.Inserir();
            pessoa.IdPessoa = -1;
            Assert.AreEqual("id de pessoa inválido.\r\n", pessoa.Excluir());
        }

        [TestMethod()]
        public void ExcluirTest_IdInexistente()
        {
            pessoa.IdPessoa = 1111;
            Assert.AreEqual("Pessoa com este ID não existe.\r\n", pessoa.Excluir());
        }

        [TestMethod()]
        public void RecuperarTest_OK()
        {
            pessoa.Inserir();

            Pessoa tmpPessoa = new Pessoa(idPessoa);

            Assert.AreEqual(pessoa.IdPessoa, tmpPessoa.IdPessoa);
            Assert.AreEqual(pessoa.Nome, tmpPessoa.Nome);
            Assert.AreEqual(pessoa.Bairro, tmpPessoa.Bairro);
            Assert.AreEqual(pessoa.Cep, tmpPessoa.Cep);
            Assert.AreEqual(pessoa.Endereco, tmpPessoa.Endereco);
            Assert.AreEqual(pessoa.Fone, tmpPessoa.Fone);
        }

        [TestMethod()]
        public void RecuperarTest_IdInvalido()
        {
            pessoa.Inserir();
            pessoa.IdPessoa = -1;
            Assert.AreEqual("id de pessoa inválido.\r\n", pessoa.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_IdInexistente()
        {
            pessoa.IdPessoa = 1111;
            Assert.AreEqual("Pessoa com este ID não existe.\r\n", pessoa.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_Todos()
        {
            pessoa.Inserir();

            Pessoa[] list = new Pessoa[] { pessoa, pessoa2 };

            List<Pessoa> pessoas = Pessoa.Recuperar("true");

            for (int i = 0; i < pessoas.Count; i++)
            {
                Assert.AreEqual(pessoas[i].IdPessoa, list[i].IdPessoa);
                Assert.AreEqual(pessoas[i].Nome, list[i].Nome);
                Assert.AreEqual(pessoas[i].Fone, list[i].Fone);
                Assert.AreEqual(pessoas[i].Endereco, list[i].Endereco);
                Assert.AreEqual(pessoas[i].Bairro, list[i].Bairro);
                Assert.AreEqual(pessoas[i].Cep, list[i].Cep);
            }
        }

        [TestMethod()]
        public void RecuperarTest_Vazio()
        {
            pessoa.Inserir();

            Pessoa[] list = new Pessoa[] { pessoa, pessoa2 };

            List<Pessoa> pessoas = Pessoa.Recuperar("false");

            Assert.AreEqual(pessoas.Count, 0);
        }
    }
}