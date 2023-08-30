using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASubStatus : MonoBehaviour, IComparable
{
    
    //The Substate will be added with the rest of the scripts and then will disappear-> DONE
    //We want to be able to deactivate the state-> DONE
    // We want to activate the state-> DONE
    // We want to know when the state is gonna disappear -> DONE
    

    protected string m_SourceName;
    
    protected float m_StatusStart;
    protected float m_StatusDuration;
    protected AEnemy m_EnemyParent;

    public abstract void ActivateSubStatus();
    public abstract void DeactivateSubStatus();
    public int CompareTo(object obj)
    {
        if (obj is ASubStatus)
        {
            ASubStatus l_sub = (ASubStatus)obj;
            return m_SourceName.CompareTo(l_sub.m_SourceName);
        }
        return -200;
    }
}
