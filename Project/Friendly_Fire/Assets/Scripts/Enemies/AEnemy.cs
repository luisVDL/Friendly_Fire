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
    protected m_EnemyIAState m_CurrentState;
    protected enum m_EnemyIAState{
        CHASE, ATTACKING, COOLDOWN, PREPARING, DEATH
    }
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
}
