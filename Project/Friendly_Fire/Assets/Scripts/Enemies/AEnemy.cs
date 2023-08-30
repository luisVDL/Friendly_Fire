using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]protected float m_SpeedMultiplier = 1;
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
    

    public void setDizzyVariables(float l_X)
    {
        if (l_X<0) m_DizzyAnimation.SetActive(true); 
        else m_DizzyAnimation.SetActive(false);
        
        m_Dizzy = l_X;
    }

    public void SetSpeedMultiplier(float l_multiplier)
    {
        m_SpeedMultiplier = l_multiplier;
    }
    public bool AddSubstate(ASubStatus l_SubStatus)
    {print("------------Adding \n");
        if (m_SubStatuses.Count != 0)
        {
            foreach (var l_SS in m_SubStatuses)
            {
                if (l_SS.getSourceName().Equals(l_SubStatus.getSourceName()))
                {
                    RemoveSubStatus(l_SS);
                    break;
                }
            
            }
        }
        
        /*
        if (m_SubStatuses.Any( x => x.getSourceName().Equals(l_SubStatus.getSourceName())=>RemoveSubStatus(x)));
        {
            RemoveSubStatus(l_SubStatus);
        }*/
        l_SubStatus.enabled = true;
        l_SubStatus.ActivateSubStatus();
        m_SubStatuses.Add(l_SubStatus);
        return true;

    }

    public void RemoveSubStatus(ASubStatus l_SubStatus)
    {
        print("------------Removing \n");
        l_SubStatus.ResetStatus();
        m_SubStatuses.Remove(l_SubStatus);
        Destroy(l_SubStatus);
    }

    protected void RemoveAllSubStatuses()
    {
        ASubStatus l_Substatus;
        while (m_SubStatuses.Count != 0)
        {
            l_Substatus = m_SubStatuses[0];
            l_Substatus.ResetStatus();
            m_SubStatuses.Remove(l_Substatus);
            Destroy(l_Substatus);
        }
    }
}
