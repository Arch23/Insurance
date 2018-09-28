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
    public class ModeloTests
    {
        private readonly int idModelo = 999;
        private readonly int idModelo2 = 1000;

        private Modelo modelo;
        private Modelo modelo2;
        private Marca marca;
        private Marca marca2;

        [TestInitialize()]
        public void StartUp()
        {
            int idMarca = 999;
            int idMarca2 = 1000;

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

            marca.Excluir();
            marca.Inserir();
            marca2.Excluir();
            marca2.Inserir();

            modelo = new Modelo
            {
                IdModelo = idModelo,
                Descricao = "Descrição 1",
                AMarca = new Marca(idMarca)
            };

            modelo2 = new Modelo
            {
                IdModelo = idModelo2,
                Descricao = "Descrição 2",
                AMarca = new Marca(idMarca)
            };

            modelo2.Excluir();
            modelo2.Inserir();

            modelo.Excluir();
        }

        [TestCleanup()]
        public void CleanUp()
        {
            modelo.Excluir();
            modelo2.Excluir();
            marca.Excluir();
            marca2.Excluir();
        }

        [TestMethod()]
        public void InserirTest_OK()
        {
            Assert.AreEqual(modelo.Inserir(), "");
        }

        [TestMethod()]
        public void InserirTest_RecuperarDados()
        {
            modelo.Inserir();

            Modelo tmpModelo = new Modelo(idModelo);

            Assert.AreEqual(modelo.IdModelo, tmpModelo.IdModelo);
            Assert.AreEqual(modelo.Descricao, tmpModelo.Descricao);
            Assert.AreEqual(modelo.AMarca.IdMarca, tmpModelo.AMarca.IdMarca);
        }

        [TestMethod()]
        public void InserirTest_IdInvalido()
        {
            modelo.IdModelo = -1;
            Assert.AreEqual(modelo.Inserir(), "id de modelo inválido.\r\n");
        }

        [TestMethod()]
        public void InserirTest_DescricaoVazia()
        {
            modelo.Descricao = "";
            Assert.AreEqual(modelo.Inserir(), "Descrição não pode ser vazia.\r\n");
        }

        [TestMethod()]
        public void InserirTest_DescricaoGrande()
        {
            modelo.Descricao = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(modelo.Inserir(), "Descrição maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_IdExistente()
        {
            modelo.IdModelo = idModelo2;
            Assert.AreEqual(modelo.Inserir(), "Id de modelo já utilizada\r\n");
        }

        [TestMethod()]
        public void InserirTest_ValidarMarca()
        {
            modelo.AMarca.IdMarca = 1111;
            Assert.AreEqual(modelo.Inserir(), "Id de marca não cadastrado!\r\n");
        }

        [TestMethod()]
        public void AlterarTest_OK()
        {
            modelo.Inserir();
            modelo.Descricao = "Descrição alterada";
            modelo.AMarca.IdMarca = marca2.IdMarca;
            Assert.AreEqual(modelo.Alterar(), "");
        }

        [TestMethod()]
        public void AlterarTest_RecuperarDados()
        {
            modelo.Inserir();
            modelo.Descricao = "Descrição alterada";
            modelo.AMarca.IdMarca = marca2.IdMarca;
            modelo.Alterar();

            Modelo tmpModelo = new Modelo(idModelo);

            Assert.AreEqual(modelo.IdModelo, tmpModelo.IdModelo);
            Assert.AreEqual(modelo.Descricao, tmpModelo.Descricao);
            Assert.AreEqual(modelo.AMarca.IdMarca, tmpModelo.AMarca.IdMarca);
        }

        [TestMethod()]
        public void AlterarTest_IdInexistente()
        {
            modelo.IdModelo = 1111;
            modelo.Descricao = "Descrição alterada";

            Assert.AreEqual(modelo.Alterar(), "Modelo com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_IdInvalido()
        {
            modelo.Inserir();
            modelo.IdModelo = -1;
            Assert.AreEqual(modelo.Alterar(), "id de modelo inválido.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_DescricaoVazia()
        {
            modelo.Inserir();
            modelo.Descricao = "";
            Assert.AreEqual(modelo.Alterar(), "Descrição não pode ser vazia.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_DescricaoGrande()
        {
            modelo.Inserir();
            modelo.Descricao = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(modelo.Alterar(), "Descrição maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_ValidarMarca()
        {
            modelo.Inserir();
            modelo.AMarca.IdMarca = 1111;
            Assert.AreEqual(modelo.Alterar(), "Id de marca não cadastrado!\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_OK()
        {
            modelo.Inserir();
            Assert.AreEqual("", modelo.Excluir());

            Assert.AreEqual(modelo.Recuperar(), "Modelo com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInvalido()
        {
            modelo.Inserir();
            modelo.IdModelo = -1;
            Assert.AreEqual("id de modelo inválido.\r\n", modelo.Excluir());
        }

        [TestMethod()]
        public void ExcluirTest_IdInexistente()
        {
            modelo.IdModelo = 1111;
            Assert.AreEqual("Modelo com este ID não existe.\r\n", modelo.Excluir());
        }

        [TestMethod()]
        public void RecuperarTest_OK()
        {
            modelo.Inserir();

            Modelo tmpModelo = new Modelo(idModelo);

            Assert.AreEqual(modelo.IdModelo, tmpModelo.IdModelo);
            Assert.AreEqual(modelo.Descricao, tmpModelo.Descricao);
            Assert.AreEqual(modelo.AMarca.IdMarca, tmpModelo.AMarca.IdMarca);
        }

        [TestMethod()]
        public void RecuperarTest_IdInvalido()
        {
            modelo.Inserir();
            modelo.IdModelo = -1;
            Assert.AreEqual("id de modelo inválido.\r\n", modelo.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_IdInexistente()
        {
            modelo.IdModelo = 1111;
            Assert.AreEqual("Modelo com este ID não existe.\r\n", modelo.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_Todos()
        {
            modelo.Inserir();

            Modelo[] list = new Modelo[] { modelo, modelo2 };

            List<Modelo> modelos = Modelo.Recuperar("true");

            for (int i = 0; i < modelos.Count; i++)
            {
                Assert.AreEqual(modelos[i].IdModelo, list[i].IdModelo);
                Assert.AreEqual(modelos[i].Descricao, list[i].Descricao);
                Assert.AreEqual(modelos[i].AMarca.IdMarca, list[i].AMarca.IdMarca);
            }
        }

        [TestMethod()]
        public void RecuperarTest_Vazio()
        {
            modelo.Inserir();

            Modelo[] list = new Modelo[] { modelo, modelo2 };

            List<Modelo> modelos = Modelo.Recuperar("false");

            Assert.AreEqual(modelos.Count, 0);
        }
    }
}