using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : HealthSystem
{
    [SerializeField] private float m_CurrentHealth;
    [SerializeField] private float m_MaxHealth=100f;
    public UnityEvent<float, float> PlayerTakesDamage;
    public UnityEvent<float, float> PlayerRecovers;
    public UnityEvent PlayerDies;
    
    /*
    [Header("Player Damaged")]
    [SerializeField] private AudioClip m_PlayerHurtClip;
    [SerializeField] private AudioSource m_AudioSource;

    [Header("Player Healed")] 
    [SerializeField] private AudioClip m_PlayerRecoverClip;
    [SerializeField] private ParticleSystem m_PlayerRecoverParticles; 
    */


        void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }


    public override void TakeDamage(float l_Damage)
    {
        //m_AudioSource.PlayOneShot(m_PlayerHurtClip);
        m_CurrentHealth -= l_Damage;
        if (m_CurrentHealth <= 0.0f)
        {
            m_CurrentHealth = 0.0f;
            PlayerTakesDamage.Invoke(m_CurrentHealth,m_MaxHealth);
            Die();
        }
        else
        {
            PlayerTakesDamage.Invoke(m_CurrentHealth,m_MaxHealth);
        }
    }

    public override void RecoverHealth(float l_Amount)
    {
        //m_PlayerRecoverParticles.Play();
        //m_AudioSource.PlayOneShot(m_PlayerRecoverClip);
        m_CurrentHealth += l_Amount;
        if (m_CurrentHealth > m_MaxHealth) m_CurrentHealth = m_MaxHealth;
        PlayerRecovers.Invoke(m_CurrentHealth, m_MaxHealth);
    }
    
    public override bool IsAlive()
    {
        return m_CurrentHealth > 0;
    }
    protected override void Die()
    {
        PlayerDies.Invoke();
    }

    public void PlayerRespawn()
    {
        RecoverHealth(m_MaxHealth);
    }
}
