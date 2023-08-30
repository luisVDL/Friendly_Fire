using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSubStatusSetter : ASubStatusSetter
{
    [SerializeField] private float m_SpeedMultiplier = 0.75f;
    
    public override void setSubStatus(AEnemy l_Enemy)
    {

        SlowDownSubStatus l_Status=l_Enemy.gameObject.AddComponent<SlowDownSubStatus>();
        l_Status.enabled = false;
        l_Status.SlowDownStatusSetter(m_Duration, l_Enemy, m_SpeedMultiplier, m_SubStateName);
        l_Enemy.AddSubstate(l_Status);
    }
}
