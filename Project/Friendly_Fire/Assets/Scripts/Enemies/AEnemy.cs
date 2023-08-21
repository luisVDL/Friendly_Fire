using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public abstract class AEnemy : MonoBehaviour, IComparable
{
    //[SerializeField] private HealthSystem m_Health;

    protected Transform m_PlayerToChase;
    protected EnemyHealth m_EnemyHealth;
    protected bool m_Recoiling;
    protected int m_ID;
    protected float m_SubstateDuration;
    protected float m_SubstateStart;
    protected m_EnemyIAState m_CurrentState;
    protected enum m_EnemyIAState{
        CHASE, ATTACKING, COOLDOWN, PREPARING, DEATH
    }
    

    //STATES VARIABLES
    protected List<ASubStatus> m_SubStatuses;
    protected float m_Dizzy;
    [SerializeField] protected GameObject m_DizzyAnimation; 
        
        
    public abstract void Chase();
    public abstract void Die();
    public abstract void Spawn(Vector3 l_Position, Transform l_Player);
    public abstract void TakeDamage(float l_amount, Vector3 l_direction);
    public abstract IEnumerator Recoil(Vector2 l_Direction);
    public abstract float GetDealtDamage();
    public abstract float getSpawnCooldown();
    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    public int getID()
    {
        return m_ID;
    }

    public void setID()
    {
        m_ID = this.GetInstanceID();
    }

    public int CompareTo(object obj)
    {
        if (obj is AEnemy)
        {
            AEnemy l_Enemy = (AEnemy)obj;
            return m_ID-l_Enemy.getID();
        }
        return -200;
    }
    
    //protected Add 

    /*public void setEnemySubState(m_EnemySubstate l_SS, float l_Duration)
    {
        m_CurrentSubState = l_SS;
        m_SubstateDuration = l_Duration;
        m_SubstateDuration = Time.time;
    }
    */

    public void setDizzyVariables(float l_X)
    {
        if (l_X<0) m_DizzyAnimation.SetActive(true); 
        else m_DizzyAnimation.SetActive(false);
        
        m_Dizzy = l_X;
    }

    public void AddSubstate(ASubStatus l_SubStatus)
    {
        if (m_SubStatuses.Contains(l_SubStatus))
        {
            RemoveSubStatus(l_SubStatus);
            m_SubStatuses.Add(l_SubStatus);
        }
    }

    public void RemoveSubStatus(ASubStatus lSubStatus)
    {
        m_SubStatuses.Remove(lSubStatus);
        Destroy(lSubStatus);
    }

    protected void RemoveAllSubStatuses()
    {
        if (m_SubStatuses.Count != 0)
        {
            foreach (var l_Substate in m_SubStatuses)
            {
                l_Substate.DeactivateSubStatus();
            } 
        }
        
    }
}
