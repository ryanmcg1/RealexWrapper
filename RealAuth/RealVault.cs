using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class RealVault
    {
        //Payer Management
        #region Payer-New

        /// <summary>
        /// Create new payer
        /// </summary>
        /// <param name="timestamp">timestamp</param>
        /// <param name="merchant">merchant details</param>
        /// <param name="order">order details</param>
        /// <param name="payer">payer details</param>
        /// <param name="comments">comment details - can use new ArrayList()</param>
        /// <returns>transaction response</returns>
        public static RealVaultTransactionResponse PayerNew(String timestamp, Merchant merchant, Order order, Payer payer, ArrayList comments)
        {
            string hashInput =
                timestamp + "." +
                merchant.MerchantId + "." +
                order.OrderId + "." +
                "" + "." + //documentation says to put order amount in here... its only wants blank
                "" + "." + //documentation says to put order currency in here... its only wants blank
                payer.PayerRef;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "payer-new";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);
                xml.WriteElementString("orderid", order.OrderId);

                //payer details
                payer.WriteXml(xml);

                xml.WriteElementString("sha1hash", SHA1Hash);

                xml.WriteStartElement("comments");
                {
                    int i = 1;
                    foreach (string s in comments)
                    {
                        xml.WriteStartElement("comment");
                        xml.WriteAttributeString("id", i.ToString());
                        xml.WriteString(s);
                        xml.WriteEndElement();
                        i++;
                    }
                }
                xml.WriteEndElement();

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);

        }

        #endregion

        #region Payer-Edit

        /// <summary>
        /// Edit existing payer
        /// </summary>
        /// <param name="timestamp">timestamp</param>
        /// <param name="merchant">merchant details</param>
        /// <param name="order">order details</param>
        /// <param name="payer">payer details</param>
        /// <param name="comments">comment details. can user new ArrayList()</param>
        /// <returns></returns>
        public static RealVaultTransactionResponse PayerEdit(String timestamp, Merchant merchant, Order order, Payer payer, ArrayList comments)
        {
            string hashInput =
                timestamp + "." +
                merchant.MerchantId + "." +
                order.OrderId + "." +
                "" + "." +
                "" + "." +
                payer.PayerRef;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "payer-edit";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);
                xml.WriteElementString("orderid", order.OrderId);

                //payer details
                payer.WriteXml(xml);

                xml.WriteElementString("sha1hash", SHA1Hash);

                xml.WriteStartElement("comments");
                {
                    int i = 1;
                    foreach (string s in comments)
                    {
                        xml.WriteStartElement("comment");
                        xml.WriteAttributeString("id", i.ToString());
                        xml.WriteString(s);
                        xml.WriteEndElement();
                        i++;
                    }
                }
                xml.WriteEndElement();

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);
        }

        #endregion


        //Card Management
        #region Card-New

        /// <summary>
        /// Create new card
        /// </summary>
        /// <param name="timestamp">timestamp</param>
        /// <param name="cardRef">name to save card against payer</param>
        /// <param name="merchant">merchant details</param>
        /// <param name="order">order details</param>
        /// <param name="cc">credit card details</param>
        /// <param name="payer">payer details</param>
        /// <returns>real vault transaction response</returns>
        public static RealVaultTransactionResponse CardNew(string timestamp, string cardRef, Merchant merchant, Order order, CreditCard cc, Payer payer)
        {
            string hashInput = timestamp + "." +
                merchant.MerchantId + "." +
                order.OrderId + "." +
                "" + "." +
                "" + "." +
                payer.PayerRef + "." +
                cc.CardholderName + "." +
                cc.CardNumber;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "card-new";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);
                xml.WriteElementString("orderid", order.OrderId);

                xml.WriteStartElement("card");
                {
                    xml.WriteElementString("ref", cardRef);
                    xml.WriteElementString("payerref", payer.PayerRef);
                    xml.WriteElementString("number", cc.CardNumber);
                    xml.WriteElementString("expdate", cc.ExpiryDate);
                    xml.WriteElementString("type", cc.CardType);
                    xml.WriteElementString("chname", cc.CardholderName);
                    if (cc.CardType.Equals("SWITCH"))
                    {
                        xml.WriteElementString("issueno", cc.IssueNumber.ToString());
                    }
                }
                xml.WriteEndElement();

                xml.WriteElementString("sha1hash", SHA1Hash);

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);

        }

        #endregion

        #region Card-Update-Card

        /// <summary>
        /// Update existing card
        /// </summary>
        /// <param name="timestamp">timestamp</param>
        /// <param name="cardRef">card ref to be updated</param>
        /// <param name="merchant">merchant details</param>
        /// <param name="card">card details</param>
        /// <param name="payer">payer details</param>
        /// <returns>real vault transaction response</returns>
        public static RealVaultTransactionResponse CardUpdateCard(string timestamp, string cardRef, Merchant merchant, CreditCard card, Payer payer)
        {
            string hashInput = timestamp + "." +
                merchant.MerchantId + "." +
                payer.PayerRef + "." +
                cardRef + "." +
                card.ExpiryDate + "." +
                card.CardNumber;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "card-update-card";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);

                xml.WriteStartElement("card");
                {
                    xml.WriteElementString("ref", cardRef);
                    xml.WriteElementString("payerref", payer.PayerRef);
                    xml.WriteElementString("number", card.CardNumber);
                    xml.WriteElementString("expdate", card.ExpiryDate);
                    xml.WriteElementString("chname", card.CardholderName);
                    xml.WriteElementString("type", card.CardType);
                    if (card.CardType.Equals("SWITCH"))
                    {
                        xml.WriteElementString("issueno", card.IssueNumber.ToString());
                    }
                }
                xml.WriteEndElement();

                xml.WriteElementString("sha1hash", SHA1Hash);

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);

        }

        #endregion

        #region Card-Cancel-Card

        /// <summary>
        /// Cancel existing card
        /// </summary>
        /// <param name="timestamp">timestamp</param>
        /// <param name="cardRef">card ref to be cancelled</param>
        /// <param name="merchant">merchant details</param>
        /// <param name="payer">payer details</param>
        /// <returns>real vault transaction response</returns>
        public static RealVaultTransactionResponse CardCancelCard(string timestamp, string cardRef, Merchant merchant, Payer payer)
        {
            string hashInput = timestamp + "." +
                merchant.MerchantId + "." +
                payer.PayerRef + "." +
                cardRef;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "card-cancel-card";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);

                xml.WriteStartElement("card");
                {
                    xml.WriteElementString("ref", cardRef);
                    xml.WriteElementString("payerref", payer.PayerRef);
                }
                xml.WriteEndElement();

                xml.WriteElementString("sha1hash", SHA1Hash);

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);

        }

        #endregion


        //Processing Payments
        #region Receipt-In

        /// <summary>
        /// Take payment from real vault
        /// </summary>
        /// <param name="merchant">merchant details</param>
        /// <param name="order">order details</param>
        /// <param name="payer">payer details</param>
        /// <param name="cardRef">card ref to charge</param>
        /// <param name="autoSettle">auto settle with bank (usually 1)</param>
        /// <param name="timestamp">timestamp</param>
        /// <param name="comments">comments</param>
        /// <returns></returns>
        public static RealVaultTransactionResponse RecieptIn(Merchant merchant, Order order, Payer payer, string cardRef, string autoSettle, string timestamp, ArrayList comments)
        {
            _3DSecure tdSecure = new _3DSecure();
            bool recurring = false; 
            string recurringType = "";
            string recurringSequence = "";
            string cvn = "";

            return RecieptIn(merchant, tdSecure, order, payer, cardRef, cvn, autoSettle, timestamp, comments, recurring, recurringType, recurringSequence);
        }

        /// <summary>
        /// Take payment from real vault - recurring
        /// </summary>
        /// <param name="merchant">merchant details</param>
        /// <param name="order">order details</param>
        /// <param name="payer">payer details</param>
        /// <param name="cardRef">card ref to chanrge</param>
        /// <param name="autoSettle">auto settle with bank (usually 1)</param>
        /// <param name="timestamp">timestamp</param>
        /// <param name="comments">comments</param>
        /// <param name="recurring">is recurring?</param>
        /// <param name="recurringType">type of recurring amount - fixed / variable</param>
        /// <param name="recurringSequence">first/subsequent/final</param>
        /// <returns></returns>
        public static RealVaultTransactionResponse RecieptIn(Merchant merchant, Order order, Payer payer, string cardRef, string autoSettle, string timestamp, ArrayList comments, bool recurring, string recurringType, string recurringSequence)
        {
            _3DSecure tdSecure = new _3DSecure();
            string cvn = "";

            return RecieptIn(merchant, tdSecure, order, payer, cardRef, cvn, autoSettle, timestamp, comments, recurring, recurringType, recurringSequence);
        }

        /// <summary>
        /// Take payment from real vault - everything
        /// </summary>
        /// <param name="merchant">merchant details</param>
        /// <param name="tdSecure">3d secure details</param>
        /// <param name="order">order details</param>
        /// <param name="payer">payer details</param>
        /// <param name="cardRef">card ref to charge</param>
        /// <param name="cvn">cvn of card</param>
        /// <param name="autoSettle">auto settle with bank (usullay 1)</param>
        /// <param name="timestamp">timestamp</param>
        /// <param name="comments">comments</param>
        /// <param name="recurring">is recurring?</param>
        /// <param name="recurringType">type of recurring amount - fixed / variable</param>
        /// <param name="recurringSequence">first/subsequent/final</param>
        /// <returns></returns>
        public static RealVaultTransactionResponse RecieptIn(Merchant merchant, _3DSecure tdSecure, Order order, Payer payer, string cardRef, string cvn, string autoSettle, string timestamp, ArrayList comments, bool recurring, string recurringType, string recurringSequence)
        {
            string hashInput = timestamp + "." +
                        merchant.MerchantId + "." +
                        order.OrderId + "." +
                        order.OrderAmount + "." +
                        order.OrderCurrency + "." +
                        payer.PayerRef;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "receipt-in";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);
                xml.WriteElementString("account", merchant.Account);
                xml.WriteElementString("orderid", order.OrderId);

                if (cvn != "")
                {
                    xml.WriteStartElement("paymentdata");
                    {
                        xml.WriteStartElement("cvn");
                        {
                            xml.WriteElementString("number", cvn);
                        }
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                    
                }

                xml.WriteStartElement("mpi");
                {
                    xml.WriteElementString("cavv", tdSecure.CAVV);
                    xml.WriteElementString("xid", tdSecure.XID);
                    xml.WriteElementString("eci", tdSecure.ECI);
                }
                xml.WriteEndElement();

                //might need dccinfo in here??

                xml.WriteStartElement("amount");
                {
                    xml.WriteAttributeString("currency", order.OrderCurrency);
                    xml.WriteString(order.OrderAmount.ToString());
                }
                xml.WriteEndElement();

                xml.WriteElementString("payerref", payer.PayerRef);
                xml.WriteElementString("paymentmethod", cardRef);

                xml.WriteStartElement("autosettle");
                {
                    xml.WriteAttributeString("flag", autoSettle);
                }
                xml.WriteEndElement();

                xml.WriteElementString("sha1hash", SHA1Hash);

                xml.WriteStartElement("comments");
                {
                    int i = 1;
                    foreach (string s in comments)
                    {
                        xml.WriteStartElement("comment");
                        xml.WriteAttributeString("id", i.ToString());
                        xml.WriteString(s);
                        xml.WriteEndElement();
                        i++;
                    }
                }
                xml.WriteEndElement();

                if (recurring)
                {
                    xml.WriteStartElement("recurring");
                    {
                        xml.WriteAttributeString("type", recurringType);
                        xml.WriteAttributeString("sequence", recurringSequence);
                    }
                    xml.WriteEndElement();
                }

                //tss??

                //supplementary data??

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);

        }

        #endregion


        //3D Secure
        #region RealVault-3DS-VerifyEnrolled

        /// <summary>
        /// Verify card stored is 3d secured
        /// </summary>
        /// <param name="merchant">merchant details</param>
        /// <param name="order">order details</param>
        /// <param name="payer">payer details</param>
        /// <param name="cardRef">card ref to verify</param>
        /// <param name="autoSettle">auto settle with bank</param>
        /// <param name="timestamp">timestamp</param>
        /// <param name="comments">comments</param>
        /// <returns></returns>
        public static RealVaultTransactionResponse RealVault3DSVerifyEnrolled(Merchant merchant, Order order, Payer payer, string cardRef, string autoSettle, string timestamp, ArrayList comments)
        {
            string hashInput = timestamp + "." +
                        merchant.MerchantId + "." +
                        order.OrderId + "." +
                        order.OrderAmount + "." +
                        order.OrderCurrency + "." +
                        payer.PayerRef;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "realvault-3ds-verifyenrolled";

            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;
            xmlSettings.NewLineOnAttributes = false;
            xmlSettings.NewLineChars = "\r\n";
            xmlSettings.CloseOutput = true;

            StringBuilder strBuilder = new StringBuilder();

            XmlWriter xml = XmlWriter.Create(strBuilder, xmlSettings);

            xml.WriteStartDocument();

            xml.WriteStartElement("request");
            {
                xml.WriteAttributeString("type", requestType);
                xml.WriteAttributeString("timestamp", timestamp);

                xml.WriteElementString("merchantid", merchant.MerchantId);
                xml.WriteElementString("account", merchant.Account);

                xml.WriteStartElement("amount");
                {
                    xml.WriteAttributeString("currency", order.OrderCurrency);
                    xml.WriteString(order.OrderAmount.ToString());
                }
                xml.WriteEndElement();

                xml.WriteElementString("payerref", payer.PayerRef);
                xml.WriteElementString("paymentmethod", cardRef);

                xml.WriteStartElement("autosettle");
                {
                    xml.WriteAttributeString("flag", autoSettle);
                }
                xml.WriteEndElement();

                xml.WriteElementString("sha1hash", SHA1Hash);

                xml.WriteStartElement("comments");
                {
                    int i = 1;
                    foreach (string s in comments)
                    {
                        xml.WriteStartElement("comment");
                        xml.WriteAttributeString("id", i.ToString());
                        xml.WriteString(s);
                        xml.WriteEndElement();
                        i++;
                    }
                }
                xml.WriteEndElement();

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealVaultRequest(xmlString);
        }

        #endregion

    }
}
