using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace SfDataGrid_Demo
{
    /// <summary>
    /// ViewModel for SfDataGrid with MVVM pattern support
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FinancialRowData> _rows;

        public ObservableCollection<FinancialRowData> Rows
        {
            get { return _rows; }
            set { SetProperty(ref _rows, value); }
        }

        public ViewModel()
        {
            GenerateRows();
        }

        private void GenerateRows()
        {
            Rows = new ObservableCollection<FinancialRowData>();
            Random rand = new Random(42); // Seed for consistency

            string[] symbols = { "AAPL", "MSFT", "GOOGL", "AMZN", "TSLA", "FB", "NVDA", "JPM", "GS", "BAC" };
            string[] optionTypes = { "Call", "Put" };

            // Generate 2000 rows
            for (int row = 0; row < 2000; row++)
            {
                var symbol = symbols[row % symbols.Length];
                var optionType = optionTypes[row % 2];

                var rowData = new FinancialRowData(row)
                {
                    Symbol = symbol,
                    Underlying = symbol,
                    OptionType = optionType,
                    StrikePrice = 100 + (row % 10) * 10,
                    IsVIP = (row % 50 == 0),
                    IsFirstByUnderlying = (row % 20 == 0),
                    State = GetRandomState(rand),
                    AlertFlag = row % 100 == 0 ? "WARNING" : "OK"
                };

                // Initialize with realistic financial data
                rowData.InitializeRandomData(rand);

                Rows.Add(rowData);
            }
        }

        private string GetRandomState(Random rand)
        {
            int stateIndex = rand.Next(0, 3);
            switch (stateIndex)
            {
                case 0:
                    return "Running";
                case 1:
                    return "Stopping";
                default:
                    return "Stopped";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
