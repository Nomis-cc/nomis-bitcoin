// ------------------------------------------------------------------------------------------------------
// <copyright file="IBtcscanClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Btcscan.Interfaces.Models;

namespace Nomis.Btcscan.Interfaces
{
    /// <summary>
    /// Btcscan client.
    /// </summary>
    public interface IBtcscanClient
    {
        /// <summary>
        /// Get the account data.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns <see cref="BtcscanAccount"/>.</returns>
        Task<BtcscanAccount> AccountAsync(string address);

        /// <summary>
        /// Get list of transactions of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of transactions of the given account.</returns>
        Task<IEnumerable<BtcscanTransaction>> TransactionsAsync(string address);
    }
}