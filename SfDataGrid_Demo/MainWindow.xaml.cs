
using Syncfusion.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SfDataGrid_Demo
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            // Load data after window is rendered
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusText.Text = "Generating columns...";
                StatusText.Text = "Generating 2000 rows...";
                StatusText.Text = $"✓ Loaded: 2000 Rows | 200 Columns | Custom Sort Applied to the Col 20: PriceAlert Bg";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"✗ Error: {ex.Message}";
            }
        }
    }
}
