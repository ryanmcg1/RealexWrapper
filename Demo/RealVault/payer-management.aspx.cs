using RealexPayments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RealVault_payer_management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void lbNewPayer_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        //You need to generate the following to complete any process, the following order might be helpful as well.
        //Timestamp ==> Merchant ==> Order ==> Address ==> PhoneNumbers ==> Payer ==> CreditCard ==> SHA1Hash

        //Current timestamp
        string timestamp = Common.GenerateTimestamp();

        //New Merchant
        //merchant id
        //account
        //shared secret
        Merchant merchant = new Merchant(merchantId, account, sharedSecret);

        //New Order
        //order id (optional)
        //currency code
        //order amount
        Order order = new Order("123123wdfsdf", "GBP", 1099);

        //New Address (optional)
        //Line 1 (optional)
        //Line 2 (optional)
        //Line 3 (optional)
        //City (optional)
        //County (optional)
        //Postcode (optional)
        //Country Code (optional)
        //Country Name (optional)
        Address address = new Address("", "", "", "", "", "", "", "");

        //New Phone Numbers (optional)
        //home (optional)
        //work (optional)
        //fax (optional)
        //mobile (optional)
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");

        //New Payer
        //Payer type (default to 'Business')
        //Payer ref
        //Title (optional)
        //First Name
        //Surname
        //Company (optional)
        //Address (optional)
        //Numbers (optional)
        //email (optional)
        //comments (optional)
        Payer payer = new Payer("Business", "test", "", "First", "Second", "", address, numbers, "", new ArrayList());

        //New Credit Card
        //Card type
        //Card number
        //Expiry date
        //Cardholder name
        //cvn
        //Is cvn present
        //Issue number (optional)
        CreditCard card = new CreditCard("MC", "5425232820001308", "0118", "Phil McCracken", "123", 1);

        //Create new payer
        //timestamp
        //SHA1Hash
        //merchant
        //order
        //payer
        //comments
        RealVaultTransactionResponse resp = RealVault.PayerNew(timestamp, merchant, order, payer, new ArrayList());

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }

    protected void lbEditPayer_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        string timestamp = Common.GenerateTimestamp();

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("123123wdfsdf", "GBP", 00);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "Phil", "McCracken", "", address, numbers, "", new ArrayList());
        CreditCard card = new CreditCard("MC", "5425232820001308", "0118", "Phil McCracken", "123", 1);

        RealVaultTransactionResponse resp = RealVault.PayerEdit(timestamp, merchant, order, payer, new ArrayList());

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }
}