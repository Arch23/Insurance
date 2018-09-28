using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class Modelo
    {
        #region atributos, getters e setters
        private int idModelo;
        private string descricao;
        private Marca aMarca;

        public Modelo() { }

        public Modelo(int pIdModelo)
        {
            idModelo = pIdModelo;
            Recuperar();
        }

        public Modelo(int pIdMarca, int pIdModelo, string pDescricao)
        {
            aMarca = new Marca(pIdMarca);
            idModelo = pIdModelo;
            descricao = pDescricao;
        }

        public Modelo(Marca pMarca, int pIdModelo, string pDescricao = "")
        {
            if(pMarca == null)
            {
                throw new Exception("Marca não pode ser nula!");
            }

            aMarca = pMarca;
            idModelo = pIdModelo;
            descricao = pDescricao;
        }

        public Marca AMarca
        {
            get { return aMarca; }
            set { aMarca = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value.Trim(); }
        }

        public int IdModelo
        {
            get { return idModelo; }
            set { idModelo = value; }
        }
        #endregion

        #region crud
        public string Inserir()
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                return retorno;

            if (ValidarIdExistente())
                return "Id de modelo já utilizada\r\n";

            string cmdInserir = $"INSERT INTO modelo(IdModelo, IdMarca, Descricao) VALUES({IdModelo}, {aMarca.IdMarca}, '{descricao}');";
            return ConexaoBD.GetInstance().ExecutarSql(cmdInserir);
        }

        public string Alterar()
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                return retorno;

            if (!ValidarIdExistente())
                return "Modelo com este ID não existe.\r\n";

            string cmdAlterar = $"UPDATE modelo SET IdMarca={aMarca.IdMarca}, Descricao='{descricao}' WHERE IdModelo={IdModelo};";
            return ConexaoBD.GetInstance().ExecutarSql(cmdAlterar);
        }

        public string Excluir()
        {
            string retorno = Utils.ValidarId(IdModelo, "modelo");
            if (retorno.Length != 0)
                return retorno;

            if (!ValidarIdExistente())
                return "Modelo com este ID não existe.\r\n";

            string cmdExcluir = $"DELETE FROM modelo WHERE IdModelo={IdModelo};";
            return ConexaoBD.GetInstance().ExecutarSql(cmdExcluir);
        }

        public string Recuperar()
        {
            string retorno = Utils.ValidarId(idModelo, "modelo");
            if (retorno.Length != 0)
                return retorno;

            if (!ValidarIdExistente())
                return "Modelo com este ID não existe.\r\n";

            string cmdRecuperar = $"SELECT * FROM modelo WHERE IdModelo={idModelo};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count != 0)
            {
                idModelo = Convert.ToInt32(tabela.Rows[0]["IdModelo"]);
                descricao = tabela.Rows[0]["Descricao"].ToString();
                aMarca = new Marca(Convert.ToInt32(tabela.Rows[0]["IdMarca"]));
                retorno = "";
            }

            return retorno;
        }

        public static List<Modelo> Recuperar(string pCondicao)
        {
            List<Modelo> lista = null;

            string retorno = "";
            string cmdRecuperar = $"SELECT IdModelo FROM modelo WHERE {pCondicao};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return lista;

            lista = new List<Modelo>();

            if (tabela.Rows.Count != 0)
            {
                foreach (DataRow linha in tabela.Rows)
                {
                    lista.Add(new Modelo(Convert.ToInt32(linha["IdModelo"])));
                }
            }

            return lista;
        }
        #endregion

        private bool ValidarIdExistente()
        {
            string retorno;
            string cmdSelectId = $"SELECT idModelo FROM modelo WHERE idModelo={IdModelo}";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdSelectId, out retorno);

            return tabela.Rows.Count != 0;
        }

        private string ValidarMarca()
        {
            string retorno = "";
            string auxRetorno = "";
            string cmdSelectMarca = $"SELECT idMarca FROM marca WHERE idMarca={aMarca.IdMarca};";

            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdSelectMarca, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count == 0)
                auxRetorno += "Id de marca não cadastrado!\r\n";

            return auxRetorno;
        }

        private string Validar()
        {
            string retorno = "";
            retorno += Utils.ValidarId(idModelo, "modelo");

            if (descricao.Length == 0)
                retorno += "Descrição não pode ser vazia.\r\n";
            if (descricao.Length > 45)
                retorno += "Descrição maior que o limite de 45 caracteres.\r\n";

            return retorno += ValidarMarca();
        }
    }
}