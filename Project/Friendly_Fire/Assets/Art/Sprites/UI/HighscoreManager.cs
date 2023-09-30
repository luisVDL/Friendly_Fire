using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighscoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_HighScoreText;
    [SerializeField] private GameObject m_FirstSelectedDeathButton;
    [SerializeField] private TextMeshProUGUI m_CurrentScoreText;

    // Start is called before the first frame update
    public void ShowScore()
    {
        int l_CurrentScore = NewEnemyManager.GetScore();
        int l_Highscore = PlayerPrefs.GetInt("Highscore");
        if (l_Highscore < l_CurrentScore)
        {
            PlayerPrefs.SetInt("Highscore", l_CurrentScore);
            l_Highscore = l_CurrentScore;
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_FirstSelectedDeathButton);
        m_HighScoreText.SetText("Highscore: " + l_Highscore);
        m_CurrentScoreText.SetText("Score: " + l_CurrentScore);

    }
}
