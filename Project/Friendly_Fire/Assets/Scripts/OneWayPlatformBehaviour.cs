using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneWayPlatformBehaviour : MonoBehaviour
{
    private GameObject m_CurrentOneWayPlatform;
    [SerializeField] private CircleCollider2D m_PlayerCollider;
    
    void Update()
    {
        if (m_CurrentOneWayPlatform != null) StartCoroutine(DisableCollision());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Check that the Player can currently pass the obstacle
            PlayerController l_PC= other.gameObject.GetComponentInParent<PlayerController>();
            if (l_PC.CanPassObstacle())
            {
                m_CurrentOneWayPlatform = other.gameObject;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_CurrentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        //BoxCollider2D l_PlatformCollider = m_CurrentOneWayPlatform.GetComponent<BoxCollider2D>();
        
        //Physics2D.IgnoreCollision(m_PlayerCollider, l_PlatformCollider, true);
        m_PlayerCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        //Physics2D.IgnoreCollision(m_PlayerCollider, l_PlatformCollider, false);
        m_PlayerCollider.enabled = true;

    }
}
