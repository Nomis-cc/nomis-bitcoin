// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts;

namespace Nomis.Btcscan.Settings
{
    /// <summary>
    /// Btcscan settings.
    /// </summary>
    internal class BtcscanSettings :
        IBlockchainSettings
    {
        /// <summary>
        /// API base URL.
        /// </summary>
        public string? ApiBaseUrl { get; set; }

        /// <inheritdoc />
        public BlockchainDescriptor? BlockchainDescriptor { get; set; }
    }
}