using System;
using System.Collections.Generic;

namespace SfDataGrid_Demo
{
    public class FinancialComparer : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            var item1 = x as FinancialRowData;
            var item2 = y as FinancialRowData;

            if (item1 == null || item2 == null)
                return 0;

            // Primary Sort - Last Price
            int result = item1.LastPrice.CompareTo(item2.LastPrice);

            if (result != 0)
                return result;

            // Secondary Sort - ExpiryDate
            result = item1.ExpiryDate.CompareTo(item2.ExpiryDate);

            if (result != 0)
                return result;

            // Tertiary Sort - Symbol
            return string.Compare(
            item1.Symbol,
            item2.Symbol,
            StringComparison.OrdinalIgnoreCase);
        }
    }

   
}
