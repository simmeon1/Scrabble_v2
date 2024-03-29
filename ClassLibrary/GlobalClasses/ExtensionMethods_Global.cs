﻿using Newtonsoft.Json;

namespace ClassLibrary
{
    public static class ExtensionMethods_Global
    {
        public static string ToJson(this object obj, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static char ToUpper(this char c)
        {
            return c.ToUpper();
        }
    }
}
