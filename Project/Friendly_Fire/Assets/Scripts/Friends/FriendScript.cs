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
    
 
    /*
     * Things I need for the friend selector
     * - friend image
     * - friend ability description
     * 
     */

    [Header("Friend pick up")] 
    [SerializeField]private Sprite m_FriendSprite;
    [SerializeField] private string m_AbilityDescription;
    

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

    public string GetFriendCreator()
    {
        return m_Creator;
    }

    public string GetFriendName()
    {
        return m_ID;
    }

    public Sprite GetFriendSprite()
    {
        return m_FriendSprite;
    }

    public string GetAbilityDescription()
    {
        return m_AbilityDescription;
    }

    public AFriend getFriendAbilityScript()
    {
        return m_FriendActivation;
    }
}
