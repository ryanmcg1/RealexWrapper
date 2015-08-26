using RealexPayments;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _3DSecure_ACSRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Order order = (Order)Session["order"];
        CreditCard card = (CreditCard)Session["cc"];
        _3DSecure tdsec = (_3DSecure)Session["tds"];


        NameValueCollection col = new NameValueCollection();
        col.Add("PaReq", "123234");
        col.Add("TermUrl", "www.google.com");
        col.Add("MD", "asdasd");

        PostToUrl("http://cots-vm-cs.cloudapp.net", col);
    }

    private void PostToUrl(string URL, NameValueCollection col)
    {
        try
        {

            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", URL);
            foreach (string a in col)
            {
                sb.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", a, col[a]);
            }
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();

        }
        catch (Exception ex)
        {
            return;
        }
    }
}