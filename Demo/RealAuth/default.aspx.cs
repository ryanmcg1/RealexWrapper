using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Required additional references
using System.Configuration;
using RealexPayments;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.Collections.Specialized;

public partial class RealAuth_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbBasicAuth_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("GBP", 999);
        //working
        CreditCard card = new CreditCard("MC", "5425232820001308", "0118", "Phil McCracken", "123", 1);
        //invalid
        //CreditCard card = new CreditCard("MC", "1234123412341234", "0118", "Phil McCracken", "123", 1);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "Phil", "McCracken", "", address, numbers, "", new ArrayList());

        string timestamp = Common.GenerateTimestamp();

        string autoSettle = "1";

        RealAuthTransactionResponse resp = RealAuthorisation.Auth(merchant, order, card, autoSettle, timestamp);

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.ResultMessage;
    }

    protected void lb3DSecureAuth_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("GBP", 999);
        CreditCard card = new CreditCard("MC", "5425232820001308", "0118", "Phil McCracken", "123", 1);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "Phil", "McCracken", "", address, numbers, "", new ArrayList());

        string timestamp = Common.GenerateTimestamp();

        string autoSettle = "1";

        RealAuthTransactionResponse resp = RealAuthorisation.RealAuth3DSecureVerifyEnrolled(merchant, order, card, timestamp);

        //00 is enrolled
        //110 is not enrolled, should be sent to Attempt ACS server is available
        if (resp.ResultCode == 00 || resp.ResultCode == 110)
        {
            string paReq = resp.PaReq;
            string url = resp.URL;
            
            if(paReq != "" && url != "")
            {
                _3DSecure tdSecure = new _3DSecure("", "", "", paReq, url);
                //resp = RealAuthorisation.RealAuth3DSecureVerifySig(merchant, order, card, tdSecure, timestamp);
            }
            
        }
        
        string termUrlPrefix = Request.ServerVariables["HTTPS"] == "ON" ? "https://" : "http://";
        string termUrl = string.Format("{0}{1}",
        termUrlPrefix,
        Request.Url.Authority + "/3DSResponse.aspx");

        pnlACS.Visible = true;

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.ResultMessage;
    }

}