using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DizzySubStatusSetter : MonoBehaviour
{
    [SerializeField] private ASubStatus m_StatusToSpread;
    [SerializeField] private float m_Duration;


    public void setSubStatus(AEnemy l_Enemy)
    {

        DizzySubStatus l_Status=l_Enemy.gameObject.AddComponent<DizzySubStatus>();
        l_Status.DizzyStatusSetter(m_Duration, l_Enemy);
        l_Enemy.AddSubstate(l_Status);
        gameObject.SetActive(false);
    }
}
