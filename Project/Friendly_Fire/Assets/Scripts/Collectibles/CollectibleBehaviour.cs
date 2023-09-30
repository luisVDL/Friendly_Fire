using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{

    [SerializeField] private int m_CollectibleScore = 10;
    [SerializeField] private GameObject m_CollectiblePrefab;
    // Start is called before the first frame update
    
    public GameObject Spawn(Vector3 l_Position)
    {
        gameObject.SetActive(true);
        transform.position = l_Position;
        return m_CollectiblePrefab;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            NewEnemyManager.AddCollectibleScore(m_CollectibleScore);
            gameObject.SetActive(false);
        }
    }
}
