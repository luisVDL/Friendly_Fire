using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyFriendScript : AFriend
{
    [SerializeField] private GameObject m_Ability;
    [SerializeField] private DizzySubStatusSetter m_SubStatusSetter;
    
    
    
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

    // Update is called once per frame
    void Update()
    {
        m_CooldownImage.fillAmount = (Time.time -m_LastActivationTime) / m_CurrentCooldown;
        if (m_CurrentCooldown + m_LastActivationTime < Time.time)
        {
            m_LastActivationTime = Time.time;
            m_Animator.SetTrigger("Attack");
            ActivateFriendAbility();
        }
    }

    public override void ApplyWolfyStatus(float l_DurationTime)
    {
        AttackUpAnimation();
        m_SubStatusSetter.IncreaseDuration(l_DurationTime);
    }

    public override void DeApplyWolfyStatus()
    {
        HideAttackUpAnimation();
        m_SubStatusSetter.ResetDuration();
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
            //print("Dizzy tried to attack but "+ e.Message);
            return false;
        }
        return true;
    }

    public void DeactivateDizzyZone()
    {
        m_Ability.SetActive(false);
    }
    
    
}
