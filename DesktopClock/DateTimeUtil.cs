﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopClock;

public static class DateTimeUtil
{
    /// <summary>
    /// A collection of DateTime formatting strings.
    /// </summary>
    public static IReadOnlyList<string> DateTimeFormats { get; } = new[]
   {
    "M",
    "dddd, MMMM dd",
    "dddd, MMMM dd, HH:mm z",
    "dddd, MMM dd, HH:mm z",
    "dddd, MMM dd, HH:mm:ss z",
    "ddd, MMMM dd, HH:mm z",
    "ddd, MMMM dd, HH:mm:ss z",
    "ddd, MMM dd, HH:mm z",
    "ddd, MMM dd, HH:mm:ss z",
    "ddd, MMM dd, HH:mm K",
    "d",
    "g",
    "G",
    "t",
    "T",
    "O",
};

    /// <summary>
    /// Common date time formatting strings and an example string for each.
    /// </summary>
    public static IReadOnlyDictionary<string, string> DateTimeFormatsAndExamples { get; } =
        DateTimeFormats.ToDictionary(f => f, DateTimeOffset.Now.ToString);

    public static IReadOnlyCollection<TimeZoneInfo> TimeZones { get; } = TimeZoneInfo.GetSystemTimeZones();

    public static bool TryGetTimeZoneById(string timeZoneId, out TimeZoneInfo timeZoneInfo)
    {
        try
        {
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return true;
        }
        catch (TimeZoneNotFoundException)
        {
            timeZoneInfo = null;
            return false;
        }
    }
}