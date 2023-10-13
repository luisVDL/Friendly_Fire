using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DizzySubStatusSetter : ASubStatusSetter
{
    public override void setSubStatus(AEnemy l_Enemy)
    {

        DizzySubStatus l_Status=l_Enemy.gameObject.AddComponent<DizzySubStatus>();
        l_Status.enabled = false;
        l_Status.DizzySubStatusSetter(m_CurrentDuration, l_Enemy, m_SubStateName);
        l_Enemy.AddSubstate(l_Status);
        gameObject.SetActive(false);
    }
}
