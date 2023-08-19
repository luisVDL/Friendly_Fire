using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text m_ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        m_ScoreText.text = "SCORE  " + 0;
        EnemySpawner.SubscribeToScoreEvent(this);
    }

    public void ChangeScore(int l_Score)
    {
        m_ScoreText.text = "SCORE  " + l_Score;
    }
}
