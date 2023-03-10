// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Btc.Settings;
using Nomis.Api.Common.Extensions;
using Nomis.Btcscan.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Btc.Extensions
{
    /// <summary>
    /// Btc extension methods.
    /// </summary>
    public static class BtcExtensions
    {
        /// <summary>
        /// Add BTC blockchain.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        // ReSharper disable once InconsistentNaming
        public static IScoringOptionsBuilder WithBTCBlockchain<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IBtcServiceRegistrar, new()
        {
            return optionsBuilder
                .With<BtcAPISettings, TServiceRegistrar>();
        }
    }
}