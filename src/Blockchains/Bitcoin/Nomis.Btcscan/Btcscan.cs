// ------------------------------------------------------------------------------------------------------
// <copyright file="Btcscan.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.Btcscan.Extensions;
using Nomis.Btcscan.Interfaces;

namespace Nomis.Btcscan
{
    /// <summary>
    /// Btcscan Explorer service registrar.
    /// </summary>
    public sealed class Btcscan :
        IBtcServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddBtcscanService();
        }
    }
}