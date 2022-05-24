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
        bestScoreDisplay.text = "Best Score: " + DataManager.Instance.bestName + " : " + DataManager.Instance.bestScore;
        DataManager.Instance.myName = "   ";
    }

    public void EnterName(TextMeshProUGUI myString)
    {
        Debug.Log("Changing name to" + myString.text);
        DataManager.Instance.myName = myString.text;
    }

    public void StartNewGame()
    {
        if (DataManager.Instance.myName == null)
            DataManager.Instance.myName = "   ";
        SceneManager.LoadScene(1);
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
