using System;

namespace SfDataGrid_Demo
{
    /// <summary>
    /// Financial Row Data Model with realistic columns
    /// Modeled after WMMStrategyDataGrid structure
    /// </summary>
    public class FinancialRowData
    {
        // ============= IDENTITY & INSTRUMENT COLUMNS =============
        public int RowIndex { get; set; }
        public string Symbol { get; set; }
        public string Underlying { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OptionType { get; set; }  // Call/Put
        public double StrikePrice { get; set; }

        // ============= PRICING COLUMNS =============
        public double BidPrice { get; set; }
        public double AskPrice { get; set; }
        public double LastPrice { get; set; }
        public double TheoreticalPrice { get; set; }
        public double PreviousBidPrice { get; set; }
        public double PreviousAskPrice { get; set; }

        // ============= VOLATILITY COLUMNS =============
        public double BidVolatility { get; set; }
        public double AskVolatility { get; set; }
        public double ImpliedVolatility { get; set; }
        public double HistoricalVolatility { get; set; }
        public double VolatilityShift { get; set; }
        public double PreviousBidVolatility { get; set; }

        // ============= QUANTITY & TRADING COLUMNS =============
        public long BidQuantity { get; set; }
        public long AskQuantity { get; set; }
        public long TradedQuantity { get; set; }
        public long OpenInterest { get; set; }
        public long Volume { get; set; }
        public int BidDepth { get; set; }
        public int AskDepth { get; set; }

        // ============= GREEKS COLUMNS =============
        public double Delta { get; set; }
        public double Gamma { get; set; }
        public double Theta { get; set; }
        public double Vega { get; set; }
        public double Rho { get; set; }
        public double Lambda { get; set; }

        // ============= RISK & EXPOSURE COLUMNS =============
        public double PnL { get; set; }
        public double DailyPnL { get; set; }
        public double Exposure { get; set; }
        public double RiskLimit { get; set; }
        public double UsedRiskLimit { get; set; }
        public double VaR { get; set; }  // Value at Risk

        // ============= EXECUTION COLUMNS =============
        public string ExecutionStatus { get; set; }  // Executed/Pending/Rejected
        public DateTime LastExecutionTime { get; set; }
        public double ExecutionPrice { get; set; }
        public long ExecutedQuantity { get; set; }
        public string OrderID { get; set; }
        public string TradeID { get; set; }

        // ============= LIQUIDITY & SPREAD COLUMNS =============
        public double Spread { get; set; }
        public double SpreadBps { get; set; }  // Basis Points
        public double MidPrice { get; set; }
        public double LiquidityScore { get; set; }
        public double SpreadVolatility { get; set; }
        public bool IsLiquidInstrument { get; set; }

        // ============= CORRELATION & HEDGING COLUMNS =============
        public double Correlation { get; set; }
        public double BetaFactor { get; set; }
        public double HedgeRatio { get; set; }
        public string HedgeStatus { get; set; }
        public double HedgeEffectiveness { get; set; }

        // ============= TIME & DECAY COLUMNS =============
        public double TimeDecay { get; set; }  // Theta effect
        public double DaysToExpiry { get; set; }
        public double YearsToExpiry { get; set; }
        public int TradingDaysRemaining { get; set; }

        // ============= MARKET DATA COLUMNS =============
        public double MarketVolume { get; set; }
        public double MarketVWAP { get; set; }  // Volume Weighted Average Price
        public double DailyHigh { get; set; }
        public double DailyLow { get; set; }
        public double PreviousClose { get; set; }
        public double OpenPrice { get; set; }

        // ============= STATUS & FLAGS COLUMNS =============
        public bool IsVIP { get; set; }
        public bool IsFirstByUnderlying { get; set; }
        public string State { get; set; }  // Active/Inactive/Error
        public string AlertFlag { get; set; }
        public bool IsAnomalous { get; set; }
        public bool LimitExceeded { get; set; }

        // ============= ACCOUNTING COLUMNS =============
        public double AccruedPnL { get; set; }
        public double CommissionPaid { get; set; }
        public double EffectiveCost { get; set; }
        public double AverageEntryPrice { get; set; }
        public double AccountingValue { get; set; }

        // ============= ADDITIONAL METRICS =============
        public DateTime UpdateTime { get; set; }
        public int UpdateFrequency { get; set; }  // Updates per second
        public double DataQualityScore { get; set; }
        public double Skewness { get; set; }
        public double Kurtosis { get; set; }

        public FinancialRowData()
        {
            UpdateTime = DateTime.Now;
            State = "Active";
        }

        public FinancialRowData(int rowIndex)
        {
            RowIndex = rowIndex;
            UpdateTime = DateTime.Now;
            State = "Active";
            ExpiryDate = DateTime.Now.AddDays(30 + (rowIndex % 365));
        }

        /// <summary>
        /// Calculate Greeks based on simplified Black-Scholes model
        /// </summary>
        public void CalculateGreeks()
        {
            // Simplified calculations for demo purposes
            double S = LastPrice;
            double K = StrikePrice;
            double T = YearsToExpiry;
            double r = 0.05; // Risk-free rate
            double sigma = ImpliedVolatility;

            // Delta
            Delta = OptionType == "Call" ? 0.5 : -0.5;

            // Gamma (always positive)
            Gamma = 0.01 / (S * sigma * Math.Sqrt(T));

            // Theta (usually negative for calls/puts)
            Theta = OptionType == "Call" ? -0.1 : 0.1;

            // Vega (sensitivity to volatility)
            Vega = S * Math.Sqrt(T) * 0.01;

            // Rho (sensitivity to interest rates)
            Rho = K * T * 0.001;

            // Lambda (leverage ratio)
            Lambda = Delta * (S / (TheoreticalPrice > 0 ? TheoreticalPrice : 1));
        }

        /// <summary>
        /// Calculate time decay and days to expiry
        /// </summary>
        public void UpdateTimeMetrics()
        {
            DaysToExpiry = (ExpiryDate - DateTime.Now).TotalDays;
            YearsToExpiry = DaysToExpiry / 365.0;
            TradingDaysRemaining = (int)DaysToExpiry;
            TimeDecay = Theta;
        }

        /// <summary>
        /// Initialize with random financial data
        /// </summary>
        public void InitializeRandomData(Random rand)
        {
            // Pricing
            LastPrice = rand.Next(1, 100) * 10;
            BidPrice = LastPrice - rand.NextDouble() * 0.5;
            AskPrice = LastPrice + rand.NextDouble() * 0.5;
            TheoreticalPrice = LastPrice + (rand.NextDouble() - 0.5) * 2;
            MidPrice = (BidPrice + AskPrice) / 2;

            // Volatility
            ImpliedVolatility = 0.1 + rand.NextDouble() * 0.4;
            BidVolatility = ImpliedVolatility - rand.NextDouble() * 0.02;
            AskVolatility = ImpliedVolatility + rand.NextDouble() * 0.02;
            HistoricalVolatility = ImpliedVolatility * 0.8;

            // Quantities
            BidQuantity = (long)(100 + rand.Next(900)) * 1000;
            AskQuantity = (long)(100 + rand.Next(900)) * 1000;
            Volume = (long)(1000000 + rand.Next(9000000));
            OpenInterest = (long)(500000 + rand.Next(4500000));

            // Greeks
            CalculateGreeks();

            // PnL & Risk
            PnL = (rand.NextDouble() - 0.5) * 10000;
            DailyPnL = (rand.NextDouble() - 0.5) * 5000;
            Exposure = (rand.NextDouble() * 5000000);
            VaR = Exposure * 0.05;

            // Spread
            Spread = AskPrice - BidPrice;
            SpreadBps = (Spread / MidPrice) * 10000;

            // Market Data
            DailyHigh = LastPrice * (1 + rand.NextDouble() * 0.02);
            DailyLow = LastPrice * (1 - rand.NextDouble() * 0.02);
            PreviousClose = LastPrice * (1 + (rand.NextDouble() - 0.5) * 0.05);
            OpenPrice = LastPrice * (1 + (rand.NextDouble() - 0.5) * 0.03);
            MarketVWAP = (OpenPrice + DailyHigh + DailyLow + LastPrice) / 4;
            IsLiquidInstrument = true;

            // Execution
            ExecutedQuantity = (long)(rand.Next(100000) * 100);
            ExecutionPrice = LastPrice + (rand.NextDouble() - 0.5) * 1;
            ExecutionStatus = "Executed";
            // Other
            Correlation = (rand.NextDouble() - 0.5) * 2;
            BetaFactor = 0.8 + rand.NextDouble() * 0.4;
            LiquidityScore = rand.NextDouble();
            DataQualityScore = 0.95 + rand.NextDouble() * 0.05;
            Skewness = (rand.NextDouble() - 0.5) * 2;
            Kurtosis = 3 + (rand.NextDouble() - 0.5) * 2;

            UpdateTimeMetrics();
        }
    }
}
