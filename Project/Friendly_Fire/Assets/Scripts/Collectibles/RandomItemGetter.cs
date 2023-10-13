using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemGetter : MonoBehaviour
{
    [SerializeField] private List<Transform> m_RandomItems;
    private static List<Transform> m_RealRandomItems;

    void Start()
    {
        m_RealRandomItems = m_RandomItems;
    }

    public static Transform GetRandomChasedObject()
    {
        int l_index = Random.Range(0, m_RealRandomItems.Count);
        return m_RealRandomItems[l_index];
    }
    
}
