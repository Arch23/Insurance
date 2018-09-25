using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Seguradora.Models
{
    public class ConexaoBD
    {
        private static ConexaoBD INSTANCE;
        private MySqlConnection conexao;

        private ConexaoBD()
        {
            conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ToString());
        }

        public static ConexaoBD GetInstance()
        {
            return INSTANCE ?? (INSTANCE = new ConexaoBD());
        }

        public string ExecutarSql(string pSQL)
        {
            string retorno;
            try
            {
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(pSQL, conexao);

                int linhasProcessadas = comando.ExecuteNonQuery();

                retorno = linhasProcessadas == 0 ? "Nenhuma alteração realizada" : "";
                //retorno = "";
            }
            catch (Exception ex)
            {
                retorno = $"Erro ao executar a operação no banco de dados. {ex.Message}";
            }
            finally
            {
                conexao.Close();
            }

            return retorno;
        }

        public DataTable ExecutarQuery(string pSQL, out string retorno)
        {
            DataTable tabela = null;
            try
            {
                conexao.Open();

                MySqlCommand comando = new MySqlCommand(pSQL, conexao);

                tabela = new DataTable();
                tabela.Load(comando.ExecuteReader());

                retorno = "";
            }
            catch (Exception ex)
            {
                retorno = $"Erro ao executar a query no banco de dados. {ex.Message}";
            }
            finally
            {
                conexao.Close();
            }

            return tabela;
        }
    }
}