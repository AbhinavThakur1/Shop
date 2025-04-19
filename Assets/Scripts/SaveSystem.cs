using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(DataFormat dataFormat)
    {
        BinaryFormatter _formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path))
        {
            File.AppendAllText(path, JsonUtility.ToJson(dataFormat));
        }
        else
        {
            File.WriteAllText(path, JsonUtility.ToJson(dataFormat));
        }
    }

    [System.Serializable]
    public class DataFormat
    {
        public string _customerName;
        public string _sumtotal;
        public string _date;
    }
}
