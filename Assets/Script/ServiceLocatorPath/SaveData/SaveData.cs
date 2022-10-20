using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SystemOfExtras.SavedData
{
    public class SaveData : ISaveData
    {
        private Dictionary<string, string> dataSaved;
        private DataCompleted _dataToSave;

        public SaveData()
        {
            dataSaved = new Dictionary<string, string>();
            //Read the data from disk and update the dictionary
            var data = PlayerPrefs.GetString("data");
            Debug.Log($"Data Save {data}");
            if (data == "") return;
            _dataToSave = JsonUtility.FromJson<DataCompleted>(data);
            foreach (var rowOfDataSaved in _dataToSave.data)
            {
                if (dataSaved.ContainsKey(rowOfDataSaved.id))
                {
                    dataSaved[rowOfDataSaved.id] = rowOfDataSaved.value;
                }
                else
                {
                    dataSaved.Add(rowOfDataSaved.id, rowOfDataSaved.value);
                }
            }
        }

        public void Save(string id, string data)
        {
            if (dataSaved.ContainsKey(id))
            {
                dataSaved[id] = data;
            }
            else
            {
                dataSaved.Add(id, data);
            }

            SaveToJson(dataSaved);
        }

        private void SaveToJson(Dictionary<string, string> dictionary)
        {
            _dataToSave = new DataCompleted
            {
                data = new List<RowOfDataSaved>()
            };
            foreach (var rows in dictionary)
            {
                _dataToSave.data.Add(new RowOfDataSaved() { id = rows.Key, value = rows.Value });
            }
            var json = JsonUtility.ToJson(_dataToSave);
            PlayerPrefs.SetString("data", json);
            Debug.Log($"saving {json}");
        }

        public string Get(string id)
        {
            return dataSaved.ContainsKey(id) ? dataSaved[id] : string.Empty;
        }
    }
    
    [Serializable]
    public class RowOfDataSaved
    {
        public string id;
        public string value;
    }

    [Serializable]
    public class DataCompleted
    {
        public List<RowOfDataSaved> data;
    }
}