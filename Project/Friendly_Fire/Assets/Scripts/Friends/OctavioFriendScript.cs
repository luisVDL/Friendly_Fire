using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class OctavioFriendScript : OffensiveFriend, IRestartable
{
    [SerializeField] private PoolScript m_TentaclesPool;
    [SerializeField] private int m_NumerOfTentacles = 5;


    // Start is called before the first frame update
    void Start()
    {
        m_CurrentDamage = m_InitialDamage;
        m_LastActivation = Time.time + 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > m_LastActivation + m_CurrentCooldown)
        {
            ActivateFriendAbility();
            m_CurrentCooldown = Time.time;
        }
    }
    

    public override bool ActivateFriendAbility()
    {
        GameObject l_GO;
        OctavioTentacleBullet l_Tentacle;
        for (int i= 0; i < m_NumerOfTentacles; i++)
        {
            l_GO = m_TentaclesPool.EnableObject();
            l_Tentacle = l_GO.GetComponent<OctavioTentacleBullet>();
            l_Tentacle.setParentFriend(this);
            try
            {
                l_Tentacle.FireBullet(Vector3.zero, NewEnemyManager.getEnemyPosition(),transform.rotation);
            }
            catch (Exception e)
            {
                
            }
            
        }
        

        
        return true;
    }

    public void Restart()
    {
        //throw new System.NotImplementedException();
    }
}
