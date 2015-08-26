using RealexPayments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RealVault_payment_management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void lbNewCard_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("123123wdfsdf", "GBP", 00);
        CreditCard card = new CreditCard("MC", "5425232820001308", "0118", "Phil McCracken", "123", 1);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "First", "Second", "", address, numbers, "", new ArrayList());

        string timestamp = Common.GenerateTimestamp();

        string cardRef = "card1";

        RealVaultTransactionResponse resp = RealVault.CardNew(timestamp, cardRef, merchant, order, card, payer);

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }

    protected void lbUpdateCard_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("123123wdfsdf", "GBP", 00);
        CreditCard card = new CreditCard("MC", "5425232820001308", "0118", "Phil McCrackenEdit", "123", 1);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "First", "Second", "", address, numbers, "", new ArrayList());

        string cardRef = "card1";

        string timestamp = Common.GenerateTimestamp();

        RealVaultTransactionResponse resp = RealVault.CardUpdateCard(timestamp, cardRef, merchant, card, payer);

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }

    protected void lbCancelCard_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "First", "Second", "", address, numbers, "", new ArrayList());

        string cardRef = "card1";

        string timestamp = Common.GenerateTimestamp();

        RealVaultTransactionResponse resp = RealVault.CardCancelCard(timestamp, cardRef, merchant, payer);

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }
}