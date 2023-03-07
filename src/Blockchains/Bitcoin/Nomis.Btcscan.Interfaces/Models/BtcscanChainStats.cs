// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanChainStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Btcscan.Interfaces.Models
{
    /// <summary>
    /// Btcscan chain stats.
    /// </summary>
    public class BtcscanChainStats
    {
        /// <summary>
        /// Funded txo count.
        /// </summary>
        [JsonPropertyName("funded_txo_count")]
        public BigInteger? FundedTxoCount { get; set; }

        /// <summary>
        /// Funded txo sum.
        /// </summary>
        [JsonPropertyName("funded_txo_sum")]
        public BigInteger? FundedTxoSum { get; set; }

        /// <summary>
        /// Spent txo sum.
        /// </summary>
        [JsonPropertyName("spent_txo_count")]
        public BigInteger? SpentTxoCount { get; set; }

        /// <summary>
        /// Spent txo sum.
        /// </summary>
        [JsonPropertyName("spent_txo_sum")]
        public BigInteger? SpentTxoSum { get; set; }

        /// <summary>
        /// Tx count.
        /// </summary>
        [JsonPropertyName("tx_count")]
        public BigInteger? TxCount { get; set; }
    }
}