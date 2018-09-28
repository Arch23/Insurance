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
    public class ApoliceTests
    {
        private readonly int idCliente = 999;
        private readonly int idCliente2 = 1000;

        private readonly int idVeiculo = 999;
        private readonly int idVeiculo2 = 1000;

        private readonly int idApolice = 999;
        private readonly int idApolice2 = 10000;

        private Marca marca;
        private Modelo modelo;
        private Cliente cliente;
        private Cliente cliente2;
        private Veiculo veiculo;
        private Veiculo veiculo2;
        private Apolice apolice;
        private Apolice apolice2;

        [TestInitialize()]
        public void StartUp()
        {
            int idMarca = 999;
            int idModelo = 999;

            //criar uma marca e modelo para ser utilizado pelo veiculo
            marca = new Marca(idMarca, "Marca 1");
            marca.Inserir();

            modelo = new Modelo(new Marca(idMarca), idModelo, "Descrição do modelo 1");
            modelo.Inserir();

            //criar as entradas para Cliente e Veiculo para serem utilizados nos testes
            cliente = new Cliente(
                idCliente,
                "Cliente 1",
                "Telefone 1",
                "Endereço 1",
                "Bairro 1",
                "Cep 1",
                "Cpf 1",
                "Rg 1",
                "M",
                Convert.ToDateTime("09/12/1990"));

            cliente2 = new Cliente(
                idCliente2,
                "Cliente 2",
                "Telefone 2",
                "Endereço 2",
                "Bairro 2",
                "Cep 2",
                "Cpf 2",
                "Rg 2",
                "F",
                Convert.ToDateTime("21/12/1994"));

            veiculo = new Veiculo(new Modelo(idModelo), idVeiculo, "Placa 1", 2000);
            veiculo2 = new Veiculo(new Modelo(idModelo), idVeiculo2, "Placa 2", 2001);

            cliente.Inserir();
            cliente2.Inserir();
            veiculo.Inserir();
            veiculo2.Inserir();

            apolice = new Apolice
            {
                IdApolice = idApolice,
                NumeroApolice = "12345",
                Franquia = 99.99,
                Valor = 450.99,
                DataContrato = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(1),
                DataFim = DateTime.Now.AddYears(1),
                ACliente = new Cliente(idCliente),
                AVeiculo = new Veiculo(idVeiculo)
            };

            apolice2 = new Apolice
            {
                IdApolice = idApolice2,
                NumeroApolice = "12346",
                Franquia = 99.99,
                Valor = 450.99,
                DataContrato = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(1),
                DataFim = DateTime.Now.AddYears(1),
                ACliente = new Cliente(idCliente),
                AVeiculo = new Veiculo(idVeiculo)
            };

            apolice2.Inserir();
        }

        [TestCleanup()]
        public void CleanUp()
        {
            apolice.Excluir();
            apolice2.Excluir();
            veiculo.Excluir();
            veiculo2.Excluir();
            modelo.Excluir();
            marca.Excluir();
            cliente.Excluir();
            cliente2.Excluir();
        }

        [TestMethod()]
        public void InserirTest_OK()
        {
            Assert.AreEqual(apolice.Inserir(), "");
        }

        [TestMethod()]
        public void InserirTest_RecuperarDados()
        {
            apolice.Inserir();

            Apolice tmpApolice = new Apolice(idApolice);

            Assert.AreEqual(apolice.IdApolice, tmpApolice.IdApolice);
            Assert.AreEqual(apolice.NumeroApolice, tmpApolice.NumeroApolice);
            Assert.AreEqual(apolice.Valor, tmpApolice.Valor);
            Assert.AreEqual(apolice.Franquia, tmpApolice.Franquia);
            Assert.AreEqual(apolice.DataContrato.Date, tmpApolice.DataContrato.Date);
            Assert.AreEqual(apolice.DataInicio.Date, tmpApolice.DataInicio.Date);
            Assert.AreEqual(apolice.DataFim.Date, tmpApolice.DataFim.Date);
            Assert.AreEqual(apolice.AVeiculo.IdVeiculo, tmpApolice.AVeiculo.IdVeiculo);
            Assert.AreEqual(apolice.ACliente.IdPessoa, tmpApolice.ACliente.IdPessoa);
        }

        [TestMethod()]
        public void InserirTest_IdInvalido()
        {
            apolice.IdApolice = -1;
            Assert.AreEqual(apolice.Inserir(), "id de apólice inválido.\r\n");
        }

        [TestMethod()]
        public void InserirTest_NumeroVazio()
        {
            apolice.NumeroApolice = "";
            Assert.AreEqual(apolice.Inserir(), "Número da apólice não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void InserirTest_NumeroGrande()
        {
            apolice.NumeroApolice = "1234567890123456789012345678901234567890123456";
            Assert.AreEqual(apolice.Inserir(), "Número da apólice maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void InserirTest_ValorNegativo()
        {
            apolice.Valor = -1.99;
            Assert.AreEqual(apolice.Inserir(), "Valor não pode ser negativo.\r\nValor da franquia não pode ser maior que o valor da apólice.\r\n");
        }

        [TestMethod()]
        public void InserirTest_FranquiaNegativo()
        {
            apolice.Franquia = -1.99;
            Assert.AreEqual(apolice.Inserir(), "Franquia não pode ser negativa.\r\n");
        }

        [TestMethod()]
        public void InserirTest_FranquiaMaiorValor()
        {
            apolice.Franquia = 99.99;
            apolice.Valor = 55.99;
            Assert.AreEqual(apolice.Inserir(), "Valor da franquia não pode ser maior que o valor da apólice.\r\n");
        }

        [TestMethod()]
        public void InserirTest_InicioMaiorFim()
        {
            apolice.DataInicio = DateTime.Now.AddDays(30);
            apolice.DataFim = DateTime.Now;
            Assert.AreEqual(apolice.Inserir(), "Data de fim menor que a data de início.\r\n");
        }

        [TestMethod()]
        public void InserirTest_ContratoMaiorQueAtual()
        {
            apolice.DataContrato = DateTime.Now.AddDays(1);
            Assert.AreEqual(apolice.Inserir(), "Data do contrato maior que a data de hoje.\r\n");
        }

        [TestMethod()]
        public void InserirTest_VeiculoInvalido()
        {
            apolice.AVeiculo.IdVeiculo = 9999;
            Assert.AreEqual(apolice.Inserir(), "Id de veiculo não cadastrado!\r\n");
        }

        [TestMethod()]
        public void InserirTest_ClienteInvalido()
        {
            apolice.ACliente.IdPessoa = 9999;
            Assert.AreEqual(apolice.Inserir(), "Id de Cliente não cadastrado!\r\n");
        }

        [TestMethod()]
        public void InserirTest_IdExistente()
        {
            apolice.IdApolice = idApolice2;
            Assert.AreEqual(apolice.Inserir(), "Id de apolice já utilizada\r\n");
        }

        [TestMethod()]
        public void AlterarTest_OK()
        {
            apolice.Inserir();

            apolice.NumeroApolice = "54321";
            apolice.Franquia = 45.99;
            apolice.Valor = 99.57;
            apolice.ACliente = cliente2;
            apolice.AVeiculo = veiculo2;
            apolice.DataContrato = DateTime.Now.AddDays(-1);
            apolice.DataInicio = DateTime.Now.AddDays(2);
            apolice.DataFim = DateTime.Now.AddDays(10);

            Assert.AreEqual(apolice.Alterar(), "");
        }

        [TestMethod()]
        public void AlterarTest_RecuperarDados()
        {
            apolice.Inserir();

            apolice.NumeroApolice = "54321";
            apolice.Franquia = 45.99;
            apolice.Valor = 99.57;
            apolice.ACliente = cliente2;
            apolice.AVeiculo = veiculo2;
            apolice.DataContrato = DateTime.Now.AddDays(-1);
            apolice.DataInicio = DateTime.Now.AddDays(2);
            apolice.DataFim = DateTime.Now.AddDays(10);

            apolice.Alterar();

            Apolice tmpApolice = new Apolice(idApolice);

            Assert.AreEqual(apolice.IdApolice, tmpApolice.IdApolice);
            Assert.AreEqual(apolice.NumeroApolice, tmpApolice.NumeroApolice);
            Assert.AreEqual(apolice.Valor, tmpApolice.Valor);
            Assert.AreEqual(apolice.Franquia, tmpApolice.Franquia);
            Assert.AreEqual(apolice.DataContrato.Date, tmpApolice.DataContrato.Date);
            Assert.AreEqual(apolice.DataInicio.Date, tmpApolice.DataInicio.Date);
            Assert.AreEqual(apolice.DataFim.Date, tmpApolice.DataFim.Date);
            Assert.AreEqual(apolice.AVeiculo.IdVeiculo, tmpApolice.AVeiculo.IdVeiculo);
            Assert.AreEqual(apolice.ACliente.IdPessoa, tmpApolice.ACliente.IdPessoa);
        }

        [TestMethod()]
        public void AlterarTest_IdInvalido()
        {
            apolice.Inserir();
            apolice.IdApolice = -1;
            Assert.AreEqual(apolice.Alterar(), "id de apólice inválido.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_NumeroVazio()
        {
            apolice.Inserir();
            apolice.NumeroApolice = "";
            Assert.AreEqual(apolice.Alterar(), "Número da apólice não pode ser vazio.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_NumeroGrande()
        {
            apolice.Inserir();
            apolice.NumeroApolice = "1234567890123456789012345678901234567890123456";
            Assert.AreEqual(apolice.Alterar(), "Número da apólice maior que o limite de 45 caracteres.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_ValorNegativo()
        {
            apolice.Inserir();
            apolice.Valor = -1.99;
            Assert.AreEqual(apolice.Alterar(), "Valor não pode ser negativo.\r\nValor da franquia não pode ser maior que o valor da apólice.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_FranquiaNegativo()
        {
            apolice.Inserir();
            apolice.Franquia = -1.99;
            Assert.AreEqual(apolice.Alterar(), "Franquia não pode ser negativa.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_FranquiaMaiorValor()
        {
            apolice.Inserir();
            apolice.Franquia = 99.99;
            apolice.Valor = 55.99;
            Assert.AreEqual(apolice.Alterar(), "Valor da franquia não pode ser maior que o valor da apólice.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_InicioMaiorFim()
        {
            apolice.Inserir();
            apolice.DataInicio = DateTime.Now.AddDays(30);
            apolice.DataFim = DateTime.Now;
            Assert.AreEqual(apolice.Alterar(), "Data de fim menor que a data de início.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_ContratoMaiorQueAtual()
        {
            apolice.Inserir();
            apolice.DataContrato = DateTime.Now.AddDays(1);
            Assert.AreEqual(apolice.Alterar(), "Data do contrato maior que a data de hoje.\r\n");
        }

        [TestMethod()]
        public void AlterarTest_VeiculoInvalido()
        {
            apolice.Inserir();
            apolice.AVeiculo.IdVeiculo = 9999;
            Assert.AreEqual(apolice.Alterar(), "Id de veiculo não cadastrado!\r\n");
        }

        [TestMethod()]
        public void AlterarTest_IdInexistente()
        {
            Assert.AreEqual(apolice.Alterar(), "Apólice com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_OK()
        {
            apolice.Inserir();
            Assert.AreEqual(apolice.Excluir(), "");
            //garantir que a apólice foi excluida
            Assert.AreEqual(apolice.Recuperar(), "Apólice com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInvalido()
        {
            apolice.Inserir();
            apolice.IdApolice = -1;
            Assert.AreEqual(apolice.Excluir(), "id de apólice inválido.\r\n");
        }

        [TestMethod()]
        public void ExcluirTest_IdInexistente()
        {
            apolice.IdApolice = 1111;
            Assert.AreEqual(apolice.Excluir(), "Apólice com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void RecuperarTest_OK()
        {
            apolice.Inserir();

            Apolice tmpApolice = new Apolice(idApolice);

            Assert.AreEqual(apolice.IdApolice, tmpApolice.IdApolice);
            Assert.AreEqual(apolice.NumeroApolice, tmpApolice.NumeroApolice);
            Assert.AreEqual(apolice.Valor, tmpApolice.Valor);
            Assert.AreEqual(apolice.Franquia, tmpApolice.Franquia);
            Assert.AreEqual(apolice.DataContrato.Date, tmpApolice.DataContrato.Date);
            Assert.AreEqual(apolice.DataInicio.Date, tmpApolice.DataInicio.Date);
            Assert.AreEqual(apolice.DataFim.Date, tmpApolice.DataFim.Date);
            Assert.AreEqual(apolice.AVeiculo.IdVeiculo, tmpApolice.AVeiculo.IdVeiculo);
            Assert.AreEqual(apolice.ACliente.IdPessoa, tmpApolice.ACliente.IdPessoa);
        }

        [TestMethod()]
        public void RecuperarTest_IdInvalido()
        {
            apolice.Inserir();
            apolice.IdApolice = -1;
            Assert.AreEqual(apolice.Recuperar(), "id de apólice inválido.\r\n");
        }

        [TestMethod()]
        public void RecuperarTest_IdInexistente()
        {
            apolice.IdApolice = 1111;
            Assert.AreEqual(apolice.Recuperar(), "Apólice com este ID não existe.\r\n");
        }

        [TestMethod()]
        public void RecuperarTest_Todos()
        {
            apolice.Inserir();

            Apolice[] list = new Apolice[] { apolice, apolice2 };

            List<Apolice> apolices = Apolice.Recuperar("true");

            for(int i = 0; i < apolices.Count; i++)
            {
                Assert.AreEqual(apolices[i].IdApolice, list[i].IdApolice);
                Assert.AreEqual(apolices[i].NumeroApolice, list[i].NumeroApolice);
                Assert.AreEqual(apolices[i].Valor, list[i].Valor);
                Assert.AreEqual(apolices[i].Franquia, list[i].Franquia);
                Assert.AreEqual(apolices[i].DataContrato.Date, list[i].DataContrato.Date);
                Assert.AreEqual(apolices[i].DataInicio.Date, list[i].DataInicio.Date);
                Assert.AreEqual(apolices[i].DataFim.Date, list[i].DataFim.Date);
                Assert.AreEqual(apolices[i].AVeiculo.IdVeiculo, list[i].AVeiculo.IdVeiculo);
                Assert.AreEqual(apolices[i].ACliente.IdPessoa, list[i].ACliente.IdPessoa);
            }
        }

        [TestMethod()]
        public void RecuperarTest_Vazio()
        {
            apolice.Inserir();

            Apolice[] list = new Apolice[] { apolice, apolice2 };

            List<Apolice> apolices = Apolice.Recuperar("false");

            Assert.AreEqual(apolices.Count, 0);
        }
    }
}