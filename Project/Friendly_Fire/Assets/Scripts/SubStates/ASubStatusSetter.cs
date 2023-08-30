using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASubStatusSetter : MonoBehaviour
{
    [SerializeField] protected ASubStatus m_StatusToSpread;
    [SerializeField] protected float m_Duration;
     
    public abstract void setSubStatus(AEnemy l_Enemy);
}
