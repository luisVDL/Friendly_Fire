using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendScript : MonoBehaviour, IComparable
{
    [SerializeField]private GameObject m_Collider;
    [SerializeField] private AFriend m_FriendActivation;
    [SerializeField]private string m_ID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFriendPosition(Transform l_Destination)
    {
        transform.SetParent(l_Destination);
        transform.localPosition = Vector3.zero;
        m_FriendActivation.enabled = true;
        m_Collider.SetActive(false);
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

    //public abstract UseFriendAbility();
}
