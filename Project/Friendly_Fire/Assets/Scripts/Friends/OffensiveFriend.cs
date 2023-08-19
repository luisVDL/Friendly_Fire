using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffensiveFriend : AFriend
{
    [SerializeField] protected float m_InitialDamage;
    protected float m_CurrentDamage;
    protected float m_LastActivation;


    public void ResetFriendDamage()
    {
        m_CurrentDamage = m_InitialDamage;
    }

    public float getFriendDamage()
    {
        return m_CurrentDamage;
    }

    public void ApplyDamageMultiplier(float l_Multiplier)
    {
        m_CurrentDamage *= l_Multiplier;
    }
    

}
