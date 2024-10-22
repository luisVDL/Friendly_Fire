using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

public abstract class AEnemy : MonoBehaviour, IComparable
{

    protected Transform m_PlayerToChase;
    protected Transform m_ChasedObject;
    protected Transform m_DizzyChaseObject;
    protected EnemyHealth m_EnemyHealth;
    protected bool m_Recoiling;
    protected int m_ID;
    protected float m_SubstateDuration;
    protected float m_SubstateStart;
    protected m_EnemyIAState m_CurrentState;
    [SerializeField]protected float m_SpeedMultiplier = 1;
    [SerializeField]protected float m_SpawnCooldown;
    [SerializeField] protected int m_EnemyScore;
    
    [Header("NavMeshAgent")]
    [SerializeField] protected NavMeshAgent m_NavMeshAgent;
    [SerializeField] protected float m_AgentSpeed = 5f;
    [SerializeField] protected float m_AgentAcceleration = 8f;
    
    protected enum m_EnemyIAState{
        CHASE, ATTACKING, COOLDOWN, PREPARING, DEATH
    }
    

    //STATUSES VARIABLES
    [Header("Subs Statuses")]
    protected List<ASubStatus> m_SubStatuses;
    protected float m_Dizzy;
    
    [SerializeField] protected GameObject m_DizzyAnimation;
    [SerializeField] protected GameObject m_SlowDownAnimation;


    public abstract void ChangeToCooldownState();

    public virtual void ChangeFromCooldown()
    {
        m_CurrentState = m_EnemyIAState.CHASE;
    }
    
    public abstract void Chase();
    public abstract void Die();
    public abstract void Spawn(Vector3 l_Position, Transform l_Player);
    public abstract void TakeDamage(float l_amount, Vector3 l_direction);
    public abstract IEnumerator Recoil(Vector2 l_Direction);
    public abstract float GetDealtDamage();
    public abstract float getSpawnCooldown();

    public void SetSpawnCooldown(float l_NewCooldown)
    {
        m_SpawnCooldown = l_NewCooldown;
    }
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
        
        //m_Dizzy = l_X;
        //we get a random object and we set the destination
        if (l_X < 0)
        {
            m_ChasedObject = RandomItemGetter.GetRandomChasedObject();
        }
        else
        {
            m_ChasedObject = m_PlayerToChase;
        }
    }

    public void SetSpeedMultiplier(float l_multiplier)
    {
        if(l_multiplier<1) m_SlowDownAnimation.SetActive(true);
        else m_SlowDownAnimation.SetActive(false);
        
        //m_SpeedMultiplier = l_multiplier;
        m_NavMeshAgent.speed = m_AgentSpeed * l_multiplier;
    }
    public void AddSubstate(ASubStatus l_SubStatus)
    {
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
        
        l_SubStatus.enabled = true;
        l_SubStatus.ActivateSubStatus();
        m_SubStatuses.Add(l_SubStatus);

    }

    public void RemoveSubStatus(ASubStatus l_SubStatus)
    {
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

    public int getScore()
    {
        return m_EnemyScore;
    }
}
