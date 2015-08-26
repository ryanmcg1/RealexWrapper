using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class RealAuthorisation
    {

        //Processing payment
        #region auth

        /// <summary>
        /// Authorise payment
        /// </summary>
        /// <param name="merchant">Merchant Details</param>
        /// <param name="order">Order Details</param>
        /// <param name="card">Card Details</param>
        /// <param name="autoSettle">Auto Settle (1 or 0)</param>
        /// <param name="timestamp">Timestamp</param>
        /// <returns>Response from realex</returns>
        public static RealAuthTransactionResponse Auth(Merchant merchant, Order order, CreditCard card, string autoSettle, string timestamp)
        {
            bool is3DSecure = false;
            _3DSecure tdSec = null;
            ArrayList comments = new ArrayList();

            return Auth(merchant, order, card, autoSettle, timestamp, comments, is3DSecure, tdSec);
        }

        /// <summary>
        /// Authorise payment
        /// </summary>
        /// <param name="merchant">Merchant Details</param>
        /// <param name="order">Order Details</param>
        /// <param name="card">Card Details</param>
        /// <param name="autoSettle">Auto Settle (1 or 0)</param>
        /// <param name="timestamp">Timestamp</param>
        /// <param name="comments">Comments</param>
        /// <returns>Response from realex</returns>
        public static RealAuthTransactionResponse Auth(Merchant merchant, Order order, CreditCard card, string autoSettle, string timestamp, ArrayList comments)
        {
            bool is3DSecure = false;
            _3DSecure tdSec = null;

            return Auth(merchant, order, card, autoSettle, timestamp, comments, is3DSecure, tdSec);
        }

        /// <summary>
        /// Authorise payment
        /// </summary>
        /// <param name="merchant">Merchant Details</param>
        /// <param name="order">Order Details</param>
        /// <param name="card">Card Details</param>
        /// <param name="autoSettle">Auto Settle (1 or 0)</param>
        /// <param name="timestamp">Timestamp</param>
        /// <param name="comments">Comments</param>
        /// <returns>Response from realex</returns>
        public static RealAuthTransactionResponse Auth(Merchant merchant, Order order, CreditCard card, string autoSettle, string timestamp, bool is3DSecure, _3DSecure tdSec)
        {
            ArrayList comments = new ArrayList();

            return Auth(merchant, order, card, autoSettle, timestamp, comments, is3DSecure, tdSec);
        }

        /// <summary>
        /// Authorise payment
        /// </summary>
        /// <param name="merchant">Merchant Details</param>
        /// <param name="order">Order Details</param>
        /// <param name="card">Card Details</param>
        /// <param name="autoSettle">Auto Settle (1 or 0)</param>
        /// <param name="timestamp">Timestamp</param>
        /// <param name="comments">Comments</param>
        /// <param name="is3DSecure">Is transaction 3D Secure</param>
        /// <param name="tdSecure">3D Secure details</param>
        /// <returns>Response from Realex</returns>
        public static RealAuthTransactionResponse Auth(Merchant merchant, Order order, CreditCard card, string autoSettle, string timestamp, ArrayList comments, bool is3DSecure, _3DSecure tdSecure)
        {
            string hashInput = timestamp + "." +
                merchant.MerchantId + "." +
                order.OrderId + "." +
                order.OrderAmount + "." +
                order.OrderCurrency + "." +
                card.CardNumber;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "auth";

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

                xml.WriteStartElement("amount");
                {
                    xml.WriteAttributeString("currency", order.OrderCurrency);
                    xml.WriteString(order.OrderAmount.ToString());
                }
                xml.WriteEndElement();

                card.WriteXML(xml);

                xml.WriteStartElement("autosettle");
                {
                    xml.WriteAttributeString("flag", autoSettle);
                }
                xml.WriteEndElement();

                if (is3DSecure)
                {
                    xml.WriteStartElement("mpi");
                    {
                        xml.WriteElementString("cavv", tdSecure.CAVV);
                        xml.WriteElementString("xid", tdSecure.XID);
                        xml.WriteElementString("eci", tdSecure.ECI);
                    }
                    xml.WriteEndElement();
                }

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

            return Common.SendRealAuthRequest(xmlString);
        }

        #endregion


        //3D Secure
        #region 3ds-verifyenrolled
        public static RealAuthTransactionResponse RealAuth3DSecureVerifyEnrolled(Merchant merchant, Order order, CreditCard card, string timestamp)
        {
            string hashInput = timestamp + "." +
                merchant.MerchantId + "." +
                order.OrderId + "." +
                order.OrderAmount + "." +
                order.OrderCurrency + "." +
                card.CardNumber;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "3ds-verifyenrolled";

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

                xml.WriteStartElement("amount");
                {
                    xml.WriteAttributeString("currency", order.OrderCurrency);
                    xml.WriteString(order.OrderAmount.ToString());
                }
                xml.WriteEndElement();

                xml.WriteStartElement("card");
                {
                    xml.WriteElementString("number", card.CardNumber);
                    xml.WriteElementString("expdate", card.ExpiryDate);
                    xml.WriteElementString("type", card.CardType);
                    xml.WriteElementString("chname", card.CardholderName);
                }
                xml.WriteEndElement();

                xml.WriteElementString("sha1hash", SHA1Hash);

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealAuthRequest(xmlString);
        }
        #endregion

        #region 3ds-verifysig

        /// <summary>
        /// 3D Secure Verify Digital Signature
        /// </summary>
        /// <param name="merchant">Merchant Details</param>
        /// <param name="order">Order Details</param>
        /// <param name="card">Card Details</param>
        /// <param name="tdSecure">3D Secure Details</param>
        /// <param name="timestamp">Timestamp</param>
        /// <returns>Response from Realex</returns>
        public static RealAuthTransactionResponse RealAuth3DSecureVerifySig(Merchant merchant, Order order, CreditCard card, _3DSecure tdSecure, string timestamp)
        {
            string hashInput = timestamp + "." +
                merchant.MerchantId + "." +
                order.OrderId + "." +
                order.OrderAmount + "." +
                order.OrderCurrency + "." +
                card.CardNumber;
            string SHA1Hash = Common.GenerateSHA1Hash(hashInput, merchant.SharedSecret);

            String xmlString = string.Empty;
            String requestType = "3ds-verifysig";

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

                xml.WriteStartElement("amount");
                {
                    xml.WriteAttributeString("currency", order.OrderCurrency);
                    xml.WriteString(order.OrderAmount.ToString());
                }
                xml.WriteEndElement();

                xml.WriteStartElement("card");
                {
                    xml.WriteElementString("number", card.CardNumber);
                    xml.WriteElementString("expdate", card.ExpiryDate);
                    xml.WriteElementString("type", card.CardType);
                    xml.WriteElementString("chname", card.CardholderName);
                }
                xml.WriteEndElement();

                xml.WriteElementString("pares", tdSecure.PaRes);

                xml.WriteElementString("sha1hash", SHA1Hash);

            }
            xml.WriteEndElement();

            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();

            xmlString = strBuilder.ToString();

            return Common.SendRealAuthRequest(xmlString);
        }

        #endregion

    }
}
