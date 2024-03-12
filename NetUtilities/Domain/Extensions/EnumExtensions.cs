using FastEnumUtility;
using NetUtilities.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NetUtilities.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue
                .GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        public static string ConvertToEnumType(this string type)
        {
            if (type.Equals("DEBUG") || type.Equals("DBG"))
            {
                type = "DEBUG";
            }
            return type;
        }

        public static string GetValueMenuEnum(this HomeMenuType homeMenuType)
        {
            var field = homeMenuType.GetType().GetField(homeMenuType.ToString());
            var attributes = field.GetCustomAttributes(typeof(LabelAttribute), false) as LabelAttribute[];

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Value;
            }

            return null;
        }
    }
}
