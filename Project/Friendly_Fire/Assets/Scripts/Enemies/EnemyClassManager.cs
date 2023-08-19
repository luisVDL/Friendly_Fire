using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyClassManager : MonoBehaviour
{

    [SerializeField] private string m_ID;
    [SerializeField] private PoolScript m_Pool;
    [SerializeField][Range(1,4)] private float m_EnemyCost;
    [SerializeField] private float m_EnemySpawnCooldown;
    [SerializeField]private int m_StartMaxNumberOfEnemiesOfType = 10;
    private int m_CurrentMaxNumberOfEnemiesOfType;
    
    private int m_CurrentEnemiesOfType;
    [SerializeField] private int m_AddedEnemiesOfTypeEachXWaves = 3;

    void Start()
    {
        m_CurrentMaxNumberOfEnemiesOfType = m_StartMaxNumberOfEnemiesOfType;
        m_CurrentEnemiesOfType = 0;
    }
    
    public AEnemy getEnemy()
    {
        GameObject l_GO = m_Pool.EnableObject();
        AEnemy  l_Enemy= l_GO.GetComponent<AEnemy>();
        return l_Enemy;
    }

    public float getEnemyCost()
    {
        return m_EnemyCost;
    }
    public float getEnemyCooldown()
    {
        return m_EnemySpawnCooldown;
    }

    public bool CanSpawn()
    {
        return m_CurrentMaxNumberOfEnemiesOfType > m_CurrentEnemiesOfType;
    }


    public void IncreaseMaxNEnemies()
    {
        m_CurrentMaxNumberOfEnemiesOfType += m_AddedEnemiesOfTypeEachXWaves;
    }
    
}
