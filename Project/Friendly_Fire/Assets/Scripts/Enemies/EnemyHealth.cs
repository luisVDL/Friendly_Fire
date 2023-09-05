using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : HealthSystem
{
    [SerializeField] private float m_MaxHealth=20f;
    [SerializeField] private Slider m_Healthbar;
    private float m_CurrentHealth;
    [SerializeField] private Collider2D m_TriggerCollider;
    [SerializeField] private Collider2D m_RigidCollider;
    public void StartEnemyHealth()
    {
        m_CurrentHealth = m_MaxHealth;
        m_Healthbar.gameObject.SetActive(true);
        m_Healthbar.SetValueWithoutNotify(m_CurrentHealth/m_MaxHealth);
        //added to control colliders
        m_TriggerCollider.enabled = true;
        m_RigidCollider.enabled = true;
    }

    public override bool IsAlive()
    {
        return m_CurrentHealth > 0f;
    }

    public override void TakeDamage(float l_Damage)
    {
        m_CurrentHealth -= l_Damage;
        m_Healthbar.SetValueWithoutNotify(m_CurrentHealth/m_MaxHealth);
        if (m_CurrentHealth <= 0f)
        {
            m_Healthbar.SetValueWithoutNotify(0f);
            Die();
        }
    }

    public override void RecoverHealth(float l_Amount)
    {
        m_CurrentHealth += l_Amount;
        if (m_CurrentHealth > m_MaxHealth) m_CurrentHealth = m_MaxHealth;
    }

    

    protected override void Die()
    {
        //added to control colliders
        m_TriggerCollider.enabled = false;
        m_RigidCollider.enabled = false;
        try
        {
            GetComponentInParent<AEnemy>().Die();
        }
        catch (Exception e)
        {
        }
        
    }
    
    
}
