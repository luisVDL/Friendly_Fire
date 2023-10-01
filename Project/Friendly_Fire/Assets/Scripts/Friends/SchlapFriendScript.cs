using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SchlapFriendScript : AFriend
{
    [SerializeField] private PoolScript m_BulletPool;
    [SerializeField] private PoolScript m_AreaPool;
    [SerializeField] private float m_StateDuration;
    //We need another for the area slime
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
        m_CooldownImage.fillAmount = (Time.time -m_LastActivationTime) / m_CurrentCooldown;
        if (m_CurrentCooldown + m_LastActivationTime < Time.time)
        {
            m_Animator.SetTrigger("Attack");
            ActivateFriendAbility();
        }
    }

    public float getAbilityTime()
    {
        return m_StateDuration;
    }
    public override bool ActivateFriendAbility()
    {
        try
        {
            GameObject l_GO = m_BulletPool.EnableObject();
            SchlapBulletBehaviour l_Bullet=l_GO.GetComponent<SchlapBulletBehaviour>();
            try
            {
                
                l_Bullet.FireBullet(transform.position-NewEnemyManager.getEnemyPosition(), transform.position, quaternion.identity);

            }
            catch (Exception e)
            {
                //print("Schlap tried to attack but "+e.Message);
                return false;
            }
            //l_Bullet.FireBullet(transform.position-NewEnemyManager.getEnemyPosition(), transform.position, quaternion.identity);
            l_Bullet.setParentSchlap(this);
            l_Bullet.setAreaPool(m_AreaPool);
            m_LastActivationTime = Time.time;
        }
        catch (Exception e)
        {
            print("An error has ocurred: "+e.Message);
        }
        return true;
    }
}
