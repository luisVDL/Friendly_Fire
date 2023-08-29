using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchlapAreaBehaviour : MonoBehaviour
{
    [SerializeField] private float m_StartNEndTimeToMax = 0.4f;
    [SerializeField] private float m_StartSize = 0.1f;

    [SerializeField] private float m_InitialMaxSize = 3f;
    [SerializeField] private float m_InitialMinSize = 2f;

    private float m_CurrentMaxSize;
    private float m_CurrentMinSize;

    private float m_TimeToLive = 0;
    private float m_LastSpawn = 0;

    private float l_TimeToGetAtStartMaxSize;
    private float l_CurrentTime;
    [SerializeField] private bool Activated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentMaxSize = m_InitialMaxSize;
        m_CurrentMinSize = m_InitialMinSize;
        m_StartNEndTimeToMax = 2.5f;
        l_TimeToGetAtStartMaxSize= Time.time + m_StartNEndTimeToMax;
        l_CurrentTime = m_StartSize;
        transform.localScale = new Vector3(m_StartSize, m_StartSize, m_StartSize);
        //StartCoroutine(UseSlimeArea());
        print("CurrentTime: "+l_CurrentTime);
        print("Time To maxSize: "+l_TimeToGetAtStartMaxSize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Activated)
        {
            while (l_CurrentTime < m_CurrentMaxSize)
            {
                print(l_CurrentTime);
                l_CurrentTime += Time.deltaTime/10;
                transform.localScale = new Vector3(l_CurrentTime, l_CurrentTime, l_CurrentTime);
                //yield return new WaitForSeconds(0.05f);
            } 
        }
        
    }

    public void Spawn(Vector3 l_Position, float l_NewTimeToLive)
    {
        m_TimeToLive = l_NewTimeToLive;
        transform.position = l_Position;
        l_TimeToGetAtStartMaxSize = Time.time + m_StartNEndTimeToMax;
        l_CurrentTime = m_StartSize;
        
    }
    
    /*
     * Things I need to do:
     * - I need to spawn the SlimeArea
     * - I need to make it bigger
     * - I need to make it change size during the duration of the ability and then reduce it's size to the original time
     * - Maybe a corroutine will help better thant the update
     * - To avoid stablish a time to get to full size it will be better to have a variable
     */

    private IEnumerator UseSlimeArea()
    {
        print("Slime Area Activation");
        float l_TimeToGetAtStartMaxSize = Time.time + m_StartNEndTimeToMax;
        float l_CurrentTime = m_StartSize;
        //To Start
        while (l_CurrentTime < l_TimeToGetAtStartMaxSize)
        {
            l_CurrentTime += Time.deltaTime;
            transform.localScale = new Vector3(l_CurrentTime, l_CurrentTime, l_CurrentTime);
            //yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0);
    }
}
