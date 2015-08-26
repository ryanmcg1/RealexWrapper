using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealexPayments
{
    public class _3DSecure
    {

        #region Properties

        private string _cavv;
        private string _xid;
        private string _eci;
        private string _pares;
        private string _acsUrl;

        public String CAVV
        {
            get { return _cavv; }
            set
            {
                Validator.assertLength("CAVV", value, 0, 50);
                _cavv = value;
            }
        }

        public String XID
        {
            get { return _xid; }
            set
            {
                Validator.assertLength("XID", value, 0, 50);
                _xid = value;
            }
        }

        public String ECI
        {
            get { return _eci; }
            set
            {
                Validator.assertLength("ECI", value, 0, 2);
                Validator.assertNumeric("ECI", value);
                _eci = value;
            }
        }

        public String PaRes
        {
            get { return _pares; }
            set
            {
                _pares = value;
            }
        }

        public String ACSURL
        {
            get { return _acsUrl; }
            set
            {
                _acsUrl = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create empty 3d secure
        /// </summary>
        public _3DSecure()
        {
            Set3DSecureValues("", "", "", "", "");
        }

        /// <summary>
        /// Create 3d secure details
        /// </summary>
        /// <param name="cavv">cavv</param>
        /// <param name="xid">xid</param>
        /// <param name="eci">eci</param>
        /// <param name="paRes">pares</param>
        /// <param name="acsUrl">acs url</param>
        public _3DSecure(String cavv, String xid, String eci, String paRes, String acsUrl)
        {
            Set3DSecureValues(cavv, xid, eci, paRes, acsUrl);
        }

        #endregion

        #region Methods

        protected void Set3DSecureValues(String cavv, String xid, String eci, String paRes, String acsUrl)
        {
            CAVV = cavv;
            XID = xid;
            ECI = eci;
            PaRes = paRes;
            ACSURL = acsUrl;
        }

        #endregion

    }
}
