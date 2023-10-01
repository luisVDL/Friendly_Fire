using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_HighScoreText;
    [SerializeField] private GameObject m_FirstSelectedDeathButton;
    [SerializeField] private TextMeshProUGUI m_CurrentScoreText;


    [Header("More statistics")] 
    [SerializeField] private TextMeshProUGUI m_FriendsCollected;
    [SerializeField] private TextMeshProUGUI m_ItemsCollected;
    [SerializeField] private TextMeshProUGUI m_EnemiesDefeated;
    [SerializeField] private TextMeshProUGUI m_WaveReached;
    
    
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
        ShowMoreStatistics();

    }

    private void ShowMoreStatistics()
    {
        m_FriendsCollected.text = "Friends collected: " + NewEnemyManager.GetFriendsCollected();
        m_EnemiesDefeated.text = "Enemies Defeated: " + NewEnemyManager.GetEnemiesDefeated();
        m_ItemsCollected.text = "Items collected: " + NewEnemyManager.GetItemsCollected();
        m_WaveReached.text = "Wave reached: " + NewEnemyManager.GetWave() ;
    }
}
