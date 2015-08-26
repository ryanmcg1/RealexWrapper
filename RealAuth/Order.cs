using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealexPayments
{
    public class Order
    {
        #region Properties

        private String _orderId = string.Empty;
        private String _orderCurrency = string.Empty;
        private uint _orderAmount = 00;

        public String OrderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                Validator.assertAlphaNumericStrictishNoSpace("Order Id", value);
                Validator.assertLength("Order Id", value, 0, 40);
                _orderId = value;
            }
        }

        public String OrderCurrency
        {
            get
            {
                return _orderCurrency;
            }
            set
            {
                Validator.assertAlphaStrict("Order Currency", value);
                Validator.assertLength("Order Currency", value, 3);
                _orderCurrency = value;
            }
        }

        public uint OrderAmount
        {
            get
            {
                return _orderAmount;
            }
            set
            {
                _orderAmount = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create new order type with all fields
        /// </summary>
        /// <param name="orderId">a-z A-Z0-9 _ - and 1-40</param>
        /// <param name="orderCurrency">a-z A-Z and 3</param>
        /// <param name="orderAmount">0-9 and 2-11</param>
        public Order(String orderId, String orderCurrency, uint orderAmount)
        {
            SetOrderValues(orderId, orderCurrency, orderAmount);
        }

        /// <summary>
        /// Create new order type with only required fields
        /// </summary>
        /// <param name="orderCurrency">a-z A-Z and 3</param>
        /// <param name="orderAmount">0-9 and 2-11</param>
        public Order(String orderCurrency, uint orderAmount)
        {
            string orderId = Common.GenerateTimestamp();

            SetOrderValues(orderId, orderCurrency, orderAmount);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set order values
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderCurrency"></param>
        /// <param name="orderAmount"></param>
        private void SetOrderValues(String orderId, String orderCurrency, uint orderAmount)
        {
            OrderId = orderId;
            OrderCurrency = orderCurrency;
            OrderAmount = orderAmount;
        }

        #endregion

    }
}