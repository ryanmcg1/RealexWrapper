using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class RealAuthTransactionResponse
    {
        #region Properties

        private int m_resultCode;
        private String m_resultMessage;
        private String m_resultAuthCode;
        private String m_resultPASRef;
        private String m_resultOrderID;
        private int m_resultSuitabilityScore;
        private Dictionary<int, int> m_resultSuitabilityScoreCheck;

        private string m_pareq;
        private string m_url;
        private string m_enrolled;

        private string m_3dStatus;
        private string m_eci;
        private string m_xid;
        private string m_cavv;
        private string m_algorithm;

        public int ResultCode
        {
            get
            {
                return (m_resultCode);
            }
        }

        public String ResultMessage
        {
            get
            {
                return (m_resultMessage);
            }
        }

        public String ResultAuthCode
        {
            get
            {
                return (m_resultAuthCode);
            }
        }

        public String ResultPASRef
        {
            get
            {
                return (m_resultPASRef);
            }
        }

        public String ResultOrderID
        {
            get
            {
                return (m_resultOrderID);
            }
        }

        public int ResultSuitabilityScore
        {
            get
            {
                return (m_resultSuitabilityScore);
            }
        }

        public int ResultSuitabilityScoreCheck(int checkID)
        {
            return (m_resultSuitabilityScoreCheck[checkID]);
        }

        public String PaReq
        {
            get
            {
                return (m_pareq);
            }
        }

        public String URL
        {
            get
            {
                return (m_url);
            }
        }

        public String Enrolled
        {
            get
            {
                return (m_enrolled);
            }
        }

        public String Status
        {
            get
            {
                return (m_3dStatus);
            }
        }

        public String ECI
        {
            get
            {
                return (m_eci);
            }
        }


        public String XID
        {
            get
            {
                return (m_xid);
            }
        }

        public String CAVV
        {
            get
            {
                return (m_cavv);
            }
        }

        public String Algorithm
        {
            get
            {
                return (m_algorithm);
            }
        }


        #endregion

        /// <summary>
        /// Generate response from real auth request
        /// </summary>
        /// <param name="responseXML">xml to generate response from</param>
        public RealAuthTransactionResponse(String responseXML)
        {

            m_resultSuitabilityScoreCheck = new Dictionary<int, int>();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(responseXML);

            try
            {

                // these *must* exist
                m_resultCode = Convert.ToInt32(xml.GetElementsByTagName("result")[0].InnerText);
                m_resultMessage = xml.GetElementsByTagName("message")[0].InnerText;

                // these should exist, but don't throw exceptions if they don't.
                XmlNode el;
                el = xml.GetElementsByTagName("pasref")[0];
                m_resultPASRef = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("authcode")[0];
                m_resultAuthCode = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("orderid")[0];
                m_resultOrderID = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("tss")[0];
                if (el != null)
                {
                    foreach (XmlNode node in el.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case ("result"):
                                m_resultSuitabilityScore = Convert.ToInt32(node.InnerText);
                                break;
                            case ("check"):
                                foreach (XmlAttribute attr in node.Attributes)
                                {
                                    if (attr.Name == "id")
                                    {
                                        m_resultSuitabilityScoreCheck.Add(Convert.ToInt32(attr.InnerText), Convert.ToInt32(node.InnerText));
                                    }
                                }
                                break;
                        }
                    }
                }

                //3D Secure
                el = xml.GetElementsByTagName("pareq")[0];
                m_pareq = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("url")[0];
                m_url = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("enrolled")[0];
                m_enrolled = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("xid")[0];
                m_xid = (el != null) ? el.InnerText : "";

                el = xml.GetElementsByTagName("threedsecure")[0];
                if (el != null)
                {
                    foreach (XmlNode node in el.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case ("status"):
                                m_3dStatus = (node.InnerText != null) ? node.InnerText : "";
                                break;
                            case ("eci"):
                                m_eci = (node.InnerText != null) ? node.InnerText : "";
                                break;
                            case ("xid"):
                                m_xid = (node.InnerText != null) ? node.InnerText : "";
                                break;
                            case ("cavv"):
                                m_cavv = (node.InnerText != null) ? node.InnerText : "";
                                break;
                            case ("algorithm"):
                                m_algorithm = (node.InnerText != null) ? node.InnerText : "";
                                break;

                        }
                    }
                }

            }
            catch (NullReferenceException e)
            {
                throw new TransactionFailedException("Error parsing XML response: mandatory fields not present. " + e.Message);
            }
        }
    }
}
