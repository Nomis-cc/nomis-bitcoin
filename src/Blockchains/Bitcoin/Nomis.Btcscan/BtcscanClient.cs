// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcscanClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Nomis.Blockchain.Abstractions.Converters;
using Nomis.Btcscan.Interfaces;
using Nomis.Btcscan.Interfaces.Models;
using Nomis.Btcscan.Settings;
using Nomis.Utils.Exceptions;

namespace Nomis.Btcscan
{
    /// <inheritdoc cref="IBtcscanClient"/>
    internal sealed class BtcscanClient :
        IBtcscanClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="BtcscanClient"/>.
        /// </summary>
        /// <param name="btcscanSettings"><see cref="BtcscanSettings"/>.</param>
        public BtcscanClient(
            IOptions<BtcscanSettings> btcscanSettings)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _client = new(httpHandler)
            {
                BaseAddress = new(btcscanSettings.Value.ApiBaseUrl ??
                                  throw new ArgumentNullException(nameof(btcscanSettings.Value.ApiBaseUrl)))
            };
        }

        /// <inheritdoc/>
        public async Task<BtcscanAccount> AccountAsync(string address)
        {
            string request =
                $"/api/address/{address}";

            var response = await _client.GetAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BtcscanAccount>(new JsonSerializerOptions
            {
                Converters = { new BigIntegerConverter() }
            }).ConfigureAwait(false) ?? throw new CustomException("Can't get account data.");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BtcscanTransaction>> TransactionsAsync(string address)
        {
            string request =
                $"/api/address/{address}/txs";

            var response = await _client.GetAsync(request).ConfigureAwait(false);

            string t = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Console.WriteLine(t);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<BtcscanTransaction>>(new JsonSerializerOptions
            {
                Converters = { new BigIntegerConverter() }
            }).ConfigureAwait(false) ?? throw new CustomException("Can't get account transactions.");
        }
    }
}