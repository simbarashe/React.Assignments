using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LoggingFramework
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DisplayNameAttribute>(false)
                      ?.DisplayName
                      ?? val.ToString();
        }
    }
}
