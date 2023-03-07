// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcWalletStatsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Nomis.Chainanalysis.Interfaces.Contracts;
using Nomis.Greysafe.Interfaces.Contracts;
using Nomis.Utils.Contracts.Requests;

namespace Nomis.Btcscan.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the wallet stats for Btc.
    /// </summary>
    public sealed class BtcWalletStatsRequest :
        WalletStatsRequest,
        IWalletGreysafeRequest,
        IWalletChainanalysisRequest
    {
        /// <inheritdoc />
        /// <example>true</example>
        [FromQuery]
        public bool GetGreysafeData { get; set; } = true;

        /// <inheritdoc />
        /// <example>true</example>
        [FromQuery]
        public bool GetChainanalysisData { get; set; } = true;
    }
}