using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private List<Collider2D> m_SpawnAreas;
    [SerializeField] private List<PoolScript> m_Pools;

    [SerializeField] private float m_SpawnCooldown = 10f;

    private float m_LastTimeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        m_LastTimeSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > m_LastTimeSpawn + m_SpawnCooldown)
        {
            m_LastTimeSpawn = Time.time;
            SpawnCollectible();
        }
    }

    private void SpawnCollectible()
    {
        print("Spawn collectible");
        int random = Random.Range(0, m_Pools.Count);
        GameObject l_GO = m_Pools[random].EnableObject();
        CollectibleBehaviour l_CB = l_GO.GetComponent<CollectibleBehaviour>();
        l_CB.Spawn(GetRandomPosition());
    }


    private Vector3 GetRandomPosition()
    {
        int l_random = Random.Range(0, m_SpawnAreas.Count);
        Bounds l_Bounds = m_SpawnAreas[l_random].bounds;
        float randomX = Random.Range(l_Bounds.min.x, l_Bounds.max.x);
        float randomY = Random.Range(l_Bounds.min.y, l_Bounds.max.y);
        return new Vector3(randomX, randomY, 0f);;
    }
}
