using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    public class StorageController : MonoSingleton<StorageController>
    {
        private const string PATH = "data.json";

        public SaveData data = new SaveData();

        public override void Init()
        {
            base.Init();
            Read();
        }

        public void Save()
        {
            var json = JsonUtility.ToJson(data);
#if UNITY_WEBGL
            PlayerPrefs.SetString(PATH, json);
#else
            var path = Application.persistentDataPath + '/' + PATH;
            File.WriteAllText(path, json);
#endif

        }

        public void Read()
        {
            var json = string.Empty;
#if UNITY_WEBGL
            json = PlayerPrefs.GetString(PATH);
#else
            var path = Application.persistentDataPath + '/' + PATH;
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
#endif
            if (!string.IsNullOrEmpty(json))
            {
                data = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                data = new SaveData();
            }
        }
     

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        private void OnApplicationPause(bool pause)
        {
            Save();
        }
#else

        private void OnApplicationQuit()
        {
            Save();
        }
#endif
    }
}