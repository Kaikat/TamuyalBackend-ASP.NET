﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
namespace WebApplication1.Controllers
{
    public static class ExtensionMethods
    {
        public static void AddParameter(this SqlCommand command, string parameterName, object value)
        {
            if (command.CommandText.Contains(parameterName))
            {
                command.Parameters.AddWithValue(parameterName, value);
            }
        }

        public static string GetQueryText(this SqlCommand command)
        {
            string query = command.CommandText;
            foreach (SqlParameter p in command.Parameters)
            {
                query = query.Replace(p.ParameterName, p.Value.ToString());
            }

            return query;
        }

        public static int ToInt(this object readerObject)
        {
            return Int32.Parse(readerObject.ToString());
        }

        public static float ToFloat(this object readerObject)
        {
            return float.Parse(readerObject.ToString());
        }

        public static T ToEnum<T>(this string enumString) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }
    }
}