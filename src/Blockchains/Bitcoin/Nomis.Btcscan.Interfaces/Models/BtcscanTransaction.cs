// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Btcscan.Interfaces.Models
{
    /// <summary>
    /// Btcscan transaction data.
    /// </summary>
    public class BtcscanTransaction
    {
        /// <summary>
        /// Tx id.
        /// </summary>
        [JsonPropertyName("txid")]
        public string? TxId { get; set; }

        /// <summary>
        /// Vin.
        /// </summary>
        [JsonPropertyName("vin")]
        public IList<BtcscanTransactionVin>? Vin { get; set; } = new List<BtcscanTransactionVin>();

        /// <summary>
        /// Vout.
        /// </summary>
        [JsonPropertyName("vout")]
        public IList<BtcscanTransactionVout>? Vout { get; set; } = new List<BtcscanTransactionVout>();

        /// <summary>
        /// Status.
        /// </summary>
        [JsonPropertyName("status")]
        public BtcscanTransactionStatus? Status { get; set; }
    }
}