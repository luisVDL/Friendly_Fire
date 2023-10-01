using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class NewEnemyManager : MonoBehaviour
{
    [Header("Player attack distance")] 
    [SerializeField] private float m_PlayerRange = 10f;
    private static float m_PlayerRangeSTATIC;
    [Space]
    
    [Header("General")] 
    [SerializeField] private int m_MaxNumberOfEnemies = 300;
    [SerializeField] private Transform m_Player;
    private static Transform m_PlayerStatic;
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
    [SerializeField] private List<Collider2D> m_MapColliders;
     private static List<SpawnAreaBehaviour> m_SpawnAreas;
     private int m_IndexArea=0;
    [Space] 
    
    [Header("Wave Points System")] 
    [Range(1, 25)] [SerializeField] private float m_maxPointsPerWave = 10;
    [Range(5, 25)] [SerializeField] private float m_AddedPointsPerWave = 5;

    //This variables will change during the game to increase to manage the wave system
    private static float m_CurrentMaxPointsPerWave;
    private static float m_CurrentAddedPointsPerWave;
    private static List<AEnemy> m_EnemiesToSpawn;
    private static List<AEnemy> m_EnemiesAlive;
    private static int m_WaveNumber;
    [Space] 
    //This value is used to limit the number of different types of enemies to spawn in the current wave
    private static int m_MaxRandomValuePerWave;
    [Range(5,25)]  [Tooltip("This is the number of waves that the player has to survive to increment the max number of each enemy")][SerializeField]private int m_WavesToIncrementMaxRandomValue = 5;
    
    
    [Space]
    [Header("Display")]
    [SerializeField] private TextMeshProUGUI m_WaveText;
    [SerializeField] private TextMeshProUGUI m_RemainingEnemiesText;
    
    [SerializeField] private TextMeshProUGUI m_WaveStartText;
    private static bool ShowNextWaveText;
    
    private static TextMeshProUGUI m_RemainingEnemiesTextSTATIC;
    private int m_CurrentWaveEnemies;

    [SerializeField] private UnityEvent EnemyDiedEnemyDied;


    [Space] 
    [Header("SCORE (DELETE LATER)")] 
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    private static int m_CurrentScore;
    private static TextMeshProUGUI m_ScoreTextStatic;

    
    //More Statistics
    private static int m_ItemsCollected;
    private static int m_EnemiesDefeated;
    private static int m_FriendsCollected;

    //We use this variable to avoid executing two times the CreateWave method
    private bool m_Spawning;
    
  
    void Start()
    {
        ShowNextWaveText = true;
        m_ItemsCollected = 0;
        m_EnemiesDefeated = 0;
        m_FriendsCollected = 0;
        
        m_PlayerRangeSTATIC = m_PlayerRange;
        m_SpawnAreas = new List<SpawnAreaBehaviour>();
        m_PlayerStatic = m_Player;
        m_ScoreTextStatic = m_ScoreText;
        m_EnemyClassesSTATIC = m_EnemyClasses;
        m_CurrentScore = 0;
        m_WaveCooldownSTATIC = m_WaveCooldown;
        m_SpawnTime = Time.time + m_WaveCooldown;
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
    
    void Update()
    {
        if (ShowNextWaveText)
        {
            ShowNextWaveText = false;
            StartCoroutine(ShowNextWaveCounter());
        }
        if(m_SpawnTime<Time.time)
            if (m_EnemiesAlive.Count == 0 && !m_Spawning)
            {
                //Apply to the enemylists the increase for each wave
                m_WaveNumber += 1;
                if (m_WaveNumber == m_WavesToIncrementMaxRandomValue) m_MaxRandomValuePerWave += 1; /////////////////////// OJO CAMBIAR CUANDO HAYA MÁS DE 2 ENEMIGOS
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
        m_EnemiesToSpawn = new List<AEnemy>();
        
        int l_Random;
        EnemyClassManager l_EnemyClass;
        while (l_currentCost<m_CurrentMaxPointsPerWave)
        {
            if (m_EnemiesToSpawn.Count >= m_MaxNumberOfEnemies) break;
            l_Random = Random.Range(0, m_MaxRandomValuePerWave);
            l_EnemyClass = m_EnemyClassesSTATIC[l_Random];
            if (l_EnemyClass.CanSpawn())
            {
                l_currentCost += l_EnemyClass.GetEnemyCost();
                m_EnemiesToSpawn.Add(l_EnemyClass.getEnemy());

            }
            
        }

        DuplicateSpawnToAlive();
        
        ShowEnemiesRemaining();
        StartCoroutine(SpawnWave(m_EnemiesToSpawn));
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
            Vector3 l_position = GetRandomPosition();
            while (l_position==Vector3.zero)
            {
                yield return new WaitForSeconds(0.2f);
                l_position = GetRandomPosition();
            }
            l_Enemy.Spawn(l_position, m_PlayerStatic);
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
    }
    
    
    
    
    //GET LOCATION
    
    private Vector3 GetRandomPosition()
    {
        Vector2 l_position = new Vector2(0,0); 
        
        BoxCollider2D l_collider;
        do
        {
            if (m_SpawnAreas.Count == 0) return Vector3.zero;
            if (m_SpawnAreas.Count == 1) m_IndexArea = 0;

            l_collider = m_SpawnAreas[m_IndexArea].GetCollider();
            l_position = GetRandomPositionInCollider(l_collider);
        } while (!DistanceIsOk(l_position));
        
        return l_position;
    }

    private Vector2 GetRandomPositionInCollider(BoxCollider2D l_collider)
    {
        Bounds l_Bounds = l_collider.bounds;
        float randomX = Random.Range(l_Bounds.min.x, l_Bounds.max.x);
        float randomY = Random.Range(l_Bounds.min.y, l_Bounds.max.y);

        return new Vector2(randomX, randomY);

    }

    private bool DistanceIsOk(Vector2 l_position)
    {
        float l_Distance = Mathf.Abs(Vector2.Distance(l_position, m_Player.position));
        return l_Distance < m_MaxDistance && l_Distance > m_MinDistance;
    }

    private bool CorrectObjectSpawnPosition(Vector2 l_position)
    {
        foreach (Collider2D l_Collider in m_MapColliders)
        {
            if (l_Collider.OverlapPoint(l_position)) return true;
        }
        return false;
    }

    public static void AddSpawnArea(SpawnAreaBehaviour l_Area)
    {
        m_SpawnAreas.Add(l_Area);
    }

    public static void RemoveSpawnArea(SpawnAreaBehaviour l_Area)
    {
        m_SpawnAreas.Remove(l_Area);
    }
    
    public static Vector3 getEnemyPosition()
    {
        
        Vector3 l_FinalEnemyPosition = new Vector3();
        float l_MinDistance = 200f;
        float l_EnemyDistance = 0;
        foreach (AEnemy l_Enemy in m_EnemiesAlive)
        {
            l_EnemyDistance = Vector3.Distance(m_PlayerStatic.transform.position, l_Enemy.transform.position);
            if (l_MinDistance > l_EnemyDistance)
            {
                l_MinDistance = l_EnemyDistance;
                l_FinalEnemyPosition = l_Enemy.transform.position;
            }
        }

        if (Vector3.Distance(l_FinalEnemyPosition, m_PlayerStatic.position) > m_PlayerRangeSTATIC)
            throw new Exception("The enemies are too far to activate the skill");
        return l_FinalEnemyPosition;
    }

    public static Vector3 getRandomEnemyPosition()
    {
        int l_random = Random.Range(0, m_EnemiesAlive.Count);
        Vector3 l_EnemyPosition = m_EnemiesAlive[l_random].transform.position;

        if (Vector3.Distance(l_EnemyPosition, m_PlayerStatic.position) > m_PlayerRangeSTATIC)
            throw new Exception("The enemies are too far to activate the skill");
        //I need to get a random position that is close enough
        return m_EnemiesAlive[l_random].transform.position;
    }


    public static void DecreaseNumberOfEnemies(AEnemy l_Enemy)
    {
        m_EnemiesDefeated += 1;
        m_EnemiesAlive.Remove(l_Enemy);
        m_CurrentScore+=l_Enemy.getScore();
        m_ScoreTextStatic.text = m_CurrentScore+"";
        ShowEnemiesRemaining();
        if (m_EnemiesAlive.Count == 0)
        {
            m_SpawnTime = Time.time+ m_WaveCooldownSTATIC;
            ShowNextWaveText = true;
            m_CurrentMaxPointsPerWave += m_CurrentAddedPointsPerWave;
            IncreaseMaxNumberEnemiesPerWave();
        }
    }
    

    private IEnumerator ShowNextWaveCounter()
    {
        m_WaveStartText.gameObject.SetActive(true);
        while (Time.time<=m_SpawnTime)
        {
            yield return new WaitForSeconds(0.1f);
            m_WaveStartText.text = "Next Wave "+(m_SpawnTime -Time.time).ToString("0");;
        }
        m_WaveStartText.gameObject.SetActive(false);
    }

    public static void AddFriendtoCollected()
    {
        m_FriendsCollected += 1;
    }


    public static void AddCollectibleScore(int l_ScoreToAdd)
    {
        m_ItemsCollected += 1;
        m_CurrentScore += l_ScoreToAdd;
        m_ScoreTextStatic.text = m_CurrentScore+"";
        
    }
    public static int GetScore()
    {
        return m_CurrentScore;
    }


    public static int GetItemsCollected()
    {
        return m_ItemsCollected;
    }

    public static int GetEnemiesDefeated()
    {
        return m_EnemiesDefeated;
    }

    public static int GetFriendsCollected()
    {
        return m_FriendsCollected;
    }

    public static int GetWave()
    {
        return m_WaveNumber;
    }
}
