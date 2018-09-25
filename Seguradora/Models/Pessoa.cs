using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class Pessoa
    {
        public Pessoa() { }

        public Pessoa(int pIdPessoa)
        {
            idPessoa = pIdPessoa;
            Recuperar();
        }

        public Pessoa(int pIdPessoa, string pNome, string pFone, string pEndereco, string pBairro, string pCep)
        {
            idPessoa = pIdPessoa;
            nome = pNome;
            fone = pFone;
            endereco = pEndereco;
            bairro = pBairro;
            cep = pCep;
        }

        #region atributos, getters e setters
        private int idPessoa;
        private string nome;
        private string fone;
        private string endereco;
        private string bairro;
        private string cep;

        public int IdPessoa
        {
            get { return idPessoa; }
            set { idPessoa = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value.Trim(); }
        }

        public string Fone
        {
            get { return fone; }
            set { fone = value.Trim(); }
        }

        public string Endereco
        {
            get { return endereco; }
            set { endereco = value.Trim(); }
        }

        public string Bairro
        {
            get { return bairro; }
            set { bairro = value.Trim(); }
        }

        public string Cep
        {
            get { return cep; }
            set { cep = value.Trim(); }
        }
        #endregion

        #region crud
        public virtual string Inserir()
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                return retorno;

            string cmdInserir = $"INSERT INTO pessoa(IdPessoa, Nome, Fone, Endereco, Bairro, Cep) " +
                $"VALUES({IdPessoa}, '{nome}', '{fone}', '{endereco}', '{bairro}', '{cep}');";

            return ConexaoBD.GetInstance().ExecutarSql(cmdInserir);
        }

        public virtual string Alterar()
        {
            string retorno = Validar();

            if (retorno.Length != 0)
                return retorno;

            string cmdAlterar = $"UPDATE pessoa SET Nome='{nome}', Fone='{fone}', Endereco='{endereco}', Bairro='{bairro}', Cep='{cep}'" +
                $"WHERE IdPessoa={idPessoa};";

            return ConexaoBD.GetInstance().ExecutarSql(cmdAlterar);
        }

        public virtual string Excluir()
        {
            string retorno = Utils.ValidarId(idPessoa, "pessoa");

            if (retorno.Length != 0)
                return retorno;

            string cmdExcluir = $"DELETE FROM pessoa WHERE IdPessoa={idPessoa};";

            return ConexaoBD.GetInstance().ExecutarSql(cmdExcluir);
        }

        public virtual string Recuperar()
        {
            string retorno = Utils.ValidarId(idPessoa, "pessoa");

            if (retorno.Length != 0)
                return retorno;

            string cmdRecuperar = $"SELECT * FROM pessoa WHERE IdPessoa={idPessoa};";

            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if(tabela.Rows.Count != 0)
            {
                idPessoa = Convert.ToInt32(tabela.Rows[0]["IdPessoa"]);
                nome = tabela.Rows[0]["Nome"].ToString();
                fone = tabela.Rows[0]["Fone"].ToString();
                endereco = tabela.Rows[0]["Endereco"].ToString();
                bairro = tabela.Rows[0]["Bairro"].ToString();
                cep = tabela.Rows[0]["Cep"].ToString();
                retorno = "";
            }
            else
            {
                retorno = "Nenhuma entrada encontrada.";
            }

            return retorno;
        }

        protected static List<Pessoa> Recuperar(string pCondicao)
        {
            List<Pessoa> lista = null;

            string retorno = "";
            string cmdRecuperar = $"SELECT IdPessoa FROM pessoa WHERE {pCondicao};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return lista;

            lista = new List<Pessoa>();

            if(tabela.Rows.Count != 0)
            {
                foreach(DataRow linha in tabela.Rows)
                {
                    lista.Add(new Pessoa(Convert.ToInt32(linha["IdPessoa"])));
                }
            }

            return lista;
        }
        #endregion

        private string Validar()
        {
            string retorno = Utils.ValidarId(IdPessoa, "Pessoa");

            if (nome.Length == 0)
                retorno += "Nome não pode ser vazio.\r\n";

            if (fone.Length == 0)
                retorno += "Telefone não pode ser vazio.\r\n";

            if (endereco.Length == 0)
                retorno += "Endereço não pode ser vazio.\r\n";

            if (bairro.Length == 0)
                retorno += "Bairro não pode ser vazio.\r\n";

            if (cep.Length == 0)
                retorno += "Cep não pode ser vazio.\r\n";

            return retorno;
        }
    }
}