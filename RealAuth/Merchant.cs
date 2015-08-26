using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealexPayments
{
    public class Merchant
    {

        #region Properties

        private String _merchantId = string.Empty;
        private String _account = string.Empty;
        private String _sharedSecret = string.Empty;

        public String MerchantId
        {
            get
            {
                return _merchantId;
            }
            set
            {
                Validator.assertAlphaNumericDot("Merchant Id", value);
                Validator.assertLength("Merchant Id", value, 1, 50);
                _merchantId = value;
            }
        }

        public String Account
        {
            get
            {
                return _account;
            }
            set
            {
                Validator.assertLength("Account", value, 1, 20);
                _account = value;
            }
        }

        public String SharedSecret
        {
            get
            {
                return _sharedSecret;
            }
            set
            {
                _sharedSecret = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create new Merchant type with requried fields
        /// </summary>
        /// <param name="merchantId">a-z A-Z 0-9 . and 1-50</param>
        /// <param name="account">1-20</param>
        /// <param name="sharedSecret"></param>
        public Merchant(String merchantId, String account, String sharedSecret)
        {
            SetMerchantValues(merchantId, account, sharedSecret);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set merchant values
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="account"></param>
        /// <param name="sharedSecret"></param>
        private void SetMerchantValues(String merchantId, String account, String sharedSecret)
        {
            MerchantId = merchantId;
            Account = account;
            SharedSecret = sharedSecret;
        }

        #endregion
    }
}
