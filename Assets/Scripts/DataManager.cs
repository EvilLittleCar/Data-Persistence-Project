using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    //public int bestScore = 0;
    //public string bestName = "   ";
    public string myName;
    public int[] myHiScores = new int [5]{ 0, 0, 0, 0, 0 };
    public string[] myHiDates= new string[5] { "", "", "", "", "" };
    public string[] myHiNames= new string[5] { "", "", "", "", "" };

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
       // public int highScore;
        //public string highName;
        public string myName;
        public int[] myHiScores = new int[5] { 0, 0, 0, 0, 0 };
        public string[] myHiDates = new string[5] { "", "", "", "", "" };
        public string[] myHiNames = new string[5] { "", "", "", "", "" };
     
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.myName = myName;
      
        data.myHiScores = myHiScores;
        data.myHiDates = myHiDates;
        data.myHiNames = myHiNames;


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        Debug.Log("LoadScores from save data");

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            myName = data.myName;
       
            myHiScores = data.myHiScores;
            myHiDates = data.myHiDates;
            myHiNames = data.myHiNames;
        }
        else
        {
            Debug.Log("Save not found, loading fresh values");
           
             myHiScores = new int[5] { 0, 0, 0, 0, 0 };
             myHiDates = new string[5] { "", "", "", "", "" };
             myHiNames = new string[5] { "", "", "", "", "" };
}
    }

}
