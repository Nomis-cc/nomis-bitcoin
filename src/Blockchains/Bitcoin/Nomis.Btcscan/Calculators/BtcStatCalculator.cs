// ------------------------------------------------------------------------------------------------------
// <copyright file="BtcStatCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nomis.Blockchain.Abstractions.Calculators;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Btcscan.Interfaces.Extensions;
using Nomis.Btcscan.Interfaces.Models;
using Nomis.Chainanalysis.Interfaces.Calculators;
using Nomis.Chainanalysis.Interfaces.Models;
using Nomis.Greysafe.Interfaces.Calculators;
using Nomis.Greysafe.Interfaces.Models;
using Nomis.Utils.Contracts;
using Nomis.Utils.Contracts.Calculators;
using Nomis.Utils.Extensions;

namespace Nomis.Btcscan.Calculators
{
    /// <summary>
    /// Btc wallet stats calculator.
    /// </summary>
    internal sealed class BtcStatCalculator :
        IWalletCommonStatsCalculator<BtcWalletStats, BtcTransactionIntervalData>,
        IWalletNativeBalanceStatsCalculator<BtcWalletStats, BtcTransactionIntervalData>,
        IWalletTransactionStatsCalculator<BtcWalletStats, BtcTransactionIntervalData>,
        IWalletGreysafeStatsCalculator<BtcWalletStats, BtcTransactionIntervalData>,
        IWalletChainanalysisStatsCalculators<BtcWalletStats, BtcTransactionIntervalData>
    {
        private readonly string _address;
        private readonly IEnumerable<BtcscanTransaction> _transactions;
        private readonly IEnumerable<GreysafeReport>? _greysafeReports;
        private readonly IEnumerable<ChainanalysisReport>? _chainanalysisReports;

        /// <inheritdoc />
        public int WalletAge => _transactions.Any()
            ? IWalletStatsCalculator.GetWalletAge(_transactions.Select(x => x.Status!.BlockTime!.ToString() !.ToDateTime()))
            : 1;

        /// <inheritdoc />
        public IList<BtcTransactionIntervalData> TurnoverIntervals
        {
            get
            {
                var turnoverIntervalsDataList =
                    _transactions.Select(x => new TurnoverIntervalsData(
                        x.Status!.BlockTime!.ToString() !.ToDateTime(),
                        new BigInteger(x.Vout?.Sum(vout => (decimal)vout.Value) ?? 0),
                        x.Vin?.Any(vin => vin.Prevout?.ScriptPubKeyAddress?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true) == true));
                return IWalletStatsCalculator<BtcTransactionIntervalData>
                    .GetTurnoverIntervals(turnoverIntervalsDataList, _transactions.Any() ? _transactions.Min(x => x.Status!.BlockTime!.ToString() !.ToDateTime()) : DateTime.MinValue).ToList();
            }
        }

        /// <inheritdoc />
        public decimal NativeBalance { get; }

        /// <inheritdoc />
        public decimal NativeBalanceUSD { get; }

        /// <inheritdoc />
        public decimal BalanceChangeInLastMonth =>
            IWalletStatsCalculator<BtcTransactionIntervalData>.GetBalanceChangeInLastMonth(TurnoverIntervals);

        /// <inheritdoc />
        public decimal BalanceChangeInLastYear =>
            IWalletStatsCalculator<BtcTransactionIntervalData>.GetBalanceChangeInLastYear(TurnoverIntervals);

        /// <inheritdoc />
        public decimal WalletTurnover =>
            _transactions.Sum(x => x.Vin?.Sum(vin => vin.Prevout?.Value.ToBtc() ?? 0) + x.Vout?.Sum(vout => vout.Value.ToBtc()) ?? 0);

        /// <inheritdoc />
        public IEnumerable<GreysafeReport>? GreysafeReports => _greysafeReports?.Any() == true ? _greysafeReports : null;

        /// <inheritdoc />
        public IEnumerable<ChainanalysisReport>? ChainanalysisReports =>
            _chainanalysisReports?.Any() == true ? _chainanalysisReports : null;

        public BtcStatCalculator(
            string address,
            decimal balance,
            decimal usdBalance,
            IEnumerable<BtcscanTransaction> transactions,
            IEnumerable<GreysafeReport>? greysafeReports,
            IEnumerable<ChainanalysisReport>? chainanalysisReports)
        {
            _address = address;
            NativeBalance = balance.ToBtc();
            NativeBalanceUSD = usdBalance;
            _transactions = transactions;
            _greysafeReports = greysafeReports;
            _chainanalysisReports = chainanalysisReports;
        }

        /// <inheritdoc />
        public BtcWalletStats Stats()
        {
            return (this as IWalletStatsCalculator<BtcWalletStats, BtcTransactionIntervalData>).ApplyCalculators();
        }

        /// <inheritdoc />
        IWalletTransactionStats IWalletTransactionStatsCalculator<BtcWalletStats, BtcTransactionIntervalData>.Stats()
        {
            if (!_transactions.Any())
            {
                return new BtcWalletStats
                {
                    NoData = true
                };
            }

            var intervals = IWalletStatsCalculator
                .GetTransactionsIntervals(_transactions.Select(x => x.Status!.BlockTime!.ToString() !.ToDateTime())).ToList();
            if (intervals.Count == 0)
            {
                return new BtcWalletStats
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);
            var yearAgo = DateTime.Now.AddYears(-1);

            return new BtcWalletStats
            {
                TotalTransactions = _transactions.Count(),
                TotalRejectedTransactions = _transactions.Count(t => t.Status?.Confirmed != true),
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                LastMonthTransactions = _transactions.Count(x => x.Status!.BlockTime!.ToString() !.ToDateTime() > monthAgo),
                LastYearTransactions = _transactions.Count(x => x.Status!.BlockTime!.ToString() !.ToDateTime() > yearAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.OrderBy(x => x.Status!.BlockTime).Last().Status!.BlockTime!.ToString() !.ToDateTime()).TotalDays / 30)
            };
        }
    }
}