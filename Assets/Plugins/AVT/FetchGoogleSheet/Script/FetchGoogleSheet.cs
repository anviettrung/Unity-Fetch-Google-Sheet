using System.Collections.Generic;

namespace Plugins.AVT.FetchGoogleSheet
{
    public static class FetchGoogleSheet
    {
        public static void SheetToList<T>(this UnityEngine.Object obj, 
            string url, 
            List<T> list) 
            where T : IGoogleSheetDataSetter, new()
        {
            obj.GetRawTextFromUrl(url, (success, result) =>
            {
                if (!success)
                {
                    UnityEngine.Debug.Log(result); 
                    return;
                }
                
                list.FromSheetData(result);
            });
        }
    }

    public interface IGoogleSheetDataSetter
    {
        void SetDataFromSheet(Dictionary<string, string> source);
    }
}

