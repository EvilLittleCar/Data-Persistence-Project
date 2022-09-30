using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    public TextMeshProUGUI bestScoreDisplay;

    private void Start()
    {
        Debug.Log("Starting scene");
        
        bestScoreDisplay.text = "Best Score\n " + DataManager.Instance.myHiNames[0] + " : " + DataManager.Instance.myHiScores[0] + "    " + DataManager.Instance.myHiDates[0];
        DataManager.Instance.myName = "   ";
    }

   

    public void StartNewGame()
    {
        if (DataManager.Instance.myName == null)
            DataManager.Instance.myName = "   ";
        SceneManager.LoadScene(1);
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        DataManager.Instance.SaveScores();
    }
}
