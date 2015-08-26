using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class PhoneNumbers
    {
        #region Properties

        private String _homeNumber = string.Empty;
        private String _workNumber = string.Empty;
        private String _faxNumber = string.Empty;
        private String _mobileNumber = string.Empty;

        public String HomeNumber
        {
            get
            {
                return _homeNumber;
            }
            set
            {
                Validator.assertNumericLoose("Home Number", value);
                _homeNumber = value;
            }
        }

        public String WorkNumber
        {
            get
            {
                return _workNumber;
            }
            set
            {
                Validator.assertNumericLoose("Work Number", value);
                _workNumber = value;
            }
        }

        public String FaxNumber
        {
            get
            {
                return _faxNumber;
            }
            set
            {
                Validator.assertNumericLoose("Fax Number", value);
                _faxNumber = value;
            }
        }

        public String MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                Validator.assertNumericLoose("Mobile Number", value);
                _mobileNumber = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create new phone number list
        /// </summary>
        /// <param name="homeNumber">Home Number</param>
        /// <param name="workNumber">Work Number</param>
        /// <param name="faxNumber">Fax Number</param>
        /// <param name="mobileNumber">Mobile Number</param>
        public PhoneNumbers(String homeNumber, String workNumber, String faxNumber, String mobileNumber)
        {
            SetPhoneNumbers(homeNumber, workNumber, faxNumber, mobileNumber);
        }


        /// <summary>
        /// Set empty list of numbers
        /// </summary>
        public PhoneNumbers()
        {
            SetPhoneNumbers("", "", "", "");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set phone number list
        /// </summary>
        /// <param name="homeNumber">Home Number</param>
        /// <param name="workNumber">Work Number</param>
        /// <param name="faxNumber">Fax Number</param>
        /// <param name="mobileNumber">Mobile Number</param>
        private void SetPhoneNumbers(String homeNumber, String workNumber, String faxNumber, String mobileNumber)
        {
            HomeNumber = homeNumber;
            WorkNumber = workNumber;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
        }

        /// <summary>
        /// Write phone numbers to xml
        /// </summary>
        /// <param name="xml">XML writer</param>
        public void WriteXML(XmlWriter xml)
        {

            xml.WriteStartElement("phonenumbers");
            {
                xml.WriteElementString("home", _homeNumber);
                xml.WriteElementString("work", _workNumber);
                xml.WriteElementString("fax", _faxNumber);
                xml.WriteElementString("mobile", _mobileNumber);
            }
            xml.WriteEndElement();
        }

        #endregion
    }
}
