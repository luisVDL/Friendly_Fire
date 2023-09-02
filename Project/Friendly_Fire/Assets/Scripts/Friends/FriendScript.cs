using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour, IComparable
{
    [SerializeField]private GameObject m_Collider;
    [SerializeField] private GameObject m_Pedestal;
    [SerializeField] private AFriend m_FriendActivation;
    [SerializeField]private string m_ID;
    [SerializeField] private string m_Creator;
    
 

    public void SetFriendPosition(Transform l_Destination)
    {
        transform.SetParent(l_Destination);
        transform.localPosition = Vector3.zero;
        m_FriendActivation.enabled = true;
        m_Collider.SetActive(false);
        m_Pedestal.SetActive(false);
    }

    public int CompareTo(object l_other)
    {
        if (l_other is FriendScript)
        {
            FriendScript l_Friend = (FriendScript)l_other;
            return m_ID.CompareTo(l_Friend.m_ID);
        }
        else return -200;
    }

    public string getFriendCreator()
    {
        return m_Creator;
    }
    //public abstract UseFriendAbility();
}
