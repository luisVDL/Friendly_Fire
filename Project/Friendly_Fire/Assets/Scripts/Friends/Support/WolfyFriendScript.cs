using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfyFriendScript : SupportFriend
{
    [SerializeField] private PlayerFriendManager m_Player;
    private bool m_AbilityActivated;

    private float m_StatusStart = 0f;


    void OnEnable()
    {
        m_CooldownImage.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        m_CooldownImage.gameObject.SetActive(false);
    }

    void Start()
    {
        m_CurrentCooldown = m_InitialCooldown;
        m_LastActivationTime = Time.time;
    }

    void Update()
    {
        if (!m_AbilityActivated)
        {
            m_CooldownImage.fillAmount = (Time.time - m_LastActivationTime) / m_CurrentCooldown; 
        }
        else
        {
            StatusDuration();
        }
            
        if (m_CurrentCooldown + m_LastActivationTime < Time.time )
        {
            m_LastActivationTime = Time.time;
            m_Animator.SetTrigger("Attack Start");
            ActivateFriendAbility();
        }
    }


    public override void ApplyWolfyStatus(float l_time)
    {
        // nothing because we can't have two Wolfies at the same time
    }

    public override void DeApplyWolfyStatus()
    {
        // nothing because we can't have two Wolfies at the same time
    }

    public override bool ActivateFriendAbility()
    {
        m_LastActivationTime = Time.time;
        m_CooldownImage.fillAmount = 0;
        
        
        List<FriendScript> l_Friends = m_Player.GetFriendScripts();
        foreach (FriendScript l_FS in l_Friends)
        {
            l_FS.getFriendAbilityScript().ApplyWolfyStatus(m_StatusDuration);
        }

        m_AbilityActivated = true;
        m_StatusStart = 0;
        return true;
    }

    private void  StatusDuration()
    {
        if (m_StatusStart < m_StatusDuration)
        {
         
            m_StatusStart += Time.deltaTime;
        }
       else
       {
           List<FriendScript> l_Friends = m_Player.GetFriendScripts();
           foreach (FriendScript l_FS in l_Friends)
           {
               l_FS.getFriendAbilityScript().DeApplyWolfyStatus();
           }
        
           m_LastActivationTime = Time.time;
           m_Animator.SetTrigger("Attack End");
           m_AbilityActivated = false;
       }
        
        
    }
}