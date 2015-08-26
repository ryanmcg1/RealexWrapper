using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RealexPayments
{
    class Validator
    {
        public static void assertAlphaNumericLoose(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z0-9_\-\.,\+@\ ]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain characters A-Z a-z 0-9 _ - . , + @ and space characters.");
            }
        }

        public static void assertAlphaNumericSpace(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z0-9\ ]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain alpha, numeric and space characters.");
            }
        }

        public static void assertAlphaNumericStrict(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z0-9]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain alphanumeric characters.");
            }
        }

        public static void assertAlphaNumericStrictishNoSpace(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z0-9_\-]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain alphanumeric characters, _ and - .");
            }
        }

        public static void assertAlphaNumericSpaceDashes(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z0-9_\-\ ]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain characters A-Z a-z 0-9 _ - and space characters.");
            }
        }

        public static void assertAlphaNumericDot(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z0-9\.]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain alpha, numeric and ..");
            }
        }

        public static void assertAlphaLoose(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z_\-\.,\+@\ ]*$"))
            {
                throw new DataValidationException(fieldName + "may only contain characters A-Z a-z _ - . , + @ and space characters.");
            }
        }

        public static void assertAlphaStrict(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain alpha characters.");
            }
        }

        public static void assertAlphaSpace(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z\ ]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain alpha and space characters.");
            }
        }

        public static void assertAlphaSpaceDash(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[A-Za-z\-\ ]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain - alpha and space characters.");
            }
        }

        public static void assertNumericLoose(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[0-9\+\-\ ]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain numeric characters.");
            }
        }

        public static void assertNumeric(String fieldName, String s)
        {
            if (!Regex.IsMatch(s, @"^[0-9]*$"))
            {
                throw new DataValidationException(fieldName + " may only contain numeric characters.");
            }
        }

        public static void assertLength(String fieldName, String s, int exactLength)
        {
            if (s.Length != exactLength)
            {
                throw new DataValidationException(fieldName + " must be exactly " + exactLength + "characters in length.");
            }
        }

        public static void assertLength(String fieldName, String s, int minLength, int maxLength)
        {
            if ((s.Length < minLength) || (s.Length > maxLength))
            {
                throw new DataValidationException(fieldName + " must be between " + minLength + " and " + maxLength + " characters (inclusive) in length.");
            }
        }
    }
}
