# Large-Dataset-Demo-in-WPF-DataGrid

## Overview

A comprehensive WPF demonstration of the Syncfusion DataGrid (`SfDataGrid`) handling large-scale datasets with complex conditional styling. This sample application showcases a realistic financial trading grid with 2,000 rows and 100+ columns, where every column applies its own `IValueConverter` for value formatting and conditional foreground, background, and font styling.

This WPF application provides a complete working example of:

- **Large Dataset Handling**: Loads 2,000 rows × 100+ columns of realistic options-trading data (symbols, prices, Greeks, risk metrics, execution status)
- **Heavy Value Conversion**: 80+ value converters declared in XAML and applied per column via `DisplayBinding`, covering percentage, price, volatility, basis points, yield, spread, ticks, quantity, and notional value formatting
- **Conditional Styling**: Foreground, background, font-weight, and visibility converters that dynamically color-code price movements, risk exposure, execution status, liquidity, anomalies, and threshold warnings
- **Custom Sort Logic**: A custom `SortComparer` (`FinancialComparer`) registered on the `AskPrice` column via `dataGrid.SortComparers`

## Project Structure

```
SfDataGrid_Demo/
├── App.xaml                              # WPF Application definition
├── App.xaml.cs                           # Application code-behind
├── MainWindow.xaml                       # Main UI with SfDataGrid, 100+ styled columns, and status bar
├── MainWindow.xaml.cs                    # Data generation, sort comparer setup, and scroll timing logic
├── ViewModel.cs                          # ViewModel with INotifyPropertyChanged for MVVM binding
├── Converters/
│   ├── FinancialConverters.cs            # 40+ financial value and styling converters + FinancialComparer
│   ├── GenericValueConverter.cs          # Generic per-column converter and row background style converter
│   └── RubyHelper.cs                     # Ruby-style color helper for converter brushes
├── Models/
│   └── FinancialRowData.cs               # Financial data model with 70+ properties (identity, pricing, Greeks, risk, execution)
├── Properties/                           # Assembly info and resources
├── packages/                            # Syncfusion assemblies (Syncfusion.Data.WPF, Syncfusion.SfGrid.WPF, Syncfusion.Shared.WPF)
└── SfDataGrid_Demo.csproj               # Project file (.NET Framework 4.7.2)
```

## How It Works

### 1. **Data Model (FinancialRowData.cs)**
- Represents a financial trading row with 70+ properties grouped by category: identity and instrument columns (Symbol, Underlying, OptionType, StrikePrice), pricing (BidPrice, AskPrice, TheoreticalPrice), volatility, quantities, Greeks (Delta, Gamma, Theta, Vega, Rho), risk and exposure (PnL, VaR, RiskLimit), execution (ExecutionStatus, OrderID), liquidity and spread, correlation and hedging, time decay, market data, status flags, and accounting values
- `InitializeRandomData(Random)` populates realistic values with a fixed seed (42) for consistent benchmarking

### 2. **ViewModel (ViewModel.cs)**
- Implements `INotifyPropertyChanged` with a `SetProperty` helper for MVVM data binding
- Exposes an `ObservableCollection<FinancialRowData>` for the grid's `ItemsSource`

### 3. **Value and Styling Converters (FinancialConverters.cs, GenericValueConverter.cs)**
- **Value converters**: `PercentageConverter`, `DecimalConverter`, `PriceConverter`, `VolatilityConverter`, `BasisPointsConverter`, `YieldConverter`, `SpreadConverter`, `TicksConverter`, `QuantityConverter`, `NotionalValueConverter`, and more
- **Conditional styling converters**:
  - *Foreground*: `PriceMoveForegroundConverter`, `PercentageChangeForegroundConverter`, `RiskForegroundConverter`, `BidAskForegroundConverter`, `VolatilityTrendForegroundConverter`, `StatusForegroundConverter`, `ExecutionStatusForegroundConverter`, `LiquidityForegroundConverter`, `CorrelationForegroundConverter`
  - *Background*: `PriceAlertBackgroundConverter`, `VolatilityAlertBackgroundConverter`, `LimitExceededBackgroundConverter`, `ThresholdWarningBackgroundConverter`, `ErrorStateBackgroundConverter`, `HighValueBackgroundConverter`, `NegativeValueBackgroundConverter`, `OutOfRangeBackgroundConverter`, `AnomalyDetectionBackgroundConverter`
  - *Font weight*: `HighPriorityFontWeightConverter`, `CriticalValueFontWeightConverter`, `ExecStatusFontWeightConverter`, `ActiveSessionFontWeightConverter`
  - *Visibility*: `NullToVisibilityConverter`, `BoolToVisibilityConverter`, `ZeroToVisibilityConverter`
- **Row styling**: `RowBackgroundStyleConverter` and `AlternateRowStyleConverter` apply green, red, yellow, or transparent row backgrounds based on data conditions or row parity
- **Custom sorting**: `FinancialComparer` implements `IComparer<object>` for domain-specific sorting

### 4. **User Interface (MainWindow.xaml)**
- SfDataGrid configured with:
  - `AllowFiltering="True"` / `AllowSorting="True"` / `AllowEditing="True"` — interactive grid operations
  - `AutoGenerateColumns="False"` — 100+ explicitly defined `GridTextColumn`s, each with a `MappingName`, `Width="60"`, and a `DisplayBinding` that applies a converter
  - `SelectionMode="Extended"` / `SelectionUnit="Row"` — multi-row selection
  - `HeaderRowHeight="60"` / `RowHeight="25"` — compact rows for dense data display
  - `FrozenColumnCount="1"` — the identity column stays visible during horizontal scrolling
  - `AllowDraggingColumns="True"` / `AllowResizingColumns="True"` — column reordering and resizing
- Column `CellStyle` resources apply conditional triggers per column (e.g., `GridCellStyle`, `HeaderCellStyle`, `EditableCellStyle`, `DataCellStyle`)
- Row styles target `syncfusion:VirtualizingCellsControl` (including an `AlternatingRowStyle`) to leverage UI virtualization

## Technology Stack

- **Framework**: .NET Framework 4.7.2
- **Language**: C#
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Component**: Syncfusion DataGrid for WPF (SfGrid.WPF 14.2.0.28, with Syncfusion.Data.WPF and Syncfusion.Shared.WPF)

## Getting Started

### Prerequisites
- Visual Studio 2017 or higher
- .NET Framework 4.7.2 or higher
- Syncfusion WPF components (installed via NuGet; Syncfusion license registration required for the application)

### Setup Instructions

1. **Clone or Download** the repository
   ```bash
   git clone https://github.com/SyncfusionExamples/How-to-Improve-SfDataGrid-Performance-for-Large-Datasets-with-Conditional-Styling.git
   ```

2. **Open in Visual Studio**
   - Open `SfDataGrid_Demo.slnx` in Visual Studio

3. **Restore NuGet Packages**
   - Visual Studio will automatically restore Syncfusion dependencies
   - Or manually run: `nuget restore`

4. **Register the Syncfusion License**
   - Register a valid Syncfusion license key in `App.xaml.cs` before initializing components

5. **Build and Run**
   - Build the solution (Ctrl+Shift+B)
   - Run the application (F5)

## Usage Guide

1. **Run the Application**
   - The window loads a 2,000-row × 100+-column financial grid with conditional styling applied
   
2. **Scroll Horizontally**
   - Use the scrollbar thumb or mouse wheel to scroll across the 100+ columns; the first (frozen) column stays visible
   - A message box reports the measured horizontal scroll/render duration when scrolling stops

3. **Interact with the Grid** (Optional)
   - Click the filter icon in any column header to filter records
   - Click a column header to sort (the `AskPrice` column uses the custom `FinancialComparer`)

## Use Cases

- **Financial Trading Dashboards**: Render thousands of option/instrument rows with live, value-based color coding (profit/loss, alerts, anomalies)
- **Performance Benchmarking**: Measure load, scroll, and render times for large virtualized grids with heavy converter usage

## Key Implementation Details

- The SfDataGrid is loaded with 2,000 rows and 200 columns of financial data
- Multiple value converters are used for conditional styling (foreground, background, font weight, and visibility) across the columns, applied per column through `DisplayBinding`
- A custom comparer (`FinancialComparer`) is registered via `SortComparers` on the `AskPrice` column for domain-specific sorting

## Important Notes

- **Virtualization**: Supports row and column virtualization for efficient handling of large numbers of records and columns.
- **Converter design**: Keep converters stateless and lightweight — they execute for every visible cell during scrolling
- **Sample Data**:  The application includes 2000 rows with the 200 columns.
- **Syncfusion License**: Requires a valid Syncfusion license; evaluation mode is available for 30 days

## Requirements

- Visual Studio 2017 or higher
- .NET Framework 4.7.2 or higher
- Syncfusion WPF components (obtained via NuGet package manager)
