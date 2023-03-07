// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcHelpers.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

namespace Nomis.Btcscan.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods for btc.
    /// </summary>
    public static class BtcHelpers
    {
        /// <summary>
        /// Convert string value to BigInteger.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total BigInteger value.</returns>
        public static BigInteger ToBigInteger(this string valueInWei)
        {
            return !BigInteger.TryParse(valueInWei, out var wei) ? 0 : wei;
        }

        /// <summary>
        /// Convert Wei value to BTC.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total BTC.</returns>
        public static decimal ToBtc(this string valueInWei)
        {
            return BigInteger
                .TryParse(valueInWei, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { CurrencyDecimalSeparator = "." }, out var value)
                ? value.ToBtc()
                : 0;
        }

        /// <summary>
        /// Convert Wei value to BTC.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total BTC.</returns>
        public static decimal ToBtc(this in BigInteger valueInWei)
        {
            if (valueInWei > new BigInteger(decimal.MaxValue))
            {
                return (decimal)(valueInWei / new BigInteger(10_000_000));
            }

            return (decimal)valueInWei * 0.000_000_01M;
        }

        /// <summary>
        /// Convert Wei value to BTC.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total BTC.</returns>
        public static decimal ToBtc(this decimal valueInWei)
        {
            return new BigInteger(valueInWei).ToBtc();
        }
    }
}