using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneWayPlatformBehaviour : MonoBehaviour
{

    private GameObject m_CurrentOneWayPlatform;

    [SerializeField] private CircleCollider2D m_PlayerCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentOneWayPlatform != null) StartCoroutine(DisableCollision());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        print("Something is touching meeeeeee");
        if (other.gameObject.tag == "Player")
        {
            print("AAAAAA it's the player");
            //Check that the Player can currently pass the obstacle
            PlayerController l_PC= other.gameObject.GetComponentInParent<PlayerController>();
            if (l_PC.CanPassObstacle())
            {
                print("--------------");
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
        print("OWO");
        BoxCollider2D l_PlatformCollider = m_CurrentOneWayPlatform.GetComponent<BoxCollider2D>();
        
        
        //THIS DOESN'T WORK CHECK AGAIN THE VIDEO
        
        //https://www.youtube.com/watch?v=7rCUt6mqqE8
        
        
        Physics2D.IgnoreCollision(m_PlayerCollider, l_PlatformCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(m_PlayerCollider, l_PlatformCollider, false);

    }
}
