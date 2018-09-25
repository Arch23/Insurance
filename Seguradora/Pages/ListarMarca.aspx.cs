using Seguradora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class ListarMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TableHeaderRow.TableSection = TableRowSection.TableHeader;
            PreencherTabela();
        }

        private void PreencherTabela()
        {
            List<Marca> lista = Marca.Recuperar("1=1");

            foreach(Marca auxMarca in lista)
            {
                TableRow linha = new TableRow();
                Table.Rows.Add(linha);
                TableCell celula = new TableCell
                {
                    Text = auxMarca.IdMarca.ToString()
                };
                linha.Cells.Add(celula);
                celula = new TableCell
                {
                    Text = auxMarca.Descricao
                };
                linha.Cells.Add(celula);

                //celula = new TableCell();
                Button novoBotao = new Button
                {
                    Text = "Editar"
                };
                //linha.Cells.Add(celula);
                linha.Controls.Add(novoBotao);
                celula = new TableCell();
                linha.Cells.Add(celula);
            }
        }
    }
}