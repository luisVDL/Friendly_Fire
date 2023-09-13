using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NewEnemyManager : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private int m_MaxNumberOfEnemies = 300;
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_MinDistance = 10f;
    [SerializeField] private float m_MaxDistance = 20f;
    [SerializeField] [Range(1, 5)]private float m_WaveCooldown = 2f;
    private static float m_WaveCooldownSTATIC;
    private static float m_SpawnTime;
    

    [Header("Enemies classes to spawn")] 
    [SerializeField] private List<EnemyClassManager> m_EnemyClasses;
    private static List<EnemyClassManager> m_EnemyClassesSTATIC;
    [Space] 
    
    
    [Header("Limits")]
    [SerializeField] private Transform m_UpLeftLimit;
    [SerializeField] private Transform m_UpRightLimit;
    [SerializeField] private Transform m_DownLeftLimit;
    [SerializeField] private Transform m_DownRightLimit;
    [Space] 
    
    [Header("Wave Points System")] 
    [Range(1, 25)] [SerializeField] private float m_maxPointsPerWave = 10;
    [Range(5, 25)] [SerializeField] private float m_AddedPointsPerWave = 5;

    //This variables will change during the game to increase to manage the wave system
    private static float m_CurrentMaxPointsPerWave;
    private static float m_CurrentAddedPointsPerWave;
    private static List<AEnemy> m_EnemiesToSpawn;
    private static List<AEnemy> m_EnemiesAlive;
    //private static int m_CurrentEnemiesAlive;
    private static int m_WaveNumber;
    [Space] 
    //This value is used to limit the number of diferent types of enemies to spawn in the current wave
    private static int m_MaxRandomValuePerWave;
    [Range(5,25)]  [Tooltip("This is the number of waves that the player has to survive to increment the max number of each enemy")][SerializeField]private int m_WavesToIncrementMaxRandomValue = 5;
    
    
    [Space]
    [Header("Display")]
    [SerializeField] private TextMeshProUGUI m_WaveText;
    [SerializeField] private TextMeshProUGUI m_RemainingEnemiesText;
    private static TextMeshProUGUI m_RemainingEnemiesTextSTATIC;
    private int m_CurrentWaveEnemies;

    [SerializeField] private UnityEvent EnemyDiedEnemyDied;


    [Space] 
    [Header("SCORE (DELETE LATER)")] 
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    private float m_CurrentScore;


    //We use this variable to avoid executing two times the CreateWave method
    private bool m_Spawning;
    
    // Start is called before the first frame update
    void Start()
    {
        m_EnemyClassesSTATIC = m_EnemyClasses;
        m_CurrentScore = 0;
        m_WaveCooldownSTATIC = m_WaveCooldown;
        m_SpawnTime = Time.time + m_WaveCooldown;
        //m_CurrentEnemiesAlive = 0;
        m_EnemiesAlive = new List<AEnemy>();
        m_EnemiesToSpawn = new List<AEnemy>();
        m_WaveNumber = 0;
        m_MaxRandomValuePerWave = 1;
        m_Spawning = false;
        m_CurrentMaxPointsPerWave = m_maxPointsPerWave;
        m_CurrentAddedPointsPerWave = m_AddedPointsPerWave;
        ShowWaveNumber();
        m_RemainingEnemiesTextSTATIC = m_RemainingEnemiesText;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_SpawnTime<Time.time)
            if (m_EnemiesAlive.Count == 0 && !m_Spawning)
            {
                //Apply to the enemylists the increase for each wave
                m_WaveNumber += 1;
                m_Spawning = true;
                ShowWaveNumber();
                CreateWave();
            }
    }

    private static void IncreaseMaxNumberEnemiesPerWave() 
    {
        foreach (EnemyClassManager l_Spawner in m_EnemyClassesSTATIC)
        {
            l_Spawner.IncreaseMaxNEnemies();
        }
    }

    private void CreateWave()
    {
        //We use this int to control the value of the enemies of that wave
        float l_currentCost = 0;
        // This list will contain the enemies to spawn. This will be sent to a coroutine to spawn those enemies.
        //List<AEnemy> l_EnemiesToSpawn = new List<AEnemy>();
        m_EnemiesToSpawn = new List<AEnemy>();
        
        int l_Random;
        EnemyClassManager l_EnemyClass;
        while (l_currentCost<m_CurrentMaxPointsPerWave)
        {
            l_Random = Random.Range(0, m_MaxRandomValuePerWave);
            l_EnemyClass = m_EnemyClassesSTATIC[l_Random];
            if (l_EnemyClass.CanSpawn())
            {
                l_currentCost += l_EnemyClass.GetEnemyCost();
                //l_EnemiesToSpawn.Add(l_EnemyClass.getEnemy());
                m_EnemiesToSpawn.Add(l_EnemyClass.getEnemy());

            }
            
        }

        DuplicateSpawnToAlive();
        
        //m_CurrentEnemiesAlive = l_EnemiesToSpawn.Count;
        ShowEnemiesRemaining();
        StartCoroutine(SpawnWave(m_EnemiesToSpawn)); //l_EnemiesToSpawn));
    }

    private void DuplicateSpawnToAlive()
    {
        foreach (AEnemy l_Enemy in m_EnemiesToSpawn)
        {
            m_EnemiesAlive.Add(l_Enemy);
        }
    }

    private IEnumerator SpawnWave(List<AEnemy> l_Enemies)
    {
        yield return new WaitForSeconds(m_WaveCooldown);
        foreach (AEnemy l_Enemy in l_Enemies)
        {
            
            l_Enemy.Spawn(GetRandomPosition(), m_Player);
            yield return new WaitForSeconds(l_Enemy.getSpawnCooldown());
        }

        
        m_Spawning = false;
    }

    private void ShowWaveNumber()
    {
        m_WaveText.SetText(m_WaveNumber+"");
        //REMAINS TO DO A FONT THAT SHOWS THE WAVE NUMBER, BUT FOR NOW THIS WORKS
    }

    private static void ShowEnemiesRemaining()
    {
        m_RemainingEnemiesTextSTATIC.SetText(m_EnemiesAlive.Count+"");
        //m_RemainingEnemiesText.SetText(m_CurrentEnemiesAlive+"");
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

    public static Vector3 getEnemyPosition()
    {
        int l_random = Random.Range(0, m_EnemiesAlive.Count);

        return m_EnemiesAlive[l_random].transform.position;
    }


    public static void DecreaseNumberOfEnemies(AEnemy l_Enemy)
    {
        m_EnemiesAlive.Remove(l_Enemy);
        ShowEnemiesRemaining();
        if (m_EnemiesAlive.Count == 0)
        {
            m_SpawnTime = Time.time+ m_WaveCooldownSTATIC;
            m_CurrentMaxPointsPerWave += m_CurrentAddedPointsPerWave;
            IncreaseMaxNumberEnemiesPerWave();
        }
    }
    
}
