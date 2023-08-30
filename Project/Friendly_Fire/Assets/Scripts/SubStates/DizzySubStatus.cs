using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzySubStatus : ASubStatus
{
    
    public void DizzySubStatusSetter(float l_Duration, AEnemy l_EnemyParent, string l_name)
    {
        m_SourceName = l_name;
        m_StatusDuration = l_Duration;
        m_EnemyParent = l_EnemyParent;
        ActivateSubStatus();
    }

    public override void ResetStatus()
    {
        m_EnemyParent.setDizzyVariables(1);
    }

    void Update()
    {
        if(m_StatusStart+m_StatusDuration<Time.time)
            DeactivateSubStatus();
    }

    public override void ActivateSubStatus()
    {
        m_StatusStart = Time.time;
        m_EnemyParent.setDizzyVariables(-1);
    }

    public override void DeactivateSubStatus()
    {
        ResetStatus();
        m_EnemyParent.RemoveSubStatus(this);
        
    }
}
