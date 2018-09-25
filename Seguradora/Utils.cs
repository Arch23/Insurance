using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Seguradora
{
    public class Utils
    {
        private static readonly string NEWLINE_CODE = "@NL";

        public static string ValidarTextBox(TextBox textBox, string nome)
        {
            string retorno = "";
            if (textBox.Text.Trim().Length == 0)
            {
                retorno = $"{nome} não pode ser vazio.\r\n";
            }
            return retorno;
        }

        public static void Alert(HttpResponse Response, string mensagem)
        {
            Response.Write($"<script>alert('{mensagem.Replace("\r\n","")}')</script>");
        }

        public static void Erro(HttpResponse Response, string header, string body, string back)
        {
            string url = $"Erro.aspx?header={EncodeNewLineUrl(header)}&body={EncodeNewLineUrl(body)}&back={back}";
            Response.Redirect(url);
        }

        public static string EncodeNewLineUrl(string pMensagem)
        {
            string retorno = pMensagem.Replace("\r\n",NEWLINE_CODE);
            retorno = retorno.Replace("<br/>", NEWLINE_CODE);
            return retorno;
        }

        public static string DecodeNewLineUrlToCode(string pMensagem)
        {
            return pMensagem.Replace(NEWLINE_CODE, "\r\n");
        }

        public static string DecodeNewLineUrlToHTML(string pMensagem)
        {
            return pMensagem.Replace(NEWLINE_CODE, "<br/>");
        }

        public static string ValidarId(int pId, string pNome)
        {
            string retorno = "";
            if (pId < 0)
            {
                retorno += $"id de {pNome} inválido.\r\n";
            }
            return retorno;
        }
    }
}