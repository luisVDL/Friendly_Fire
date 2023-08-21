using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyFriendScript : AFriend
{
    [SerializeField] private GameObject m_Ability;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentCooldown = m_InitialCooldown;
        m_LastTimeAbility = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentCooldown + m_LastTimeAbility < Time.time)
        {
            m_LastTimeAbility = Time.time;
            ActivateFriendAbility();
        }
    }

    public override bool ActivateFriendAbility()
    {
        try
        {
            m_Ability.transform.position = NewEnemyManager.getEnemyPosition();
            m_Ability.SetActive(true);
        }
        catch (Exception e)
        {
            
        }
        return true;
    }

    public void DeactivateDizzyZone()
    {
        m_Ability.SetActive(false);
    }
    
    
}
