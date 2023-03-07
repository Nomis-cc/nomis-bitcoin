// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanTransactionVin.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Btcscan.Interfaces.Models
{
    /// <summary>
    /// Btcscan transaction Vin data.
    /// </summary>
    public class BtcscanTransactionVin
    {
        /// <summary>
        /// Tx id.
        /// </summary>
        [JsonPropertyName("txid")]
        public string? TxId { get; set; }

        /// <summary>
        /// Vout.
        /// </summary>
        [JsonPropertyName("vout")]
        public int Vout { get; set; }

        /// <summary>
        /// Vout.
        /// </summary>
        [JsonPropertyName("prevout")]
        public BtcscanTransactionVout? Prevout { get; set; }
    }
}