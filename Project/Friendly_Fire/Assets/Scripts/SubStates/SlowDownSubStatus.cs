using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSubStatus : ASubStatus
{
    private float m_SpeedMultiplier;
    
    void Update()
    {
        if(m_StatusStart+m_StatusDuration<Time.time)
            DeactivateSubStatus();
    }

    public override void ActivateSubStatus()
    {
        m_EnemyParent.SetSpeedMultiplier(m_SpeedMultiplier);
        
    }

    public  void SlowDownStatusSetter(float l_Duration, AEnemy l_EnemyParent, float l_Multiplier)
    {
        m_StatusDuration = l_Duration;
        m_EnemyParent = l_EnemyParent;
        m_SpeedMultiplier = l_Multiplier;
        ActivateSubStatus();
    }

    public override void DeactivateSubStatus()
    {
        m_EnemyParent.SetSpeedMultiplier(1f);
    }
}
