﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class Cliente : Pessoa
    {
        public Cliente() { }

        public Cliente(int pIdCliente): base(pIdCliente)
        {
            Recuperar();
        }

        public Cliente(int pIdCliente, string pNome, string pFone, string pEndereco, string pBairro, string pCep, string pCpf, string pRg, string pSexo, DateTime pDataNascimento)
        : base(pIdCliente, pNome, pFone, pEndereco, pBairro, pCep)
        {
            cpf = pCpf;
            rg = pRg;
            sexo = pSexo;
            dataNascimento = pDataNascimento;
        }

        public Cliente(Pessoa pPessoa)
        {
            IdPessoa = pPessoa.IdPessoa;
            Nome = pPessoa.Nome;
            Fone = pPessoa.Fone;
            Endereco = pPessoa.Endereco;
            Bairro = pPessoa.Bairro;
            Cep = pPessoa.Cep;
        }

        #region atributos, getters e setters
        private string cpf;
        private string rg;
        private string sexo;
        private DateTime dataNascimento;

        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        public string Sexo
        {
            get { return sexo; }
            set { sexo = value.Trim(); }
        }

        public string Rg
        {
            get { return rg; }
            set { rg = value.Trim(); }
        }

        public string Cpf
        {
            get { return cpf; }
            set { cpf = value.Trim(); }
        }
        #endregion

        #region crud
        public override string Inserir()
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                return retorno;

            retorno = base.Inserir();

            if (retorno.Length != 0)
                return retorno;

            string cmdInserir = $"INSERT INTO cliente(IdCliente, Cpf, Rg, Sexo, DataNascimento) " +
                $"VALUES({IdPessoa}, '{cpf}','{rg}', '{sexo}', '{dataNascimento.ToString("yyyy/MM/dd")}');";

            return ConexaoBD.GetInstance().ExecutarSql(cmdInserir);
        }

        public override string Alterar()
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                return retorno;

            retorno = base.Alterar();

            if (retorno.Length != 0)
                return retorno;

            string cmdAlterar = $"UPDATE cliente SET Cpf='{cpf}', Rg='{rg}', Sexo='{sexo}', DataNascimento='{dataNascimento.ToString("yyyy/MM/dd")}' " +
                $"WHERE IdCliente={IdPessoa};";

            return ConexaoBD.GetInstance().ExecutarSql(cmdAlterar);
        }

        public override string Excluir()
        {
            string retorno = Utils.ValidarId(IdPessoa, "cliente");

            if (retorno.Length != 0)
                return retorno;

            string cmdExcluir = $"DELETE FROM cliente WHERE IdCliente={IdPessoa};";

            retorno = ConexaoBD.GetInstance().ExecutarSql(cmdExcluir);

            if (retorno.Length != 0)
                return retorno;

            return base.Excluir();
        }

        public override string Recuperar()
        {
            string retorno = Utils.ValidarId(IdPessoa, "cliente");

            if (retorno.Length != 0)
                return retorno;

            retorno = base.Recuperar();

            if (retorno.Length != 0)
                return retorno;

            string cmdRecuperar = $"SELECT * FROM cliente WHERE IdCliente={IdPessoa};";

            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count != 0)
            {
                DataRow linha = tabela.Rows[0];
                cpf = linha["Cpf"].ToString();
                rg = linha["Rg"].ToString();
                sexo = linha["Sexo"].ToString();
                dataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                retorno = "";
            }
            else
            {
                retorno = "Nenhuma entrada encontrada. ";
            }

            return retorno;
        }

        public new static List<Cliente> Recuperar(string pCondicao)
        {
            List<Pessoa> listaPessoas = Pessoa.Recuperar(pCondicao);

            if (listaPessoas == null)
                return null;

            List<Cliente> listaClientes = new List<Cliente>();

            foreach(Pessoa pessoa in listaPessoas)
            {
                Cliente auxCliente = new Cliente(pessoa);
                auxCliente.Recuperar();

                listaClientes.Add(auxCliente);
            }

            return listaClientes;
        }
        #endregion

        private string Validar()
        {
            string retorno = Utils.ValidarId(IdPessoa, "cliente");

            if (cpf.Length == 0)
                retorno += "cpf não pode ser vazio.\r\n";

            if (rg.Length == 0)
                retorno += "rg não pode ser vazio.\r\n";

            if (sexo.Length == 0)
                retorno += "sexo não pode ser vazio.\r\n";

            return retorno;
        }
    }
}