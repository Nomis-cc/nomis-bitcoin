// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanTransactionVout.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Btcscan.Interfaces.Models
{
    /// <summary>
    /// Btcscan transaction Vout data.
    /// </summary>
    public class BtcscanTransactionVout
    {
        /// <summary>
        /// Script pub key address.
        /// </summary>
        [JsonPropertyName("scriptpubkey_address")]
        public string? ScriptPubKeyAddress { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        [JsonPropertyName("value")]
        public BigInteger Value { get; set; }
    }
}