using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class Marca
    {
        #region atributos, getters e setters
        private int idMarca;
        private string descricao;

        public Marca() { }

        public Marca(int pIdMarca)
        {
            idMarca = pIdMarca;
            Recuperar();
        }

        public Marca(int pIdMarca, string pDescricao)
        {
            idMarca = pIdMarca;
            descricao = pDescricao;
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value.Trim(); }
        }

        public int IdMarca
        {
            get { return idMarca; }
            set { idMarca = value; }
        }
        #endregion

        #region crud
        public string Inserir()
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                return retorno;

            string cmdInserir = $"INSERT INTO marca(IdMarca, Descricao) VALUES({idMarca}, '{descricao}');";
            return ConexaoBD.GetInstance().ExecutarSql(cmdInserir);
        }

        public string Alterar()
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                return retorno;

            string cmdAlterar = $"UPDATE marca SET Descricao='{descricao}' WHERE IdMarca={idMarca};";
            return ConexaoBD.GetInstance().ExecutarSql(cmdAlterar);
        }

        public string Excluir()
        {
            string retorno = Utils.ValidarId(idMarca, "marca");
            if (retorno.Length != 0)
                return retorno;

            string cmdExcluir = $"DELETE FROM marca WHERE IdMarca={idMarca};";
            return ConexaoBD.GetInstance().ExecutarSql(cmdExcluir);
        }

        public string Recuperar()
        {
            string retorno = Utils.ValidarId(idMarca, "marca");
            if (retorno.Length != 0)
                return retorno;

            string cmdRecuperar = $"SELECT * FROM marca WHERE IdMarca={idMarca};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count != 0)
            {
                idMarca = Convert.ToInt32(tabela.Rows[0]["IdMarca"]);
                descricao = tabela.Rows[0]["Descricao"].ToString();
                retorno = "";
            }
            else
            {
                retorno = "Nenhuma marca encontrada. ";
            }

            return retorno;
        }

        public static List<Marca> Recuperar(string pCondicao)
        {
            List<Marca> lista = null;

            string retorno = "";
            string cmdRecuperar = $"SELECT IdMarca FROM marca WHERE {pCondicao};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return lista;

            lista = new List<Marca>();

            if (tabela.Rows.Count != 0)
            {
                foreach(DataRow linha in tabela.Rows)
                {
                    lista.Add(new Marca(Convert.ToInt32(linha["idMarca"])));
                }
            }

            return lista;
        }
        #endregion

        

        private string Validar()
        {
            string retorno = "";
            retorno += Utils.ValidarId(idMarca, "marca");
            if (descricao.Length == 0)
            {
                retorno += "descrição não pode ser vazia.\r\n";
            }

            return retorno;
        }
    }
}