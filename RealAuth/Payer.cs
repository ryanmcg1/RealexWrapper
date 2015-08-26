using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace RealexPayments
{
    public class Payer
    {
        #region Properties

        //Needs set
        private String _payerType;
        private String _payerRef;
        private String _title;
        private String _firstName;
        private String _surname;
        private String _company;
        private Address _address;
        private PhoneNumbers _phoneNumbers;
        private String _email;
        private ArrayList _comments;


        public String PayerType
        {
            get { return _payerType; }
            set
            {
                Validator.assertAlphaStrict("Payer Type", value);
                Validator.assertLength("Payer Type", value, 1, 20);
                _payerType = value;
            }
        }

        public String PayerRef
        {
            get { return _payerRef; }
            set
            {
                Validator.assertAlphaNumericSpaceDashes("Payer Ref", value);
                Validator.assertLength("Payer Ref", value, 1, 50);
                _payerRef = value;
            }
        }

        public String Title
        {
            get { return _title; }
            set
            {
                Validator.assertAlphaStrict("Title", value);
                _title = value;
            }
        }

        public String FirstName
        {
            get { return _firstName; }
            set
            {
                Validator.assertAlphaSpace("First Name", value);
                Validator.assertLength("First Name", value, 1, 30);
                _firstName = value;
            }
        }

        public String Surname
        {
            get { return _surname; }
            set
            {
                Validator.assertAlphaSpaceDash("Surname", value);
                Validator.assertLength("Surname", value, 1, 50);
                _surname = value;
            }
        }

        public String Company
        {
            get { return _company; }
            set
            {
                Validator.assertAlphaNumericSpace("Company", value);
                Validator.assertLength("Company", value, 0, 50);
                _company = value;
            }
        }

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public PhoneNumbers PhoneNumbers
        {
            get { return _phoneNumbers; }
            set { _phoneNumbers = value; }
        }

        public String Email
        {
            get { return _email; }
            set
            {
                Validator.assertAlphaNumericLoose("Email", value);
                Validator.assertLength("Email", value, 0, 30);
                _email = value;
            }
        }

        public ArrayList Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new payer with all fields
        /// </summary>
        /// <param name="payerType">Payer Type (Can be defaulted to 'Business')</param>
        /// <param name="payerRef">Payer Ref</param>
        /// <param name="title">Title</param>
        /// <param name="firstName">First Name</param>
        /// <param name="surname">Surname</param>
        /// <param name="company">Company</param>
        /// <param name="address">Address</param>
        /// <param name="phoneNumbers">Phone Numbers</param>
        /// <param name="email">Email</param>
        /// <param name="comments">Comments</param>
        public Payer(String payerType, String payerRef, String title, String firstName, String surname, String company, Address address, PhoneNumbers phoneNumbers, String email, ArrayList comments)
        {
            SetPayerValues(payerType, payerRef, title, firstName, surname, company, address, phoneNumbers, email, comments);
        }

        /// <summary>
        /// Create a new payer object with only required fields
        /// </summary>
        /// <param name="payerType">Payer Type (Can be defaulted to 'Business')</param>
        /// <param name="payerRef">Payer ref</param>
        /// <param name="firstName">First Name</param>
        /// <param name="surname">Surname</param>
        public Payer(String payerType, String payerRef, String firstName, String surname)
        {
            SetPayerValues(payerType, payerRef, "", firstName, surname, "", new Address(), new PhoneNumbers(), "", new ArrayList());
        }

        #endregion

        #region Methods
        private void SetPayerValues(String payerType, String payerRef, String title, String firstName, String surname, String company, Address address, PhoneNumbers phoneNumbers, String email, ArrayList comments)
        {
            PayerType = payerType;
            PayerRef = payerRef;
            Title = title;
            FirstName = firstName;
            Surname = surname;
            Company = company;
            Address = address;
            PhoneNumbers = phoneNumbers;
            Email = email;
            Comments = comments;
        }

        public void WriteXml(XmlWriter xml)
        {
            xml.WriteStartElement("payer");
            xml.WriteAttributeString("type", _payerType);
            xml.WriteAttributeString("ref", _payerRef);

            xml.WriteElementString("title", _title);
            xml.WriteElementString("firstname", _firstName);
            xml.WriteElementString("surname", _surname);
            xml.WriteElementString("company", _company);

            _address.WriteXML(xml);
            _phoneNumbers.WriteXML(xml);

            xml.WriteElementString("email", _email);

            xml.WriteStartElement("comments");
            {
                int i = 1;
                foreach (string s in _comments)
                {
                    xml.WriteStartElement("comment");
                    xml.WriteAttributeString("id", i.ToString());
                    xml.WriteString(s);
                    xml.WriteEndElement();
                    i++;
                }
            }
            xml.WriteEndElement();

            xml.WriteEndElement();
        }

        #endregion
    }
}
