using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class CreditCard
    {
        #region Properties

        public const int CVN_PRESENT = 1;
        public const int CVN_ILLEGIBLE = 2;
        public const int CVN_NOT_REQUESTED_BY_MERCHANT = 3;
        public const int CVN_NOT_ON_CARD = 4;

        private String m_cctype;
        private String m_number;
        private int m_issueNumber;	// Switch cards only
        private String m_expiryDate;
        private String m_cardholderName;
        private String m_cvn;
        private int m_cvnPresent;

        public String CardNumber
        {
            get
            {
                return (m_number);
            }

            set
            {
                if ((value.Length >= 12) && (value.Length <= 19))
                {
                    if (Mod10Check(value))
                    {
                        m_number = value;
                    }
                    else
                    {
                        throw new DataValidationException("Card number fails Luhn check.");
                    }
                }
                else
                {
                    throw new DataValidationException("Card number fails Luhn check.");
                }
            }
        }

        public String CardType
        {
            get
            {
                return (m_cctype);
            }

            set
            {
                if (value.Equals(""))
                {
                    throw new DataValidationException("Card type must not be blank.");
                }

                value = value.ToUpper();

                switch (value)
                {
                    case ("VISA"):
                    case ("MC"):
                    case ("AMEX"):
                    case ("LASER"):
                    case ("DINERS"):
                    case ("SWITCH"):
                    case ("SOLO"):
                    case ("JCB"):
                        m_cctype = value;
                        break;
                    default:
                        throw new DataValidationException("Invalid credit card type specified.");
                }
            }
        }

        public int IssueNumber
        {
            get
            {
                return (m_issueNumber);
            }

            set
            {
                if (!m_cctype.Equals("SWITCH"))
                {
                    throw new DataValidationException("Issue numbers are only used by Switch cards.");
                }

                m_issueNumber = value;
            }
        }

        public String ExpiryDate
        {
            get
            {
                return (m_expiryDate);
            }

            set
            {
                if (value.Length != 4)
                {
                    throw new DataValidationException("Card expiry length is incorrect (must be exactly 4 digits).");
                }

                String sMonth = value.Substring(0, 2);
                String sYear = value.Substring(2, 2);
                int iMonth = Int16.Parse(sMonth);
                int iYear = Int16.Parse(sYear);
                int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));

                if (!((iMonth >= 1) && (iMonth <= 12)))
                {
                    throw new DataValidationException("Card expiry month must be between 01 and 12 inclusive.");
                }

                if (iYear < (currentYear - 1))
                {	// refunds can be made to expired cards
                    throw new DataValidationException("Card expiry year is too far into the past.");
                }

                if (iYear > (currentYear + 20))
                {
                    throw new DataValidationException("Card expiry year is too far into the future.");
                }

                m_expiryDate = value;
            }
        }

        public String CardholderName
        {
            get
            {
                return (m_cardholderName);
            }

            set
            {
                if (value.Equals(""))
                {
                    throw new DataValidationException("Cardholder name must not be empty.");
                }

                m_cardholderName = value;
            }
        }

        public String CVN
        {
            get
            {
                return (m_cvn);
            }

            set
            {
                if ((value.Length != 3) && (value.Length != 4))
                {
                    throw new DataValidationException("CVN must be 3 or 4 digits in length.");
                }

                m_cvn = value;
            }
        }

        public int CVNPresent
        {
            get
            {
                return (m_cvnPresent);
            }

            set
            {
                if (!(value >= 1) && (value <= 4))
                {
                    throw new DataValidationException("Invalid CVN status. Please use the defined constants.");
                }

                m_cvnPresent = value;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Create credit card type
        /// </summary>
        /// <param name="cctype">card tpye</param>
        /// <param name="number">card number</param>
        /// <param name="expiryDate">card expiery date (ddmm)</param>
        /// <param name="cardholderName">cardholder name</param>
        /// <param name="cvn">cvn</param>
        /// <param name="cvnPresent">is cvn present (1/0)</param>
        public CreditCard(String cctype, String number, String expiryDate, String cardholderName, String cvn, int cvnPresent)
        {
            CardType = cctype;
            CardNumber = number;
            ExpiryDate = expiryDate;
            CardholderName = cardholderName;
            CVN = cvn;
            CVNPresent = cvnPresent;
        }

        /// <summary>
        /// Create credit card type
        /// </summary>
        /// <param name="cctype">card tpye</param>
        /// <param name="number">card number</param>
        /// <param name="expiryDate">card expiery date (ddmm)</param>
        /// <param name="cardholderName">cardholder name</param>
        /// <param name="cvn">cvn</param>
        /// <param name="cvnPresent">is cvn present (1/0)</param>
        /// <param name="issueNumber">issue number</param>
        public CreditCard(String cctype, String number, String expiryDate, String cardholderName, String cvn, int cvnPresent, int issueNumber)
        {
            CardType = cctype;
            CardNumber = number;
            ExpiryDate = expiryDate;
            CardholderName = cardholderName;
            CVN = cvn;
            CVNPresent = cvnPresent;
            IssueNumber = issueNumber;
        }

        #endregion


        #region Methods

        /// <summary>
        /// write card details as xml (dont use this)
        /// </summary>
        /// <param name="xml">xml writer</param>
        public void WriteXML(XmlWriter xml)
        {

            xml.WriteStartElement("card");
            {
                xml.WriteElementString("number", m_number);
                xml.WriteElementString("expdate", m_expiryDate);
                xml.WriteElementString("type", m_cctype);
                xml.WriteElementString("chname", m_cardholderName);
                if (m_cctype.Equals("SWITCH"))
                {
                    xml.WriteElementString("issueno", m_issueNumber.ToString());
                }
                xml.WriteStartElement("cvn");
                {
                    xml.WriteElementString("number", m_cvn);
                    xml.WriteElementString("presind", m_cvnPresent.ToString());
                }
                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }

        /// <summary>
        /// Verifys card luhn
        /// </summary>
        /// <param name="creditCardNumber">card number to verify</param>
        /// <returns>is valid or not</returns>
        private bool Mod10Check(string creditCardNumber)
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            //// 1.	Starting with the check digit double the value of every other digit 
            //// 2.	If doubling of a number results in a two digits number, add up
            ///   the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);


            //// If the final sum is divisible by 10, then the credit card number
            //   is valid. If it is not divisible by 10, the number is invalid.            
            return sumOfDigits % 10 == 0;
        }

        #endregion

    }
}
