using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzySubStatus : ASubStatus
{
    
    public void DizzyStatusSetter(float l_Duration, AEnemy l_EnemyParent)
    {
        m_StatusDuration = l_Duration;
        m_EnemyParent = l_EnemyParent;
        ActivateSubStatus();
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
        m_EnemyParent.setDizzyVariables(1);
        m_EnemyParent.RemoveSubStatus(this);
        
    }
}
