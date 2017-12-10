using System;
using System.Globalization;

namespace Bitcoin.Core
{
    public static class Utils
    {
        private static readonly DateTime EpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public static DateTime DateTimeFromTimeStamp(long timestamp)
        {
            return EpochStart.AddSeconds(timestamp).ToLocalTime();
        }

        public static string BeautifyString(string input)
        {
            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
              return textInfo.ToTitleCase(input);
        }
    }
}