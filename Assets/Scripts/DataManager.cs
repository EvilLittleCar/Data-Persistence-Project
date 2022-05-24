using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int bestScore = 0;
    public string bestName = "   ";
    public string myName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScores();
        }
    }


    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highName;
        public string myName;
    }
    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.myName = myName;
        data.highName = bestName;
        data.highScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            myName = data.myName;
            bestName = data.highName;
            bestScore = data.highScore;
        }
        else
        {
            myName = "   ";
            bestName = "   ";
            bestScore = 0;
        }
    }

}
