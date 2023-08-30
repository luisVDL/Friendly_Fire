using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SchlapAreaBehaviour : AbsBullet
{
    
    [SerializeField] private GameObject m_AreaPrefab;
    [SerializeField] private float m_StartSize = 0.1f;

    [SerializeField] private float m_InitialMaxSize = 3f;
    [SerializeField] private float m_InitialMinSize = 2f;

    private float m_CurrentMaxSize;
    private float m_CurrentMinSize;

    private float m_TimeToLive = 0;
    private float m_LastSpawn = 0;

    private float m_CurrentSize;

    private int m_Phase = 0;
    private bool m_Decreasing = false;
    private float m_StartTime;
    
    
    void Start()
    {
        m_CurrentMaxSize = m_InitialMaxSize;
        m_CurrentMinSize = m_InitialMinSize;
        m_CurrentSize = m_StartSize;
        transform.localScale = new Vector3(m_StartSize, m_StartSize, m_StartSize);

    }

    void OnEnable()
    {
        m_Phase = 1;
        transform.localScale = new Vector3(m_StartSize, m_StartSize, m_StartSize);
    }

    void OnDisable()
    {
        m_Phase = 0;
    }
    
    void FixedUpdate()
    {
        switch (m_Phase)
        {
            case 1: //phase 1
                IncreaseToMaxInitialSize();
                break;
            case 2: //phase 2
                Ondulate();
                break;
            case 3: //phase 3
                DecreaseToMinSize();
                break;
        }
    }

    private void IncreaseToMaxInitialSize() //phase 1
    {
        if (m_CurrentSize < m_CurrentMaxSize)
        {
            m_CurrentSize += Time.deltaTime * 10;
            transform.localScale = new Vector3(m_CurrentSize, m_CurrentSize, m_CurrentSize);
        }
        else
        {
            m_Decreasing = true;
            m_Phase = 2;
            m_StartTime = 0f;
        }
    }

    private void Ondulate() //phase 2
    {
        m_StartTime += Time.deltaTime;
        if (m_StartTime >= m_TimeToLive)
        {
            m_Phase = 3;
        }
        else
        {
            if (m_Decreasing)

                if (m_CurrentSize < m_CurrentMinSize)
                {
                    m_Decreasing = false;
                }
                else
                {
                    m_CurrentSize -= Time.deltaTime*2;
                    transform.localScale = new Vector3(m_CurrentSize, m_CurrentSize, m_CurrentSize);
                }
            else
            {
                //increasing
                if (m_CurrentSize > m_CurrentMaxSize)
                {
                    m_Decreasing = true;
                }
                else
                {
                    m_CurrentSize += Time.deltaTime*2;
                    transform.localScale = new Vector3(m_CurrentSize, m_CurrentSize, m_CurrentSize);
                }
            }
        }
    }

    private void DecreaseToMinSize() //phase 3
    {
        if(m_CurrentSize > m_StartSize)
        {
            m_CurrentSize -= Time.deltaTime*10;
            transform.localScale = new Vector3(m_CurrentSize, m_CurrentSize, m_CurrentSize);
        }
        else
        {
            DeactivateBullet();
        }
    }

    public void Spawn(float l_NewTimeToLive)
    {
        m_TimeToLive = l_NewTimeToLive;
        m_CurrentSize = m_StartSize;
        
        m_Phase = 1;
    }

    /*
     * Things I need to do:
     * - I need to spawn the SlimeArea
     * - I need to make it bigger --> Phase 1
     * - I need to make it change size during the duration of the ability and then reduce it's size to the original time -->Phase 2
     * - I need to make it shorter
     */

    public override GameObject FireBullet(Vector3 l_Direction, Vector3 l_Position, Quaternion l_rotation)
    {
        transform.position = l_Position;
        gameObject.SetActive(true);
        return m_AreaPrefab;
    }

    public override void Trajectory()
    {
        //Nothing
    }

    public override float GetDamage()
    {
        return 0;
    }

    public override void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}
