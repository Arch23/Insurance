using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seguradora.Pages
{
    public partial class PaginaErro : System.Web.UI.Page
    {
        private string backUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblHeader.Text = Utils.DecodeNewLineUrlToHTML(Request.QueryString["header"].ToString());
            lblBody.Text = Utils.DecodeNewLineUrlToHTML(Request.QueryString["body"].ToString());
            backUrl = Request.QueryString["back"].ToString();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(backUrl);
        }
    }
}