using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class Veiculo
    {
        #region atributos, getters e setters
        private int idVeiculo;
        private string placa;
        private int ano;
        private Modelo aModelo;

        public Veiculo() { }

        public Veiculo(int pIdVeiculo)
        {
            idVeiculo = pIdVeiculo;
            Recuperar();
        }

        public Veiculo(int pIdModelo, int pIdVeiculo, string pPlaca, int pAno)
        {
            aModelo = new Modelo(pIdModelo);
            idVeiculo = pIdVeiculo;
            placa = pPlaca;
            ano = pAno;
        }

        public Veiculo(Modelo pModelo, int pIdVeiculo, string pPlaca = "", int pAno = -1)
        {

            if (pModelo == null)
                throw new Exception("Modelo não pode ser nulo!");

            idVeiculo = pIdVeiculo;
            placa = pPlaca;
            ano = pAno;
            aModelo = pModelo;
        }

        public Modelo AModelo
        {
            get { return aModelo; }
            set { aModelo = value; }
        }

        public int Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        public string Placa
        {
            get { return placa; }
            set { placa = value.Trim(); }
        }

        public int IdVeiculo
        {
            get { return idVeiculo; }
            set { idVeiculo = value; }
        }
        #endregion

        #region crud
        public string Inserir()
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                return retorno;

            if (ValidarIdExistente())
                return "Id de veiculo já utilizada\r\n";

            string cmdInserir = $"INSERT INTO veiculo(IdVeiculo, IdModelo, Placa, Ano) VALUES({IdVeiculo}, {aModelo.IdModelo}, '{placa}', {ano});";
            return ConexaoBD.GetInstance().ExecutarSql(cmdInserir);
        }

        public string Alterar()
        {
            string retorno = Validar();
            if (retorno.Length != 0)
                return retorno;

            if (!ValidarIdExistente())
                return "Veiculo com este ID não existe.\r\n";

            string cmdAlterar = $"UPDATE veiculo SET IdModelo={aModelo.IdModelo}, Placa='{placa}', Ano={ano} WHERE IdVeiculo={IdVeiculo};";
            return ConexaoBD.GetInstance().ExecutarSql(cmdAlterar);
        }

        public string Excluir()
        {
            string retorno = Utils.ValidarId(idVeiculo, "veiculo");
            if (retorno.Length != 0)
                return retorno;

            if (!ValidarIdExistente())
                return "Veiculo com este ID não existe.\r\n";

            string cmdExcluir = $"DELETE FROM veiculo WHERE IdVeiculo={idVeiculo};";
            return ConexaoBD.GetInstance().ExecutarSql(cmdExcluir);
        }

        public string Recuperar()
        {
            string retorno = Utils.ValidarId(idVeiculo, "veiculo");
            if (retorno.Length != 0)
                return retorno;

            if (!ValidarIdExistente())
                return "Veiculo com este ID não existe.\r\n";

            string cmdRecuperar = $"SELECT * FROM veiculo WHERE IdVeiculo={idVeiculo};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count != 0)
            {
                DataRow linha = tabela.Rows[0];
                idVeiculo = Convert.ToInt32(linha["IdVeiculo"]);
                placa = linha["Placa"].ToString();
                ano = Convert.ToInt32(linha["Ano"]);
                aModelo = new Modelo(Convert.ToInt32(linha["IdModelo"]));
                retorno = "";
            }

            return retorno;
        }

        public static List<Veiculo> Recuperar(string pCondicao)
        {
            List<Veiculo> lista = null;

            string retorno = "";
            string cmdRecuperar = $"SELECT * FROM veiculo WHERE {pCondicao};";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdRecuperar, out retorno);

            if (retorno.Length != 0)
                return lista;

            lista = new List<Veiculo>();

            if (tabela.Rows.Count != 0)
            {
                foreach (DataRow linha in tabela.Rows)
                {
                    lista.Add(new Veiculo(Convert.ToInt32(linha["IdVeiculo"])));
                }
            }

            return lista;
        }
        #endregion

        private bool ValidarIdExistente()
        {
            string retorno;
            string cmdSelectId = $"SELECT idVeiculo FROM veiculo WHERE idVeiculo={IdVeiculo}";
            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdSelectId, out retorno);

            return tabela.Rows.Count != 0;
        }

        private string ValidarModelo()
        {
            string retorno = "";
            string auxRetorno = "";
            string cmdSelectModelo = $"SELECT idModelo FROM modelo WHERE idModelo={aModelo.IdModelo};";

            DataTable tabela = ConexaoBD.GetInstance().ExecutarQuery(cmdSelectModelo, out retorno);

            if (retorno.Length != 0)
                return retorno;

            if (tabela.Rows.Count == 0)
                auxRetorno += "Id de modelo não cadastrado!\r\n";

            return auxRetorno;
        }

        private string Validar()
        {
            {
                string retorno = "";
                retorno += Utils.ValidarId(idVeiculo, "veiculo");

                if (placa.Length == 0)
                    retorno += "Placa não pode ser vazia.\r\n";
                if (placa.Length > 45)
                    retorno += "Placa maior que o limite de 45 caracteres.\r\n";

                if (ano < 0)
                    retorno += "ano não pode ser negativo.\r\n";

                return retorno += ValidarModelo();
            }
        }
    }
}