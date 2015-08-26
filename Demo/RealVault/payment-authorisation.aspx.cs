using RealexPayments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RealVault_payment_authorisation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    //this doesnt work and I dont need it so tough
    protected void lb3DSecureReciept_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("transaction01", "EUR", 9999);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "Phil", "McCracken", "", address, numbers, "", new ArrayList());

        string timestamp = Common.GenerateTimestamp();

        string cardRef = "card1";
        string cvn = "123";
        string autoSettle = "1";

        RealVaultTransactionResponse resp = RealVault.RealVault3DSVerifyEnrolled(merchant, order, payer, cardRef, autoSettle, timestamp, new ArrayList());
        //TODO: Run RealAuth verify signed
        //TODO: Run Reciept-In with 3d secure details

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }

    protected void lblbRecieptIn_Click(object sender, EventArgs e)
    {
        string merchantId = ConfigurationManager.AppSettings["MerchantID"];
        string account = ConfigurationManager.AppSettings["Account"];
        string sharedSecret = ConfigurationManager.AppSettings["SharedSecret"];

        Merchant merchant = new Merchant(merchantId, account, sharedSecret);
        Order order = new Order("GBP", 9999);
        Address address = new Address("", "", "", "", "", "", "", "");
        PhoneNumbers numbers = new PhoneNumbers("", "", "", "");
        Payer payer = new Payer("Business", "test", "", "Phil", "McCracken", "", address, numbers, "", new ArrayList());

        string timestamp = Common.GenerateTimestamp();

        //only needed if 
        //string cvn = "123";
        string cardRef = "card1";
        string autoSettle = "1";

        //not needed if not a recurring payment, use reciept in overload without recurring
        bool recurring = true; 

        //fixed or variable
        //fixed - order amount is the same every transaction
        //variable - order amount is different in every transaction
        string recurringType = "variable";

        //first - first payment in a sequence
        //subsequent - any subsequent payments after the initial
        //final - no more payments in sequence will follow
        string recurringSequence = "first";
        
        RealVaultTransactionResponse resp = RealVault.RecieptIn(merchant, order, payer, cardRef, autoSettle, timestamp, new ArrayList(), recurring, recurringType, recurringSequence);

        lblErrorCode.Text = resp.ResultCode.ToString();
        lblResult.Text = resp.Message;
    }
}