﻿using System;

namespace GoldenLeague.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime BeginOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }
    }
}
