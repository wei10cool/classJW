using System;
using System.Globalization;

namespace classJW
{
    public static class StringExtensions
    {
        /// <summary>
        /// 輸入西元年yyymmdd，輸出民國年yyymmdd
        /// </summary>
        /// <param name="dateStr">yyyymmdd</param>
        /// <returns>yyymmdd</returns>
        public static string JW_2TaiwanDateStr(this String dateStr)
        {
            int yy = Convert.ToInt16(dateStr.Substring(0, 4));
            int mm = Convert.ToInt16(dateStr.Substring(4, 2));
            int dd = Convert.ToInt16(dateStr.Substring(6, 2));
            DateTime datetime = new DateTime(yy,mm,dd);
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

            return string.Format("{0}{1}{2}",
                taiwanCalendar.GetYear(datetime),
                datetime.Month,
                datetime.Day);
        }
    }
}
