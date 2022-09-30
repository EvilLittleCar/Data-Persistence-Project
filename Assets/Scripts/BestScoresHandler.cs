using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BestScoresHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {

        UpdateScores();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }
     void UpdateScores()
    {
        bestScoreDisplay.text = "Best Scores: \n\n";

        for (int i = 0; i < 5; i++)
        {
            bestScoreDisplay.text += (i+1) + ". " + DataManager.Instance.myHiNames[i] + "   " + DataManager.Instance.myHiScores[i] + "    " + DataManager.Instance.myHiDates[i] + "\n";
        }
    }


    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ClearScoresButton()
    {
        for (int i = 0; i < 5; i++)
        {
            DataManager.Instance.myHiScores = new int[5] { 0, 0, 0, 0, 0 };
            DataManager.Instance.myHiDates = new string[5] { "", "", "", "", "" };
            DataManager.Instance.myHiNames = new string[5] { "", "", "", "", "" };
        }
        UpdateScores();
    }
}
