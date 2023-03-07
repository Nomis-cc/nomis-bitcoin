// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanTransactionStatus.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Btcscan.Interfaces.Models
{
    /// <summary>
    /// Btcscan transaction status data.
    /// </summary>
    public class BtcscanTransactionStatus
    {
        /// <summary>
        /// Confirmed.
        /// </summary>
        [JsonPropertyName("confirmed")]
        public bool Confirmed { get; set; }

        /// <summary>
        /// Block height.
        /// </summary>
        [JsonPropertyName("block_height")]
        public ulong? BlockHeight { get; set; }

        /// <summary>
        /// Block hash.
        /// </summary>
        [JsonPropertyName("block_hash")]
        public string? BlockHash { get; set; }

        /// <summary>
        /// Block time.
        /// </summary>
        [JsonPropertyName("block_time")]
        public ulong? BlockTime { get; set; }
    }
}