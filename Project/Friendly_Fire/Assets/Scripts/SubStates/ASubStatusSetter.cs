using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASubStatusSetter : MonoBehaviour
{
    [SerializeField] protected ASubStatus m_StatusToSpread;
    [SerializeField] protected float m_Duration;
    protected float m_CurrentDuration;
    [SerializeField] protected string m_SubStateName;


    void Start()
    {
        m_CurrentDuration = m_Duration;
    }
    public abstract void setSubStatus(AEnemy l_Enemy);

    public void IncreaseDuration(float l_multiplier)
    {
        m_CurrentDuration *= l_multiplier;
    }

    public void ResetDuration()
    {
        m_CurrentDuration = m_Duration;
    }
}
