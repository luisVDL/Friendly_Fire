using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpawnAreaBehaviour : MonoBehaviour, IComparable
{
    [SerializeField] private BoxCollider2D m_Collider;

    [SerializeField] private string m_ID;



    void OnTriggerEnter2D(Collider2D l_Other)
    {
        if (l_Other.tag == "Player")
        {
            NewEnemyManager.AddSpawnArea(this);
        }
    }

    void OnTriggerExit2D(Collider2D l_Other)
    {
        if (l_Other.tag == "Player")
        {
            NewEnemyManager.RemoveSpawnArea(this);
        }
    }

    public int CompareTo(object obj)
    {
        if (obj is SpawnAreaBehaviour)
        {
            SpawnAreaBehaviour l_SpawnArea = (SpawnAreaBehaviour)obj;
            return m_ID.CompareTo(l_SpawnArea.m_ID);
        }

        return -200;
    }

    public BoxCollider2D GetCollider()
    {
        return m_Collider;
    }
}
