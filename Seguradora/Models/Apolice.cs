﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class Apolice
    {
        private string FORMATO_DATA = "yyyy/MM/dd";

        public Apolice() { }

        public Apolice(int pIdApolice)
        {
            idApolice = pIdApolice;
            Recuperar();
        }

        public Apolice(int pIdApolice, string pNumeroApolice, double pValor, double pFranquia, DateTime pDataInicio, DateTime pDataFim, DateTime pDataContrato, int pIdCliente, int pIdVeiculo)
        {
            idApolice = pIdApolice;
            numeroApolice = pNumeroApolice;
            valor = pValor;
            franquia = pFranquia;
            dataInicio = pDataInicio;
            dataFim = pDataFim;
            dataContrato = pDataContrato;
            aCliente = new Cliente(pIdCliente);
            aVeiculo = new Veiculo(pIdVeiculo);
        }

        public Apolice(Cliente pCliente, Veiculo pVeiculo)
        {
            if (pCliente == null)
                throw new Exception("Cliente não pode ser nulo! ");

            if (pVeiculo == null)
                throw new Exception("Veiculo não pode ser nulo! ");
        }

        #region atributos, getters e setters
        private int idApolice;
        private string numeroApolice;
        private double valor;
        private double franquia;
        private DateTime dataInicio;
        private DateTime dataFim;
        private DateTime dataContrato;
        private Cliente aCliente;
        private Veiculo aVeiculo;

        public Veiculo AVeiculo
        {
            get { return aVeiculo; }
        }

        public Cliente ACliente
        {
            get { return aCliente; }
        }


        public DateTime DataContrato
        {
            get { return dataContrato; }
            set { dataContrato = value; }
        }

        public double Franquia
        {
            get { return franquia; }
            set { franquia = value; }
        }

        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public DateTime DataFim
        {
            get { return dataFim; }
            set { dataFim = value; }
        }

        public DateTime DataInicio
        {
            get { return dataInicio; }
            set { dataInicio = value; }
        }

        public string NumeroApolice
        {
            get { return numeroApolice; }
            set { numeroApolice = value.Trim(); }
        }

        public int IdApolice
        {
            get { return idApolice; }
            set { idApolice = value; }
        }
        #endregion

        #region crud
        public string Inserir()
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                return retorno;

            string cmdInserir = $"INSERT INTO apolice(IdApolice, IdCliente, IdVeiculo, NumeroApolice, DataInicio, DataFim, Valor, Franquia, DataContrato) " +
                $"VALUES({IdApolice}, {aCliente.IdPessoa}, {aVeiculo.IdVeiculo}, '{numeroApolice}', '{DataInicio.ToString(FORMATO_DATA)}', '{dataFim.ToString(FORMATO_DATA)}', {valor.ToString(CultureInfo.InvariantCulture)}, {franquia.ToString(CultureInfo.InvariantCulture)}, '{DataContrato.ToString(FORMATO_DATA)}');";

            return ConexaoBD.GetInstance().ExecutarSql(cmdInserir);
        }

        public string Alterar()
        {
            string retorno = Validar();


            if (retorno.Length != 0)
                return retorno;

            string cmdAlterar = $"UPDATE apolice SET IdCliente={aCliente.IdPessoa}, IdVeiculo={aVeiculo.IdVeiculo}, NumeroApolice='{numeroApolice}', DataInicio='{DataInicio.ToString(FORMATO_DATA)}', DataFim='{dataFim.ToString(FORMATO_DATA)}', Valor={valor.ToString(CultureInfo.InvariantCulture)}, Franquia={franquia.ToString(CultureInfo.InvariantCulture)}, DataContrato='{DataContrato.ToString(FORMATO_DATA)}' " +
                $"WHERE IdApolice={idApolice};";

            return ConexaoBD.GetInstance().ExecutarSql(cmdAlterar);
        }

        public string Excluir()
        {
            string retorno = Utils.ValidarId(idApolice, "apólice");

            if (retorno.Length != 0)
                return retorno;

            string cmdExluir = $"DELETE FROM apolice WHERE IdApolice={idApolice};";

            return ConexaoBD.GetInstance().ExecutarSql(cmdExluir);
        }

        public string Recuperar()
        {
            string retorno = Utils.ValidarId(idApolice, "apólice");

            if (retorno.Length != 0)
                return retorno;

            string cmdRecuperar = $"SELECT * FROM apolice WHERE IdApolice={idApolice}";

            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count != 0)
            {
                DataRow linha = tabela.Rows[0];
                idApolice = Convert.ToInt32(linha["IdApolice"]);
                numeroApolice = linha["NumeroApolice"].ToString();
                valor = Convert.ToDouble(linha["Valor"]);
                franquia = Convert.ToDouble(linha["Franquia"]);
                dataInicio = Convert.ToDateTime(linha["DataInicio"]);
                dataFim = Convert.ToDateTime(linha["DataFim"]);
                dataContrato = Convert.ToDateTime(linha["DataContrato"]);

                aCliente = new Cliente(Convert.ToInt32(linha["IdCliente"]));
                aVeiculo = new Veiculo(Convert.ToInt32(linha["IdVeiculo"]));

                retorno = "";
            }
            else
            {
                retorno = "Nenhuma entrada encontrada. ";
            }

            return retorno;
        }

        public static List<Apolice> Recuperar(string pCondicao)
        {
            List<Apolice> lista = null;

            string retorno = "";
            string cmdRecuperar = $"SELECT IdApolice FROM apolice WHERE {pCondicao};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return lista;

            lista = new List<Apolice>();

            if(tabela.Rows.Count != 0)
            {
                foreach(DataRow linha in tabela.Rows)
                {
                    lista.Add(new Apolice(Convert.ToInt32(linha["IdApolice"])));
                }
            }

            return lista;
        }
        #endregion

        private string Validar()
        {
            string retorno = Utils.ValidarId(idApolice, "apólice");

            if (numeroApolice.Length == 0)
                retorno += "Número da apólice não pode ser vazio.\r\n";

            if (valor < 0)
                retorno += "Valor não pode ser negativo.\r\n";

            if (franquia < 0)
                retorno += "Franquia não pode ser negativa.\r\n";

            if (dataFim < dataInicio)
                retorno += "Data de fim menor que a data de início.\r\n";

            return retorno;
        }
    }
}