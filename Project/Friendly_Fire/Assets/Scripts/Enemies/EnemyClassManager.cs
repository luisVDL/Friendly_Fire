using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyClassManager : MonoBehaviour
{

    [SerializeField] private string m_ID;
    [SerializeField] private PoolScript m_EnemyPool;
    [SerializeField] private PoolScript m_BulletPool;
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
        GameObject l_GO = m_EnemyPool.EnableObject();
        AEnemy  l_Enemy= l_GO.GetComponent<AEnemy>();
        l_Enemy.SetSpawnCooldown(m_EnemySpawnCooldown);
        if (l_Enemy is SniperEnemy)
        {
            SniperEnemy l_Sniper = l_Enemy.transform.GetComponent<SniperEnemy>();
            l_Sniper.SetBulletPool(m_BulletPool);
        }
        return l_Enemy;
    }

    public float GetEnemyCost()
    {
        return m_EnemyCost;
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
