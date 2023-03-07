// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanAccount.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Btcscan.Interfaces.Models
{
    /// <summary>
    /// Btcscan account.
    /// </summary>
    public class BtcscanAccount
    {
        /// <summary>
        /// Address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Chain stats.
        /// </summary>
        [JsonPropertyName("chain_stats")]
        public BtcscanChainStats? ChainStats { get; set; }
    }
}