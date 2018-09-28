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
    public class VeiculoTests
    {
        private readonly int idVeiculo = 999;
        private readonly int idVeiculo2 = 1000;

        private Veiculo veiculo;
        private Veiculo veiculo2;
        private Modelo modelo;
        private Modelo modelo2;
        private Marca marca;
        private Marca marca2;

        [TestInitialize()]
        public void StartUp()
        {
            int idModelo = 999;
            int idModelo2 = 1000;
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

            modelo.Excluir();
            modelo.Inserir();
            modelo2.Excluir();
            modelo2.Inserir();

            veiculo = new Veiculo
            {
                IdVeiculo = idVeiculo,
                Placa = "ABC-1234",
                Ano = 2000,
                AModelo = new Modelo(idModelo)
            };

            veiculo2 = new Veiculo
            {
                IdVeiculo = idVeiculo2,
                Placa = "ABC-5678",
                Ano = 2001,
                AModelo = new Modelo(idModelo)
            };

            veiculo2.Excluir();
            veiculo2.Inserir();

            veiculo.Excluir();
        }

        [TestCleanup()]
        public void CleanUp()
        {
            veiculo.Excluir();
            veiculo2.Excluir();
            modelo.Excluir();
            modelo2.Excluir();
            marca.Excluir();
            marca2.Excluir();
        }

        [TestMethod()]
        public void InserirTest_OK()
        {
            Assert.AreEqual(veiculo.Inserir(), "");
        }

        [TestMethod()]
        public void InserirTest_RecuperarDados()
        {
            veiculo.Inserir();

            Veiculo tmpVeiculo = new Veiculo(idVeiculo);

            Assert.AreEqual(veiculo.IdVeiculo, tmpVeiculo.IdVeiculo);
            Assert.AreEqual(veiculo.Placa, tmpVeiculo.Placa);
            Assert.AreEqual(veiculo.AModelo.IdModelo, tmpVeiculo.AModelo.IdModelo);
        }

        [TestMethod()]
        public void InserirTest_IdInvalido()
        {
            veiculo.IdVeiculo = -1;
            Assert.AreEqual(veiculo.Inserir(), "id de veiculo inválido.\r\n");
        }

        [TestMethod()]
        public void InserirTest_PlacaVazia()
        {
            veiculo.Placa = "";
            Assert.AreEqual(veiculo.Inserir(), "Placa não pode ser vazia.\r\n");
        }

        [TestMethod()]
        public void InserirTest_PlacaGrande()
        {
            veiculo.Placa = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(veiculo.Inserir(), "Placa maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_IdExistente()
        {
            veiculo.IdVeiculo = idVeiculo2;
            Assert.AreEqual(veiculo.Inserir(), "Id de veiculo já utilizada\r\n");
        }

        [TestMethod()]
        public void InserirTest_ValidarModelo()
        {
            veiculo.AModelo.IdModelo = 1111;
            Assert.AreEqual(veiculo.Inserir(), "Id de modelo não cadastrado!\r\n");
        }

        [TestMethod()]
        public void AlterarTest_OK()
        {
            veiculo.Inserir();
            veiculo.Placa = "Placa alterada";
            veiculo.AModelo.IdModelo = modelo2.IdModelo;
            Assert.AreEqual(veiculo.Alterar(), "");
        }

        [TestMethod()]
        public void AlterarTest_RecuperarDados()
        {
            veiculo.Inserir();
            veiculo.Placa = "Placa alterada";
            veiculo.AModelo.IdModelo = modelo2.IdModelo;
            veiculo.Alterar();

            Veiculo tmpVeiculo = new Veiculo(idVeiculo);

            Assert.AreEqual(veiculo.IdVeiculo, tmpVeiculo.IdVeiculo);
            Assert.AreEqual(veiculo.Placa, tmpVeiculo.Placa);
            Assert.AreEqual(veiculo.AModelo.IdModelo, tmpVeiculo.AModelo.IdModelo);
        }

        [TestMethod()]
        public void AlterarTest_IdInexistente()
        {
            veiculo.IdVeiculo = 1111;
            veiculo.Placa = "Placa alterada";

            Assert.AreEqual(veiculo.Alterar(), "Veiculo com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_IdInvalido()
        {
            veiculo.Inserir();
            veiculo.IdVeiculo = -1;
            Assert.AreEqual(veiculo.Alterar(), "id de veiculo inválido.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_PlacaVazia()
        {
            veiculo.Inserir();
            veiculo.Placa = "";
            Assert.AreEqual(veiculo.Alterar(), "Placa não pode ser vazia.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_PlacaGrande()
        {
            veiculo.Inserir();
            veiculo.Placa = "abcdefghijklmnoprstuvxzabcdefghijklmnopqrstuvx";
            Assert.AreEqual(veiculo.Alterar(), "Placa maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_ValidarModelo()
        {
            veiculo.Inserir();
            veiculo.AModelo.IdModelo = 1111;
            Assert.AreEqual(veiculo.Alterar(), "Id de modelo não cadastrado!\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_OK()
        {
            veiculo.Inserir();
            Assert.AreEqual("", veiculo.Excluir());

            Assert.AreEqual(veiculo.Recuperar(), "Veiculo com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInvalido()
        {
            veiculo.Inserir();
            veiculo.IdVeiculo = -1;
            Assert.AreEqual("id de veiculo inválido.\r\n", veiculo.Excluir());
        }

        [TestMethod()]
        public void ExcluirTest_IdInexistente()
        {
            veiculo.IdVeiculo = 1111;
            Assert.AreEqual("Veiculo com este ID não existe.\r\n", veiculo.Excluir());
        }

        [TestMethod()]
        public void RecuperarTest_OK()
        {
            veiculo.Inserir();

            Veiculo tmpVeiculo = new Veiculo(idVeiculo);

            Assert.AreEqual(veiculo.IdVeiculo, tmpVeiculo.IdVeiculo);
            Assert.AreEqual(veiculo.Placa, tmpVeiculo.Placa);
            Assert.AreEqual(veiculo.AModelo.IdModelo, tmpVeiculo.AModelo.IdModelo);
        }

        [TestMethod()]
        public void RecuperarTest_IdInvalido()
        {
            veiculo.Inserir();
            veiculo.IdVeiculo = -1;
            Assert.AreEqual("id de veiculo inválido.\r\n", veiculo.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_IdInexistente()
        {
            veiculo.IdVeiculo = 1111;
            Assert.AreEqual("Veiculo com este ID não existe.\r\n", veiculo.Recuperar());
        }

        [TestMethod()]
        public void RecuperarTest_Todos()
        {
            veiculo.Inserir();

            Veiculo[] list = new Veiculo[] { veiculo, veiculo2 };

            List<Veiculo> veiculos = Veiculo.Recuperar("true");

            for (int i = 0; i < veiculos.Count; i++)
            {
                Assert.AreEqual(veiculos[i].IdVeiculo, list[i].IdVeiculo);
                Assert.AreEqual(veiculos[i].Placa, list[i].Placa);
                Assert.AreEqual(veiculos[i].AModelo.IdModelo, list[i].AModelo.IdModelo);
            }
        }

        [TestMethod()]
        public void RecuperarTest_Vazio()
        {
            veiculo.Inserir();

            Veiculo[] list = new Veiculo[] { veiculo, veiculo2 };

            List<Veiculo> veiculos = Veiculo.Recuperar("false");

            Assert.AreEqual(veiculos.Count, 0);
        }
    }
}