using System;
using System.Globalization;

namespace SportsStoreApi.Helpers
{
    public static class DateTimeHelper
    {
        public static void ParseDateRange(string from, string to, out DateTime fromDate, out DateTime toDate)
        {
            bool hasFrom = TryParseExact(from, out fromDate);
            bool hasTo = TryParseExact(to, out toDate);
            if (!hasFrom && !hasTo)
            {
                fromDate = DateTime.Today;
                toDate = DateTime.Today;
                return;
            }

            if (hasFrom && hasTo && fromDate > toDate)
            {
                fromDate = DateTime.Parse(to);
                toDate = DateTime.Parse(from);
            }

            if (hasFrom)
            {
                if (fromDate > DateTime.Today) 
                {
                    fromDate = DateTime.Today;
                }
                if (!hasTo)
                {
                    toDate = DateTime.Today;
                }
            }

            if (hasTo)
            {
                if (toDate > DateTime.Today) 
                {
                    toDate = DateTime.Today;
                }
                if (!hasFrom)
                {
                    fromDate = toDate.AddYears(-1);
                }
            }

            bool TryParseExact (string dataIn, out DateTime dataOut) =>
                DateTime.TryParseExact(dataIn, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataOut);

        } // ParseFromToDate




    }
}