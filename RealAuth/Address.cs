using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class Address
    {
        #region Properties

        private String _line1;
        private String _line2;
        private String _line3;
        private String _city;
        private String _county;
        private String _postcode;
        private String _countryCode;
        private String _countryName;

        public String Line1
        {
            get { return _line1; }
            set
            {
                Validator.assertLength("Line 1", value, 0, 50);
                _line1 = value;
            }
        }

        public String Line2
        {
            get { return _line2; }
            set
            {
                Validator.assertLength("Line 2", value, 0, 50);
                _line3 = value;
            }
        }

        public String Line3
        {
            get { return _line3; }
            set
            {
                Validator.assertLength("Line 3", value, 0, 50);
                _line2 = value;
            }
        }

        public String City
        {
            get { return _city; }
            set 
            {
                Validator.assertAlphaLoose("City", value);
                Validator.assertLength("City", value, 0, 20);
                _city = value; 
            }
        }

        public String County
        {
            get { return _county; }
            set 
            {
                Validator.assertAlphaLoose("County", value);
                Validator.assertLength("County", value, 0, 20);
                _county = value; 
            }
        }

        public String Postcode
        {
            get { return _postcode; }
            set 
            {
                Validator.assertAlphaNumericSpace("Postcode", value);
                Validator.assertLength("Postcode", value, 0, 8);
                _postcode = value; 
            }
        }

        public String CountryCode
        {
            get { return _countryCode; }
            set 
            { 
                value = value.ToUpper();
                Validator.assertAlphaStrict("Country Code", value);
                Validator.assertLength("Country Code", value, 0, 2);
                _countryCode = value; 
            }
        }

        public String CountryName
        {
            get { return _countryName; }
            set{ _countryName = value; }
        }

        #endregion

        #region Constructors
            
        /// <summary>
        /// Set all address values
        /// </summary>
        /// <param name="line1">Address line 1</param>
        /// <param name="line2">Address line 2</param>
        /// <param name="line3">Address line 3</param>
        /// <param name="city">City</param>
        /// <param name="county">County</param>
        /// <param name="postcode">Postcode</param>
        /// <param name="countryCode">Country Code</param>
        /// <param name="countryName">Country Name</param>
        public Address(String line1, String line2, String line3, String city, String county, String postcode, String countryCode, String countryName)
        {
            SetAddressValues(line1, line2, line3, city, county, postcode, countryCode, countryName);
        }

        /// <summary>
        /// Set only required address values
        /// </summary>
        public Address()
        {
            SetAddressValues("", "", "", "", "", "", "", "");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set address values
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="line3"></param>
        /// <param name="city"></param>
        /// <param name="county"></param>
        /// <param name="postcode"></param>
        /// <param name="countryCode"></param>
        /// <param name="countryName"></param>
        private void SetAddressValues(String line1, String line2, String line3, String city, String county, String postcode, String countryCode, String countryName)
        {
            Line1 = line1;
            Line2 = line2;
            Line3 = line3;
            City = city;
            County = county;
            Postcode = postcode;
            CountryCode = countryCode;
            CountryName = countryName;
        }

        /// <summary>
        /// Write address to payer xml fields
        /// </summary>
        /// <param name="xml"></param>
        public void WriteXML(XmlWriter xml)
        {

            xml.WriteStartElement("address");
            {
                xml.WriteElementString("line1", _line1);
                xml.WriteElementString("line2", _line2);
                xml.WriteElementString("line3", _line3);
                xml.WriteElementString("city", _city);
                xml.WriteElementString("county", _county);
                xml.WriteElementString("postcode", _postcode);
            }
                    
            xml.WriteStartElement("country");
            xml.WriteAttributeString("code", _countryCode);
            xml.WriteString(_countryName);
            xml.WriteEndElement();
                
            xml.WriteEndElement();
        }

        #endregion
    }

}
