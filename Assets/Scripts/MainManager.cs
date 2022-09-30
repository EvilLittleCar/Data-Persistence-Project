using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public List<Brick> myBricks = new List<Brick>();
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    public GameObject NewHiScorePanel;
    //public GameObject EnterTextInputField;
 
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private bool m_newHiScore = false;

    private int currentHiScore = DataManager.Instance.myHiScores[0];

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                myBricks.Add(brick);
            }
        }

        UpdateScoreText();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //launches the ball
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !m_newHiScore) //space reloads game but not if input hi score is up
            {
                updateScores();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) //move to Main menu no matter what
            {
                updateScores();
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score: {m_Points}";
        CheckHiScore();

    }

     void CheckHiScore()
    {
        if (m_Points > currentHiScore) { currentHiScore = m_Points; }
      
        for (int i = 0; i < 5 ; i++)
        {
            if (m_Points > DataManager.Instance.myHiScores[i]) { m_newHiScore = true; }
        }
        UpdateScoreText();
    }

    public void updateScores()
    {
        int tempScore1, tempScore2;
        string tempDate1, tempDate2;
        string tempName1, tempName2;

        tempScore1 = m_Points;
        tempName1 = DataManager.Instance.myName;
        tempDate1 = DateTime.Today.ToShortDateString();

        for (int i = 0; i < 5; i++)
        {
            if (tempScore1 > DataManager.Instance.myHiScores[i])
            {
                tempScore2 = DataManager.Instance.myHiScores[i];
                tempName2 = DataManager.Instance.myHiNames[i];
                tempDate2 = DataManager.Instance.myHiDates[i];
               // Debug.Log("Temp1 (" + tempScore1 + ") > Temp2 (" + tempScore2 + ")");

                DataManager.Instance.myHiScores[i] = tempScore1;
                DataManager.Instance.myHiNames[i] = tempName1;
                DataManager.Instance.myHiDates[i] = tempDate1;
                tempScore1 = tempScore2;
                tempName1 = tempName2;
                tempDate1 = tempDate2;
            }
        }

        UpdateScoreText();
        
    }


    public void UpdateScoreText()
    {
        BestScoreText.text = "Hi Score  " + currentHiScore;

    }
    public void GameOver()
    {
        m_GameOver = true;

        for (int i = 0; i < myBricks.Count; i++)
        {
            if (myBricks[i] != null) {Destroy(myBricks[i].gameObject); }
             
        }

        GameOverText.SetActive(true);
        if (m_newHiScore)
        {
            NewHiScorePanel.SetActive(true);
        }
       // bool myTest = true;
       
      
    }

    public void EnterName(TextMeshProUGUI myString)
    {
        Debug.Log("Changing name to" + myString.text);
        DataManager.Instance.myName = myString.text;
        m_newHiScore = false;
       
        NewHiScorePanel.SetActive(false);
        //EnterTextInputField.SetActive(false);
    }

}
