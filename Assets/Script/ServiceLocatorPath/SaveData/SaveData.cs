using System;
using System.Collections.Generic;

namespace SystemOfExtras.SavedData
{
    public class SaveData : ISaveData
    {
        private Dictionary<string, string> dataSaved;

        public SaveData()
        {
            dataSaved = new Dictionary<string, string>();
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
        }

        public string Get(string id)
        {
            if (dataSaved.ContainsKey(id))
            {
                return dataSaved[id];
            }
            throw new Exception($"value from {id} not found");
        }
    }
}