using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace RealexPayments
{
    public class Common
    {
        /// <summary>
        /// Generate new timestamp
        /// </summary>
        /// <returns>Current time in yyyyMMddHHmmss</returns>
        public static String GenerateTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        #region Hash brownies

        /// <summary>
        /// Generate a new hash for RealVault orders
        /// </summary>
        /// <param name="normalPassword"></param>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="card"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static String GenerateSHA1Hash(string hashInput, string sharedSecret)
        {
            SHA1 sha = new SHA1Managed();

            String hashStage1 =
                hexEncode(sha.ComputeHash(Encoding.UTF8.GetBytes(hashInput))) + "." +
                sharedSecret;

            String hashStage2 =
                hexEncode(sha.ComputeHash(Encoding.UTF8.GetBytes(hashStage1)));

            return hashStage2;

        }

        private static String hexEncode(byte[] data)
        {

            String result = "";
            foreach (byte b in data)
            {
                result += b.ToString("X2");
            }
            result = result.ToLower();

            return (result);
        }

        #endregion

        #region Web Requests

        /// <summary>
        /// Rend RealAuth Transaction to Realex
        /// </summary>
        /// <param name="requestXML">Request XML</param>
        /// <returns>Parsed Response XML</returns>
        public static RealAuthTransactionResponse SendRealAuthRequest(String requestXML)
        {
            string responseXML = SendToRealex(requestXML, "RealAuth");
            RealAuthTransactionResponse realAuthResp = new RealAuthTransactionResponse(responseXML);

            return realAuthResp;
        }

        /// <summary>
        /// Rend RealVault Transaction to Realex
        /// </summary>
        /// <param name="requestXML">Request XML</param>
        /// <returns>Parsed Response XML</returns>
        public static RealVaultTransactionResponse SendRealVaultRequest(String requestXML)
        {
            string responseXML = SendToRealex(requestXML, "RealVault");
            RealVaultTransactionResponse realVaultResp = new RealVaultTransactionResponse(responseXML);

            return realVaultResp;
        }

        /// <summary>
        /// Send xml to realex for processing
        /// </summary>
        /// <param name="requestXML">XML Request to send to Realex</param>
        /// <param name="requestType">RealAuth or RealVault</param>
        /// <returns></returns>
        private static String SendToRealex(String requestXML, String requestType)
        {
            string requestURL = string.Empty;
            requestType = requestType.ToLower();

            switch (requestType)
            {
                case "realauth":
                    requestURL = "https://epage.payandshop.com/epage-remote.cgi";
                    break;
                case "realvault":
                    requestURL = " https://epage.payandshop.com/epage-remote-plugins.cgi";
                    break;
                default:
                    break;
            }


            HttpWebRequest wReq = (HttpWebRequest) WebRequest.Create(requestURL);
			wReq.ContentType = "text/xml";
			wReq.UserAgent = "Etain Realex Payments";
			wReq.Timeout = 45 * 1000;	// milliseconds
			wReq.AllowAutoRedirect = false;
			wReq.ContentLength = requestXML.Length;
			wReq.Method = "POST";

            try
            {
                StreamWriter sReq = new StreamWriter(wReq.GetRequestStream());
                sReq.Write(requestXML);
                sReq.Flush();
                sReq.Close();

                // dump i/o to files for debugging purposes
                //TODO: if you have trouble with your requests, uncomment the line below to save a copy.
                // PLEASE remember to remove the line again before you go live; otherwise you will be keeping
                // your customers' credit card data in cleartext on your server.
                //File.WriteAllText("request.xml", requestXML);

                HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
                StreamReader sResp = new StreamReader(wResp.GetResponseStream());

                String responseXML = sResp.ReadToEnd();
                sResp.Close();

                // dump i/o to files for debugging purposes
                //TODO: if you have trouble with your requests, uncomment the line below to save a copy.
                // PLEASE remember to remove the line again before you go live; otherwise you will be keeping
                // your customers' credit card data in cleartext on your server.
                //File.WriteAllText("response.xml", responseXML);

                return responseXML;
            }
            catch (WebException e)
            {
                throw new TransactionFailedException("Web request failed or timed out: " + e.Message);
            }
        }

        #endregion
    }
}
