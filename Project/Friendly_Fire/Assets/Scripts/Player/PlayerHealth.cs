using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : HealthSystem
{
    [Header("General")]
    [SerializeField] private float m_CurrentHealth;
    [SerializeField] private float m_MaxHealth=100f;
    public UnityEvent<float, float> PlayerTakesDamage;
    public UnityEvent<float, float> PlayerRecovers;
    public UnityEvent PlayerDies;

    [Space]
    
    [Header("Feedback Player Hurt")] 
    [SerializeField] private SpriteRenderer m_PlayerSprite;
    [SerializeField] private float m_TransitionTime = 0.2f;
    [SerializeField] private Color m_HurtColor;
    
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
        StartCoroutine(FeedbackPlayer());
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

    private IEnumerator FeedbackPlayer()
    {
        m_PlayerSprite.color = m_HurtColor;
        yield return new WaitForSeconds(m_TransitionTime);
        m_PlayerSprite.color = Color.white;
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
