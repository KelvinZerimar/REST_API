using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Nancy;
using System.IO;

namespace Aka.Microservice.Framework.Extensions
{

    /// <summary>
    /// Clase estática con métodos de extensión para las clases del Framework
    /// </summary>
    public static class Extensions
    {

        public static string APIBasePath = "/" + ConfigurationManager.AppSettings["APIVersion"].ToNStr();

        /// <summary>
        /// Converts a stream to a nancy response
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Response ToResponse(this string str)
        {
            Response resp = new Response();
            resp.Contents = stream =>
            {
                using (var tmpStream = new MemoryStream(Encoding.UTF8.GetBytes(str.ToString())))
                    tmpStream.CopyTo(stream);
            };
            return resp;

        }


        #region Métodos de extensión para colecciones de objetos - tipos

        /// <summary>
        /// Obtiene el array de bytes de la lista
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <returns>Array de bytes</returns>
        public static byte[] ToByteArray(this IEnumerable<int> lista)
        {
            if (lista.Count() == 0) return new byte[0];
            List<byte> res = new List<byte>();
            byte[] bytes = null;
            foreach (int value in lista)
            {
                bytes = BitConverter.GetBytes(value);
                Array.Reverse(bytes);
                res.AddRange(bytes);
            }
            return res.ToArray();
        }

        /// <summary>
        /// Obtiene el array de bytes de la lista
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <returns>Array de bytes</returns>
        public static byte[] ToByteArray(this IEnumerable<long> lista)
        {
            if (lista.Count() == 0) return new byte[0];
            List<byte> res = new List<byte>();
            byte[] bytes = null;
            foreach (long value in lista)
            {
                bytes = BitConverter.GetBytes(value);
                Array.Reverse(bytes);
                res.AddRange(bytes);
            }
            return res.ToArray();
        }
        /// <summary>
        /// Obtiene el array de bytes de la lista
        /// </summary>
        /// <param name="lista">Lista</param>
        /// <returns>Array de bytes</returns>
        public static byte[] ToByteArray(this IEnumerable<byte> lista)
        {
            if (lista.Count() == 0) return new byte[0];
            List<byte> res = new List<byte>();
            byte[] bytes = null;
            foreach (byte value in lista)
            {
                bytes = BitConverter.GetBytes(value);
                Array.Reverse(bytes);
                res.AddRange(bytes);
            }
            return res.ToArray();
        }



        #endregion

        #region Métodos de extensión para tipos nulables de BBDD

        /// <summary>
        /// Convierte un objeto de BD en un entero de 32 bits con signo equivalente, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Entero de 32 bits con signo equivalente</returns>
        public static int ToInt(this object obj, int defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// Convierte un objeto de BD en un entero de 32 bits con signo equivalente
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Entero de 32 bits con signo equivalente</returns>
        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// Convierte un objeto de BD en un char
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Elemento char</returns>
        public static char ToChar(this object obj)
        {
            return Convert.ToChar(obj);
        }
        /// <summary>
        /// Convierte un objeto de BD en un entero de 32 bits con signo equivalente, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Entero de 32 bits con signo equivalente o null</returns>
        public static int? ToNInt(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// Convierte un null de BD a un entero de 16 bits o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Objeto con el valor de BD</param>
        /// <returns>Entero de 16 bits o null</returns>
        public static short? ToNShort(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            return Convert.ToInt16(obj);
        }
        /// <summary>
        /// Convierte un null de BD a un entero de 16 bits o en defaultValue si el valor es nulo
        /// </summary>
        /// <param name="obj">Objeto con el valor de BD</param>
        /// <param name="defaultValue">Valor a retornar cuando el parametro obj es nulo</param>
        /// <returns>Entero de 16 bits o defaultValue</returns>
        public static short ToShort(this object obj, short defaultValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defaultValue;
            return Convert.ToInt16(obj);
        }
        /// <summary>
        /// Convierte un objeto de BD en un entero de 32 bits con signo equivalente, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object string</param>
        /// <returns>Entero de 32 bits o un null</returns>
        public static int? ToNIntString(this string obj)
        {
            if (obj == null || obj == string.Empty) return null;
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un entero de 64 bits con signo equivalente, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Entero de 64 bits con signo equivalente</returns>
        public static long ToLong(this object obj, long defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un entero de 64 bits con signo equivalente
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Entero de 64 bits con signo equivalente</returns>
        public static long ToLong(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un entero de 64 bits con signo equivalente, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Entero de 64 bits con signo equivalente o null</returns>
        public static long? ToNLong(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj) || obj == string.Empty) return null;
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un entero de 64 bits con signo equivalente, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Entero de 64 bits con signo equivalente o null</returns>
        public static long? ToNLongString(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj) || obj.ToString() == string.Empty) return null;
            return Convert.ToInt64(obj);

        }

        /// <summary>
        /// Convierte un objeto de BD en un número de punto flotante con precisión simple, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Número de punto flotante con precisión simple</returns>
        public static float ToFloat(this object obj, float defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToSingle(obj);
        }


        /// <summary>
        /// Convierte un objeto de BD en un número de punto flotante con precisión simple
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Número de punto flotante con precisión simple</returns>
        public static float ToFloat(this object obj)
        {
            return Convert.ToSingle(obj);
        }


        /// <summary>
        /// Convierte un objeto de BD en un número de punto flotante con precisión simple, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Número de punto flotante con precisión simple</returns>
        public static float? ToNFloat(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            return Convert.ToSingle(obj);

        }

        /// <summary>
        /// Convierte un objeto de BD en un número de punto flotante con precisión simple, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Número de punto flotante con precisión simple</returns>
        public static float? ToNFloatString(this string obj)
        {
            if (obj == null || obj == string.Empty) return null;
            return Convert.ToSingle(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo DateTime, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this object obj, DateTime defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo DateTime
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo DateTime, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>DateTime o null</returns>
        public static DateTime? ToNDateTime(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch { return null; }
        }

        /// <summary>
        /// Convierte un objeto string a un datetime
        /// </summary>
        /// <param name="obj">Objeto de entrada</param>
        /// <returns>Datetime o el valor maximo de la fecha</returns>
        public static DateTime ToDateTimeString(this string obj)
        {
            if (obj == null || obj == string.Empty) return DateTime.MaxValue;
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch { return DateTime.MaxValue; }
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Boolean, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Bool</returns>
        public static bool ToBool(this object obj, bool defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Boolean
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Bool</returns>
        public static bool ToBool(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Boolean, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Bool o null</returns>
        public static bool? ToNBool(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Double, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Double</returns>
        public static double ToDouble(this object obj, double defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToDouble(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Double
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Double</returns>
        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Double, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Double o null</returns>
        public static double? ToNDouble(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            return Convert.ToDouble(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Decimal, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(this object obj, decimal defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Decimal
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo Decimal, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>Decimal o null</returns>
        public static decimal? ToNDecimal(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo String, o en el valor por defecto
        /// pasado por parámetro si es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <param name="defValue">Valor por defecto si el objecto DB es nulo</param>
        /// <returns>Decimal</returns>
        public static string ToStr(this object obj, string defValue)
        {
            if (obj == null || Convert.IsDBNull(obj)) return defValue;
            return Convert.ToString(obj);
        }

        /// <summary>
        /// Convierte un objeto de BD en un tipo String, o en string.Empty si el valor es nulo
        /// </summary>
        /// <param name="obj">Object con el campo de la BD</param>
        /// <returns>String o string.Empty</returns>
        public static string ToNStr(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return string.Empty;
            return Convert.ToString(obj);
        }

        /// <summary>
        /// Convierte un objeto en un tipo Dbnull, si dicho objeto es nulo
        /// </summary>
        /// <param name="obj">Object con el dato a convertir</param>
        /// <returns>Object</returns>
        public static object ToDBNull(this object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return Convert.DBNull;
            return obj;
        }

        public static object ToStringDBNull(this string obj)
        {
            if (obj == null || Convert.IsDBNull(obj) || obj == string.Empty) return Convert.DBNull;
            return obj;
        }

        public static object ToDateTimeDBNull(this DateTime obj)
        {
            if (obj == null || Convert.IsDBNull(obj) || obj == DateTime.MaxValue || obj == DateTime.MinValue || obj.Year <= 1900)
                return DBNull.Value;
            return obj;
        }

        public static object ToDateTimeDBNull(this DateTime? obj)
        {
            if (obj == null || Convert.IsDBNull(obj) || obj.Value == DateTime.MaxValue || obj.Value == DateTime.MinValue || obj.Value.Year <= 1900)
                return DBNull.Value;
            return obj;
        }


        /// <summary>
        /// Convierte un string en un entero de 32 bits con signo equivalente, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">String</param>
        /// <returns>Entero de 32 bits con signo equivalente o null</returns>
        public static int? ToNIntEmpty(this string obj)
        {
            if (obj.Trim() != string.Empty)
                return obj.ToInt();
            else
                return null;
        }

        /// <summary>
        /// Convierte un string en un entero de 64 bits con signo equivalente, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">String</param>
        /// <returns>Entero de 64 bits con signo equivalente o null</returns>
        public static long? ToNLongEmpty(this string obj)
        {
            if (obj.Trim() != string.Empty)
                return obj.ToLong();
            else
                return null;
        }

        /// <summary>
        /// Convierte un string en un número de punto flotante con precisión simple, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">String</param>
        /// <returns>Número de punto flotante con precisión simple o null</returns>
        public static float? ToNFloatEmpty(this string obj)
        {
            if (obj.Trim() != string.Empty)
                return obj.ToFloat();
            else
                return null;
        }

        /// <summary>
        /// Convierte un string en DateTime, o en null si el valor es nulo
        /// </summary>
        /// <param name="obj">String</param>
        /// <returns>DateTime o null</returns>
        public static DateTime? ToNDateTimeEmpty(this string obj)
        {
            if (obj.Trim() != string.Empty)
                return obj.ToDateTime();
            else
                return null;
        }


        #endregion


        #region #lists

        public static List<T> ToNList<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return Enumerable.Empty<T>().ToList();

            return source.ToList();
        }

        public static List<T> ToNList<T>(this IQueryable<T> source)
        {
            if (source == null)
                return Enumerable.Empty<T>().ToList();

            return source.ToList();
        }

        public static List<T> ToNList<T>(this IOrderedQueryable<T> source)
        {
            if (source == null)
                return Enumerable.Empty<T>().ToList();

            return source.ToList();
        }

        #endregion

    }
}
