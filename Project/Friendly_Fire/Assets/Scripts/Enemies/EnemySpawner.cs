using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Score")] 
    private static ScoreDisplay m_Score;
    private static ExpManager m_Exp;
    [SerializeField] private static int m_ChaserScorePoints = 50;

    private static int m_TotalScore;


    [Header("General")] 
    [SerializeField] private int m_MaxNumberOfEnemies = 300;
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_MinDistance = 10f;
    [SerializeField] private float m_MaxDistance = 20f;

    private static int m_TotalNumberOfEnemies = 0;
    //[SerializeField] private List<BoxCollider2D> m_TempleAreas;

//--------------------------------------------------------------------------------------------------------------------------------------------
    //ADDED PENDING TO BE USEFUL
    [Header("Wave Points System")] 
    [SerializeField] private int m_MaxPointsInitialWave = 10;
    private static int m_CurrentMaxPointsPerWave;
    private static int m_CurrentEnemiesAlive;
    [SerializeField] private int m_AddedPointsPerWave = 5;
    
    [SerializeField] private int m_ChaserInvokerPoints = 1;
    [SerializeField] private int m_SniperInvokerPoints = 1;
    [SerializeField] private int m_FourWaysInvokerPoints = 2;
    [SerializeField] private int m_AngelInvokerPoints = 2;
    [SerializeField] private int m_HunterInvokerPoints = 2;
    //FEARFUL DOEN'T SPAWN WITH THE WAVE SYSTEM, BUT WITH A TIME COOLDOWN
    //[SerializeField] private int m_FearfulInvokerPoints = 10;
    
    //--------------------------------------------------------------------------------------------------------------------------------------------
    
    [Header("Limits")]
    [SerializeField] private Transform m_UpLeftLimit;
    [SerializeField] private Transform m_UpRightLimit;
    [SerializeField] private Transform m_DownLeftLimit;
    [SerializeField] private Transform m_DownRightLimit;

    [Header("Chasers")] 
    [SerializeField] private PoolScript m_ChaserPool;
    [SerializeField]private int m_MaxNumberOfChasers = 150;
    [SerializeField] private int m_ChasersPerWave=10;
    [SerializeField] private float m_CooldownSpawnChasersWave = 3f;
    [SerializeField] private float m_CooldownSpawnChasers = 0.4f;
    private static int m_CurrentNumberOfChasers = 0;
    private float m_LastTimeChaserSpawn = -1f;

     
     
    void Start()
    {
        m_LastTimeChaserSpawn = Time.time;
    }
    void Update()
    {

        if (m_LastTimeChaserSpawn + m_CooldownSpawnChasersWave <= Time.time)
            StartCoroutine(ChaserWave());
        
    }
    
    //SPAWNING
    private IEnumerator ChaserWave()
    {
        for (int i = 0; i < m_ChasersPerWave; i++)
        {
            if (m_CurrentNumberOfChasers < m_MaxNumberOfChasers && m_TotalNumberOfEnemies < m_MaxNumberOfEnemies)
            {
                //Get the GameObject
                GameObject l_GameObject = m_ChaserPool.EnableObject();
                //Get The ChaserEnemy Component
                ChaserEnemy l_Chaser = l_GameObject.GetComponent<ChaserEnemy>();
                //Spawn the Chaser at a certain radius from the  Player
                l_Chaser.Spawn(GetRandomPosition(),m_Player);
                m_LastTimeChaserSpawn = Time.time;
                yield return new WaitForSeconds(m_CooldownSpawnChasers);
                m_CurrentNumberOfChasers++;
                m_TotalNumberOfEnemies++;
            }
        }
    }
    
    //GET LOCATION
    private Vector3 GetRandomPosition()
    {
        Vector2 l_position = new Vector2(0,0); //m_TempleAreas[0].transform.position;
        do
        {
            int l_random = Random.Range(0,4);
            switch (l_random)
        {
            case 0:
                l_position =
                    new Vector2(Random.Range(m_Player.position.x + m_MinDistance, m_Player.position.x + m_MaxDistance),
                        Random.Range(m_Player.position.y + m_MinDistance, m_Player.position.y + m_MaxDistance));
                break;
                
            case 1 :
                l_position = 
                    new Vector2(Random.Range(m_Player.position.x - m_MaxDistance, m_Player.position.x - m_MinDistance),
                        Random.Range(m_Player.position.y + m_MinDistance, m_Player.position.y + m_MaxDistance));

                break;
                
            case 2:
                l_position =
                    new Vector2(Random.Range(m_Player.position.x + m_MinDistance, m_Player.position.x + m_MaxDistance),
                        Random.Range(m_Player.position.y - m_MaxDistance, m_Player.position.y - m_MinDistance));
                break;
                
            default:
                l_position =
                    new Vector2(Random.Range(m_Player.position.x - m_MaxDistance, m_Player.position.x - m_MinDistance),
                        Random.Range(m_Player.position.y - m_MaxDistance, m_Player.position.y - m_MinDistance));
                break;
                
        }
        } while (!CorrectSpawnPosition(l_position));
        
        return l_position;
    }
    

    private bool CorrectSpawnPosition(Vector2 l_position)
    {
        if (l_position.x<m_DownLeftLimit.position.x || l_position.x>m_DownRightLimit.position.x) return false;
        else if (l_position.y<m_DownLeftLimit.position.y ||l_position.y>m_UpRightLimit.position.y) return false;
        /*
        foreach(BoxCollider2D l_collider in m_TempleAreas)
        {
            if (l_collider.OverlapPoint(l_position)) return false;
        }
        */
        return true;
    }

    //Decrease NUMBER OF ENEMIES
    public static void DecreaseCurrentChasers(Vector3 l_position)
    {
        //l_RealSwordSpawner.SpawnSword(l_position);
        m_CurrentNumberOfChasers--;
        m_TotalNumberOfEnemies--;
        m_TotalScore += m_ChaserScorePoints;
        m_Score.ChangeScore(m_TotalScore);
        m_Exp.AddExperience(m_ChaserScorePoints);
        
    }
    
    //EVENTS
    public static void SubscribeToScoreEvent(ScoreDisplay l_Score)
    {
        m_Score = l_Score;
    }

    public static void SubscribeToExpEvent(ExpManager l_Exp)
    {
        m_Exp = l_Exp;
    }

    public static int GetScore()
    {
        return m_TotalScore;
    }

    public static void RestartGame()
    {
        m_CurrentNumberOfChasers = 0;
       
        m_TotalNumberOfEnemies = 0;
        m_TotalScore = 0;
        m_Score.ChangeScore(m_TotalScore);
        m_Exp.RestartExp();
    }
    

}
