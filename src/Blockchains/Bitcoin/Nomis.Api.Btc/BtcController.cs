// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.Btcscan.Interfaces;
using Nomis.Btcscan.Interfaces.Models;
using Nomis.Btcscan.Interfaces.Requests;
using Nomis.Utils.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Btc
{
    /// <summary>
    /// A controller to aggregate all Bitcoin-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Bitcoin blockchain.")]
    public sealed class BtcController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/btc";

        /// <summary>
        /// Common tag for Btc actions.
        /// </summary>
        internal const string BtcTag = "Btc";

        private readonly ILogger<BtcController> _logger;
        private readonly IBtcScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="BtcController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IBtcScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public BtcController(
            IBtcScoringService scoringService,
            ILogger<BtcController> logger)
        {
            _scoringService = scoringService ?? throw new ArgumentNullException(nameof(scoringService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Nomis Score for given wallet address.
        /// </summary>
        /// <param name="request">Request for getting the wallet stats.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>An Nomis Score value and corresponding statistical data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/btc/wallet/1NiVzjW9WZmBTjne43pyABxHPGxvi5N598/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetBtcWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetBtcWalletScore",
            Tags = new[] { BtcTag })]
        [ProducesResponseType(typeof(Result<BtcWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetBtcWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] BtcWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<BtcWalletStatsRequest, BtcWalletScore, BtcWalletStats, BtcTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}