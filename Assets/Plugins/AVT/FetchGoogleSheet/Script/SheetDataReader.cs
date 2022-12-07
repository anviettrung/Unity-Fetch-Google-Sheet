using System;
using System.Collections.Generic;

namespace Plugins.AVT.FetchGoogleSheet
{
    public static class SheetDataReader
    {
        public static void FromSheetData<T>(this List<T> list, 
            string sheet, 
            SheetFormat format = SheetFormat.CSV)
            where T : IGoogleSheetDataSetter, new()
        {
            var rows = sheet.ToRows();
            var propKeys = rows[0].ToCells(format);
            
            list.Clear();
            for (var i = 1; i < rows.Length; i++)
            {
                var record = new T();
                var propValues = rows[i].ToCells(format);
                record.SetDataFromSheet(CreateRecord(propKeys, propValues));
                list.Add(record);
            }
        }
        
        public static string[] ToRows(this string source) 
            => source.Split("\n"[0]);
        public static string[] ToCells(this string source, params char[] separator) 
            => source.Trim().Split(separator);

        public static string[] ToCells(this string source, 
            SheetFormat format = SheetFormat.CSV)
        {
            switch (format)
            {
                case SheetFormat.CSV:
                    return source.ToCells(","[0]);
                case SheetFormat.TSV:
                    return source.ToCells("\t"[0]);
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }

        public static Dictionary<string, string> CreateRecord(string[] keys, string[] values)
        {
            var result = new Dictionary<string, string>();
            for (var i = 0; i < keys.Length && i < values.Length; i++)
                result.Add(keys[i], values[i]);

            return result;
        }
    }

    public enum SheetFormat
    {
        CSV,
        TSV
    }
}

