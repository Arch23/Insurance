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
    public class MarcaTests
    {
        private readonly int idMarca = 999;
        private readonly int idMarca2 = 1000;

        private Marca marca;
        private Marca marca2;

        [TestInitialize()]
        public void StartUp()
        {
            marca = new Marca
            {
                IdMarca = idMarca,
                Descricao = "Descrição 1"
            };

            marca2 = new Marca
            {
                IdMarca = idMarca2,
                Descricao = "Descrição 2"
            };

            marca2.Inserir();
        }

        [TestCleanup()]
        public void CleanUp()
        {
            marca.Excluir();
            marca2.Excluir();
        }

        [TestMethod()]
        public void InserirTest_OK()
        {
            Assert.AreEqual(marca.Inserir(), "");
        }

        [TestMethod()]
        public void InserirTest_RecuperarDados()
        {
            marca.Inserir();

            Marca tmpMarca = new Marca(idMarca);

            Assert.AreEqual(marca.IdMarca, tmpMarca.IdMarca);
            Assert.AreEqual(marca.Descricao, tmpMarca.Descricao);
        }

        [TestMethod()]
        public void InserirTest_IdInvalido()
        {
            marca.IdMarca = -1;
            Assert.AreEqual(marca.Inserir(), "id de marca inválido.\r\n");
        }

        [TestMethod()]
        public void InserirTest_DescricaoVazia()
        {
            marca.Descricao = "";
            Assert.AreEqual(marca.Inserir(), "Descrição não pode ser vazia.\r\n");
        }

        [TestMethod()]
        public void InserirTest_DescricaoGrande()
        {
            marca.Descricao = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(marca.Inserir(), "Descrição maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_IdExistente()
        {
            marca.IdMarca = idMarca2;
            Assert.AreEqual(marca.Inserir(), "Id de marca já utilizada\r\n");
        }

        [TestMethod()]
        public void AlterarTest_OK()
        {
            marca.Inserir();
            marca.Descricao = "Descrição alterada";
            Assert.AreEqual(marca.Alterar(), "");
        }

        [TestMethod()]
        public void AlterarTest_RecuperarDados()
        {
            marca.Inserir();
            marca.Descricao = "Descrição alterada";
            marca.Alterar();

            Marca tmpMarca = new Marca(idMarca);

            Assert.AreEqual(tmpMarca.IdMarca, marca.IdMarca);
            Assert.AreEqual(tmpMarca.Descricao, marca.Descricao);
        }

        [TestMethod()]
        public void AlterarTest_IdInexistente()
        {
            marca.Descricao = "Descrição alterada";

            Assert.AreEqual(marca.Alterar(), "Marca com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_IdInvalido()
        {
            marca.Inserir();
            marca.IdMarca = -1;
            Assert.AreEqual(marca.Alterar(), "id de marca inválido.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_DescricaoVazia()
        {
            marca.Inserir();
            marca.Descricao = "";
            Assert.AreEqual(marca.Alterar(), "Descrição não pode ser vazia.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_DescricaoGrande()
        {
            marca.Inserir();
            marca.Descricao = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(marca.Alterar(), "Descrição maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_OK()
        {
            marca.Inserir();
            Assert.AreEqual("", marca.Excluir());

            Assert.AreEqual(marca.Recuperar(), "Marca com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInvalido()
        {
            marca.Inserir();
            marca.IdMarca = -1;
            Assert.AreEqual("id de marca inválido.\r\n", marca.Excluir());
        }

        [TestMethod()]
        public void ExcluirTest_IdInexistente()
        {
            marca.IdMarca = 1111;
            Assert.AreEqual("Marca com este ID não existe.\r\n", marca.Excluir());
        }

        [TestMethod()]
        public void RecuperarTest_OK()
        {
            marca.Inserir();
            
            Marca tmpMarca = new Marca(idMarca);

            Assert.AreEqual(tmpMarca.IdMarca, marca.IdMarca);
            Assert.AreEqual(tmpMarca.Descricao, marca.Descricao);
        }

        [TestMethod()]
        public void RecuperarTest_IdInvalido()
        {
            marca.Inserir();
            marca.IdMarca = -1;
            Assert.AreEqual("id de marca inválido.\r\n", marca.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_IdInexistente()
        {
            marca.IdMarca = 1111;
            Assert.AreEqual("Marca com este ID não existe.\r\n", marca.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_Todos()
        {
            marca.Inserir();

            Marca[] list = new Marca[] { marca, marca2 };

            List<Marca> marcas = Marca.Recuperar("true");

            for (int i = 0; i < marcas.Count; i++)
            {
                Assert.AreEqual(marcas[i].IdMarca, list[i].IdMarca);
                Assert.AreEqual(marcas[i].Descricao, list[i].Descricao);
            }
        }

        [TestMethod()]
        public void RecuperarTest_Vazio()
        {
            marca.Inserir();

            Marca[] list = new Marca[] { marca, marca2 };

            List<Marca> marcas = Marca.Recuperar("false");

            Assert.AreEqual(marcas.Count, 0);
        }
    }
}