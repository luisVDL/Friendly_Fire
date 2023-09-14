using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class OctavioFriendScript : OffensiveFriend, IRestartable
{
    [SerializeField] private PoolScript m_TentaclesPool;
    [SerializeField] private int m_NumerOfTentacles = 5;


    // Start is called before the first frame update
    void Start()
    {
        m_CurrentDamage = m_InitialDamage;
        m_LastActivationTime = Time.time;
        m_CurrentCooldown = m_InitialCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_CooldownImage.fillAmount = (Time.time - m_LastActivationTime) / m_CurrentCooldown;
        if (Time.time > m_LastActivationTime + m_CurrentCooldown)
        {
            m_Animator.SetTrigger("Attack");
            ActivateFriendAbility();
            m_LastActivationTime = Time.time;
        }
    }

    void OnEnable()
    {
        m_CooldownImage.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        m_CooldownImage.gameObject.SetActive(false);
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
                l_Tentacle.FireBullet(Vector3.zero, NewEnemyManager.getRandomEnemyPosition(),transform.rotation);
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
        m_CooldownImage.gameObject.SetActive(false);
    }
}
