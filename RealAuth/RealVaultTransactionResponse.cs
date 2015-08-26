using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class RealVaultTransactionResponse
    {
        #region Properties

        public string MerchantId { get; set; }
        public string Account { get; set; }
        public string OrderId { get; set; }
        public Int32 ResultCode { get; set; }
        public string Message { get; set; }
        public string PasRef { get; set; }
        public string AuthCode { get; set; }
        public string BatchId { get; set; }
        public string TimeTaken { get; set; }
        public string ProcessingTimeTaken { get; set; }
        public string MD5Hash { get; set; }
        public string SHA1Hash { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a transaction responce from xml returned from realex
        /// </summary>
        /// <param name="responseXML">xml from realex as string</param>
        public RealVaultTransactionResponse(String responseXML)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXML);

                //for future reference ignore everything the realex documentation says its a pile of incorrect shit, mandatory fields my ass
                //MerchantId = xml.GetElementsByTagName("merchantid")[0].InnerText;
                //Account = xml.GetElementsByTagName("account")[0].InnerText;
                ResultCode = Convert.ToInt32(xml.GetElementsByTagName("result")[0].InnerText);
                Message = xml.GetElementsByTagName("message")[0].InnerText;

                XmlNode el;
                el = xml.GetElementsByTagName("merchantid")[0];
                MerchantId = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("account")[0];
                Account = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("orderid")[0];
                OrderId = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("pasref")[0];
                PasRef = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("authcode")[0];
                AuthCode = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("batchid")[0];
                BatchId = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("timetaken")[0];
                TimeTaken = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("processingtimetaken")[0];
                ProcessingTimeTaken = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("md5hash")[0];
                MD5Hash = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("sha1hash")[0];
                SHA1Hash = (el != null) ? el.InnerText : "";

            }
            catch (NullReferenceException e)
            {
                throw new TransactionFailedException("Error parsing XML response: mandatory fields not present. " + e.Message);
            }

        }

        #endregion

        #region Methods



        #endregion

    }
}
